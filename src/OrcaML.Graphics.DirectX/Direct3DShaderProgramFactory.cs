using System;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.Shading;
using SharpDX.D3DCompiler;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DShaderProgramFactory : IShaderProgramFactory
    {
        private readonly Direct3DContext _direct3DContext;

        public Direct3DShaderProgramFactory(Direct3DContext direct3DContext)
        {
            _direct3DContext = direct3DContext;
        }

        public IShaderProgram<TProgramSpec, TVertexSpec> Create<TProgramSpec, TVertexSpec>(IShader<TProgramSpec, TVertexSpec> shader)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            var device = _direct3DContext.Device;

            string vertexCode = String.Join("\n",
                "float4 main(float4 position: POSITION) : SV_POSITION",
                "{",
                "    return position;",
                "}");

            string pixelCode = String.Join("\n",
                "float4 main(float4 position : SV_POSITION) : SV_TARGET",
                "{",
                "    return float4(1.0, 0.0, 0.0, 1.0);",
                "}");

            D3D11.VertexShader vertexShader;
            D3D11.PixelShader pixelShader;

            using (var vertexShaderByteCode = ShaderBytecode.Compile(vertexCode, "main", "vs_4_0", ShaderFlags.Debug))
            {
                vertexShader = new D3D11.VertexShader(device, vertexShaderByteCode);
            }
            using (var pixelShaderByteCode = ShaderBytecode.Compile(pixelCode, "main", "ps_4_0", ShaderFlags.Debug))
            {
                pixelShader = new D3D11.PixelShader(device, pixelShaderByteCode);
            }

            return new Direct3DShaderProgram<TProgramSpec, TVertexSpec>(vertexShader, pixelShader);
        }
    }
}