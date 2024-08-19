namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterChatHandler : MessageSessionHandler<C2G_EnterChat, G2C_EnterChat>
    {
        protected override async ETTask Run(Session session, C2G_EnterChat request, G2C_EnterChat response)
        {
            ////首先，获取聊天服
            //var chatInstanceId = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Chat");
            //Chat2G_UnitTransferResponse chat2G_UnitTransfer=await session.Fiber().Root.GetComponent<MessageSender>().Call(
            //chatInstanceId.ActorId, new G2Chat_UnitTransferRequest() { GateId=session.InstanceId}) as Chat2G_UnitTransferResponse;
            //Player player = session.GetComponent<SessionPlayerComponent>().Player;
            //Unit unit = UnitFactory.Create(session.Fiber().Root, player.Id, UnitType.Player);

            //Player player = session.GetComponent<SessionPlayerComponent>().Player;
            ////Unit unit = session.Root().GetComponent<UnitComponent>().Get(request.Id);
            //GateChatComponent gateChatComponent = player.AddComponent<GateChatComponent>();
            //gateChatComponent.Scene = await GateChatFactory.Create(gateChatComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "Chat");
            //Scene scene = gateChatComponent.Scene;
            //Unit unit = UnitFactory.Create(scene, player.Id, UnitType.Chat);
            //StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Chat");
            //player.Id是[BsonId]，而player.InstanceId是[BsonIgnore]
            //response.MyId = unit.Id;
            //response.Message = startSceneConfig.ActorId.ToString();
            await ETTask.CompletedTask;
        }

    }
}