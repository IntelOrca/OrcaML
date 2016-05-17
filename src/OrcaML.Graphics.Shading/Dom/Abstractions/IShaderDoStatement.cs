namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderDoStatement : IShaderStatement
    {
        IShaderBlock Block { get; }
        IShaderExpression WhileCondition { get; }
    }
}
