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
    [FriendOf(typeof(YIUIPanelComponent))]
    [EntitySystemOf(typeof(SettingsPanelComponent))]
    public static partial class SettingsPanelComponentSystem
    {
        [EntitySystem]
        private static void Awake(this SettingsPanelComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this SettingsPanelComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this SettingsPanelComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIComponent>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIPanel = self.UIBase.GetComponent<YIUIPanelComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIPanel.Layer = EPanelLayer.Panel;
            self.UIPanel.PanelOption = EPanelOption.TimeCache;
            self.UIPanel.StackOption = EPanelStackOption.VisibleTween;
            self.UIPanel.Priority = 0;
            self.UIPanel.CachePanelTime = 10;

            self.u_ComButton_Language = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_Language");
            self.u_ComHomeButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComHomeButton");
            self.u_ComBackButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComBackButton");
            self.u_ComLogoutButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComLogoutButton");

        }
    }
}