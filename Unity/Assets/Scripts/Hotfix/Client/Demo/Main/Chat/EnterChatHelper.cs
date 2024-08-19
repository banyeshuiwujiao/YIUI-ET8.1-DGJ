using System;

namespace ET.Client
{
    public static partial class EnterChatHelper
    {
        public static async ETTask EnterChatAsync(Scene root)
        {
            try
            {
                G2C_EnterChat g2CEnterChat = await root.GetComponent<ClientSenderComponent>().Call(C2G_EnterChat.Create()) as G2C_EnterChat;
                //var msg = "���ѽ����������" + g2CEnterChat.Message.ToString() + "; �û���" + g2CEnterChat.MyId + "��ַ��" + root.GetComponent<SessionComponent>().Session.RemoteAddress;
                Log.Debug("���ѽ����������" + g2CEnterChat.Message.ToString() + "; �û���" + g2CEnterChat.MyId);
                //EventSystem.Instance.Publish(root, new EnterChatFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}