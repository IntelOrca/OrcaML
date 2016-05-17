using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaML.Graphics.Abstractions
{
    public interface IVertexBuffer<TVertexSpec> : IVertexBufferSegment<TVertexSpec>
        where TVertexSpec : struct
    {
    }
}
