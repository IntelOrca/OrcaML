using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderFieldExpression : IShaderFieldExpression
    {
        public IShaderExpression Child { get; }
        public string FieldName { get; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return Child;
            }
        }

        public ShaderFieldExpression(IShaderDataType child, string fieldName)
        {
            Child = child.Expression;
            FieldName = fieldName;
        }

        public override string ToString()
        {
            return $"{Child}.{FieldName}";
        }
    }
}
