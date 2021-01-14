using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.TreeGrid;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Grids
{

    public class FwBaseGridTree<TModel> : OXGridComponentBase<TModel>
    {
        protected IEnumerable<TModel> _datasource;
        protected SfTreeGrid<TModel> TreeGrid { get; set; }
        protected IEnumerable<TModel> GetDataSource { get => _datasource; set { _datasource = value; OnChanged(); } }
        [Parameter] public double ColumnIndex { get; set; } = 1;
        [Parameter] public string ColumnId { get; set; } = "Id";
        [Parameter] public string ColumnParent { get; set; } = "Ancestral";   
        [Parameter] public EventCallback<TModel> OnAddItem { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            GetDataSource = DataSource;
        }

        public override Task ReloadAsync(object data)
        {
            if (this.TreeGrid.IsNull()) return Task.CompletedTask;

            this.GetDataSource = data as IEnumerable<TModel>;
            this.OnChanged();
            this.TreeGrid.Refresh();
            return Task.CompletedTask;
        }
        public void Filter(string column,string args)
        {
            if (args.IsEmpty())
            {
                this.TreeGrid.ClearFiltering();
            }
            else
            {
                this.TreeGrid.FilterByColumn(column, "equal", args);
            }
        }
        
    }
       

}
