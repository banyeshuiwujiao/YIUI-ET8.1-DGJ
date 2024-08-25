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

            // ���������޸ĵ�Main Fiber��SceneType
            GlobalComponent globalComponent = root.AddComponent<GlobalComponent>();
            root.SceneType = EnumHelper.FromString<SceneType>(globalComponent.GlobalConfig.AppType.ToString());

            //YIUI��ʼ��
            YIUIBindHelper.InternalGameGetUIBindVoFunc = YIUICodeGenerated.YIUIBindProvider.Get;
            await root.AddComponent<YIUIMgrComponent>().Initialize();
            //�����������д��� ��editor���Զ���  Ҳ���Ը��ݸ�����Χ���� ���� GM�ȼ���
            //if (Define.IsEditor) //����Ĭ�϶���
            {
                root.AddComponent<GMCommandComponent>();
            }
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
        }
    }
}