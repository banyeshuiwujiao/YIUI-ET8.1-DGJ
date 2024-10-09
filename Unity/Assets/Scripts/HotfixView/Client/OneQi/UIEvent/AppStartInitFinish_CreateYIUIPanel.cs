using UnityEngine.Networking;

namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class AppStartInitFinish_CreateYIUIPanel : AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene root, AppStartInitFinish args)
        {
#if !PLATFORM_ANDROID
            TapTapPanelComponent tapTapPanelComponent = await YIUIMgrComponent.Inst.Root.OpenPanelAsync<TapTapPanelComponent>();
#else
            SteamPanelComponent steamPanelComponent = await YIUIMgrComponent.Inst.Root.OpenPanelAsync<SteamPanelComponent>();
#endif
        }

    }
}
