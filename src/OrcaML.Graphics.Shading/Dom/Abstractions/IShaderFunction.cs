using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderFunction : IShaderBlock
    {
        string Name { get; }
        string GeneratedName { get; }
        IShaderDataTypeInfo ReturnDataType { get; }
        IReadOnlyList<IShaderFunctionParameter> Parameters { get; }
    }

    public interface IShaderFunctionParameter
    {
        string Name { get; }
        string GeneratedName { get; }
        StorageModifierKind ModifierKind { get; }
        IShaderDataTypeInfo DataType { get; }
        IShaderStorageItem StorageItem { get; }
    }
}
