using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderIfStatement : IShaderIfStatement
    {
        public List<IShaderIfBlock> IfBlocks { get; } = new List<IShaderIfBlock>();

        public IShaderBlock ElseBlock { get; set; }
        IReadOnlyList<IShaderIfBlock> IShaderIfStatement.IfBlocks => IfBlocks;

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                foreach (IShaderNode node in IfBlocks)
                {
                    yield return node;
                }
                if (ElseBlock != null)
                {
                    yield return ElseBlock;
                }
            }
        }

        public override string ToString()
        {
            return $"If: {IfBlocks[0].Condition}";
        }
    }

    public class ShaderIfBlock : IShaderIfBlock
    {
        public IShaderExpression Condition { get; set; }
        public IShaderStatement[] Statements { get; set; }

        IReadOnlyList<IShaderStatement> IShaderBlock.Statements => Statements;

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return Condition;
                foreach (IShaderNode node in Statements)
                {
                    yield return node;
                }
            }
        }
    }
}
