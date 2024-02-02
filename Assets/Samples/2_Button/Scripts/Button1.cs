using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Button1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Components")]
        [SerializeField] RectTransform rectTransform;
        [SerializeField] TMP_Text text;

        [Header("Settings")]
        [SerializeField] float duration = 0.07f;
        [SerializeField] Ease ease = Ease.OutQuad;
        [SerializeField] Vector2 animationSizeDelta = new(14f, 7f);
        [SerializeField] float animationFontSizeDelta = 1f;

        Vector2 initialSize;
        float initialFontSize;

        CompositeMotionHandle motionHandles = new();

        void Start()
        {
            initialSize = rectTransform.sizeDelta;
            initialFontSize = text.fontSize;
        }

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(rectTransform.sizeDelta, initialSize - animationSizeDelta, duration)
                .WithEase(ease)
                .BindToSizeDelta(rectTransform)
                .AddTo(motionHandles);

            LMotion.Create(text.fontSize, initialFontSize - animationFontSizeDelta, duration)
                .WithEase(ease)
                .BindToFontSize(text)
                .AddTo(motionHandles);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(rectTransform.sizeDelta, initialSize, duration)
                .WithEase(ease)
                .BindToSizeDelta(rectTransform)
                .AddTo(motionHandles);

            LMotion.Create(text.fontSize, initialFontSize, duration)
                .WithEase(ease)
                .BindToFontSize(text)
                .AddTo(motionHandles);
        }
    }
}
