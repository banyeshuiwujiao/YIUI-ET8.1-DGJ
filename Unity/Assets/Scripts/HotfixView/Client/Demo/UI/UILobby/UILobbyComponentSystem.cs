using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILobbyComponent))]
    [FriendOf(typeof(UILobbyComponent))]
    public static partial class UILobbyComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UILobbyComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.enterMap = rc.Get<GameObject>("EnterMap");
            self.enterMap.GetComponent<Button>().onClick.AddListener(() => { self.EnterMap().Coroutine(); });
            //MyTestMessage
            self.sendHello = rc.Get<GameObject>("SendHello");
            self.sendHello.GetComponent<Button>().onClick.AddListener(() => { self.SendHello(); });
            self.callHello = rc.Get<GameObject>("CallHello");
            self.callHello.GetComponent<Button>().onClick.AddListener(() => { self.CallHello().Coroutine(); });

            self.enterChat = rc.Get<GameObject>("EnterChat");
            self.enterChat.GetComponent<Button>().onClick.AddListener(() => { self.EnterChat(); });

            self.inputField = rc.Get<GameObject>("InputField").GetComponent<InputField>();

            self.sendButton = rc.Get<GameObject>("SendButton");
            self.sendButton.GetComponent<Button>().onClick.AddListener(() => { self.SendChatMsg(); });

            self.chatPanel = rc.Get<GameObject>("ChatPanel");
            self.chatPanel.SetActive(false);

        }

        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            Scene root = self.Root();
            await EnterMapHelper.EnterMapAsync(root);
            await UIHelper.Remove(root, UIType.UILobby);
        }

        //MyTestMessage
        public static void SendHello(this UILobbyComponent self)
        {
            try
            {
                C2G_MyTestSend c2G_MyTestSend = C2G_MyTestSend.Create();
                c2G_MyTestSend.Message = "这是我的Send消息.";
                self.Root().GetComponent<ClientSenderComponent>().Send(c2G_MyTestSend);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        private static async ETTask CallHello(this UILobbyComponent self)
        {
            try
            {
                C2G_MyTestMessage c2G_MyTestMessage = C2G_MyTestMessage.Create();
                c2G_MyTestMessage.Message = "这是我的Request消息.";
                G2C_MyTestMessage g2C_MyTestMessage = await self.Root().GetComponent<ClientSenderComponent>().Call(c2G_MyTestMessage) as G2C_MyTestMessage;
                Log.Debug(msg: $"收到了服务端发来的消息，他内部的消息是{g2C_MyTestMessage.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        private static void EnterChat(this UILobbyComponent self)
        {
            //Scene root = self.Root();
            //await EnterChatHelper.EnterChatAsync(root);
            //暂时先不把UIChat模块独立出来；
            //await UIHelper.Create(root, UIType.UIChat, UILayer.Mid);
            self.chatPanel.SetActive(true);
        }
        private static void SendChatMsg(this UILobbyComponent self)
        {
            try
            {
                C2Chat_SendMsg c2Chat_SendMsg = C2Chat_SendMsg.Create();
                c2Chat_SendMsg.Message = self.inputField.text;
                self.Root().GetComponent<ClientSenderComponent>().Send(c2Chat_SendMsg);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
    }
}