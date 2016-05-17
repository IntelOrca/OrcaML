using System;

namespace OrcaML.Graphics.Shading
{
    public interface IShaderDataTypeInfo
    {
        string Name { get; }
        bool IsArray { get; }
        bool IsCustom { get; }
        bool IsPrimitive { get; }
        Type EquivalentCLRType { get; }
        Type BackingType { get; }
    }
}
