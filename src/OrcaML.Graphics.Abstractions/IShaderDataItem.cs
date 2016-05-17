namespace OrcaML.Graphics.Abstractions
{
    public interface IShaderDataItem
    {
        string Name { get; }
        int Components { get; }
        ShaderDataType Type { get; }
    }
}
