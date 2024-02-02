using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Hover5 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] Image fill;
        [SerializeField] TMP_Text label;

        [Header("Settings")]
        [SerializeField] Color hoverFillColor = Color.white;
        [SerializeField] Color hoverLabelColor = Color.white;
        [SerializeField] float hoverCharacterSpacing = 12f;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.25f;

        Color initialFillColor;
        Color initialLabelColor;

        CompositeMotionHandle motionHandles = new(3);

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

            LMotion.Create(label.characterSpacing, hoverCharacterSpacing, duration)
                .WithEase(ease)
                .BindWithState(label, (x, label) =>
                {
                    label.characterSpacing = x;
                })
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

            LMotion.Create(label.characterSpacing, 0f, duration)
                .WithEase(ease)
                .BindWithState(label, (x, label) =>
                {
                    label.characterSpacing = x;
                })
                .AddTo(motionHandles);
        }
    }
}