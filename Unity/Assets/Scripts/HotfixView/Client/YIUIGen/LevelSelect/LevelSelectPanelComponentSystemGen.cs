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
    [EntitySystemOf(typeof(LevelSelectPanelComponent))]
    public static partial class LevelSelectPanelComponentSystem
    {
        [EntitySystem]
        private static void Awake(this LevelSelectPanelComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this LevelSelectPanelComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this LevelSelectPanelComponent self)
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

            self.u_ComButton_ReadyButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_ReadyButton");
            self.u_ComButton_BackButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_BackButton");
            self.u_ComButton_HomeButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_HomeButton");

        }
    }
}