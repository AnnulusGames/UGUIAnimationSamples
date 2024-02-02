using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Hover4 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] TMP_Text labelNormal;
        [SerializeField] TMP_Text labelHover;

        [Header("Settings")]
        [SerializeField] float width = 200f;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.2f;

        CompositeMotionHandle motionHandles = new(2);

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(labelNormal.rectTransform.anchoredPosition.x, width, duration)
                .WithEase(ease)
                .BindToAnchoredPositionX(labelNormal.rectTransform)
                .AddTo(motionHandles);

            LMotion.Create(labelHover.rectTransform.anchoredPosition.x, 0f, duration)
                .WithEase(ease)
                .BindToAnchoredPositionX(labelHover.rectTransform)
                .AddTo(motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(labelNormal.rectTransform.anchoredPosition.x, 0f, duration)
                .WithEase(ease)
                .BindToAnchoredPositionX(labelNormal.rectTransform)
                .AddTo(motionHandles);

            LMotion.Create(labelHover.rectTransform.anchoredPosition.x, -width, duration)
                .WithEase(ease)
                .BindToAnchoredPositionX(labelHover.rectTransform)
                .AddTo(motionHandles);
        }
    }
}