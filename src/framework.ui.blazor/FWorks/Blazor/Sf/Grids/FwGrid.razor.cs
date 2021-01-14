using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Reflection;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Ui.Blazor.Components;

namespace  Orion.Framework.Ui.FWorks.Blazor.Sf.Grids
{
    public abstract class FwBaseGrid<TModel> : OXGridComponentBase<TModel>  
    {
        protected SfGrid<TModel> SfGridView { get; set; }
        [Parameter] public RenderFragment GridCommand { get; set; }
        [Parameter] public EventCallback<CommandClickEventArgs<TModel>> OnCommand { get; set; }

        public Task OnCommandClicked(CommandClickEventArgs<TModel> args)
        {
            return OnCommand.InvokeAsync(args);
        }
        public override Task ReloadAsync(object datasource)
        {
            this.DataSource = datasource as IEnumerable<TModel>;
            this.SfGridView.Refresh();
            this.OnChanged();
            return Task.CompletedTask;
        }

    }

   
       

}
