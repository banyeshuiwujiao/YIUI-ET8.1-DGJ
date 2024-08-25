using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.6.26
    /// Desc
    /// </summary>
    [FriendOf(typeof(LevelSelectPanelComponent))]
    public static partial class LevelSelectPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this LevelSelectPanelComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this LevelSelectPanelComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this LevelSelectPanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }
        
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}