using System;
using System.Runtime.InteropServices;
using OrcaML.Geometry;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.DirectX.Extensions;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DGraphicsService : IGraphicsService
    {
        private readonly Direct3DContext _direct3DContext;
        private readonly Direct3DOutputBuffer _backBuffer;

        public Direct3DGraphicsService(Direct3DContext direct3DContext)
        {
            _direct3DContext = direct3DContext;
            _backBuffer = new Direct3DOutputBuffer(this, direct3DContext.BackBufferRenderTargetView);
        }

        public IOutputBuffer BackBuffer => _backBuffer;

        public void ClearBuffer(IOutputBuffer buffer, Vector4f colour)
        {
            var deviceContext = _direct3DContext.DeviceContext;
            var renderTargetView = _backBuffer.RenderTargetView;
            deviceContext.ClearRenderTargetView(renderTargetView, colour.ToRawColor4());
        }

        public void Render<TProgramSpec, TVertexSpec>(IOutputBuffer buffer, IVertexBufferSegment<TVertexSpec> vertices, IShaderProgram<TProgramSpec, TVertexSpec> program, IShaderProgramState<TProgramSpec> programState)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            int stride = Marshal.SizeOf<TVertexSpec>();
            var vertexBuffer = (vertices as Direct3DVertexBuffer<TVertexSpec>).Buffer;
            var shaderProgram = program as Direct3DShaderProgram<TProgramSpec, TVertexSpec>;
            var vertexShader = shaderProgram.VertexShader;
            var pixelShader = shaderProgram.PixelShader;

            var deviceContext = _direct3DContext.DeviceContext;
            deviceContext.VertexShader.Set(vertexShader);
            deviceContext.PixelShader.Set(pixelShader);
            deviceContext.InputAssembler.SetVertexBuffers(0, new D3D11.VertexBufferBinding(vertexBuffer, stride, 0));
            deviceContext.Draw(vertices.Length, 0);

            throw new NotImplementedException();
        }
    }
}