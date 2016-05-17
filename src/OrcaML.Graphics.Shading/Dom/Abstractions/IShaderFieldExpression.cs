namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderFieldExpression : IShaderExpression
    {
        IShaderExpression Child { get; }
        string FieldName { get; }
    }
}
