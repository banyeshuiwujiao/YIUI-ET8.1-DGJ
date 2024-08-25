namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class Chat2C_SendMsgHandler : MessageHandler<Scene, Chat2C_SendMsg>
    {
        protected override async ETTask Run(Scene scene, Chat2C_SendMsg message)
        {
            Log.Debug("��ǰScene�ǣ�" + scene.Name.ToString() + scene.Id);
            ChatViewComponent chatViewComponent = YIUIMgrComponent.Inst.GetPanelView<LobbyPanelComponent, ChatViewComponent>();
            Log.Debug("������������Ϣ�ǣ�" + message.Message.ToString());
            chatViewComponent.GetChatText().text += message.Message + "\n";
            await ETTask.CompletedTask;
        }
    }
}