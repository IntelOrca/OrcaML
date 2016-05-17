using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderNode
    {
        IEnumerable<IShaderNode> Children { get; }
    }
}
