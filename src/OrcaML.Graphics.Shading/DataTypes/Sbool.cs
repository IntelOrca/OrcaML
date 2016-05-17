using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class Sbool : IShaderDataType<Sbool>
    {
        public static readonly IShaderDataTypeInfo StaticDataTypeInfo =
            new ShaderDataTypeInfo<Sbool>("bool", typeof(Sbool));

        public IShaderExpression Expression { get; }

        public IShaderDataTypeInfo DataTypeInfo => StaticDataTypeInfo;

        public Sbool() { }

        public Sbool(bool constant)
        {
            Expression = new ShaderConstantExpression(constant);
        }

        public Sbool(IShaderExpression expr)
        {
            Expression = expr;
        }

        public override string ToString()
        {
            return Expression.ToString();
        }

        public static implicit operator Sbool(bool constant)
        {
            return new Sbool(constant);
        }
    }
}