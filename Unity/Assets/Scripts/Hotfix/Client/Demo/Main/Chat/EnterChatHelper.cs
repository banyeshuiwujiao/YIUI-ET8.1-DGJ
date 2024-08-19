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
                //var msg = "你已进入聊天服：" + g2CEnterChat.Message.ToString() + "; 用户：" + g2CEnterChat.MyId + "地址：" + root.GetComponent<SessionComponent>().Session.RemoteAddress;
                Log.Debug("你已进入聊天服：" + g2CEnterChat.Message.ToString() + "; 用户：" + g2CEnterChat.MyId);
                //EventSystem.Instance.Publish(root, new EnterChatFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}