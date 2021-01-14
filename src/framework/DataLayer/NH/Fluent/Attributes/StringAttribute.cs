using System;

namespace  Orion.Framework.DataLayer.NH.Fluent
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class StringAttribute : Attribute
    {
        public const int MaxLength = 9999;

        public int Length { get; }

        protected StringAttribute(int maxLenght)
        {
            this.Length = maxLenght;
        }
    }
}