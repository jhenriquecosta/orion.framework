using System.ComponentModel;

namespace Framework.Web.Blazor.Components.Enums
{
    public enum XTTypeDialog { Sucesso, Info, Warning, Erro }

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
    public enum XTActionModel { New, Edit, Delete }
    public enum StyleButton
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
