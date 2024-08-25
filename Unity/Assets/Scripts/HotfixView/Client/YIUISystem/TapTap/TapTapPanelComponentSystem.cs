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
            self.u_ComText_NoRaycastText.text = "";
            self.u_ComScrollViewText_NoRaycastScrollRect.gameObject.SetActive(false);
            self.u_ComServerNoticeText.gameObject.SetActive(false);
            self.u_ComButton_StartButton.gameObject.SetActive(false);
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
        public static Button GetManualComfyStart(this TapTapPanelComponent self)
        {
            return self.u_ComStart_ComfyButton;
        }
        public static void SetComfyNotify(this TapTapPanelComponent self)
        {
            self.u_ComScrollViewText_NoRaycastScrollRect.gameObject.SetActive(true);
            self.u_ComServerNoticeText.gameObject.SetActive(true);
            self.u_ComServerNoticeText.text = "正在启动Comfy服务，可能会有5-8s的卡顿...";
        }
        public static void UpdateTextAndScroll(this TapTapPanelComponent self, string text)
        {
            self.u_ComText_NoRaycastText.text += text + "\n";
            self.u_ComScrollViewText_NoRaycastScrollRect.verticalNormalizedPosition = 0f;
        }
        public static void ComfyUIOnReady(this TapTapPanelComponent self)
        {
            self.u_ComServerNoticeText.text = "ComfyUI已经准备好，请点击登陆，进入游戏.";
            self.u_ComButton_StartButton.onClick.AddListener(() =>
            {
                EventSystem.Instance.Publish(self.Root(), new EnterHome() { });
            });
            self.u_ComButton_StartButton.gameObject.SetActive(true);
        }
            #region YIUIEvent开始
            #endregion YIUIEvent结束
    }
}