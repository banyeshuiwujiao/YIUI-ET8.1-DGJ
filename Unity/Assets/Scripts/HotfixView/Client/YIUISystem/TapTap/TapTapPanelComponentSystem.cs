using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.6.23
    /// Desc
    /// </summary>
    [FriendOf(typeof(TapTapPanelComponent))]
    public static partial class TapTapPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this TapTapPanelComponent self)
        {
          
        }

        [EntitySystem]
        private static void Destroy(this TapTapPanelComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this TapTapPanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }
            #region YIUIEvent开始
            #endregion YIUIEvent结束
    }
}