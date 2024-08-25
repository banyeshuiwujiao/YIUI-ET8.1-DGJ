namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class GameReady_SelectHeros : AEvent<Scene, GameReady>
    {
        protected override async ETTask Run(Scene root, GameReady args)
        {
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<CharacterSelectPanelComponent>();
            await YIUIMgrComponent.Inst.ClosePanelAsync<HomePanelComponent>();
        }
    }
}
