namespace  Orion.Framework.DataLayer.NH.Fluent
{
    public sealed class FixedLengthStringAttribute : StringAttribute
    {
        public FixedLengthStringAttribute(int maxLenght) : base(maxLenght) { }
    }
}