using AlphaFillEffect;
using Vortice.Direct2D1;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace YMM4SamplePlugin.VideoEffect.SampleHLSLShaderVideoEffect
{
    internal class AlphaFillEffectProcessor : IVideoEffectProcessor
    {
        readonly AlphaFillEffect item;
        bool isFirst = true;
        double threshold, r, g, b, invert;
        ColorMode colorMode;

        readonly AlphaFillCustomEffect? effect;
        readonly ID2D1Image? output;
        ID2D1Image? input;

        public ID2D1Image Output => output ?? input ?? throw new NullReferenceException();

        public AlphaFillEffectProcessor(IGraphicsDevicesAndContext devices, AlphaFillEffect item)
        {
            this.item = item;

            effect = new AlphaFillCustomEffect(devices);
            if (!effect.IsEnabled)
            {
                effect.Dispose();
                effect = null;
            }
            else
            {
                output = effect.Output;
            }
        }

        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
            effect?.SetInput(0, input, true);
        }

        public void ClearInput()
        {
            effect?.SetInput(0, null, true);
        }

        public DrawDescription Update(EffectDescription effectDescription)
        {
            if (effect is null)
                return effectDescription.DrawDescription;

            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;

            var threshold = item.Threshold.GetValue(frame, length, fps) / 255;
            var r = item.Color.R / 255f;
            var g = item.Color.G / 255f;
            var b = item.Color.B / 255f;
            var invert = item.Invert ? 1 : 0;
            var colorMode = item.ColorMode;

            if (isFirst || this.threshold != threshold)
                effect.Threshold = (float)threshold;

            if (isFirst || this.r != r)
                effect.R = (float)r;

            if (isFirst || this.g != g)
                effect.G = (float)g;

            if (isFirst || this.b != b)
                effect.B = (float)b;

            if (isFirst || this.invert != invert)
                effect.Invert = invert;

            if (isFirst || this.colorMode != colorMode)
            {
                effect.KeepColor = colorMode == ColorMode.KeepColor ? 1 : 0;
            }

            isFirst = false;

            this.threshold = threshold;
            this.r = r;
            this.g = g;
            this.b = b;
            this.invert = invert;
            this.colorMode = colorMode;

            return effectDescription.DrawDescription;
        }

        public void Dispose()
        {
            output?.Dispose();
            effect?.SetInput(0, null, true);
            effect?.Dispose();
        }
    }
}