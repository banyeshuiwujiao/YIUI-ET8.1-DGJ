using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{

    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [YIUI(EUICodeType.View)]
    public partial class ComfyUIViewComponent: Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize, IYIUIOpen
    {
        public const string PkgName = "Steam";
        public const string ResName = "ComfyUIView";

        public EntityRef<YIUIComponent> u_UIBase;
        public YIUIComponent UIBase => u_UIBase;
        public EntityRef<YIUIWindowComponent> u_UIWindow;
        public YIUIWindowComponent UIWindow => u_UIWindow;
        public EntityRef<YIUIViewComponent> u_UIView;
        public YIUIViewComponent UIView => u_UIView;
        public UnityEngine.UI.Button u_ComConnectionButton;
        public UnityEngine.RectTransform u_ComDownConnetionRectTransform;
        public UnityEngine.UI.Button u_ComLocalServerButton;
        public UnityEngine.UI.Button u_ComCustomServerButton;
        public UnityEngine.UI.Button u_ComOnlineServerButton;
        public UnityEngine.RectTransform u_ComOnlinePanelRectTransform;
        public TMPro.TextMeshProUGUI u_ComHttpTextTextMeshProUGUI;
        public UnityEngine.UI.Button u_ComLoginOnlineButton;
        public UnityEngine.RectTransform u_ComLocalPanelRectTransform;
        public UnityEngine.RectTransform u_ComCustomPanelRectTransform;
        public UnityEngine.RectTransform u_ComLocalCheckmarkRectTransform;
        public UnityEngine.RectTransform u_ComCustomCheckmarkRectTransform;
        public UnityEngine.RectTransform u_ComOnlineCheckmarkRectTransform;
        public TMPro.TMP_InputField u_ComInputFieldTMPTMP_InputField;
        public UnityEngine.UI.Button u_ComConnectButton;
        public UnityEngine.UI.Button u_ComViewLogsButton;
        public TMPro.TextMeshProUGUI u_ComStatusTextMeshProUGUI;
        public UnityEngine.UI.Button u_ComCloseButton;
        public UnityEngine.UI.Button u_ComOKButton;
        public TMPro.TextMeshProUGUI u_ComSystemStatusTextMeshProUGUI;
        public UnityEngine.UI.Button u_ComOpenFolderButton;
        public UnityEngine.UI.Text u_ComShowPathText;
        public UnityEngine.UI.Button u_ComInstallButton;
        public TMPro.TextMeshProUGUI u_ComLogOutputTextMeshProUGUI;

    }
}