using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{

    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [YIUI(EUICodeType.Panel, EPanelLayer.Panel)]
    public partial class CharacterSelectPanelComponent: Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize, IYIUIOpen
    {
        public const string PkgName = "CharacterSelect";
        public const string ResName = "CharacterSelectPanel";

        public EntityRef<YIUIComponent> u_UIBase;
        public YIUIComponent UIBase => u_UIBase;
        public EntityRef<YIUIWindowComponent> u_UIWindow;
        public YIUIWindowComponent UIWindow => u_UIWindow;
        public EntityRef<YIUIPanelComponent> u_UIPanel;
        public YIUIPanelComponent UIPanel => u_UIPanel;
        public YIUIFramework.UI3DDisplay u_ComYIUI3DDisplayUI3DDisplay;
        public UnityEngine.RectTransform u_ComStarRectTransform;
        public TMPro.TextMeshProUGUI u_ComText_PowerTextMeshProUGUI;
        public UnityEngine.RectTransform u_ComSelectRoleIconRectTransform;
        public UnityEngine.RectTransform u_ComCharacterLevelRectTransform;
        public UnityEngine.RectTransform u_ComStatsRectTransform;
        public UnityEngine.UI.Button u_ComSelectButton;
        public UnityEngine.UI.Button u_ComAbilityButton;
        public UnityEngine.UI.Button u_ComButton_BackButton;
        public UnityEngine.UI.Button u_ComButton_HomeButton;

    }
}