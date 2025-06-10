using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin.Effects;

namespace YMM4SamplePlugin.VideoEffect.SampleHLSLShaderVideoEffect
{
    [VideoEffect("塗りつぶしアルファ", ["装飾"], ["fill", "alpha"])]
    internal class AlphaFillEffect : VideoEffectBase
    {
        public override string Label => "塗りつぶしアルファ";

        [Display(GroupName = "塗りつぶしアルファ", Name = "色", Description = "塗りつぶしの色")]
        [ColorPicker]
        public Color Color { get => color; set => Set(ref color, value); }
        Color color = Colors.White;

        [Display(GroupName = "塗りつぶしアルファ", Name = "閾値", Description = "不透明度が閾値以上の場合塗りつぶします")]
        [AnimationSlider("F0", "", 0, 256)]
        public Animation Threshold { get; set; } = new Animation(1, 0, 256);

        [Display(GroupName = "塗りつぶしアルファ", Name = "反転", Description = "塗りつぶし領域を反転します")]
        [ToggleSlider]
        public bool Invert { get => invert; set => Set(ref invert, value); }
        bool invert = false;

        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            return [];
        }

        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new AlphaFillEffectProcessor(devices, this);
        }

        protected override IEnumerable<IAnimatable> GetAnimatables() => [Threshold];
    }
}