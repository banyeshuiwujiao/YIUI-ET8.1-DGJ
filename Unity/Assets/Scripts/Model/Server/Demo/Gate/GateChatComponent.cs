namespace ET.Server
{
    //Gate上动态添加Chat，跟GatMap一样？？
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