namespace  Orion.Framework.DataLayer.NH.Fluent
{
    public sealed class UnicodeStringAttribute : StringAttribute
    {
        public UnicodeStringAttribute(int maxLenght = MaxLength) : base(maxLenght) { }
    }
}