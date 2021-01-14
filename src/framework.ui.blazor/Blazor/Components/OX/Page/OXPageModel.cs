using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Linq;
using Orion.Framework.Validations;
using Orion.Framework.Domains;
using Orion.Framework.Ui.Blazor.Enums;
using Orion.Framework.Ui.Blazor.Components.OXSyncFusion;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orion.Framework.DataLayer.NH.Applications.Dao;

namespace Orion.Framework.Ui.Blazor.Components
{


    public abstract class OXPageModel<TModel> : OXPageService where TModel : class, IEntity<TModel, int>, new()
    {

        [Parameter] public string Caption { get; set; }

        protected OXModal ModalEdit { get; set; }
        protected TModel PageModel { get; set; }
        protected FWorkActionModel ActionModel { get; set; } = FWorkActionModel.New;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            PageModel = new TModel();            
        }
        public virtual void ShowDialog(TModel model, FWorkActionModel actionModel)
        {
            this.IsVisible = true;
            this.Init(model, actionModel);
            this.OnShowModal(ModalEdit);
            this.OnChanged();
        }
        public virtual void Init(TModel model, FWorkActionModel actionModel)
        {
            PageModel = model ?? new TModel();
            ActionModel = actionModel;
        }
        protected void OnHideModal(OXModal oxModal)
        {
            if (oxModal != null) oxModal.Hide();
        }
        protected void OnShowModal(OXModal oxModal)
        {
            if (oxModal != null) oxModal.Show();
        }
    }

    public abstract class OXPageModel<TModel, TDataService> : OXPageModel<TModel> where TDataService : DaoDomainService<TModel>  where TModel : class, IEntity<TModel, int>,new()
    {
        #region Properties


        [Inject] public TDataService DataContext { get; set; }
        [Parameter] public Expression<Func<TModel, bool>> Filter { get; set; } = null;
       
        [Parameter] public IOxGridBase GridView { get; set; }

        protected IEnumerable<TModel> GetAll { get; set; }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.OnFindAllAsync();
        }

        protected async Task RefreshGridAsync()
        {
            if (GridView != null) await GridView.ReloadAsync(GetAll);

        }
        protected virtual async Task OnPageRefreshAsync()
        {
            await this.OnFindAllAsync();          
            await RefreshGridAsync();
            OnHideModal(ModalEdit);            
        }
        
        protected virtual async Task<bool> OnSaveOrUpdateAsync()
        {
            var model = PageModel;
            var isValid = await IsValidAsync(model);
            if (!isValid) return false;
            var yes = await QuestionAsync($"Gravar os dados do registro {model} ?");
            if (!yes) return false;
            
            try
            {
                await DataContext.SaveOrUpdateAsync(model);
            }
            catch (Exception  ex)
            {
                
                var erro = $"O seguinte erro ocorreu ao SALVAR o registro solicitado:<br> <b>{ex.InnerException}</b>";
                await ErrorAsync(erro);
                return false;
            }
            Toast.ShowInfo("registro gravado...", "Gravado");
            await OnPageRefreshAsync();
            return true;
        }

        public async Task OnFindAllAsync()
        {
            await LoadingTaskService.StartTaskAsync(async (task) =>
            {
                await Task.Delay(5);
                GetAll = new List<TModel>();
                var result = await DataContext.FindAllAsync(Filter);
                GetAll = result.ToList();
            }, "on-panel-context", "Consultando registro...", "Aguarde..");
        }
         

        protected virtual async Task<bool> OnDeleteAsync()
        {
           
            var model = PageModel;
            var question = await QuestionAsync($"Deletar o registro {model}?");
            if (!question) return false;
            var item = model as IEntity;
            if (item.IsTransient())
            {
                Toast.ShowInfo("Registro não localizado!", "Deletando...");
                return false;
            }
            try
            {
                await DataContext.DeleteAsync(PageModel);
            }
            catch (Exception ex)
            {
                var erro = $"O seguinte erro ocorreu ao DELETAR o registro solicitado:<br> <b>{ex.Message}</b>";
                await ErrorAsync(erro);
                return false;
            }
            Toast.ShowInfo("registro deletado...", "Deletando...");
            await OnPageRefreshAsync();

            return true;
        }
      
       
        protected async Task<bool> IsValidAsync(TModel model)
        {
            var errorList = new StringBuilder();
            var isValid = true;
            var modelValid = (model as IValidation);
            if (model == null)
            {
                await ErrorAsync("Entidade nula!");
                return !isValid;
            }
            var errorMessages = modelValid.Validate();
            if (errorMessages.Count > 0)
            {
                foreach (var result in errorMessages)
                {
                    var item = $"Campo: <b>{result.MemberNames.First().ToString()}</b> Erro: <b>{result.ErrorMessage}</b></br>";
                    errorList.AppendLine(item);
                }
                await ErrorAsync(errorList.ToString());
                isValid = false;
            }
            return isValid;
        }
    }

}
