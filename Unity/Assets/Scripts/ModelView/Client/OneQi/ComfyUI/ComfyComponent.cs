using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class ComfyComponent : Entity, IAwake, IDestroy
    {
        public Process comfyProcess;
        public Process ComfyProcess
        {
            get
            {
                return this.comfyProcess;
            }
            set
            {
                this.comfyProcess = value;
            }
        }
        public string cmd = ".\\envs\\DGJ\\python.exe -s .\\main.py --windows-standalone-build --disable-xformers  --disable-auto-launch";
        public string workDirectory = "E:\\Comfyui";
#if UNITY_OSX || UNITY_LINUX
        public    string app = "bash";
        public    string arguments = "-c";
#else
        public string app = "cmd.exe";
        public string arguments = "/c";
#endif
        public readonly long interval = 1000*60*5+100; // 5min∂‡“ªµ„;
        public bool serverOn = false;
        public bool ServerOn
        {
            get
            {
                return this.serverOn;
            }
            set
            {
                this.serverOn = value;
            }
        }
        public bool startTimeOut = false;

    }
}