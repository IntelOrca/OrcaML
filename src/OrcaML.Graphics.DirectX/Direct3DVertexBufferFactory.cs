using OrcaML.Graphics.Abstractions;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DVertexBufferFactory : IVertexBufferFactory
    {
        private readonly Direct3DContext _direct3DContext;

        public Direct3DVertexBufferFactory(Direct3DContext direct3DContext)
        {
            _direct3DContext = direct3DContext;
        }

        public IVertexBuffer<TVertexSpec> Create<TVertexSpec>() where TVertexSpec : struct
        {
            var device = _direct3DContext.Device;
            var buffer = D3D11.Buffer.Create<TVertexSpec>(device,
                                                          D3D11.BindFlags.VertexBuffer,
                                                          new TVertexSpec[1],
                                                          usage: D3D11.ResourceUsage.Dynamic,
                                                          accessFlags: D3D11.CpuAccessFlags.Write);
            return new Direct3DVertexBuffer<TVertexSpec>(buffer);
        }
    }
}