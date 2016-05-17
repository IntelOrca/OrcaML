using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Sint : IShaderDataType<Sint>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Sint>("int", typeof(Sint));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        public Sint() { }

        public Sint(int constant)
        {
            Expression = new ShaderConstantExpression(constant);
        }

        public Sint(IShaderExpression expr)
        {
            Expression = expr;
        }

        public override string ToString()
        {
            return Expression.ToString();
        }

        public static implicit operator Sint(int constant)
        {
            return new Sint(constant);
        }

        public static Sint operator +(Sint left, Sint right)
        {
            return new Sint(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Add, right));
        }

        public static Sint operator -(Sint left, Sint right)
        {
            return new Sint(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Subtract, right));
        }

        public static Sint operator *(Sint left, Sint right)
        {
            return new Sint(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Multiply, right));
        }

        public static Sint operator /(Sint left, Sint right)
        {
            return new Sint(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Divide, right));
        }

        public static Sbool operator <(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.LessThan, right));
        }

        public static Sbool operator >(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.GreaterThan, right));
        }

        public static Sbool operator <=(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.LessThanOrEqual, right));
        }

        public static Sbool operator >=(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.GreaterThanOrEqual, right));
        }

        public static Sbool operator ==(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Equal, right));
        }

        public static Sbool operator !=(Sint left, Sint right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.NotEqual, right));
        }
    }
}
