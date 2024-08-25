using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
namespace ET.Client
{
    [EntitySystemOf(typeof(ComfyComponent))]
    [FriendOf(typeof(ComfyComponent))]
    public static partial class ComfyComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this ComfyComponent self)
        {
        }

        [EntitySystem]
        private static void Awake(this ComfyComponent self)
        {
        }
        public static async ETTask StartComfyServerAsync(this ComfyComponent self)
        {
            self.ComfyProcess = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = self.app,
                    Arguments = self.arguments + " \"" + self.cmd + "\"",
                    CreateNoWindow = true,
                    ErrorDialog = true,
                    UseShellExecute = false,
                    WorkingDirectory = self.workDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            TapTapPanelComponent tapTapPanelComponent = YIUIMgrComponent.Inst.GetPanel<TapTapPanelComponent>();
            tapTapPanelComponent.SetComfyNotify();
            self.ComfyProcess.Start();
            while (!self.ComfyProcess.HasExited)
            {
                await self.Root().GetComponent<TimerComponent>().WaitAsync(200);
                if (self.serverOn)
                {
                    tapTapPanelComponent.ComfyUIOnReady();
                    break;
                }
                string line;
                try
                {
                    line = await self.ReadStandardErrorWithTimeout(self.ComfyProcess, TimeSpan.FromMinutes(5));
                    Debug.Log(line);
                    tapTapPanelComponent.UpdateTextAndScroll(line);
                    if (line.Contains("To see the GUI go to"))
                    {
                        string log = "ComfyUI has started successfully. \nListening on: " + line.Split("to:")[1];
                        Debug.Log(log);
                        tapTapPanelComponent.ComfyUIOnReady();
                        break;
                    }
                }
                catch (TimeoutException ex)
                {
                    tapTapPanelComponent.UpdateTextAndScroll("Timed out: " + ex.Message);
                    break;
                }
                
            }
            self.ComfyProcess.Close();
            self.ComfyProcess.Dispose();
            self.StartComfyServerAsync().Coroutine();
        }
        public static async Task<string> ReadStandardErrorWithTimeout(this ComfyComponent self, Process process, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            // 启动一个定时器任务，用于在指定时间内取消读取操作
            Task timeoutTask = Task.Delay(timeout, cts.Token);
            Task<string> readLineTask = process.StandardError.ReadLineAsync();
            // 等待读取操作完成或超时
            Task completedTask = await Task.WhenAny(readLineTask, timeoutTask);
            if (completedTask == readLineTask)
            {
                // 读取成功完成
                return await readLineTask;
            }
            else
            {
                // 超时发生
                cts.Cancel();
                throw new TimeoutException("The operation timed out.");
            }
        }
        public static async ETTask GetComfyHttpAsync(this ComfyComponent self)
        {
            while (self.interval > 0 && !self.serverOn)
            {
                self.interval -= 1000;
                await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);
                string url = "http://127.0.0.1:8188/system_stats";
                UnityWebRequest request = new(url, "GET")
                {
                    downloadHandler = new DownloadHandlerBuffer()
                };
                request.SetRequestHeader("Content-Type", "application/json");

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    self.serverOn = true;
                    Debug.Log("Comfy Server Start successfully. \n" + request.downloadHandler.text);
                    return;
                }
                else
                {
                    self.serverOn = false;
                    Debug.Log(request.error);
                }
            }
            if (!self.serverOn)
            {
                TapTapPanelComponent tapTapPanelComponent = YIUIMgrComponent.Inst.GetPanel<TapTapPanelComponent>();
                tapTapPanelComponent.UpdateTextAndScroll("10min等待时间耗尽,请重启游戏重试");
            }
        }
    }
}