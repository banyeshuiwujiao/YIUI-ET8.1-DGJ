namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_MyTestMessageHandler : MessageSessionHandler<C2G_MyTestMessage, G2C_MyTestMessage>
    {
        protected override async ETTask Run(Session session, C2G_MyTestMessage request, G2C_MyTestMessage response)
        {
            Log.Debug(msg: $"�յ��˿ͻ��˷�����Call��Ϣ������������{request.Message}");
            response.Message = "���Ƿ�����������Call��Ӧ��";
            await ETTask.CompletedTask;

        }
    }
}