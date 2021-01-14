﻿using Microsoft.AspNetCore.Components;
using Orion.Framework.Domains.Attributes;

using Orion.Framework.Ui.Blazor.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Grids
{
    public abstract class OXGridComponentBase<TModel> : FWorkBlazorComponent, IOxGridBase
    {
        protected string[] Tool { get; set; }
        protected IList<ColumnModel> GetColumnsFromModel;
        [Parameter] public string ColumnFields { get; set; }
        [Parameter] public IEnumerable<TModel> DataSource { get; set; }
        [Parameter] public TModel CurrentModel { get; set; }
        [Parameter] public bool AllowFilter { get; set; } = false;
        [Parameter] public bool AllowResize { get; set; } = false;
        [Parameter] public bool AllowPrint { get; set; } = false;
        [Parameter] public bool AllowPaging { get; set; } = true;
        [Parameter] public bool AllowButtons { get; set; } = false;
        [Parameter] public bool AllowButtonReport { get; set; } = false;
        [Parameter] public EventCallback OnAdd { get; set; }
        [Parameter] public EventCallback<TModel> OnEdit { get; set; }
        [Parameter] public EventCallback<TModel> OnRemove { get; set; }
        [Parameter] public EventCallback<TModel> OnSelect { get; set; }
        [Parameter] public EventCallback OnPrint { get; set; }

        protected bool IsShouldRender = true;

        public virtual Task ReloadAsync(object data)
        {
            return Task.CompletedTask;
        }
        protected override async Task OnParametersSetAsync()
        {
            //await base.OnParametersSetAsync();
            if (AllowPrint)
            {
                Tool = (new string[] { "Search", "Print" });
                GetColumnsFromModel = GetColumns();
            }

        }
        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }
        public async Task OnRowSelected(RowSelectEventArgs<TModel> args)
        {
            try
            {
                CurrentModel = DataSource.SingleOrDefault(f => f.Equals(args.Data));
                await OnSelect.InvokeAsync(CurrentModel);
                IsShouldRender = false;
            }
            catch (Exception ex)
            {
                Toast.ShowWarning($"Erro ao carregar dados da entidade {typeof(TModel)} <br> Erro {ex.Message}");
            }
        }




        private IList<ColumnModel> GetColumns()
        {
            var lstProperties = typeof(TModel).GetProperties();
            var lstFields = new List<PropertyInfo>();
            var lstColumns = new List<ColumnModel>();

            if (!ColumnFields.IsNullOrEmpty())
            {
                lstProperties = ColumnFields.Split(',')
                    .Where(s => lstProperties.Any(x => x.Name.Equals(s)))
                    .Select(x => lstProperties.SingleOrDefault(f => f.Name.Equals(x))).ToArray();

            }
            var _order = 100;
            foreach (var prop in lstProperties)
            {
                var pType = prop.PropertyType;
                var pName = prop.Name;

                var _field = new ColumnModel
                {
                    Field = prop.Name,
                    Width = "150",
                    IsResizeble = false,
                    Caption = prop.Name,
                    IsPrimaryKey = false,
                    IsEditable = true,
                    Format = "",
                    Order = _order++
                };
                if (pName.Equals("Id"))
                {
                    _field.IsEditable = false;
                    _field.Order = 1;
                    _field.IsPrimaryKey = true;
                    _field.Width = "60";
                }
                if (prop.Name.Equals("Nome"))
                {
                    //_field.Width = "250";
                    _field.IsResizeble = true;
                    _field.Order = 2;
                }
                if (prop.PropertyType.IsCollection())
                {
                    _field.Ignore = true;
                }
                var attr = prop.GetAttribute<ModelFieldAttribute>();
                if (attr != null)
                {
                    if (attr.Order > 0) _field.Order = attr.Order;

                    _field.Ignore = attr.Ignore;
                    _field.IsResizeble = attr.AutoFit;
                    _field.IsEditable = attr.AllowEdit;
                    _field.Caption = attr.Display.IsEmpty() ? prop.Name : attr.Display;
                    _field.Field = attr.FieldMapping.IsEmpty() ? prop.Name : attr.FieldMapping;
                }
                if (pType == typeof(bool))
                {
                    _field.Width = "60";
                    _field.Type = ColumnType.Boolean;
                    _field.IsCheckBox = true;
                }
                else if (pType == typeof(decimal?) || pType == typeof(decimal))
                {
                    _field.Width = "120";
                    _field.Format = "n2";
                    _field.Type = ColumnType.Number;
                }
                else if (pType == typeof(int?) || pType == typeof(int))
                {
                    _field.Width = "60";
                    _field.Type = ColumnType.Number;
                }
                else if (pType == typeof(DateTime?) || pType == typeof(DateTime))
                {
                    _field.Width = "130";
                    _field.Format = "dd/MM/yyyy";
                    _field.Type = ColumnType.DateTime;
                }
                if (!AllowResize) _field.IsResizeble = false;
                lstColumns.Add(_field);
            }
            return lstColumns.Where(f => !f.Ignore).OrderBy(f => f.Order).ToList();
        }
    }
}
