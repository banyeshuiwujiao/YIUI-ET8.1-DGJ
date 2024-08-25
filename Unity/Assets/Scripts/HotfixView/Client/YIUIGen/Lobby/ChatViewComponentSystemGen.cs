using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [FriendOf(typeof(YIUIComponent))]
    [FriendOf(typeof(YIUIWindowComponent))]
    [FriendOf(typeof(YIUIViewComponent))]
    [EntitySystemOf(typeof(ChatViewComponent))]
    public static partial class ChatViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ChatViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this ChatViewComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this ChatViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIComponent>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_ComInputFieldInputField = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.InputField>("u_ComInputFieldInputField");
            self.u_ComSendButtonButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComSendButtonButton");
            self.u_ComChatTextText = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Text>("u_ComChatTextText");

        }
    }
}