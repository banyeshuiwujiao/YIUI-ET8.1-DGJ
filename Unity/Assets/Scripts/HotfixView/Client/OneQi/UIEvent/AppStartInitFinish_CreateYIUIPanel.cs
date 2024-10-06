using UnityEngine.Networking;

namespace ET.Client
{
    [Event(SceneType.OneQi)]
    public class AppStartInitFinish_CreateYIUIPanel : AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene root, AppStartInitFinish args)
        {
#if PLATFORM_ANDROID
            TapTapPanelComponent tapTapPanelComponent = await YIUIMgrComponent.Inst.Root.OpenPanelAsync<TapTapPanelComponent>();
            ComfyComponent comfyComponent = root.AddComponent<ComfyComponent>();
            tapTapPanelComponent.GetManualComfyStart().onClick.AddListener(async () =>
            {
                if (comfyComponent.ComfyProcess != null) 
                {
                    Log.Debug("Comfy���������Ѿ������������ĵȴ�����");
                }
                else if(comfyComponent.ServerOn)
                {
                    string url = "http://127.0.0.1:8188/system_stats";
                    UnityWebRequest request = new(url, "GET")
                    {
                        downloadHandler = new DownloadHandlerBuffer()
                    };
                    request.SetRequestHeader("Content-Type", "application/json");

                    await request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Log.Debug("Comfy�����Ѿ��������벻Ҫ�ظ����\n" + request.downloadHandler.text);
                    }
                    else
                    {
                        comfyComponent.ServerOn = false;
                        Log.Debug(request.error);
                        comfyComponent.GetComfyHttpAsync().Coroutine();
                        await comfyComponent.StartComfyServerAsync();
                    }
                    
                }
                else
                {
                    //��Ӧ����CoroutineЭ�̣����¼�ϵͳ�ȽϺ�
                    comfyComponent.GetComfyHttpAsync().Coroutine();
                    await comfyComponent.StartComfyServerAsync();
                }
            });
#else
            SteamPanelComponent steamPanelComponent = await YIUIMgrComponent.Inst.Root.OpenPanelAsync<SteamPanelComponent>();
            //root.AddComponent<ConnectComfyUIComponent>();
#endif
        }

    }
}
