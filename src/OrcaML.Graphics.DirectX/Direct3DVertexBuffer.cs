using System;
using OrcaML.Graphics.Abstractions;
using SharpDX;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DVertexBuffer<TVertexSpec> : IVertexBuffer<TVertexSpec>
        where TVertexSpec : struct
    {
        private readonly D3D11.DeviceContext _deviceContext;
        private D3D11.Buffer _buffer;

        public int StartIndex => 0;
        public int Length { get; private set; }

        public D3D11.Buffer Buffer => _buffer;

        public Direct3DVertexBuffer(D3D11.Buffer buffer)
        {
            _deviceContext = buffer.Device.ImmediateContext;
            _buffer = buffer;
        }

        public IVertexBufferSegment<TVertexSpec> GetSegment(int index, int length)
        {
            throw new NotImplementedException();
        }

        public void Set(TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            _buffer.Dispose();
            _buffer = D3D11.Buffer.Create<TVertexSpec>(_deviceContext.Device,
                                                       D3D11.BindFlags.VertexBuffer,
                                                       source);

            // DataStream dataStream;
            // _deviceContext.MapSubresource(_buffer, D3D11.MapMode.Write, D3D11.MapFlags.None, out dataStream);
            // dataStream.WriteRange(source, sourceIndex, sourceLength);
            // _deviceContext.UnmapSubresource(_buffer, 0);

            Length = sourceLength;
        }

        public void SetRange(int destinationIndex, TVertexSpec[] source, int sourceIndex, int sourceLength)
        {
            throw new NotImplementedException();
        }
    }
}