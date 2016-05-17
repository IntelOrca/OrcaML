using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderDoStatement : IShaderDoStatement
    {
        public IShaderBlock Block { get; set; }
        public IShaderExpression WhileCondition { get; set; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return Block;
                yield return WhileCondition;
            }
        }

        public override string ToString()
        {
            return $"Do: ... While {WhileCondition}";
        }
    }
}
