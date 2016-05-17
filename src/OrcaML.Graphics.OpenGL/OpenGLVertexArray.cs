using System;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OrcaML.Geometry;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLVertexArray
    {
        private int _vertexArrayId;
        private bool _disposed;

        public OpenGLVertexArray()
        {
            _vertexArrayId = GL.GenVertexArray();
            OpenGLException.ThrowIfZero(_vertexArrayId, "glGenVertexArrays");
        }

        public OpenGLVertexArray(int vertexArrayId)
        {
            _vertexArrayId = vertexArrayId;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GL.DeleteVertexArray(_vertexArrayId);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(_vertexArrayId);
        }

        public void Setup<TProgramSpec, TVertexSpec>(IShaderProgram<TProgramSpec, TVertexSpec> shaderProgram)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            var glShaderProgram = shaderProgram as OpenGLShaderProgram<TProgramSpec, TVertexSpec>;
            if (glShaderProgram == null)
            {
                throw new OpenGLException("Shader program is not OpenGL.");
            }

            Bind();

            int stride = Marshal.SizeOf<TVertexSpec>();
            VertexFieldInfo[] vertexFieldInfos = VertexSpec.GetVertexFieldInfo<TVertexSpec>();
            foreach (VertexFieldInfo vfi in vertexFieldInfos)
            {
                int location = glShaderProgram.GetAttributeLocation(vfi.Name);
                VertexAttribPointerType attributeType = GetVertexAttributePointerTypeFromVertexFieldType(vfi.Type);
                SetupAttribute(location, vfi.Components, attributeType, stride, vfi.Offset);
            }
        }

        private void SetupAttribute(int location, int numComponents, VertexAttribPointerType type, int stride, int offset)
        {
            GL.VertexAttribPointer(location, numComponents, type, false, stride, offset);
            GL.EnableVertexAttribArray(location);
        }

        private static VertexAttribPointerType GetVertexAttributePointerTypeFromVertexFieldType(ShaderDataType type)
        {
            switch (type) {
            case ShaderDataType.Float32:   return VertexAttribPointerType.Float;
            case ShaderDataType.Integer32: return VertexAttribPointerType.Int;
            default:
                throw new NotSupportedException();
            }
        }
    }
}
