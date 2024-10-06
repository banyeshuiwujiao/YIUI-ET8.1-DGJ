using System;
using System.Diagnostics;
using System.IO;
using UnityEngine.Networking;
using System.IO.Compression;
using TMPro;

namespace ET.Client
{
    public static class ComfyHelper
    {
        public static async ETTask<string> GetComfyUIStatusAsync(string ip)
        {
            string url = $"http://{ip}/system_stats";
            UnityWebRequest request = new(url, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };
            request.SetRequestHeader("Content-Type", "application/json");
            try
            {
                await request.SendWebRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"发生异常: {ex.Message}");
                return string.Empty;
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Log.Debug("Comfy Server Start successfully. \n" + request.downloadHandler.text);
                return request.downloadHandler.text;
            }
            else
            {
                Log.Warning(request.error);
                return string.Empty;
            }
        }
        public static string ShowSystemStatus(string text)
        {
            string systemStatus = text.Replace("{\"system\":", "<color=blue><b>System</b></color>:\n     ")
                .Replace("}, \"devices\":", "\n<color=blue><b>Devices</b></color>:\n     ");
            string[] result = systemStatus.Split(new string[] { ", \"argv", "Devices" }, StringSplitOptions.None);
            result[0] = result[0].Replace("\", \"", ",\n      ");
            result[2] = result[2].Replace("\", \"", ",\n      ").Replace(", \"", ",\n      ").Replace("[", "").Replace("]", "");
            systemStatus = result[0] + ",\n      \"argv" + result[1] + "Devices" + result[2];
            systemStatus = systemStatus.Replace("{", "").Replace("}", "").Replace("\"", "");
            return systemStatus;
        }

        public static Process CreateProcess(string fileName, string arguments, string workingDirectory = null)
        {
            Process process = new ()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName, // 假设Git已经添加到系统路径中
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = workingDirectory,
                },
            };
            process.OutputDataReceived += (sender, e) => { Log.Debug(e.Data); };
            process.ErrorDataReceived += (sender, e) => { Log.Error(e.Data); };
            return process;
        }

        public static async ETTask<byte[]> HttpGetDataAsync(Scene scene,string name, string Url, TextMeshProUGUI processBar)
        {
            TimerComponent timerComponent = scene.GetComponent<TimerComponent>();
            Log.Debug($"Game is ready to download file from {Url}. Please wait...");
            try
            {
                using UnityWebRequest www = UnityWebRequest.Get(Url);
                www.downloadHandler = new DownloadHandlerBuffer();
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();
                while (!operation.isDone)
                {
                    float progress = www.downloadProgress;
                    processBar.text = $"{name} is Downloading in: {progress * 100}%";
                    await timerComponent.WaitAsync(500);
                }
                Log.Debug("Download complete!");
                return www.downloadHandler.data;
            }
            catch (Exception e)
            {
                throw new Exception($"Dowload file failed: {e}");
            }
        }
        public static async ETTask WriteDataAsync(byte[] results, string exePath)
        {
            string outputDirectory = Path.GetDirectoryName(exePath);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            //Git程序很小，BufferedStream意义不大；该卡还是卡顿
            await File.WriteAllBytesAsync(exePath, results);
            ////BufferedStream是一个缓冲流，它可以减少磁盘操作次数，提高写入速度。
            //using (BufferedStream bufferedStream = new(File.Create(exePath)))
            //{
            //    await bufferedStream.WriteAsync(results, 0, results.Length);
            //}
        }
        //public static async ETTask RunGitInstallerAsync(string exePath)
        //{
        //    ProcessStartInfo start = new ProcessStartInfo
        //    {
        //        FileName = exePath,
        //        Arguments = "/VERYSILENT /NORESTART /NOCANCEL /SP- /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS /COMPONENTS=\"Git Bash Here,assoc,AutoRun,DesktopIcon,Explorer Integration,Ext,Git LFS,SSH,Symbolic Links,Windows Explorer integration\"",
        //        UseShellExecute = false,
        //        CreateNoWindow = true
        //    };
        //    try
        //    {
        //        using Process process = Process.Start(start);
        //        //process.WaitForExit();
        //        while (!process.HasExited)
        //        {
        //            Log.Debug("Git is installing, please not close the game.");
        //            await Tween.Delay(1f);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"Git install cmd fail: {e}");
        //    }
        //}
        public static void SetFileEnvPath(string filePath)
        {
            string currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
            Log.Debug($"current Path: {currentPath}");
            if (!currentPath.Contains(filePath))
            {
                Environment.SetEnvironmentVariable("Path", currentPath + ";" + filePath, EnvironmentVariableTarget.User);
                Log.Debug("Path: " + Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User));
            }
        }
        public static void ZipFileDecompress(byte[] data, string dir)
        {
            using MemoryStream ms = new(data);
            using ZipArchive archive = new(ms);
            archive.ExtractToDirectory(dir);
        }
        public static async ETTask ReadWithTimeout(TimerComponent timerComponent, StreamReader outputReader, StreamReader errorReader, ETCancellationToken cancellationToken = null)
        {
            var timeoutTask = timerComponent.WaitAsync(2000, cancellationToken);
            var readLineTask = ReadLineAsync(outputReader, errorReader);
            ETTask[] ettaskList = new ETTask[2];
            ettaskList[0] = timeoutTask;
            ettaskList[1] = readLineTask;
            // 等待读取操作完成或超时
            await ETTaskHelper.WaitAny(ettaskList);
        }
        public static async ETTask ReadLineAsync(StreamReader outputReader, StreamReader errorReader)
        {
            string line1 = await outputReader.ReadLineAsync();
            string line2 = await errorReader.ReadLineAsync();
            Log.Debug($"Output: {line1}; Error: {line2}") ;
        }

    }
}
