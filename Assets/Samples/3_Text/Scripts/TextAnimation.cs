using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public sealed class TextAnimation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Button button1;
        [SerializeField] Button button2;
        [SerializeField] Button button3;
        [SerializeField] Button button4;
        [SerializeField] Button button5;
        [SerializeField] Button button6;
        [SerializeField] TMP_Text text;

        Color color;

        const string ColorCode = "#FF5353";
        static readonly string FillerTextWithRichtext = $"<color={ColorCode}>Grumpy wizards</color> make <b>toxic brew</b> for the evil <i>Queen</i> and <i>Jack</i>.";
        static readonly string FillerText = "Grumpy wizards make toxic brew for the evil Queen and Jack.";

        readonly CompositeMotionHandle motionHandles = new();

        void Start()
        {
            ColorUtility.TryParseHtmlString(ColorCode, out color);

            button1.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation1())
                .AddTo(destroyCancellationToken);

            button2.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation2())
                .AddTo(destroyCancellationToken);

            button3.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation3())
                .AddTo(destroyCancellationToken);

            button4.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation4())
                .AddTo(destroyCancellationToken);

            button5.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation5())
                .AddTo(destroyCancellationToken);

            button6.OnClickAsAsyncEnumerable()
                .Subscribe(_ => Animation6())
                .AddTo(destroyCancellationToken);
        }

        void OnDestroy()
        {
            motionHandles.Complete();
        }

        void Animation1()
        {
            motionHandles.Complete();

            LMotion.String.Create512Bytes("", FillerTextWithRichtext, 0.05f * FillerText.Length)
                .WithRichText()
                .BindToText(text)
                .AddTo(motionHandles);
        }

        void Animation2()
        {
            motionHandles.Complete();

            LMotion.String.Create512Bytes("", FillerTextWithRichtext, 0.05f * FillerText.Length)
                .WithRichText()
                .WithScrambleChars(ScrambleMode.Lowercase)
                .BindToText(text)
                .AddTo(motionHandles);
        }

        void Animation3()
        {
            motionHandles.Complete();

            text.text = FillerText;
            text.ForceMeshUpdate(true);

            for (var i = 0; i < text.textInfo.characterCount; i++)
            {
                LMotion.Create(-90f, 0f, 0.25f)
                    .WithEase(Ease.OutBack)
                    .WithDelay(i * 0.05f, skipValuesDuringDelay: false)
                    .BindToTMPCharEulerAnglesY(text, i)
                    .AddTo(motionHandles);
            }
        }

        void Animation4()
        {
            motionHandles.Complete();

            text.text = FillerText;
            text.ForceMeshUpdate(true);

            for (var i = 0; i < text.textInfo.characterCount; i++)
            {
                LMotion.Create(Vector3.zero, Vector3.one, 0.2f)
                    .WithEase(Ease.OutSine)
                    .WithDelay(i * 0.05f, skipValuesDuringDelay: false)
                    .BindToTMPCharScale(text, i)
                    .AddTo(motionHandles);

                LMotion.Create(-50f, 0f, 0.2f)
                    .WithEase(Ease.OutSine)
                    .WithDelay(i * 0.05f, skipValuesDuringDelay: false)
                    .BindToTMPCharPositionY(text, i)
                    .AddTo(motionHandles);

                LMotion.Create(Color.white, color, 0.2f)
                    .WithEase(Ease.OutSine)
                    .WithDelay(0.3f + i * 0.05f, skipValuesDuringDelay: false)
                    .BindToTMPCharColor(text, i)
                    .AddTo(motionHandles);
            }
        }

        void Animation5()
        {
            motionHandles.Complete();

            text.text = FillerText;
            text.ForceMeshUpdate(true);

            for (var i = 0; i < text.textInfo.characterCount; i++)
            {
                LMotion.Punch.Create(Vector3.one, Vector3.one * 0.7f, 1.4f)
                    .WithEase(Ease.OutQuad)
                    .WithDelay(i * 0.025f, skipValuesDuringDelay: false)
                    .WithFrequency(7)
                    .BindToTMPCharScale(text, i)
                    .AddTo(motionHandles);
            }
        }

        void Animation6()
        {
            motionHandles.Complete();

            text.text = FillerText;
            text.ForceMeshUpdate(true);

            for (var i = 0; i < text.textInfo.characterCount; i++)
            {
                var shakeDuration = 1.4f * i * 0.025f;

                LMotion.Create(color, Color.white, 0.07f)
                    .WithEase(Ease.OutSine)
                    .WithDelay(i * 0.025f, skipValuesDuringDelay: false)
                    .BindToTMPCharColor(text, i)
                    .AddTo(motionHandles);

                LMotion.Shake.Create(Vector3.zero, Vector3.one * 30f, shakeDuration)
                    .WithFrequency((int)(shakeDuration / 0.2f))
                    .WithDampingRatio(1)
                    .BindToTMPCharPosition(text, i)
                    .AddTo(motionHandles);
            }
        }
    }
}