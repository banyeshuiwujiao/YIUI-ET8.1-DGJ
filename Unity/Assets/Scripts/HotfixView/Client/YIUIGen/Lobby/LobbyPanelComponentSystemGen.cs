﻿using System;
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
    [FriendOf(typeof(YIUIPanelComponent))]
    [EntitySystemOf(typeof(LobbyPanelComponent))]
    public static partial class LobbyPanelComponentSystem
    {
        [EntitySystem]
        private static void Awake(this LobbyPanelComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this LobbyPanelComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this LobbyPanelComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIComponent>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIPanel = self.UIBase.GetComponent<YIUIPanelComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIPanel.Layer = EPanelLayer.Panel;
            self.UIPanel.PanelOption = EPanelOption.None;
            self.UIPanel.StackOption = EPanelStackOption.VisibleTween;
            self.UIPanel.Priority = 0;

            self.u_ComEnterChatButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComEnterChatButton");
            self.u_ComSendHelloButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComSendHelloButton");
            self.u_ComEnterMapButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComEnterMapButton");
            self.u_ComCallHelloButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComCallHelloButton");

        }
    }
}