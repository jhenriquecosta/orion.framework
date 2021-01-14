﻿using Orion.Framework.DataLayer.Web.Services;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Helpers;
using Syncfusion.EJ2.Blazor.DropDowns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxLookUpBase<TEntity,TValue> : OxComboBase<object>
    {
        protected EjsComboBox<int?>  objComboEdit { get; set; }
        protected internal int? InternalValue { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await BuilderDataSourceAsync();
            if (this.Value == null) this.Value = 0;    //Value.To<int>();
            InternalValue = Value.To<int?>();
            if (InternalValue == 0) InternalValue = null;
            await base.OnParametersSetAsync();
        }
        async Task BuilderDataSourceAsync()
        {
            if (DataSource == null)
            {
                if (typeof(TEntity).IsEnum)
                {
                    var lstData = System.Enum.GetNames(typeof(TEntity))
                    .ToDictionary(key => HelperEnum.GetValue<TEntity>(key), value => HelperEnum.GetDescription<TEntity>(value))
                    .ToList().Select(x => new DataItemCombo { Key = x.Key, Text = x.Value, Value = x.Key }).ToList();
                    DataSource = lstData;
                }
                else
                {
                    DataSource = await LookupService.GetLookUpAsync<TEntity>();
                }
                OnChanged();
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
                                var record = (TEntity)rsFound.Value;
                                ValueChanged.InvokeAsync(record);
                        }
                        else
                        {
                            ValueChanged.InvokeAsync(default(TEntity));
                        }
                    }
                    else
                    {
                        ValueChanged.InvokeAsync(default(TEntity));
                    }
                }
                else
                {

                    ValueChanged.InvokeAsync(default(TEntity));

                }
            }
            catch (Exception ex)
            {
              
            }
            this.OnChanged();
        }
    }
}
