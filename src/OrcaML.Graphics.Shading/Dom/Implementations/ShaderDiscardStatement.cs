using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderDiscardStatement : IShaderDiscardStatement
    {
        public IEnumerable<IShaderNode> Children => null;

        public override string ToString()
        {
            return $"Discard";
        }
    }
}
