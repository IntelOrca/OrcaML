using System;
using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLShaderProgram<TProgramSpec, TVertexSpec> : IShaderProgram<TProgramSpec, TVertexSpec>,
                                                                    IDisposable
        where TProgramSpec : class, new()
        where TVertexSpec : struct
    {
        private readonly int _programId;
        private readonly int _vertexShaderId;
        private readonly int _fragmentShaderId;

        public OpenGLShaderProgram(int programId, int vertexShaderId, int fragmentShaderId)
        {
            _programId = programId;
            _vertexShaderId = vertexShaderId;
            _fragmentShaderId = fragmentShaderId;
        }

        public void Dispose()
        {
            GL.DeleteProgram(_programId);
            GL.DeleteShader(_vertexShaderId);
            GL.DeleteShader(_fragmentShaderId);
        }
        
        public int GetAttributeLocation(string attributeName)
        {
            int location = GL.GetAttribLocation(_programId, GLSL.PrefixAttribute + attributeName);
            // OpenGLException.ThrowIfNegativeOne(location, "glGetAttribLocation");

            return location;
        }

        public int GetUniformLocation(string uniformName)
        {
            int location = GL.GetUniformLocation(_programId, GLSL.PrefixUniform + uniformName);
            // OpenGLException.ThrowIfNegativeOne(location, "getUniformLocation");

            return location;
        }

        public void SetUniform<T>(UniformFieldInfo uniformFieldInfo, T value)
        {
            int uniformLocation = GetUniformLocation(uniformFieldInfo.Name);
            switch (uniformFieldInfo.Components) {
            case 1:
                switch (uniformFieldInfo.Type) {
                case ShaderDataType.Integer32:
                    GL.Uniform1(uniformLocation, (int)(object)value);
                    break;
                case ShaderDataType.Float32:
                    GL.Uniform1(uniformLocation, (float)(object)value);
                    break;
                }
                break;
            }
        }

        public void Use()
        {
            GL.UseProgram(_programId);
        }
    }
}
