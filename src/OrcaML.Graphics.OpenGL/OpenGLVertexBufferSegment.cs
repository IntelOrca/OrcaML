using OpenML.Common;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLVertexBufferSegment<TVertexSpec> : IVertexBufferSegment<TVertexSpec>
        where TVertexSpec : struct
    {
        private readonly OpenGLVertexBuffer<TVertexSpec> _vertexBuffer;

        public int StartIndex { get; }
        public int Length { get; }

        public OpenGLVertexBufferSegment(OpenGLVertexBuffer<TVertexSpec> buffer, int startIndex, int length)
        {
            _vertexBuffer = buffer;
            StartIndex = startIndex;
            Length = length;
        }

        public IVertexBufferSegment<TVertexSpec> GetSegment(int index, int length)
        {
            Guard.ArgumentInRange(nameof(index), index, StartIndex, StartIndex + Length);
            Guard.ArgumentInRange(nameof(length), length, 0, StartIndex + Length - index);

            return _vertexBuffer.GetSegment(StartIndex + index, length);
        }

        public void Set(TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            Guard.ArgumentNotNull(nameof(source), source);
            Guard.ArgumentInRange(nameof(sourceIndex), sourceIndex, 0, source.Length);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, source.Length - sourceIndex);

            _vertexBuffer.Set(source, StartIndex + sourceIndex, sourceLength);
        }

        public void SetRange(int destinationIndex, TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            Guard.ArgumentInRange(nameof(destinationIndex), destinationIndex, 0, Length);
            Guard.ArgumentNotNull(nameof(source), source);
            Guard.ArgumentInRange(nameof(sourceIndex), sourceIndex, 0, source.Length);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, source.Length - sourceIndex);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, Length - destinationIndex);

            _vertexBuffer.SetRange(destinationIndex, source, StartIndex + sourceIndex, sourceLength);
        }
    }
}
