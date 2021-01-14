using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Helpers;
using Orion.Framework.Web.Applications.Services;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Inputs
{
    public class FwBaseComboBox<TItem> : FwBaseCombo<TItem,object>
    {
        protected internal int? InternalValue { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await BuilderDataSourceAsync();
            if (this.Value == null) this.Value = 0;    //Value.To<int>();
            InternalValue = Value.To<int?>();
            if (InternalValue == 0) InternalValue = null;
            
        }
        protected async Task BuilderDataSourceAsync()
        {
            if (DataSource == null)
            {
                if (typeof(TItem).IsEnum)
                {
                    var lstData = System.Enum.GetNames(typeof(TItem))
                    .ToDictionary(key => HelperEnum.GetValue<TItem>(key), value => HelperEnum.GetDescription<TItem>(value))
                    .ToList().Select(x => new DataItemCombo { Key = x.Key, Text = x.Value, Value = x.Key }).ToList();
                    DataSource = lstData;
                }
                else
                {
                    DataSource = await LookupService.GetLookUpAsync<TItem>();
                }
            }
        }
        protected void OnValueChanged(ChangeEventArgs<int?> args)
        {
            try
            {
                if (args.Value != null)
                {
                    if (args.Value > 0)
                    {
                        var rsFound = DataSource.FirstOrDefault(f => f.Key == args.Value);
                        if (rsFound != null)
                        {
                            var record = (TItem)rsFound.Value;
                            ValueChanged.InvokeAsync(record);
                        }
                        else
                        {
                            ValueChanged.InvokeAsync(default(TItem));
                        }
                    }
                    else
                    {
                        ValueChanged.InvokeAsync(default(TItem));
                    }
                }
                else
                {

                    ValueChanged.InvokeAsync(default(TItem));

                }
            }
            catch (Exception ex)
            {
                Toast.ShowError(ex.Message, "Erro");
            }
           // this.OnChanged();
        }
    }
}