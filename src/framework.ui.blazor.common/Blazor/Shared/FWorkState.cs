using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orion.Framework.Security.Domain.Dtos;

namespace Orion.Framework.Ui.Blazor.Shared
{
    public class FWorkAppState
    {
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
        public void Update(NotifyProperties prop)
        {
            if (Notify != null)
            {
                this.Prop = prop;
                Notify.Invoke(prop);
            }
        }
        public NotifyProperties Prop
        {
            get; set;
        } = new NotifyProperties();

        public event Func<NotifyProperties, Task> Notify;
    }


    public class NotifyProperties
    {
        public bool HideSpinner { get; set; }

        public bool RestricMouseEvents { get; set; }
    }
}
