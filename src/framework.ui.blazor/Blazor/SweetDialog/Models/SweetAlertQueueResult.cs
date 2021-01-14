using System.Collections.Generic;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class SweetAlertQueueResult
    {
        public IEnumerable<string> Value { get; set; }

        public DismissReason? Dismiss { get; set; }
    }
}
