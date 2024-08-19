
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class Chat2C_SendMsgHandler : MessageHandler<Scene, Chat2C_SendMsg>
    {
        protected override async ETTask Run(Scene scene, Chat2C_SendMsg message)
        {
            Log.Debug("��ǰScene�ǣ�" + scene.Name.ToString() + scene.Id);

            UI ui = scene.GetComponent<UIComponent>().Get(UIType.UILobby);
            ReferenceCollector rc = ui.GameObject.GetComponent<ReferenceCollector>();
            Log.Debug("������������Ϣ�ǣ�" + message.Message.ToString());
            var chattext = rc.Get<GameObject>("ChatText").GetComponent<Text>();
            chattext.text += message.Message + "\n";
            Log.Debug(chattext.ToString());
            await ETTask.CompletedTask;
        }

    }
}