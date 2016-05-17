using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderIfStatement : IShaderStatement
    {
        IReadOnlyList<IShaderIfBlock> IfBlocks { get; }
        IShaderBlock ElseBlock { get; }
    }

    public interface IShaderIfBlock : IShaderBlock
    {
        IShaderExpression Condition { get; }
    }
}
