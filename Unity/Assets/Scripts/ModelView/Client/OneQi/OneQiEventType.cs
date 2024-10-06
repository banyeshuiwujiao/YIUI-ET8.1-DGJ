using TMPro;
using UnityEngine.Networking;

namespace ET.Client
{
    public struct EnterHome { }
    public struct BacktoHome { }
    public struct GameReady { }

    public struct StartComfyUIServer
    {
        public string SelectedDirectory;
        public ComfyUIViewComponent ComfyUIViewComponent;
    }
}