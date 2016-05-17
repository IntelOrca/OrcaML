using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderWhileStatement : IShaderDoStatement
    {
        public IShaderExpression WhileCondition { get; set; }
        public IShaderBlock Block { get; set; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return WhileCondition;
                yield return Block;
            }
        }

        public override string ToString()
        {
            return $"While: {WhileCondition} ...";
        }
    }
}
