
using Microsoft.AspNetCore.Components;
using Syncfusion.EJ2.Blazor.Calendars;
using Syncfusion.EJ2.Blazor.Inputs;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxDateBase<TValue> : OxInputBase<TValue>
    {
       protected EjsDatePicker<TValue> objDateEdit { get; set; }
       
    }
}
