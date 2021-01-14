using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public class OXToastBase : FWorkBlazorComponent
    {

        [CascadingParameter] private OXToasts ToastsContainer { get; set; }

        [Parameter] public Guid ToastId { get; set; }
        [Parameter] public ToastSettings ToastSettings { get; set; }

        protected void Close()
        {
            ToastsContainer.RemoveToast(ToastId);
        }
        public SfToast SelfComponent;
        [Parameter] public double Timeout { get; set; } = 1500;

        public bool ShowButtons { get; set; } = false;
        public bool Checked { get; set; } = true;
        public string ShowEasing { get; set; } = "ease";
        public string HideEasing { get; set; } = "ease";
        public string ShowEffect { get; set; } = "FadeZoomIn";
        public string HideEffect { get; set; } = "FadeZoomOut";
        public string EasingValue { get; set; } = "ease";

        public bool ShowCloseBtn = true;
        public bool ShowProgressBtn = false;
        public bool ShowNewestOnTop = true;
      
        public string AnimationValue { get; set; } = "FadeZoomIn";
        public string HideEasingValue { get; set; } = "ease";
        public string HideAnimationValue { get; set; } = "FadeZoomOut";

        public string TimeoutValue { get; set; } = "5000";
        public double ShowDuration = 400;
        public double HideDuration = 400;
        public string ShowDurationtValue = "400";
        public string HideDurationtValue = "400";

        public List<ToastButtonModelProp> ToastButtons;
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);            
            
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }
        protected void OnCreated(object args)
        {
            
            OnShow();
        }
        public void OnShow()
        {
            this.StateHasChanged();
            this.SelfComponent.Show();
        }


    }
}