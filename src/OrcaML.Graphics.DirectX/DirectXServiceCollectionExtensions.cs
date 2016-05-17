using Microsoft.Extensions.DependencyInjection;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.DirectX
{
    public static class DirectXServiceCollectionExtensions
    {
        public static IDirect3DBuilder AddDirect3D(this IServiceCollection services)
        {
            services.AddSingleton<Direct3DContext>();
            services.AddSingleton<IGraphicsService, Direct3DGraphicsService>();
            services.AddSingleton<IShaderProgramFactory, Direct3DShaderProgramFactory>();
            services.AddSingleton<IVertexBufferFactory, Direct3DVertexBufferFactory>();
            return null;
        }
    }
}
