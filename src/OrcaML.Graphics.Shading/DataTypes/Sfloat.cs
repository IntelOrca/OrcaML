using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Sfloat : IShaderDataType<Sfloat>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Sfloat>("float", typeof(Sfloat));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        public Sfloat() { }

        public Sfloat(float constant)
        {
            Expression = new ShaderConstantExpression(constant);
        }

        public Sfloat(IShaderExpression expr)
        {
            Expression = expr;
        }

        public override string ToString()
        {
            return Expression.ToString();
        }

        public static implicit operator Sfloat(float constant)
        {
            return new Sfloat(constant);
        }

        public static implicit operator Sfloat(Sint value)
        {
            return new Sfloat(value.Expression);
        }

        public static Sfloat operator +(Sfloat left, Sfloat right)
        {
            return new Sfloat(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Add, right));
        }

        public static Sfloat operator -(Sfloat left, Sfloat right)
        {
            return new Sfloat(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Subtract, right));
        }

        public static Sfloat operator *(Sfloat left, Sfloat right)
        {
            return new Sfloat(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Multiply, right));
        }

        public static Sfloat operator /(Sfloat left, Sfloat right)
        {
            return new Sfloat(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Divide, right));
        }

        public static Sbool operator <(Sfloat left, Sfloat right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.LessThan, right));
        }

        public static Sbool operator >(Sfloat left, Sfloat right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.GreaterThan, right));
        }

        public static Sbool operator ==(Sfloat left, Sfloat right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.Equal, right));
        }

        public static Sbool operator !=(Sfloat left, Sfloat right)
        {
            return new Sbool(new ShaderBinaryExpression(left, ShaderBinaryExpressionOp.NotEqual, right));
        }
    }
}
