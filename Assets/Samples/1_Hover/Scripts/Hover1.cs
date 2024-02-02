using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Hover1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] Image fill;
        [SerializeField] TMP_Text label;

        [Header("Settings")]
        [SerializeField] Color hoverFillColor = Color.white;
        [SerializeField] Color hoverLabelColor = Color.white;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.2f;

        Color initialFillColor;
        Color initialLabelColor;

        CompositeMotionHandle motionHandles = new(2);

        void Awake()
        {
            initialFillColor = fill.color;
            initialLabelColor = label.color;
        }

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(fill.color, hoverFillColor, duration)
                .WithEase(ease)
                .BindToColor(fill)
                .AddTo(motionHandles);

            LMotion.Create(label.color, hoverLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(fill.color, initialFillColor, duration)
                .WithEase(ease)
                .BindToColor(fill)
                .AddTo(motionHandles);

            LMotion.Create(label.color, initialLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }
    }
}