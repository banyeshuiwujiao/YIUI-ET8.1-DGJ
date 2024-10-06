using System.Diagnostics;
using System;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using System.Security.Policy;

namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class StartComfyUIServerEvent : AEvent<Scene, StartComfyUIServer>
    {
        protected override async ETTask Run(Scene root, StartComfyUIServer args)
        {
            args.ComfyUIViewComponent.SetInstallButtonInteractable(false);
            ConnectComfyUIComponent connectComfyUIComponent = root.GetComponent<ConnectComfyUIComponent>();
            if (connectComfyUIComponent != null)
            {
                TextMeshProUGUI processBar = args.ComfyUIViewComponent.GetProcessBar();
                TimerComponent timerComponent = root.GetComponent<TimerComponent>();
                //git
                string result = string.Empty;
                string error = string.Empty;
                using Process gitProcess = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "git", // 假设Git已经添加到系统路径中
                        Arguments = "--version",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    },
                };
                gitProcess.OutputDataReceived += (sender, e) => { result = result + e.Data; };
                gitProcess.ErrorDataReceived += (sender, e) => { error = error + e.Data; };
                try
                {
                    //using Process gitProcess = ComfyHelper.CreateProcess("git", "--version");
                    gitProcess.Start();
                    gitProcess.BeginOutputReadLine();
                    gitProcess.BeginErrorReadLine();
                    gitProcess.WaitForExit();
                }
                catch (Exception e)
                {
                    error += e.Message;
                }
                Log.Debug($"result: {result}");
                if (!string.IsNullOrEmpty(result) && result.Contains("git version"))
                {
                    Log.Debug("Git is installed: " + result);
                }
                else
                {
                    Log.Debug("Git is not installed or not found in PATH: " + error);
                    string url = connectComfyUIComponent.GetGitURL();
                    byte[] resultsGit = await ComfyHelper.HttpGetDataAsync(root, "Git", url, processBar);
                    string exePath = Path.Combine(Application.dataPath, "/Download/", Path.GetFileName(url));
                    await ComfyHelper.WriteDataAsync(resultsGit, exePath);
                    string arguments = "/VERYSILENT /NORESTART /NOCANCEL /SP- /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS /COMPONENTS=\"Git Bash Here,assoc,AutoRun,DesktopIcon,Explorer Integration,Ext,Git LFS,SSH,Symbolic Links,Windows Explorer integration\"; /LOG \"{exePath}.log\"";
                    try
                    {
                        using Process gitInstallProcess = ComfyHelper.CreateProcess(exePath, arguments);
                        gitInstallProcess.Start();
                        gitInstallProcess.BeginOutputReadLine();
                        gitInstallProcess.BeginErrorReadLine();
                        //gitInstallProcess.WaitForExit();
                        while (!gitInstallProcess.HasExited)
                        {
                            Log.Debug("Git is installing, please not close the game.");
                            await timerComponent.WaitAsync(2000);
                        }
                        ComfyHelper.SetFileEnvPath(@"C:\Program Files\Git\bin");
                        File.Delete(exePath);
                        Log.Debug("Git installed successfully!");
                    }
                    catch (Exception e)
                    {
                        Log.Error($"process fail: {e}");
                        return;
                    }
                }
                
                //python
                string pythonDir = @$"{args.SelectedDirectory}\python_embeded\";
                string pythonExe = "python.exe";
                if (!Directory.Exists(pythonDir)) Directory.CreateDirectory(pythonDir);
                string pythonPath = Directory.EnumerateFiles(pythonDir, pythonExe, SearchOption.AllDirectories).FirstOrDefault();
                if (string.IsNullOrEmpty(pythonPath))
                {
                    string pythonUrl = connectComfyUIComponent.GetPythonUrl();
                    byte[] resultsPy = await ComfyHelper.HttpGetDataAsync(root, "Python", pythonUrl, processBar);
                    //ComfyHelper.ZipFileDecompress(resultsPy, pythonDir);
                    string pythonName = Path.GetFileName(pythonUrl);
                    string exePath = Path.Combine(Application.dataPath, "/Download/", pythonName);
                    await ComfyHelper.WriteDataAsync(resultsPy, exePath);
                    using Process pythonInstallProcess = new()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            FileName = exePath,
                            Arguments = $"/quiet InstallAllUsers=1 PrependPath=1 Include_test=0 TargetDir={pythonDir}",
                            Verb = "runas", // 请求管理员权限
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        }
                    };
                    pythonInstallProcess.OutputDataReceived += (sender, e) => Log.Debug(e.Data);
                    pythonInstallProcess.ErrorDataReceived += (sender, e) => Log.Error(e.Data);
                    try
                    {
                        pythonInstallProcess.Start();
                        pythonInstallProcess.BeginOutputReadLine();
                        pythonInstallProcess.BeginErrorReadLine();
                        while (!pythonInstallProcess.HasExited)
                        {
                            Log.Debug("Python is installing, please not close the game.");
                            await timerComponent.WaitAsync(2000);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception ($"Python install failed: {e}");
                    }
                    pythonPath = Directory.EnumerateFiles(pythonDir, pythonExe, SearchOption.AllDirectories).FirstOrDefault();
                    File.Delete(exePath);
                    Log.Debug($"Python installed successfully in {pythonPath}");
                    #region 安装pip
                    //string pipUrl = connectComfyUIComponent.GetPipUrl();
                    //byte[] getPipData = await ComfyHelper.HttpGetDataAsync(root,"get-Pip", pipUrl, processBar);
                    //string name = Path.GetFileName(pipUrl);
                    //string getPipPath = Path.Combine(pythonDir, name);
                    //await ComfyHelper.WriteDataAsync(getPipData, getPipPath);
                    ////string arguments = $".\\python.exe {name}";
                    //try
                    //{
                    //    //using Process getPipProcess = ComfyHelper.CreateProcess(connectComfyUIComponent.Getapp(), arguments, pythonDir);
                    //    using Process getPipProcess = ComfyHelper.CreateProcess(pythonPath, getPipPath, pythonDir);
                    //    getPipProcess.Start();
                    //    getPipProcess.BeginOutputReadLine();
                    //    getPipProcess.BeginErrorReadLine();
                    //    //getPipProcess.WaitForExit();
                    //    while (!getPipProcess.HasExited)
                    //    {
                    //        await timerComponent.WaitAsync(2000);
                    //    }
                    //    ComfyHelper.SetFileEnvPath(@$"{pythonDir}Scripts\");
                    //    Log.Debug($"Python is installed successfully in {pythonPath}.");
                    //}
                    //catch (Exception e)
                    //{
                    //    Log.Error($"process fail: {e}");
                    //    return;
                    //}
                    #endregion
                }
                else
                {
                    Log.Debug($"Python had been installed in {pythonPath}.");
                }
                //comfyui
                string startMainPy = "main.py";
                string comfyUIDir = args.SelectedDirectory+@"\ComfyUI";
                Log.Debug($"the ComfyUI git path is {comfyUIDir}");
                if (!Directory.Exists(comfyUIDir)) Directory.CreateDirectory(comfyUIDir);
                string mainPyPath = Directory.EnumerateFiles(comfyUIDir, startMainPy, SearchOption.AllDirectories).FirstOrDefault();
                if (string.IsNullOrEmpty(mainPyPath))
                {
                    string comfyUrl = connectComfyUIComponent.GetComfyUIURL();
                    string arguments = $"clone {comfyUrl}";
                    using Process gitComfyUI = ComfyHelper.CreateProcess("git", arguments, args.SelectedDirectory);
                    try
                    {
                        gitComfyUI.Start();
                        gitComfyUI.BeginOutputReadLine();
                        gitComfyUI.BeginErrorReadLine();
                        while (!gitComfyUI.HasExited)
                        {
                            await timerComponent.WaitAsync(2000);
                        }
                        Log.Debug("Git ComfyUI Successfully");
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Torch torchvision torchaudio for GPUs failed: {e.Message}");
                    }
                    //byte[] resultsComfyUI = await ComfyHelper.HttpGetDataAsync(root, "ComfyUI", comfyUrl, processBar);
                    //ComfyHelper.ZipFileDecompress(resultsComfyUI, args.SelectedDirectory);
                    mainPyPath = Directory.EnumerateFiles(comfyUIDir, startMainPy, SearchOption.AllDirectories).FirstOrDefault();
                    Log.Debug($"ComfyUI/main.py is installed successfully in {mainPyPath}.");
                }
                else
                {
                    Log.Debug($"ComfyUI/main.py had been installed in {mainPyPath}.");
                }
                //install ComfyUI torch torchvision torchaudio in different GPUs
                string arguments1 = $"-m pip install --pre torch torchvision torchaudio --index-url https://download.pytorch.org/whl/nightly/cu124";
                Process torchProcess = ComfyHelper.CreateProcess(pythonPath, arguments1, Path.GetDirectoryName(mainPyPath));
                try
                {
                    torchProcess.Start();
                    torchProcess.BeginOutputReadLine();
                    torchProcess.BeginErrorReadLine();
                    while (!torchProcess.HasExited)
                    {
                        await timerComponent.WaitAsync(2000);
                    }
                    Log.Debug("Torch torchvision torchaudio for GPUs installed Successfully");
                }
                catch (Exception e)
                {
                    throw new Exception($"Torch torchvision torchaudio for GPUs failed: {e.Message}");
                }
                //install ComfyUI requirements
                string arguments2 = $"-m pip install -r requirements.txt";
                Process requirements = ComfyHelper.CreateProcess(pythonPath, arguments2, Path.GetDirectoryName(mainPyPath));
                try
                {
                    requirements.Start();
                    requirements.BeginOutputReadLine();
                    requirements.BeginErrorReadLine();
                    while (!requirements.HasExited)
                    {
                        await timerComponent.WaitAsync(2000);
                    }
                    Log.Debug("ComfyUI's requirements installed Successfully");
                }
                catch (Exception e)
                {
                    throw new Exception($"ComfyUI's requirements failed: {e.Message}");
                }
                //Start ComfyUI Server
                string arguments3 = $"main.py";
                //Process serverProcess = ComfyHelper.CreateProcess(pythonPath, arguments3, Path.GetDirectoryName(mainPyPath));
                Process serverProcess = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = pythonPath, // 假设Git已经添加到系统路径中
                        Arguments = arguments3,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        //CreateNoWindow = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        WorkingDirectory = Path.GetDirectoryName(mainPyPath),
                    },
                };
                //serverProcess.OutputDataReceived += (sender, e) => { Log.Debug(e.Data); };
                //serverProcess.ErrorDataReceived += (sender, e) => { Log.Error(e.Data); };
                try
                {
                    serverProcess.Start();
                    //serverProcess.BeginOutputReadLine();
                    //serverProcess.BeginErrorReadLine();
                    //while (!serverProcess.HasExited)
                    //{
                    //    await timerComponent.WaitAsync(2000);
                    //}
                    Log.Debug("omfyUI Server Initialization Successfully");
                }
                catch (Exception e)
                {
                    throw new Exception($"ComfyUI Server Initialization failed: {e.Message}");
                }
            }
            args.ComfyUIViewComponent.SetInstallButtonInteractable(true);
        }
    }
}