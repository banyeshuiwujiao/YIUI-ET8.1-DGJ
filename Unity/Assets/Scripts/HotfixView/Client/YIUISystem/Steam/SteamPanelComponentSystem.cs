using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

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
        }

        [EntitySystem]
        private static void Destroy(this SteamPanelComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this SteamPanelComponent self)
        {
       
            await ETTask.CompletedTask;
            return true;
        }
        
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}