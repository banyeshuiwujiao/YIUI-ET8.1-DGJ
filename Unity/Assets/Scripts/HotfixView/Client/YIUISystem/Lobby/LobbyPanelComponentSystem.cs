using System;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(LobbyPanelComponent))]
    public static partial class LobbyPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this LobbyPanelComponent self)
        {
            self.u_ComEnterChatButton.onClick.AddListener(() => { self.EnterChat().Coroutine(); });
            self.u_ComEnterMapButton.onClick.AddListener(() => { self.EnterMap().Coroutine(); });
            //MyTestMessage
            self.u_ComSendHelloButton.onClick.AddListener(() => { self.SendHello(); });
            self.u_ComCallHelloButton.onClick.AddListener(() => { self.CallHello().Coroutine(); });
        }

        [EntitySystem]
        private static void Destroy(this LobbyPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this LobbyPanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        
        private static async ETTask OnEventEnterAction(this LobbyPanelComponent self)
        {
            var banId = YIUIMgrComponent.Inst.BanLayerOptionForever();
            await EnterMapHelper.EnterMapAsync(self.Root());
            YIUIMgrComponent.Inst.RecoverLayerOptionForever(banId);
            self.UIPanel.Close(false,true);
        }

        #endregion YIUIEvent结束

        public static async ETTask EnterMap(this LobbyPanelComponent self)
        {
            var banId = YIUIMgrComponent.Inst.BanLayerOptionForever();
            await EnterMapHelper.EnterMapAsync(self.Root());
            YIUIMgrComponent.Inst.RecoverLayerOptionForever(banId);
            //这里应该先打开UIHelp的HelpPanel
            self.UIPanel.Close(false, true);
        }

        //MyTestMessage
        public static void SendHello(this LobbyPanelComponent self)
        {
            try
            {
                C2G_MyTestSend c2G_MyTestSend = C2G_MyTestSend.Create();
                c2G_MyTestSend.Message = "这是DGJ的Send消息.";
                self.Root().GetComponent<ClientSenderComponent>().Send(c2G_MyTestSend);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        private static async ETTask CallHello(this LobbyPanelComponent self)
        {
            try
            {
                C2G_MyTestMessage c2G_MyTestMessage = C2G_MyTestMessage.Create();
                c2G_MyTestMessage.Message = "这是DGJ的Request消息.";
                G2C_MyTestMessage g2C_MyTestMessage = await self.Root().GetComponent<ClientSenderComponent>().Call(c2G_MyTestMessage) as G2C_MyTestMessage;
                Log.Debug(msg: $"收到了服务端发来的消息，他内部的消息是: {g2C_MyTestMessage.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        private static async ETTask EnterChat(this LobbyPanelComponent self)
        {
            //Scene root = self.Root();
            //await EnterChatHelper.EnterChatAsync(root);
            //暂时先不把UIChat模块独立出来；
            await self.UIPanel.OpenViewAsync<ChatViewComponent>();
        }
        
    }
}