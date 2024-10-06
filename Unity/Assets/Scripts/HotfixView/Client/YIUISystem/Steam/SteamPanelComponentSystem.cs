using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;
using System.Security;

namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.6.23
    /// Desc
    /// </summary>
    [FriendOf(typeof(SteamPanelComponent))]
    public static partial class SteamPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this SteamPanelComponent self)
        {
            self.u_ComButton_StartButton.onClick.AddListener(() =>
            {
                EventSystem.Instance.Publish(self.Root(), new EnterHome() { });
            });
            self.u_ComButton_ComfyUIButton.onClick.AddListener(async () =>
            {
               await self.UIPanel.OpenViewAsync<ComfyUIViewComponent>();
            });

            self.Root().AddComponent<ConnectComfyUIComponent>();
        }

        [EntitySystem]
        private static void Destroy(this SteamPanelComponent self)
        {
            self.Root().RemoveComponent<ConnectComfyUIComponent>();
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this SteamPanelComponent self)
        {
            //self.SetConnectedStatus(false);
            await ETTask.CompletedTask;
            return true;
        }
        public static void SetConnectedStatus(this SteamPanelComponent self,bool connected)
        {
            self.u_ComButton_StartButton.gameObject.SetActive(connected);
            self.u_ComGreenRectTransform.gameObject.SetActive(connected);
            self.u_ComDescriptionTextMeshProUGUI.gameObject.SetActive(!connected);
        }
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}