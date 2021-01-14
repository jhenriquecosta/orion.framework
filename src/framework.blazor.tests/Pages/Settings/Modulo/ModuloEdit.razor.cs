
using Orion.Framework.DataLayer.Dao;
using Orion.Framework.Ui.Blazor.Components;

using Orion.Prometheus.Domain.Entities;

namespace Orion.Prometheus.Blazor.Pages
{
    public class ModuloEditBase : OXPageModel<Modulo,DbDao<Modulo>>
    {
        protected Modulo Ancestral { get; set; }
        public void GetAncestral(Modulo modulo)
        {
            this.Ancestral = modulo;

           
        }

        //protected override Task OnAfterRenderAsync(bool first)
        //{

        //    var result = TransferObjectService.Result as object[,];
        //    if (result != null)
        //    {
        //        DataModel = result[0, 0] as Organizacao;
        //        DataModel.Tipo = TipoPessoa.Juridica;          
        //        ActionModel = (XTActionModel)result[0, 1];
        //        UrlSource = (string)result[0, 2];
        //        ShowDialog(DataModel, ActionModel);
        //    }
        //    return base.OnAfterRenderAsync(first);

        //}

        //protected override async Task<bool> OnSaveOrUpdateAsync(Organizacao argValue)
        //{
        //    var result = await base.OnSaveOrUpdateAsync(argValue);
        //    if (!result) return false;
        //    TransferObjectService.Result = null;
        //    NavigationManager.NavigateTo(UrlSource);
        //    return true;
        //}
        //protected override async Task<bool> OnDeleteAsync(Organizacao argValue)
        //{

        //    var result = await base.OnDeleteAsync(argValue);
        //    if (!result) return false;
        //    TransferObjectService.Result = null;
        //    NavigationManager.NavigateTo(UrlSource);
        //    return true;
        //}
        //protected void OnCloseForm()
        //{
        //    this.XTEditForm.Hide();
        //    TransferObjectService.Result = null;
        //    NavigationManager.NavigateTo(UrlSource);
        //}

    }

}
