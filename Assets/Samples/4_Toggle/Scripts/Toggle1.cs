using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Toggle1 : MonoBehaviour, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] Image checkmark;
        [SerializeField] Image fill;

        [Header("Settings")]
        [SerializeField] float duration = 0.1f;
        [SerializeField] float delay = 0.025f;
        [SerializeField] Ease ease = Ease.OutSine;

        bool isOn;

        CompositeMotionHandle motionHandles = new();

        public void OnPointerDown(PointerEventData eventData)
        {
            motionHandles.Cancel();

            if (isOn)
            {
                LMotion.Create(checkmark.rectTransform.localScale, Vector3.zero, duration)
                    .WithEase(ease)
                    .WithDelay(delay)
                    .BindToLocalScale(checkmark.rectTransform)
                    .AddTo(motionHandles);

                LMotion.Create(fill.color.a, 0f, duration)
                    .WithEase(ease)
                    .BindToColorA(fill)
                    .AddTo(motionHandles);
            }
            else
            {
                LMotion.Create(checkmark.rectTransform.localScale, Vector3.one, duration)
                    .WithEase(ease)
                    .BindToLocalScale(checkmark.rectTransform)
                    .AddTo(motionHandles);

                LMotion.Create(fill.color.a, 1f, duration)
                    .WithEase(ease)
                    .WithDelay(delay)
                    .BindToColorA(fill)
                    .AddTo(motionHandles);
            }

            isOn = !isOn;
        }
    }
}
