using UnityEngine;
using SimpleFileBrowser;
using System.Text.RegularExpressions;
using TMPro;


namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.9.9
    /// Desc
    /// </summary>
    [FriendOf(typeof(ComfyUIViewComponent))]
    public static partial class ComfyUIViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this ComfyUIViewComponent self)
        {
            self.u_ComCloseButton.onClick.AddListener(() =>
            {
                self.UIView.Close();
            });
            self.u_ComOKButton.onClick.AddListener(() =>
            {
                self.UIView.Close();
            });
            self.u_ComConnectionButton.onClick.AddListener(() =>
            {
                self.u_ComDownConnetionRectTransform.gameObject.SetActive(true);
                //self.u_ComDownStylesRectTransform.gameObject.SetActive(false);
                //self.u_ComDownPerformanceRectTransform.gameObject.SetActive(false);
            });
            self.u_ComLocalServerButton.onClick.AddListener(() =>
            {
                self.u_ComLocalCheckmarkRectTransform.gameObject.SetActive(true);
                self.u_ComCustomCheckmarkRectTransform.gameObject.SetActive(false);
                self.u_ComOnlineCheckmarkRectTransform.gameObject.SetActive(false);

                self.u_ComLocalPanelRectTransform.gameObject.SetActive(true);
                self.u_ComCustomPanelRectTransform.gameObject.SetActive(false);
                self.u_ComOnlinePanelRectTransform.gameObject.SetActive(false);
            });
            self.u_ComCustomServerButton.onClick.AddListener(() =>
            {
                self.u_ComLocalCheckmarkRectTransform.gameObject.SetActive(false);
                self.u_ComCustomCheckmarkRectTransform.gameObject.SetActive(true);
                self.u_ComOnlineCheckmarkRectTransform.gameObject.SetActive(false);

                self.u_ComLocalPanelRectTransform.gameObject.SetActive(false);
                self.u_ComCustomPanelRectTransform.gameObject.SetActive(true);
                self.u_ComOnlinePanelRectTransform.gameObject.SetActive(false);
            });
            self.u_ComOnlineServerButton.onClick.AddListener(() =>
            {
                self.u_ComLocalCheckmarkRectTransform.gameObject.SetActive(false);
                self.u_ComCustomCheckmarkRectTransform.gameObject.SetActive(false);
                self.u_ComOnlineCheckmarkRectTransform.gameObject.SetActive(true);

                self.u_ComLocalPanelRectTransform.gameObject.SetActive(false);
                self.u_ComCustomPanelRectTransform.gameObject.SetActive(false);
                self.u_ComOnlinePanelRectTransform.gameObject.SetActive(true);
            });

            self.u_ComOpenFolderButton.onClick.AddListener(() =>
            {
                //成功获取地址后，需要检测是否存在python.exe和Comfyui/main.py,如果存在，install按钮应该变成Start按钮
                FileBrowser.ShowLoadDialog((paths) => { self.u_ComShowPathText.text = paths[0]; },
                                  () => { self.u_ComShowPathText.text = self.selectFileDir; },
                                  FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select");
            });
            self.u_ComLogOutputTextMeshProUGUI.text = string.Empty;
            self.u_ComInstallButton.onClick.AddListener(() =>
            {
                string dir = self.u_ComShowPathText.text;
                if (dir == self.selectFileDir || string.IsNullOrEmpty(dir))
                {
                    Log.Error("Please select a folder to install ComfyUI.");
                    return;
                }
                EventSystem.Instance.Publish(self.Root(), new StartComfyUIServer() { SelectedDirectory = dir, ComfyUIViewComponent = self }) ;
            });
            self.u_ComLoginOnlineButton.onClick.AddListener(() =>
            {
                Application.OpenURL("https://UWR.baidu.com/");
            });

            self.u_ComConnectButton.onClick.AddListener(async () => await self.TryManualConnection());
            self.u_ComViewLogsButton.onClick.AddListener(() =>
            {
                //TODO 打开游戏目录文件夹
            });

            //InputField每次输入完后要保存
            self.u_ComInputFieldTMPTMP_InputField.onEndEdit.AddListener((arg) =>
            {
                if (Regex.IsMatch(arg, self.pattern))
                {
                    PlayerPrefs.SetString("ComfyUIIPPort", arg);
                }
                else
                {
                    Log.Error("输入不符合IP地址和端口号的组合格式");
                    self.u_ComInputFieldTMPTMP_InputField.text = PlayerPrefs.GetString("ComfyUIIPPort");
                }
            });
        }
        //手动检测
        public static async ETTask TryManualConnection(this ComfyUIViewComponent self)
        {
            self.u_ComConnectButton.interactable = false;
            self.u_ComStatusTextMeshProUGUI.text = "Connecting";
            self.u_ComStatusTextMeshProUGUI.color = Color.blue;
            string ip = PlayerPrefs.GetString("ComfyUIIPPort", "127.0.0.1:8188");
            Log.Debug($"Ready to Get the System Status for ComfyUI Server: {ip}");
            string text = await ComfyHelper.GetComfyUIStatusAsync(ip);
            self.u_ComSystemStatusTextMeshProUGUI.text = ComfyHelper.ShowSystemStatus(text);
            self.SetConnectedStatus(!string.IsNullOrEmpty(text));
            self.u_ComConnectButton.interactable = true;
        }
        public static void SetConnectedStatus(this ComfyUIViewComponent self, bool connected)
        {
            if (connected)
            {
                self.u_ComStatusTextMeshProUGUI.text = "Connected";
                self.u_ComStatusTextMeshProUGUI.color = Color.green;
            }
            else
            {
                self.u_ComStatusTextMeshProUGUI.text = "Disconnected";
                self.u_ComStatusTextMeshProUGUI.color = Color.red;
            }
        }
        [EntitySystem]
        private static void Destroy(this ComfyUIViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this ComfyUIViewComponent self)
        {
            self.u_ComDownConnetionRectTransform.gameObject.SetActive(true);
            //self.u_ComDownStylesRectTransform.gameObject.SetActive(false);
            //self.u_ComDownPerformanceRectTransform.gameObject.SetActive(false);
            self.u_ComLocalCheckmarkRectTransform.gameObject.SetActive(false);
            self.u_ComCustomCheckmarkRectTransform.gameObject.SetActive(false);
            self.u_ComOnlineCheckmarkRectTransform.gameObject.SetActive(false);

            self.u_ComLocalPanelRectTransform.gameObject.SetActive(false);
            self.u_ComCustomPanelRectTransform.gameObject.SetActive(false);
            self.u_ComOnlinePanelRectTransform.gameObject.SetActive(false);

            self.u_ComInputFieldTMPTMP_InputField.text = PlayerPrefs.GetString("ComfyUIIPPort", "127.0.0.1:8188");
            await ETTask.CompletedTask;
            return true;
        }

        public static TextMeshProUGUI GetProcessBar(this ComfyUIViewComponent self)
        {
            return self.u_ComLogOutputTextMeshProUGUI;
        }

        public static void SetInstallButtonInteractable(this ComfyUIViewComponent self,bool value)
        {
            self.u_ComInstallButton.interactable = value;
        }
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}