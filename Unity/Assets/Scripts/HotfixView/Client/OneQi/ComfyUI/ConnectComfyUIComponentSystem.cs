using System;
using System.Diagnostics;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace ET.Client
{
    [EntitySystemOf(typeof(ConnectComfyUIComponent))]
    [FriendOf(typeof(ConnectComfyUIComponent))]
    public static partial class ConnectComfyUIComponentSystem
    {
        [Invoke(TimerInvokeType.ComfyUIConnectionTimer)]
        public class ComfyUIConnectionTimer : ATimer<ConnectComfyUIComponent>
        {
            protected override void Run(ConnectComfyUIComponent self)
            {
                try
                {
                    self.GetSystemStatus().Coroutine();
                }
                catch (Exception e)
                {
                    Log.Error($"Connecting ComfyUI timer error: {self.Id}\n{e}");
                }
            }
        }
        public static async ETTask GetSystemStatus(this ConnectComfyUIComponent self)
        {
            SteamPanelComponent steamPanelComponent = YIUIMgrComponent.Inst.GetPanel<SteamPanelComponent>();
            if (steamPanelComponent == null) return;

            string ip = PlayerPrefs.GetString("ComfyUIIPPort", "127.0.0.1:8188");
            string text = await ComfyHelper.GetComfyUIStatusAsync(ip);
            self.ServerOn = !string.IsNullOrEmpty(text);
                steamPanelComponent.SetConnectedStatus(self.ServerOn);
                ComfyUIViewComponent comfyUIViewComponent = YIUIMgrComponent.Inst.GetPanelView<SteamPanelComponent, ComfyUIViewComponent>();
                comfyUIViewComponent?.SetConnectedStatus(self.ServerOn);
        }
        [EntitySystem]
        private static void Destroy(this ConnectComfyUIComponent self)
        {
            self.Root().GetComponent<TimerComponent>()?.Remove(ref self.TimerId);
        }

        [EntitySystem]
        private static void Awake(this ConnectComfyUIComponent self)
        {
             self.TimerId = self.Root().GetComponent<TimerComponent>().NewRepeatedTimer(self.ConnectionInterval, TimerInvokeType.ComfyUIConnectionTimer, self);
        }

        public static string GetGitURL(this ConnectComfyUIComponent self)
        {
            string url = self.isChina ? self.gitProxy + self.gitUrl : self.gitUrl;
            return url;
        }
        public static string GetPythonUrl(this ConnectComfyUIComponent self)
        {
            return self.pythonUrl;
        }
        public static string GetPipUrl(this ConnectComfyUIComponent self)
        {
            return self.pipUrl;
        }
        public static string GetComfyUIURL(this ConnectComfyUIComponent self)
        {
            string url = self.isChina ? self.gitProxy + self.comfyUIUrl : self.comfyUIUrl;
            return url;
        }
        public static string Getapp(this ConnectComfyUIComponent self)
        {
            return self.app;
        }

        public static async ETTask ReadStandardErrorWithTimeout(this ConnectComfyUIComponent self, Process process, ETCancellationToken cancellationToken = null)
        {
            var timeoutTask = self.Root().GetComponent<TimerComponent>().WaitAsync(8000, cancellationToken);
            var readLineTask = self.ReadLineAsync(process);
            ETTask[] ettaskList = new ETTask[2];
            ettaskList[0] = timeoutTask;
            ettaskList[1] = readLineTask;
            // 等待读取操作完成或超时
            await ETTaskHelper.WaitAny(ettaskList);
        }
        public static async ETTask ReadLineAsync(this ConnectComfyUIComponent self, Process process)
        {
            string line = await process.StandardError.ReadLineAsync();
            Log.Info(line);
            if (line.Contains("To see the GUI go to"))
            {
                self.ServerOn = true;
            }
        }
    }
}