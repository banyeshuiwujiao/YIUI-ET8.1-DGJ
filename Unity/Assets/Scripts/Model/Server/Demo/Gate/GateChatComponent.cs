namespace ET.Server
{
    //Gate�϶�̬���Chat����GatMapһ������
    [ComponentOf(typeof(Player))]
    public class GateChatComponent : Entity, IAwake
    {
        private EntityRef<Scene> scene;

        public Scene Scene
        {
            get
            {
                return this.scene;
            }
            set
            {
                this.scene = value;
            }
        }
    }
}