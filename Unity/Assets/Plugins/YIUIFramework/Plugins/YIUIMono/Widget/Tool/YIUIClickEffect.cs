using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using UnityEngine.UI;

namespace YIUIFramework
{
    /// <summary>
    /// 点击按钮影响组件大小
    /// </summary>
    [AddComponentMenu("YIUIFramework/Widget/点击动画 【YIUIClickEffect】")]
    public class YIUIClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Tooltip("被影响的目标")]
        public RectTransform targetTsf;

        [Tooltip("变化大小 (倍数)")]
        public float scaleValue = 0.9f;

        [Tooltip("变小时间")]
        public float scaleTime = 0;

        [Tooltip("变大时间")]
        public float popTime = 0;

        private Button m_button;

        private Vector3 targetScale; //目标大小
        private Vector3 atScale;     //当前大小

        /// <summary>
        /// 可调整动画状态
        /// </summary>
        public Ease ease = Ease.OutElastic;

        private void Awake()
        {
            m_button = GetComponent<Button>(); //需要先挂button 否则无效
            if (targetTsf == null)             //如果没有目标则默认自己为目标
            {
                targetTsf = transform.gameObject.GetComponent<RectTransform>();
            }

            atScale     = targetTsf.localScale;
            targetScale = atScale * scaleValue;
        }

        private void OnDestroy()
        {
            Tween.StopAll(onTarget: targetTsf);
        }

        //按下
        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_button)
            {
                if (m_button.enabled && m_button.interactable)
                {
                    Tween.Scale(targetTsf, targetScale, scaleTime, ease:ease);
                }
            }
            else
            {
                Tween.Scale(targetTsf, targetScale, scaleTime, ease: ease);
            }
        }

        //抬起
        public void OnPointerUp(PointerEventData eventData)
        {
            Tween.StopAll(onTarget: targetTsf);
            Tween.Scale(targetTsf, atScale, popTime, ease: ease); //回到本来大小
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (targetTsf == null) //如果没有目标则默认自己为目标
            {
                targetTsf = transform.gameObject.GetComponent<RectTransform>();
            }
        }
        #endif
    }
}