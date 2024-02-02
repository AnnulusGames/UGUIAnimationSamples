using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class Hover3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] Image fill;
        [SerializeField] TMP_Text label;

        [Header("Settings")]
        [SerializeField] Color hoverLabelColor = Color.white;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.2f;

        Color initialLabelColor;

        CompositeMotionHandle motionHandles = new(2);

        void Awake()
        {
            initialLabelColor = label.color;
        }

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(fill.transform.localScale, Vector3.one, duration)
                .WithEase(ease)
                .BindToLocalScale(fill.transform)
                .AddTo(motionHandles);

            LMotion.Create(label.color, hoverLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            motionHandles.Cancel();

            LMotion.Create(fill.transform.localScale, Vector3.zero, duration)
                .WithEase(ease)
                .BindToLocalScale(fill.transform)
                .AddTo(motionHandles);

            LMotion.Create(label.color, initialLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }
    }
}