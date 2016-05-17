namespace OrcaML.Graphics.Shading.Dom
{
    public enum ShaderBinaryExpressionOp
    {
        Add,
        Subtract,
        Multiply,
        Divide,

        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Equal,
        NotEqual,
    }

    public interface IShaderBinaryExpression : IShaderExpression
    {
        IShaderExpression Left { get; }
        ShaderBinaryExpressionOp Operation { get; }
        IShaderExpression Right { get; }
    }
}
