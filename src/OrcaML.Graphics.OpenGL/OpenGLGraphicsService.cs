using System;
using OpenTK.Graphics.OpenGL;
using OrcaML.Geometry;
using OrcaML.Graphics.Abstractions;
using static OrcaML.Graphics.OpenGL.OpenGLConstants;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLGraphicsService : IGraphicsService
    {
        public IOutputBuffer BackBuffer { get; }

        private Vector4f _clearColour;
        private IOutputBuffer _outputBuffer;
        private object _shaderProgram;
        private OpenGLVertexArray _vertexArray;

        public OpenGLGraphicsService()
        {
            BackBuffer = new OpenGLOutputBuffer(this, OpenGLConstants.FRAMEBUFFER_BACKBUFFER_ID);
            _outputBuffer = BackBuffer;
        }

        public void ClearBuffer(IOutputBuffer buffer, Vector4f colour)
        {
            UseBuffer(buffer);
            if (colour != _clearColour)
            {
                _clearColour = colour;
                GL.ClearColor(colour.R, colour.G, colour.B, colour.A);
            }
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        private static OpenGLVertexArray vao;

        public void Render<TProgramSpec, TVertexSpec>(IOutputBuffer buffer,
                                                      IVertexBufferSegment<TVertexSpec> vertexBuffer,
                                                      IShaderProgram<TProgramSpec, TVertexSpec> shaderProgram,
                                                      IShaderProgramState<TProgramSpec> shaderProgramState)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            if (vertexBuffer.Length == 0)
            {
                return;
            }

            if (vao == null)
            {
                vao = new OpenGLVertexArray();
                vao.Setup(shaderProgram);
            }

            UseBuffer(buffer);
            UseVertexArray(vao);
            UseProgram(shaderProgram);
            GL.DrawArrays(PrimitiveType.Quads, vertexBuffer.StartIndex, vertexBuffer.Length * 4);
        }

        private void UseBuffer(IOutputBuffer buffer)
        {
            if (buffer != _outputBuffer)
            {
                _outputBuffer = buffer;

                var openglOutputBuffer = (OpenGLOutputBuffer)buffer;
                openglOutputBuffer.Bind();
            }
        }

        private void UseVertexArray(OpenGLVertexArray vertexArray)
        {
            if (vertexArray != _vertexArray)
            {
                _vertexArray = vertexArray;
                vertexArray.Bind();
            }
        }

        private void UseProgram<TProgramSpec, TVertexSpec>(IShaderProgram<TProgramSpec, TVertexSpec> shaderProgram)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            if (shaderProgram != _shaderProgram)
            {
                _shaderProgram = shaderProgram;

                var openglShaderProgram = (OpenGLShaderProgram<TProgramSpec, TVertexSpec>)shaderProgram;
                openglShaderProgram.Use();
            }
        }
    }
}
