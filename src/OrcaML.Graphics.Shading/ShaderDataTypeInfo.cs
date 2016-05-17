using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class ShaderDataTypeInfo<T> : IShaderDataTypeInfo
        where T : IShaderDataType
    {
        public string Name { get; }
        public Type EquivalentCLRType { get; }
        public bool IsArray { get; }
        public bool IsCustom { get; }

        public Type BackingType => typeof(T);
        public bool IsPrimitive => !IsCustom;

        public ShaderDataTypeInfo(string name, Type clrType)
        {
            Name = name;
            EquivalentCLRType = clrType;
        }

        public override string ToString()
        {
            return $"{Name} <-> {EquivalentCLRType}";
        }
    }
}
