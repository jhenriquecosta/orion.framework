
using Blazorise;
using Microsoft.AspNetCore.Components;
using Orion.Framework;
using Orion.Framework.DataLayer.Dao;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Helpers;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Ui.Blazor.Enums;
using Orion.Framework.Ui.Blazor.Icons;
using Orion.Prometheus.Domain.Entities;
using Orion.Prometheus.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Prometheus.Blazor.Pages
{
    public class SysComponenteManagerBase : OXPageModel<SysComponent, SysComponentService>
    {
        Result<SysComponent> DataResult = new Result<SysComponent>();

        [Inject]
        protected IconManager iconManager {get;set;}
        protected List<DataItemCombo> Icons { get; set; }
        protected string TipoComponente { get; set; } = "SISTEMA";
        protected bool AllowEdit { get; set; } = false;

        protected SysComponent AncestralPageModel { get; set; } = new SysComponent();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Icons = iconManager.GetIcons();
        }
        protected override async Task<bool> OnSaveOrUpdateAsync()
        {
            if (PageModel.Item == SysComponentItem.Form)
            {
                if (PageModel.Area.IsNull())
                {
                    await WarningAsync("Informe Area");
                    return false;
                }
                if (PageModel.Home.IsNull())
                {
                    await WarningAsync("Informe Home");
                    return false;
                }
                if (PageModel.Target.IsNull())
                {
                    await WarningAsync("Informe o nome do FORMULARIO!");
                    return false;
                }
            }
            return await base.OnSaveOrUpdateAsync();
        }
        
        protected async Task OnManagerItemAsync(SysComponent item, OxActionModel actionModel)
        {
            DataResult = new Result<SysComponent>
            {
                IsOk = true,
                Data = item
            };
            // var treeItem = item;
            if (actionModel == OxActionModel.New)
            {

                if ((DataResult = await GetResultAsync(await DataContext.AddComponentAsync(item))).IsOk)
                {
                    AncestralPageModel = item ?? new SysComponent();
                }
                else return;
                
            }
            else if (actionModel == OxActionModel.Edit)
            {
               
            }
            else if (actionModel == OxActionModel.Delete)
            {

                if (item == null)
                {
                    var erro = $"Selecione um item para remover!";
                    Toast.ShowWarning(erro);
                    return;
                }
                var hasChild = await TreeEntityHelper.CounterChildrensAsync<SysComponent>(f => f.Ancestral == item.Id);
                if (hasChild > 0)
                {
                    var erro = $"Não é possivel remover item com elementos!";
                    Toast.ShowWarning(erro);
                    return;
                }
                
            }
            PageModel = DataResult.Data;
            GetCaption();
            if (DataResult.IsOk) this.ModalEdit.Show();

        }
        protected void GetCaption()
        {
            this.AllowEdit = false;
            if (PageModel.Item == SysComponentItem.System)
            {
                this.Caption = "Manutenção de Sistema";
                this.TipoComponente = "SISTEMA";
            }
            if (PageModel.Item == SysComponentItem.Module)
            {
                this.Caption = "Manutenção de Módulo";
                this.TipoComponente = "MÓDULO";
               

            }
            if (PageModel.Item == SysComponentItem.Application)
            {
                this.Caption = "Manutenção de Aplicação";
                this.TipoComponente = "APLICAÇÃO";
                 
            }
            if (PageModel.Item == SysComponentItem.Form)
            {
                this.Caption = "Manutenção de Formulário";
                this.TipoComponente = "FORMULÁRIO";
                this.AllowEdit = true;
            }
            
        }
    }
    

}
