namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderDataType
    {
        IShaderDataTypeInfo DataTypeInfo { get; }
        IShaderExpression Expression { get; }
    }

    public interface IShaderDataType<T> : IShaderDataType
    {

    }
}
