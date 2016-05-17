using System;
using System.Runtime.InteropServices;
using OpenML.Common;
using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLVertexBuffer<TVertexSpec> : IVertexBuffer<TVertexSpec>, IDisposable
        where TVertexSpec : struct
    {
        private readonly BufferUsageHint _usageHint = BufferUsageHint.DynamicDraw;

        public int StartIndex { get; } = 0;
        public int Length { get; private set; }

        private int _bufferId;
        private bool _disposed;

        public OpenGLVertexBuffer(int bufferId)
        {
            _bufferId = bufferId;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GL.DeleteBuffer(_bufferId);
            }
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferId);
        }

        public IVertexBufferSegment<TVertexSpec> GetSegment(int index, int length)
        {
            Guard.ArgumentInRange(nameof(index), index, StartIndex, StartIndex + Length);
            Guard.ArgumentInRange(nameof(length), length, 0, StartIndex + Length - index);

            return new OpenGLVertexBufferSegment<TVertexSpec>(this, index, length);
        }

        public void Set(TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            Guard.ArgumentNotNull(nameof(source), source);
            Guard.ArgumentInRange(nameof(sourceIndex), sourceIndex, 0, source.Length);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, source.Length - sourceIndex);

            if (sourceLength == 0)
            {
                return;
            }

            long vertexSize = Marshal.SizeOf<TVertexSpec>();
            IntPtr data = Marshal.UnsafeAddrOfPinnedArrayElement(source, sourceIndex);
            IntPtr size = new IntPtr(sourceLength * vertexSize);

            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, size, data, _usageHint);

            Length = sourceLength;
        }

        public void SetRange(int destinationIndex, TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            Guard.ArgumentInRange(nameof(destinationIndex), destinationIndex, 0, Length);
            Guard.ArgumentNotNull(nameof(source), source);
            Guard.ArgumentInRange(nameof(sourceIndex), sourceIndex, 0, source.Length);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, source.Length - sourceIndex);
            Guard.ArgumentInRange(nameof(sourceLength), sourceLength, 0, Length - destinationIndex);

            if (sourceLength == 0)
            {
                return;
            }

            long vertexSize = Marshal.SizeOf<TVertexSpec>();
            IntPtr offset = new IntPtr(destinationIndex * vertexSize);
            IntPtr size = new IntPtr(sourceLength * vertexSize);
            GL.BufferSubData(BufferTarget.ArrayBuffer, offset, size, source);
        }
    }
}
