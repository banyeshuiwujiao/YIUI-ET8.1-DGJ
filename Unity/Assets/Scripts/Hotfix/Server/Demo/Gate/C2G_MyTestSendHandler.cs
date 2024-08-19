namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_MyTestSendHandler : MessageSessionHandler<C2G_MyTestSend>
    {
        protected override async ETTask Run(Session session, C2G_MyTestSend message)
        {
            Log.Debug(msg: $"收到了客户端发来的Send消息，他的内容是{message.Message}");
            await ETTask.CompletedTask;
        }
    }
}