using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    public class OpenGLVertexBufferFactory : IVertexBufferFactory
    {
        public IVertexBuffer<TVertexSpec> Create<TVertexSpec>() where TVertexSpec : struct
        {
            int bufferId = 0;
            try
            {
                bufferId = GL.GenBuffer();
                OpenGLException.ThrowIfZero(bufferId, "glGenBuffers");

                return new OpenGLVertexBuffer<TVertexSpec>(bufferId);
            }
            catch
            {
                if (bufferId != 0)
                {
                    GL.DeleteBuffer(bufferId);
                }
                throw;
            }
        }
    }
}
