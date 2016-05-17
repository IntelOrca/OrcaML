using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderBinaryExpression : IShaderBinaryExpression
    {
        public IShaderExpression Left { get; }
        public ShaderBinaryExpressionOp Operation { get; }
        public IShaderExpression Right { get; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        public ShaderBinaryExpression(IShaderDataType left, ShaderBinaryExpressionOp type, IShaderDataType right)
        {
            Left = left.Expression;
            Operation = type;
            Right = right.Expression;
        }

        public override string ToString()
        {
            string op = "?";
            switch (Operation) {
            case ShaderBinaryExpressionOp.Add:                op = "+";  break;
            case ShaderBinaryExpressionOp.Subtract:           op = "-";  break;
            case ShaderBinaryExpressionOp.Multiply:           op = "*";  break;
            case ShaderBinaryExpressionOp.Divide:             op = "/";  break;
            case ShaderBinaryExpressionOp.LessThan:           op = "<";  break;
            case ShaderBinaryExpressionOp.LessThanOrEqual:    op = "<="; break;
            case ShaderBinaryExpressionOp.GreaterThan:        op = ">";  break;
            case ShaderBinaryExpressionOp.GreaterThanOrEqual: op = ">="; break;
            case ShaderBinaryExpressionOp.Equal:              op = "=="; break;
            case ShaderBinaryExpressionOp.NotEqual:           op = "!="; break;
            }
            return $"({Left} {op} {Right})";
        }
    }
}
