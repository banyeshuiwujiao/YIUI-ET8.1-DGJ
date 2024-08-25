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
    [EntitySystemOf(typeof(CharacterSelectPanelComponent))]
    public static partial class CharacterSelectPanelComponentSystem
    {
        [EntitySystem]
        private static void Awake(this CharacterSelectPanelComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this CharacterSelectPanelComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this CharacterSelectPanelComponent self)
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

            self.u_ComYIUI3DDisplayUI3DDisplay = self.UIBase.ComponentTable.FindComponent<YIUIFramework.UI3DDisplay>("u_ComYIUI3DDisplayUI3DDisplay");
            self.u_ComStarRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComStarRectTransform");
            self.u_ComText_PowerTextMeshProUGUI = self.UIBase.ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComText_PowerTextMeshProUGUI");
            self.u_ComSelectRoleIconRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComSelectRoleIconRectTransform");
            self.u_ComCharacterLevelRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComCharacterLevelRectTransform");
            self.u_ComStatsRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComStatsRectTransform");
            self.u_ComSelectButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComSelectButton");
            self.u_ComAbilityButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComAbilityButton");
            self.u_ComButton_BackButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_BackButton");
            self.u_ComButton_HomeButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComButton_HomeButton");

        }
    }
}