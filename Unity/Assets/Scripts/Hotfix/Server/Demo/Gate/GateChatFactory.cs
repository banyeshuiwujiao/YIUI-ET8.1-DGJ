namespace ET.Server
{
    //²Î¿¼GateMapFactoryÌí¼ÓChat??
    public static class GateChatFactory
    {
        public static async ETTask<Scene> Create(Entity parent, long id, long instanceId, string name)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(parent, id, instanceId, SceneType.Chat, name);

            scene.AddComponent<UnitComponent>();
            //scene.AddComponent<RoomManagerComponent>();

            scene.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);

            return scene;
        }

    }
}