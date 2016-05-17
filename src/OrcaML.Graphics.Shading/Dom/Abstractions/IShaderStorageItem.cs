namespace OrcaML.Graphics.Shading.Dom
{
    /// <summary>
    /// Represents a local variable, argument, uniform, input or output item.
    /// </summary>
    public interface IShaderStorageItem
    {
        string Name { get; }
        string GeneratedName { get; }
        IShaderDataTypeInfo DataTypeInfo { get; }
        StorageItemKind Kind { get; }
        StorageModifierKind ModifierKind { get; }
    }

    public enum StorageItemKind
    {
        Uniform,
        Local,
        Argument,
        Input,
        Output,
        Vertex,
        Special,
    }

    public enum StorageModifierKind
    {
        Input       = (1 << 0),
        Output      = (1 << 1),
        InputOutput = Input | Output,
    }
}
