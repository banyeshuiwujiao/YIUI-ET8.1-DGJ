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
                    //CreateNoWindow����Ϊfalse���ֶ��ر�ComfyUI,��Ȼû����Unity��ر�
                    CreateNoWindow = false,
                    ErrorDialog = true,
                    //UseShellExecute=true���޷��ض�������/�����CreateNoWindow����Ҳ�ᱻ���ԡ�
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
                //serverOn����
                if (self.serverOn)
                {
                    tapTapPanelComponent.ComfyUIOnReady();
                    break;
                }
                //����ʱ��ľ�����
                if (self.startTimeOut)
                {
                    string log = "6min�ȴ�ʱ��ľ�,���ٴε����������Comfy����";
                    Log.Info(log);
                    tapTapPanelComponent.UpdateTextAndScroll(log);
                    break;
                    //await self.StartComfyServerAsync(); //����������ӣ�����������python
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
            //����������ɱ�����̻��ǹرս��̶����ܽ���ComfyUI����Ϊ��һЩ�ӽ���pythonû����ȡ�ر�
            //���� CreateNoWindow = false ����
            self.comfyProcess.Kill();
            self.comfyProcess.Dispose();
            self.comfyProcess = null;
            
        }
        //public static async Task<string> ReadStandardErrorWithTimeout(this ComfyComponent self, Process process, TimeSpan timeout)
        //{
        //    using var cts = new CancellationTokenSource();
        //    // ����һ����ʱ������������ָ��ʱ����ȡ����ȡ����
        //    Task timeoutTask = Task.Delay(timeout, cts.Token);
        //    Task<string> readLineTask = process.StandardError.ReadLineAsync();
        //    // �ȴ���ȡ������ɻ�ʱ
        //    Task completedTask = await Task.WhenAny(readLineTask, timeoutTask);
        //    if (completedTask == readLineTask)
        //    {
        //        // ��ȡ�ɹ����
        //        return await readLineTask;
        //    }
        //    else
        //    {
        //        // ��ʱ����
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
            // �ȴ���ȡ������ɻ�ʱ
            await ETTaskHelper.WaitAny(ettaskList);
        }
        public static async ETTask GetComfyHttpAsync(this ComfyComponent self)
        {
            self.startTimeOut = false;
            //�ȵ�5s����python����һ��ʱ��
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