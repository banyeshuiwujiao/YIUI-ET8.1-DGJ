namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_MyTestMessageHandler : MessageSessionHandler<C2G_MyTestMessage, G2C_MyTestMessage>
    {
        protected override async ETTask Run(Session session, C2G_MyTestMessage request, G2C_MyTestMessage response)
        {
            Log.Debug(msg: $"收到了客户端发来的Call消息，他的内容是{request.Message}");
            response.Message = "这是服务器发来的Call回应。";
            await ETTask.CompletedTask;

        }
    }
}