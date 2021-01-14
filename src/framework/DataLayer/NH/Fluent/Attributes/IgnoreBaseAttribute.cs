using System;

namespace  Orion.Framework.DataLayer.NH.Fluent
{
    /// <summary>
    /// CacheAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public class IgnoreBaseAttribute : Attribute
    {
    }
}
