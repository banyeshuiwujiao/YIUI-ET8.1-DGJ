 namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class EnterHome_OpenPanel : AEvent<Scene, EnterHome>
    {
        protected override async ETTask Run(Scene root, EnterHome args)
        {
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<HomePanelComponent>();
#if PLATFORM_ANDROID
            await YIUIMgrComponent.Inst.ClosePanelAsync<TapTapPanelComponent>();
#else
            await YIUIMgrComponent.Inst.ClosePanelAsync<SteamPanelComponent>();
#endif
        }
    }
}
