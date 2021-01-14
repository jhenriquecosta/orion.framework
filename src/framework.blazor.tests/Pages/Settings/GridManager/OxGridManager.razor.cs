using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;
using Orion.Framework;
using Orion.Framework.Ui.Blazor.Common;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Ui.Blazor.Components.OXBlazorise;
using Orion.Framework.Ui.Blazor.Components.OXSyncFusion;
using Orion.Framework.Ui.Blazor.Enums;
using System;

namespace Orion.Prometheus.Blazor.Pages
{

    public class OxGridManagerBase : OXBase
    {
        [Parameter] public RenderFragment ComponentContent { get; set; }
        protected dynamic viewEdit { get; set; }
        protected dynamic oxGrid { get; set; }
    
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            ComponentContent = builder =>
            {
                BuilderComponent(builder);
            };
           
        }
       
        private void BuilderComponent(RenderTreeBuilder builder)
        {

            var gridAux = AppDataTransfer.Result as GridGenericView;
            
            if (gridAux == null)
            {
                Toast.ShowInfo("Erro ao carregar a entidade!");
                return;
            }
            var seq = 0;
            Type generic = typeof(OXGrid<>);
            Type[] typeArgs = { gridAux.Model };
            Type constructed = generic.MakeGenericType(typeArgs);
           
            viewEdit = TypeHelper.CreateInstance(gridAux.View);
            oxGrid = TypeHelper.CreateInstance(constructed);
            var grid = TypeHelper.ConvertTo(oxGrid, constructed);
            //create layout 
          
            builder.AddAttribute(seq++, "ChildContent", (RenderFragment)((builder2) =>
            {
                builder2.AddMarkupContent(seq++, "\r\n        ");
                builder2.OpenComponent<OXPanel>(3);
                    builder2.AddAttribute(seq++, "Title", gridAux.Caption);
                    builder2.AddAttribute(seq++, "SubTitle", gridAux.Descricao);
                    builder2.AddAttribute(seq++, "ChildContent", (RenderFragment)((builder3) =>
                    {
                        RenderGrid(builder3,seq);

                    }));
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
            builder.AddMarkupContent(seq++, "\r\n");

            var viewComp = viewEdit.GetType();
            builder.OpenComponent(seq++,viewComp);
            builder.AddAttribute(seq++, "Caption", gridAux.Caption);
            builder.AddAttribute(seq++, "GridView", grid);
            builder.AddComponentReferenceCapture(seq++, (value) => 
            {
                var getRef = TypeHelper.ConvertTo(value, gridAux.View);
                viewEdit = getRef;
            });
            builder.CloseComponent();
        }
      

        private void RenderGrid(RenderTreeBuilder builder,int seq)
        {
            var constructed = oxGrid.GetType();

            builder.AddMarkupContent(seq++, "\r\n            ");
            builder.OpenComponent(seq++,constructed);
                    builder.AddAttribute(seq++, "AllowButtons", RuntimeHelpers.TypeCheck<System.Boolean>(true));
                    builder.AddAttribute(seq++, "AllowPrint", RuntimeHelpers.TypeCheck<System.Boolean>(true));
                    builder.AddAttribute(seq++, "OnAdd", RuntimeHelpers.TypeCheck(EventCallback.Factory.Create(this, (value) => viewEdit.ShowDialog(null, OxActionModel.New))));
                    builder.AddAttribute(seq++, "OnEdit", RuntimeHelpers.TypeCheck(EventCallback.Factory.Create(this,(value) => viewEdit.ShowDialog(value, OxActionModel.Edit))));
                    builder.AddAttribute(seq++, "OnDelete", RuntimeHelpers.TypeCheck(EventCallback.Factory.Create(this,(value) => viewEdit.ShowDialog(value, OxActionModel.Delete))));
            builder.AddComponentReferenceCapture(seq++, (value) =>
                    {
                        var getRef = TypeHelper.ConvertTo(value, constructed);
                        oxGrid =getRef;
                    });
            builder.CloseComponent();
            builder.AddMarkupContent(seq++, "\r\n        ");       
        }
        

    }
}
