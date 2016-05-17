using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderConstantExpression : IShaderConstantExpression
    {
        public object Value { get; }

        public IEnumerable<IShaderNode> Children => null;

        public ShaderConstantExpression(object value) { Value = value; }

        public override string ToString()
        {
            if (Value is float) {
                return ((float)Value).ToString("0.0#############################f");
            }
            return Value.ToString();
        }
    }
}
