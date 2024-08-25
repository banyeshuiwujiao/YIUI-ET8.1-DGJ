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
        public Process ComfyProcess { get; set; }
        public string cmd = ".\\envs\\DGJ\\python.exe -s .\\main.py --windows-standalone-build --disable-xformers  --disable-auto-launch";
        public string workDirectory = "E:\\Comfyui";
#if UNITY_OSX || UNITY_LINUX
        public    string app = "bash";
        public    string arguments = "-c";
#else
        public string app = "cmd.exe";
        public string arguments = "/c";

        public long interval = 1000*60*10+100; // 10min∂‡“ªµ„;
        public bool serverOn = false;
#endif
    }
}