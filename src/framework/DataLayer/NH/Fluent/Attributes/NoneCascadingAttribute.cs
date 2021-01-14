using System;

namespace  Orion.Framework.DataLayer.NH.Fluent
{
    /// <summary>
    /// NoneCascadingAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property, AllowMultiple = false )]
    public sealed class NoneCascadingAttribute : Attribute
    {
    }
}
