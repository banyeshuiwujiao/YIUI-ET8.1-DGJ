namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class BacktoHomeEvent : AEvent<Scene, BacktoHome>
    {
        protected override async ETTask Run(Scene root, BacktoHome args)
        {
            await YIUIMgrComponent.Inst.HomePanel<HomePanelComponent>();
        }
    }
}