using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaML.Graphics.Shading.Dom
{
    public interface IShaderBlock : IShaderNode
    {
        IReadOnlyList<IShaderStatement> Statements { get; }
    }
}
