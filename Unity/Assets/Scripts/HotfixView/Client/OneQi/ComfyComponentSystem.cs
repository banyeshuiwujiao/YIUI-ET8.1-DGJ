using dnlib.Threading;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
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
            self.comfyProcess = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = self.app,
                    Arguments = self.arguments + " \"" + self.cmd + "\"",
                    //CreateNoWindow必须为false来手动关闭ComfyUI,不然没法在Unity里关闭
                    CreateNoWindow = false,
                    ErrorDialog = true,
                    //UseShellExecute=true将无法重定向输入/输出，CreateNoWindow属性也会被忽略。
                    UseShellExecute = false,
                    WorkingDirectory = self.workDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            TapTapPanelComponent tapTapPanelComponent = YIUIMgrComponent.Inst.GetPanel<TapTapPanelComponent>();
            tapTapPanelComponent.SetComfyNotify();
            self.comfyProcess.Start();
            while (!self.comfyProcess.HasExited)
            {
                await self.Root().GetComponent<TimerComponent>().WaitAsync(200);
                //serverOn跳出
                if (self.serverOn)
                {
                    tapTapPanelComponent.ComfyUIOnReady();
                    break;
                }
                //启动时间耗尽跳出
                if (self.startTimeOut)
                {
                    string log = "6min等待时间耗尽,请再次点击尝试启动Comfy服务";
                    Log.Info(log);
                    tapTapPanelComponent.UpdateTextAndScroll(log);
                    break;
                    //await self.StartComfyServerAsync(); //这个不能随便加，会无限启动python
                }
                //string line;
                ETCancellationToken cancellationToken = new ETCancellationToken();
                try
                {
                    //line = await self.ReadStandardErrorWithTimeout(self.comfyProcess, TimeSpan.FromMinutes(5));
                    //Debug.Log(line);
                    //tapTapPanelComponent.UpdateTextAndScroll(line);
                    //if (line.Contains("To see the GUI go to"))
                    //{
                    //    string log = "ComfyUI has started successfully. \nListening on: " + line.Split("to:")[1];
                    //    Debug.Log(log);
                    //    tapTapPanelComponent.ComfyUIOnReady();
                    //    break;
                    //}

                    await self.ReadStandardErrorWithTimeout(self.comfyProcess, TimeSpan.FromMinutes(5), cancellationToken);
                }
                catch (TimeoutException ex)
                {
                    tapTapPanelComponent.UpdateTextAndScroll("Timed out: " + ex.Message);
                    break;
                }
                finally
                {
                    cancellationToken.Cancel();
                }
                
            }
            //这里无论是杀死进程还是关闭进程都不能结束ComfyUI，因为有一些子进程python没法获取关闭
            //建议 CreateNoWindow = false 试试
            self.comfyProcess.Kill();
            self.comfyProcess.Dispose();
            self.comfyProcess = null;
            
        }
        //public static async Task<string> ReadStandardErrorWithTimeout(this ComfyComponent self, Process process, TimeSpan timeout)
        //{
        //    using var cts = new CancellationTokenSource();
        //    // 启动一个定时器任务，用于在指定时间内取消读取操作
        //    Task timeoutTask = Task.Delay(timeout, cts.Token);
        //    Task<string> readLineTask = process.StandardError.ReadLineAsync();
        //    // 等待读取操作完成或超时
        //    Task completedTask = await Task.WhenAny(readLineTask, timeoutTask);
        //    if (completedTask == readLineTask)
        //    {
        //        // 读取成功完成
        //        return await readLineTask;
        //    }
        //    else
        //    {
        //        // 超时发生
        //        cts.Cancel();
        //        throw new TimeoutException("The operation timed out.");
        //    }
        //}
        public static async ETTask ReadLineAsync(this ComfyComponent self, Process process)
        {
            string line = await process.StandardError.ReadLineAsync();
            Log.Info(line);
            TapTapPanelComponent tapTapPanelComponent = YIUIMgrComponent.Inst.GetPanel<TapTapPanelComponent>();
            tapTapPanelComponent.UpdateTextAndScroll(line);
            if (line.Contains("To see the GUI go to"))
            {
                self.serverOn = true;
            }
        }
        public static async ETTask ReadStandardErrorWithTimeout(this ComfyComponent self, Process process, TimeSpan timeout, ETCancellationToken cancellationToken = null)
        {
            var timeoutTask = self.Root().GetComponent<TimerComponent>().WaitAsync(8000, cancellationToken);
            var readLineTask = self.ReadLineAsync(process);
            ETTask[] ettaskList = new ETTask[2];
            ettaskList[0] = timeoutTask;
            ettaskList[1] = readLineTask;
            // 等待读取操作完成或超时
            await ETTaskHelper.WaitAny(ettaskList);
        }
        public static async ETTask GetComfyHttpAsync(this ComfyComponent self)
        {
            self.startTimeOut = false;
            //先等5s，让python先跑一段时间
            await self.Root().GetComponent<TimerComponent>().WaitAsync(5000);
            long timer = self.interval;
            while (timer > 0 && !self.serverOn)
            {
                timer -= 2000;
                await self.Root().GetComponent<TimerComponent>().WaitAsync(2000);
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
            self.startTimeOut = true;
        }
    }
}