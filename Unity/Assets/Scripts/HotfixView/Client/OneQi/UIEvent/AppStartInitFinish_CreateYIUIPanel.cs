namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class AppStartInitFinish_CreateYIUIPanel : AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene root, AppStartInitFinish args)
        {
#if !PLATFORM_ANDROID
            TapTapPanelComponent tapTapPanelComponent = await YIUIMgrComponent.Inst.Root.OpenPanelAsync<TapTapPanelComponent>();
            ComfyComponent comfyComponent = root.AddComponent<ComfyComponent>();
            tapTapPanelComponent.GetManualComfyStart().onClick.AddListener(async () =>
            {
                comfyComponent.ComfyProcess?.Close();
                comfyComponent.ComfyProcess?.Dispose();
                await root.GetComponent<TimerComponent>().WaitAsync(1000);
                await comfyComponent.StartComfyServerAsync();
                await comfyComponent.GetComfyHttpAsync();
            });
#else
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<SteamPanelComponent>();
#endif

        }

    }
}
