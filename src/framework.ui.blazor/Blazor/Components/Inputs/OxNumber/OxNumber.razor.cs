
using Microsoft.AspNetCore.Components;
using Syncfusion.EJ2.Blazor.Calendars;
using Syncfusion.EJ2.Blazor.Inputs;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxNumberBase<TValue> : OxInputBase<TValue>
    {
       protected EjsNumericTextBox<TValue> objNumberEdit { get; set; }
       [Parameter] public int Decimals { get; set; } = 0;
       [Parameter] public bool ShowSpinButton { get; set; } = false;
        protected override Task OnParametersSetAsync()
        {
            
            if (typeof(TValue) == typeof(decimal?) || typeof(TValue) == typeof(decimal))
            {
                this.Format = "C2";
                this.Decimals = 2;
            }
            return base.OnParametersSetAsync();
        }
    }
}
