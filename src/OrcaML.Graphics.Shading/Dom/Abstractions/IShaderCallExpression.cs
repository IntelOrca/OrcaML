namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderCallExpression : IShaderExpression
    {
        string Function { get; }
        IShaderExpression[] Arguments { get; }
    }
}
