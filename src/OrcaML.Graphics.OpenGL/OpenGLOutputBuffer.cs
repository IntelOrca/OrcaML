using System;
using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;
using static OrcaML.Graphics.OpenGL.OpenGLConstants;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLOutputBuffer : IOutputBuffer, IDisposable
    {
        public IGraphicsService GraphicsService { get; }
        public int FramebufferId { get; }

        public OpenGLOutputBuffer(IGraphicsService service, int framebufferId)
        {
            GraphicsService = service;
            FramebufferId = framebufferId;
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferId);
        }

        public void Dispose()
        {
            if (FramebufferId != FRAMEBUFFER_BACKBUFFER_ID)
            {
                GL.DeleteFramebuffer(FramebufferId);
            }
        }
    }
}
