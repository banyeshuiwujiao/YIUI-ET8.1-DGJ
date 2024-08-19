namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_MyTestSendHandler : MessageSessionHandler<C2G_MyTestSend>
    {
        protected override async ETTask Run(Session session, C2G_MyTestSend message)
        {
            Log.Debug(msg: $"�յ��˿ͻ��˷�����Send��Ϣ������������{message.Message}");
            await ETTask.CompletedTask;
        }
    }
}