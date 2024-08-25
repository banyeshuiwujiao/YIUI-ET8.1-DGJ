using System;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.8.25
    /// Desc
    /// </summary>
    [FriendOf(typeof(ChatViewComponent))]
    public static partial class ChatViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this ChatViewComponent self)
        {
            self.u_ComSendButtonButton.onClick.AddListener(() => { self.SendChatMsg(); });
        }
        private static void SendChatMsg(this ChatViewComponent self)
        {
            try
            {
                C2Chat_SendMsg c2Chat_SendMsg = C2Chat_SendMsg.Create();
                c2Chat_SendMsg.Message = self.u_ComInputFieldInputField.text;
                self.Root().GetComponent<ClientSenderComponent>().Send(c2Chat_SendMsg);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        [EntitySystem]
        private static void Destroy(this ChatViewComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this ChatViewComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }
        public static Text GetChatText(this ChatViewComponent self)
        {
            return self.u_ComChatTextText;
        }
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}