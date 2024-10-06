namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class ConnectComfyUIComponent : Entity, IAwake, IDestroy
    {
        public long ConnectionInterval = 8000; //1s=1000ms
        public long TimerId;

        public readonly long interval = 1000 * 60 * 5 + 100; // 5min∂‡“ªµ„;
        private bool serverOn = false;
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

        public bool isChina = false;


        public string gitUrl = "https://github.com/git-for-windows/git/releases/download/v2.36.1.windows.1/Git-2.36.1-64-bit.exe";
        public string gitProxy = "https://mirror.ghproxy.com/";
        public string pythonUrl = "https://www.python.org/ftp/python/3.10.11/python-3.10.11-amd64.exe";
        public string pipUrl = "https://bootstrap.pypa.io/get-pip.py";
        //public string comfyUIUrl = "https://github.com/comfyanonymous/ComfyUI/archive/refs/heads/master.zip";
        public string comfyUIUrl = "https://github.com/comfyanonymous/ComfyUI.git";

#if UNITY_OSX || UNITY_LINUX
        public    string app = "bash";
        public    string argument = "-c";
#else
        public string app = "cmd.exe";
        public string argument = "/c";
#endif
    }
}