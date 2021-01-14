using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class IndicatorOptions
    {
        public Type IndicatorTemplate { get; set; } = typeof(DefaultTemplate);

        public IndicatorChildContentHideModes ChildContentHideMode { get; set; } = IndicatorChildContentHideModes.CssDisplayNone;
    }

    public enum IndicatorChildContentHideModes
    {
        CssDisplayNone = 0,
        CssVisibilityHidden = 1,
        RemoveFromTree = 2
    }
}
