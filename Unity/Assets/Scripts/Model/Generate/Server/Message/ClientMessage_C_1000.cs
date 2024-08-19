using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    // using
    [MemoryPackable]
    [Message(ClientMessage.Main2NetClient_Login)]
    [ResponseType(nameof(NetClient2Main_Login))]
    public partial class Main2NetClient_Login : MessageObject, IRequest
    {
        public static Main2NetClient_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Main2NetClient_Login), isFromPool) as Main2NetClient_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int OwnerFiberId { get; set; }

        [MemoryPackOrder(2)]
        public string Account { get; set; }

        [MemoryPackOrder(3)]
        public string Password { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.OwnerFiberId = default;
            this.Account = default;
            this.Password = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.NetClient2Main_Login)]
    public partial class NetClient2Main_Login : MessageObject, IResponse
    {
        public static NetClient2Main_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(NetClient2Main_Login), isFromPool) as NetClient2Main_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long PlayerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.PlayerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.C2G_MyTestSend)]
    public partial class C2G_MyTestSend : MessageObject, ISessionMessage
    {
        public static C2G_MyTestSend Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2G_MyTestSend), isFromPool) as C2G_MyTestSend;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.C2G_MyTestMessage)]
    [ResponseType(nameof(G2C_MyTestMessage))]
    public partial class C2G_MyTestMessage : MessageObject, ISessionRequest
    {
        public static C2G_MyTestMessage Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2G_MyTestMessage), isFromPool) as C2G_MyTestMessage;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.G2C_MyTestMessage)]
    public partial class G2C_MyTestMessage : MessageObject, ISessionResponse
    {
        public static G2C_MyTestMessage Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(G2C_MyTestMessage), isFromPool) as G2C_MyTestMessage;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.C2G_EnterChat)]
    [ResponseType(nameof(G2C_EnterChat))]
    public partial class C2G_EnterChat : MessageObject, ISessionRequest
    {
        public static C2G_EnterChat Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2G_EnterChat), isFromPool) as C2G_EnterChat;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.G2C_EnterChat)]
    public partial class G2C_EnterChat : MessageObject, ISessionResponse
    {
        public static G2C_EnterChat Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(G2C_EnterChat), isFromPool) as G2C_EnterChat;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long MyId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.MyId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.C2Chat_SendMsg)]
    public partial class C2Chat_SendMsg : MessageObject, ISessionMessage
    {
        public static C2Chat_SendMsg Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2Chat_SendMsg), isFromPool) as C2Chat_SendMsg;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.Chat2C_SendMsg)]
    public partial class Chat2C_SendMsg : MessageObject, IMessage
    {
        public static Chat2C_SendMsg Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Chat2C_SendMsg), isFromPool) as Chat2C_SendMsg;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    public static class ClientMessage
    {
        public const ushort Main2NetClient_Login = 1001;
        public const ushort NetClient2Main_Login = 1002;
        public const ushort C2G_MyTestSend = 1003;
        public const ushort C2G_MyTestMessage = 1004;
        public const ushort G2C_MyTestMessage = 1005;
        public const ushort C2G_EnterChat = 1006;
        public const ushort G2C_EnterChat = 1007;
        public const ushort C2Chat_SendMsg = 1008;
        public const ushort Chat2C_SendMsg = 1009;
    }
}