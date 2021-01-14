using System.ComponentModel;

namespace Orion.Framework.Ui.Blazor.Enums
{

    
    public enum OxTypeDialog { Sucesso, Info, Warning, Erro }

    public enum InfoType
    {
        Default = 0,
        Crumb,
        Component,
        Description,
        Section
    }
    public enum OverlayType
    {
        Default = 0,
        Body,
        Content,
    }
    public enum OxFloatLabel
    {
        Never = 0,
        Always = 1,
        Auto = 2
    }   

    public enum FWorkActionModel { New, Edit, Delete }
    public enum FWorkStyleButton
    {
        [Description("none")]
        None,
        [Description("e-primary")]
        Primary,
        [Description("e-success")]
        Success,
        [Description("e-info")]
        Info,
        [Description("e-warning")]
        Warning,
        [Description("e-danger")]
        Danger,
        [Description("e-link")]
        Link
    }

  
}
