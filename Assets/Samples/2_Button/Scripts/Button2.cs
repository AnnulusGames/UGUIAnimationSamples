using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Button2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Components")]
        [SerializeField] Image hover;
        [SerializeField] Image fill;

        [Header("Settings")]
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float hoverDuration = 0.15f;
        [SerializeField] float fillDuration = 0.15f;
        [SerializeField] float fadeOutDuration = 0.25f;

        CompositeMotionHandle hoverMotionHandles = new();
        CompositeMotionHandle buttonMotionHandles = new();

        void OnDestroy()
        {
            buttonMotionHandles.Cancel();
            hoverMotionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hoverMotionHandles.Cancel();

            LMotion.Create(0f, 1f, hoverDuration)
                .WithEase(ease)
                .BindToColorA(hover)
                .AddTo(hoverMotionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoverMotionHandles.Cancel();

            LMotion.Create(1f, 0f, hoverDuration)
                .WithEase(ease)
                .BindToColorA(hover)
                .AddTo(hoverMotionHandles);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonMotionHandles.Complete();

            fill.rectTransform.position = eventData.position;

            LMotion.Create(0f, 1f, fillDuration)
                .WithEase(ease)
                .BindToColorA(fill)
                .AddTo(buttonMotionHandles);

            LMotion.Create(Vector3.zero, Vector3.one, fillDuration)
                .WithEase(ease)
                .BindToLocalScale(fill.transform)
                .AddTo(buttonMotionHandles);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonMotionHandles.Complete();

            LMotion.Create(1f, 0f, fadeOutDuration)
                .WithEase(ease)
                .BindToColorA(fill)
                .AddTo(buttonMotionHandles);
        }
    }
}
