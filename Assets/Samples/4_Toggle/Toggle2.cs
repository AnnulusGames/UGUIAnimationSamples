using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Toggle2 : MonoBehaviour, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] Image fill;
        [SerializeField] Image handle;

        [Header("Settings")]
        [SerializeField] float slideOffset = 40f;
        [SerializeField] float duration = 0.1f;
        [SerializeField] Ease ease = Ease.OutSine;

        Vector2 initialHandlePosition;
        bool isOn;

        CompositeMotionHandle motionHandles = new();

        void Start()
        {
            initialHandlePosition = handle.rectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            motionHandles.Cancel();

            if (isOn)
            {
                LMotion.Create(handle.rectTransform.anchoredPosition.x, initialHandlePosition.x, duration)
                    .WithEase(ease)
                    .BindToAnchoredPositionX(handle.rectTransform)
                    .AddTo(motionHandles);

                LMotion.Create(fill.color.a, 0f, duration)
                    .WithEase(ease)
                    .BindToColorA(fill)
                    .AddTo(motionHandles);
            }
            else
            {
                LMotion.Create(handle.rectTransform.anchoredPosition.x, initialHandlePosition.x + slideOffset, duration)
                    .WithEase(ease)
                    .BindToAnchoredPositionX(handle.rectTransform)
                    .AddTo(motionHandles);

                LMotion.Create(fill.color.a, 1f, duration)
                    .WithEase(ease)
                    .BindToColorA(fill)
                    .AddTo(motionHandles);
            }

            isOn = !isOn;
        }
    }
}
