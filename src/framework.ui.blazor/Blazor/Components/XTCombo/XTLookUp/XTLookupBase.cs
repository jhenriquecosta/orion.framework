using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Web;
using Framework.ValueObjects;
using Framework.Web.Blazor.Components.Base;

namespace Framework.Web.Blazor.Components.XT
{ 
    public abstract class XTLookupBase : XTComponentBase
    {

        [Parameter] public object Value { get; set; }
        #region Members

        #endregion

        #region Methods

        protected void HandleSelectedValueChanged(object value)
        {
            Value = value;
            //busca data
            var record = DataSource.FirstOrDefault(f => f.Key == value.ToString().ToInt()).Value;
            ValueChanged.InvokeAsync(record);
        }

        #endregion

        #region Properties
       
        [Parameter] public IEnumerable<DataItem> DataSource { get; set; }

        [Parameter] public EventCallback<object> ValueChanged { get; set; }

       

        #endregion
    }

}
