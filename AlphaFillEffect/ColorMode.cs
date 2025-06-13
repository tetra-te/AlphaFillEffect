using System.ComponentModel.DataAnnotations;

namespace AlphaFillEffect
{
    internal enum ColorMode
    {
        [Display(Name = "指定色で塗りつぶす", Description = "指定色で塗りつぶす")]
        Overwrite = 0,

        [Display(Name = "色を保持", Description = "色を保持")]
        KeepColor = 1
    }
}