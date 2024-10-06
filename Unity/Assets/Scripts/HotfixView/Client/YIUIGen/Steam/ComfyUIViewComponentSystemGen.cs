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
    [EntitySystemOf(typeof(ComfyUIViewComponent))]
    public static partial class ComfyUIViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ComfyUIViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this ComfyUIViewComponent self)
        {
            self.UIBind();
        }
        
        private static void UIBind(this ComfyUIViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIComponent>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_ComConnectionButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComConnectionButton");
            self.u_ComDownConnetionRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComDownConnetionRectTransform");
            self.u_ComLocalServerButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComLocalServerButton");
            self.u_ComCustomServerButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComCustomServerButton");
            self.u_ComOnlineServerButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComOnlineServerButton");
            self.u_ComOnlinePanelRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComOnlinePanelRectTransform");
            self.u_ComHttpTextTextMeshProUGUI = self.UIBase.ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComHttpTextTextMeshProUGUI");
            self.u_ComLoginOnlineButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComLoginOnlineButton");
            self.u_ComLocalPanelRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComLocalPanelRectTransform");
            self.u_ComCustomPanelRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComCustomPanelRectTransform");
            self.u_ComLocalCheckmarkRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComLocalCheckmarkRectTransform");
            self.u_ComCustomCheckmarkRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComCustomCheckmarkRectTransform");
            self.u_ComOnlineCheckmarkRectTransform = self.UIBase.ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComOnlineCheckmarkRectTransform");
            self.u_ComInputFieldTMPTMP_InputField = self.UIBase.ComponentTable.FindComponent<TMPro.TMP_InputField>("u_ComInputFieldTMPTMP_InputField");
            self.u_ComConnectButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComConnectButton");
            self.u_ComViewLogsButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComViewLogsButton");
            self.u_ComStatusTextMeshProUGUI = self.UIBase.ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComStatusTextMeshProUGUI");
            self.u_ComCloseButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComCloseButton");
            self.u_ComOKButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComOKButton");
            self.u_ComSystemStatusTextMeshProUGUI = self.UIBase.ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComSystemStatusTextMeshProUGUI");
            self.u_ComOpenFolderButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComOpenFolderButton");
            self.u_ComShowPathText = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Text>("u_ComShowPathText");
            self.u_ComInstallButton = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComInstallButton");
            self.u_ComLogOutputTextMeshProUGUI = self.UIBase.ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComLogOutputTextMeshProUGUI");

        }
    }
}