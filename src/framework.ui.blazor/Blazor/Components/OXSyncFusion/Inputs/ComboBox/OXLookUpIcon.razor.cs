using Syncfusion.Blazor.DropDowns;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public class OXLookUpIconBase : FwBaseCombo<string>
    {   
        protected string InternalValue { get; set; }
      
        protected void OnValueChanged(ChangeEventArgs<string> args)
        {
            try
            {
                this.InternalValue = args.Value;
                ValueChanged.InvokeAsync(args.Value);
                OnChanged();
            
            }
            catch (Exception)
            {
                ValueChanged.InvokeAsync(string.Empty);
            }

        }
       
       
        protected override async Task OnParametersSetAsync()
        {
           
            await base.OnParametersSetAsync();
            this.FieldText = "Descricao";
            this.FieldValue = "Text";
            if (this.Value == null) Value = string.Empty;
            InternalValue = Value;        
        }
         
        protected override bool ShouldRender()
        {
           return base.ShouldRender();
        }
        protected void OnCreated()
        {
            

        }
        protected void OnDestroyed()
        {
            

        }
        protected void OnDataBound()
        {

           
        }
    }
}
