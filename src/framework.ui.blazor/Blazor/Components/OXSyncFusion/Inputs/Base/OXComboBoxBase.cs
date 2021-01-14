using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Ui.Blazor.Builders;
using Orion.Framework.Ui.Blazor.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public abstract class FwBaseCombo<TValue> : OXSFBase<TValue>
    {
        [Parameter] public bool AllowFiltering { get; set; } = true;
        [Parameter] public bool IgnoreAccent { get; set; } = true;
        [Parameter] public string FieldText { get; set; } = "Text";
        [Parameter] public string FieldValue { get; set; } = "Key";
        [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter] public IEnumerable<DataItemCombo> DataSource { get; set; }
    }
}
