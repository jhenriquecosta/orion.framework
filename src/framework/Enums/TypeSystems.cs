using System.ComponentModel;

namespace Orion.Framework.Domains.Enums
{
    public enum ColumnType
    {
        [Description("User Defined")] UserDefined,
        [Description("string")] String,
        [Description("char")] Char,
        [Description("int")] Int,
        [Description("uint")] UInt,
        [Description("long")] Long,
        [Description("ulong")] ULong,
        [Description("float")] Float,
        [Description("double")] Double,
        [Description("decimal")] Decimal,
        [Description("short")] Short,
        [Description("ushort")] UShort,
        [Description("byte")] Byte,
        [Description("sbyte")] SByte,
        [Description("bool")] Bool,
        [Description("object")] Object,
        [Description("DateTime")] DateTime,
        [Description("Image")] Image
       // [Description("Default")] Default = String


    }
}
