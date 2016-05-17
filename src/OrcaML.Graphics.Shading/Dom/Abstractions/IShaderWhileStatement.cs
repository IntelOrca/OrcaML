namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderWhileStatement : IShaderStatement
    {
        IShaderExpression WhileCondition { get; }
        IShaderBlock Block { get; }
    }
}
