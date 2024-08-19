
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ComponentOf(typeof(UI))]
	public class UILobbyComponent : Entity, IAwake
	{
		public GameObject enterMap;
		public GameObject sendHello;
        public GameObject callHello;

		public GameObject enterChat;
		public GameObject sendButton;
		public InputField inputField;
		public Text chatText;
		public GameObject chatPanel;

        public Text text;
	}
}
