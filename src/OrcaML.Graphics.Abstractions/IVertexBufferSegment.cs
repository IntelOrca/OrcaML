using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaML.Graphics.Abstractions
{
    public interface IVertexBufferSegment<TVertexSpec> where TVertexSpec : struct
    {
        int StartIndex { get; }
        int Length { get; }

        IVertexBufferSegment<TVertexSpec> GetSegment(int index, int length);
        void Set(TVertexSpec[] source, int sourceIndex, int sourceLength);
        void SetRange(int destinationIndex, TVertexSpec[] source, int sourceIndex, int sourceLength);
    }
}
