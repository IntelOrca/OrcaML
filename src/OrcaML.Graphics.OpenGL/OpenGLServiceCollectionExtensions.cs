using Microsoft.Extensions.DependencyInjection;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    public static class OpenGLServiceCollectionExtensions
    {
        public static IOpenGLBuilder AddOpenGL(this IServiceCollection services)
        {
            services.AddSingleton<OpenGLContext>();
            services.AddSingleton<IGraphicsService, OpenGLGraphicsService>();
            services.AddSingleton<IShaderProgramFactory, OpenGLShaderProgramFactory>();
            services.AddSingleton<IVertexBufferFactory, OpenGLVertexBufferFactory>();
            return null;
        }
    }
}
