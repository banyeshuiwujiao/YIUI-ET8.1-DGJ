namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.6.25
    /// Desc
    /// </summary>
    [FriendOf(typeof(CharacterSelectPanelComponent))]
    public static partial class CharacterSelectPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CharacterSelectPanelComponent self)
        {
            self.u_ComButton_HomeButton.onClick.AddListener(() =>
            {
                EventSystem.Instance.Publish(self.Root(), new BacktoHome() { });
            });
        }

        [EntitySystem]
        private static void Destroy(this CharacterSelectPanelComponent self)
        {
        }
        
        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this CharacterSelectPanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }
        
        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}