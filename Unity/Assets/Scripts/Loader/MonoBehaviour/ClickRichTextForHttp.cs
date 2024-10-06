using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ET.Client
{
    public class ClickRichTextForHttp : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI text;
        private void Start()
        {
                text = GetComponent<TextMeshProUGUI>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 pos = new Vector3(eventData.position.x, eventData.position.y, 0);
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, pos, YIUIMgrComponent.Inst.UICamera); //--UI���
            ////Canvas��Ⱦģʽ=Overlayʱ
            //int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, pos, null); //--UI���
            if (linkIndex > -1)
            {
                TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
                Debug.Log(linkInfo.GetLinkText());
                Debug.Log(linkInfo.GetLinkID());
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }
    }
}