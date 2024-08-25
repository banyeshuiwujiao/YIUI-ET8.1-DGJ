namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.6.25
    /// Desc
    /// </summary>
    [FriendOf(typeof(HomePanelComponent))]
    public static partial class HomePanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this HomePanelComponent self)
        {
            self.u_ComPlayButton.onClick.AddListener(() =>
            {
                EventSystem.Instance.Publish(self.Root(), new GameReady() { });
            });
        }

        [EntitySystem]
        private static void Destroy(this HomePanelComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this HomePanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }
        
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}