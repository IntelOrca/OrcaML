using System;
using OrcaML.Graphics.Abstractions;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DShaderProgram<TProgramSpec, TVertexSpec> : IShaderProgram<TProgramSpec, TVertexSpec>, IDisposable
        where TProgramSpec : class, new()
        where TVertexSpec : struct
    {
        private readonly D3D11.VertexShader _vertexShader;
        private readonly D3D11.PixelShader _pixelShader;

        public D3D11.VertexShader VertexShader => _vertexShader;
        public D3D11.PixelShader PixelShader => _pixelShader;

        public Direct3DShaderProgram(D3D11.VertexShader vertexShader, D3D11.PixelShader pixelShader)
        {
            _vertexShader = vertexShader;
            _pixelShader = pixelShader;
        }

        public void Dispose()
        {
            _vertexShader.Dispose();
            _pixelShader.Dispose();
        }

        public void SetUniform<T>(UniformFieldInfo uniformFieldInfo, T value)
        {
            // throw new NotImplementedException();
        }
    }
}