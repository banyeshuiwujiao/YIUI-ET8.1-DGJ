using System.Collections.Generic;
namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2Chat_SendMsgHandler : MessageSessionHandler<C2Chat_SendMsg>
    {
        protected override async ETTask Run(Session session, C2Chat_SendMsg message)
        {
            Log.Debug("收到聊天消息,开始广播");

            // MessageLocationSenderOneType相比 MessageLocationSenderComponent,网络底层做了优化，同一个消息不会多次序列化
            (message as MessageObject).IsFromPool = false;
            MessageLocationSenderOneType oneTypeMessageLocationType = session.Fiber().Root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession);

            Dictionary<int, Player> dict = new Dictionary<int, Player>();
            dict = session.Fiber().Root.GetComponent<PlayerComponent>().GetByAllAccounts();

            foreach (Player key in dict.Values)
            {
                Chat2C_SendMsg chat2C_SendMsg = Chat2C_SendMsg.Create();
                chat2C_SendMsg.Message = message.Message.ToString();
                oneTypeMessageLocationType.Send(key.Id, chat2C_SendMsg);
            }

            await ETTask.CompletedTask;
        }
    }
}