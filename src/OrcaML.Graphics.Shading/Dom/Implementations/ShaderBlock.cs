using System.Collections.Generic;
using System.Linq;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderBlock : IShaderBlock
    {
        public IShaderStatement[] Statements { get; }

        public ShaderBlock(IEnumerable<IShaderStatement> statements)
        {
            Statements = statements.ToArray();
        }

        public IEnumerable<IShaderNode> Children => Statements;
        IReadOnlyList<IShaderStatement> IShaderBlock.Statements => Statements;
    }
}
