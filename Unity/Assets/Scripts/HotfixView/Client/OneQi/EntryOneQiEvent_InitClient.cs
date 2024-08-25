using System;
using System.Collections.Generic;
using System.IO;
using YIUIFramework;

namespace ET.Client
{
    [Event(SceneType.Main)]
    public class EntryOneQiEvent_InitClient : AEvent<Scene, EntryOneQiEvent>
    {
        protected override async ETTask Run(Scene root, EntryOneQiEvent args)
        {
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ResourcesLoaderComponent>();
            root.AddComponent<PlayerComponent>();
            root.AddComponent<CurrentScenesComponent>();

            // 根据配置修改掉Main Fiber的SceneType
            GlobalComponent globalComponent = root.AddComponent<GlobalComponent>();
            root.SceneType = EnumHelper.FromString<SceneType>(globalComponent.GlobalConfig.AppType.ToString());

            //YIUI初始化
            YIUIBindHelper.InternalGameGetUIBindVoFunc = YIUICodeGenerated.YIUIBindProvider.Get;
            await root.AddComponent<YIUIMgrComponent>().Initialize();
            //根据需求自行处理 在editor下自动打开  也可以根据各种外围配置 或者 GM等级打开
            //if (Define.IsEditor) //这里默认都打开
            {
                root.AddComponent<GMCommandComponent>();
            }
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
        }
    }
}