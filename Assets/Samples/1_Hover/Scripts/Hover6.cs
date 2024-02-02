using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Hover6 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] Image pulse;
        [SerializeField] TMP_Text label;

        [Header("Settings")]
        [SerializeField] Color hoverLabelColor = Color.white;
        [SerializeField] float pulseSize = 40f;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.2f;

        Vector2 initialPulseImageSize;
        Color initialPulseColor;
        Color initialLabelColor;

        CompositeMotionHandle motionHandles = new(3);

        void Awake()
        {
            initialPulseImageSize = pulse.rectTransform.sizeDelta;
            initialPulseColor = pulse.color;
            initialLabelColor = label.color;
        }

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            motionHandles.Complete();

            LMotion.Create(initialPulseImageSize, new Vector2(initialPulseImageSize.x + pulseSize, initialPulseImageSize.y + pulseSize), duration)
                .WithEase(ease)
                .BindToSizeDelta(pulse.rectTransform)
                .AddTo(motionHandles);

            LMotion.Create(initialPulseColor.a, 0f, duration)
                .WithEase(ease)
                .BindToColorA(pulse)
                .AddTo(motionHandles);

            LMotion.Create(label.color, hoverLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            motionHandles.Complete();

            LMotion.Create(label.color, initialLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }
    }
}