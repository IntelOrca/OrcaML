using System;
using System.Text;
using OpenML.Common;
using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.OpenGL.Shading;
using OrcaML.Graphics.Shading;
using static OrcaML.Graphics.OpenGL.OpenGLConstants;

namespace OrcaML.Graphics.OpenGL
{
    internal class OpenGLShaderProgramFactory : IShaderProgramFactory
    {
        public OpenGLShaderProgramFactory(OpenGLContext context)
        {

        }

        public IShaderProgram<TProgramSpec, TVertexSpec> Create<TProgramSpec, TVertexSpec>(IShader<TProgramSpec, TVertexSpec> shader)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            int programId = 0;
            int vertexShaderId = 0;
            int fragmentShaderId = 0;
            try
            {
                // Generate GLSL code for each shader
                var glslGenerator = new GLSLGenerator<TProgramSpec, TVertexSpec>();
                glslGenerator.CompileShader(shader);

                // Create and compile the shaders with the generated GLSL code
                vertexShaderId = CreateShader(ShaderType.VertexShader, glslGenerator.VertexSource);
                fragmentShaderId = CreateShader(ShaderType.FragmentShader, glslGenerator.FragmentSource);

                // Create the program, attach the shaders and link it
                programId = GL.CreateProgram();
                OpenGLException.ThrowIfZero(programId, "glCreateProgram");

                GL.AttachShader(programId, vertexShaderId);
                GL.AttachShader(programId, fragmentShaderId);

                // TODO Use glBindFragDataLocation to attach output buffers to shader fragment outputs
                //      although we will probably just do it in the shader source code

                GL.LinkProgram(programId);

                // Finally create the program shader wrapper
                return new OpenGLShaderProgram<TProgramSpec, TVertexSpec>(programId, vertexShaderId, fragmentShaderId);
            }
            catch
            {
                // Clean up the GL objects, this only needs to happen if something goes wrong, otherwise
                // this will happen in OpenGLShaderProgram.Dispose.
                if (programId != 0)
                {
                    GL.DeleteProgram(programId);
                }
                if (vertexShaderId != 0)
                {
                    GL.DeleteShader(vertexShaderId);
                }
                if (fragmentShaderId != 0)
                {
                    GL.DeleteShader(fragmentShaderId);
                }
                throw;
            }
        }

        private static int CreateShader(ShaderType type, string source)
        {
            Guard.ArgumentNotNull(nameof(source), source);

            int shaderId = 0;
            try
            {
                shaderId = GL.CreateShader(type);
                OpenGLException.ThrowIfZero(shaderId, "glCreateShader");

                GL.ShaderSource(shaderId, source);
                GL.CompileShader(shaderId);

                int status;
                GL.GetShader(shaderId, ShaderParameter.CompileStatus, out status);
                if (status == GL_TRUE)
                {
                    return shaderId;
                }

                var buffer = new StringBuilder(capacity: 512);
                int infoLogLength;
                GL.GetShaderInfoLog(shaderId, buffer.Capacity, out infoLogLength, buffer);

                throw new OpenGLException("Failed to compile shader:" + Environment.NewLine + buffer);
            }
            catch
            {
                if (shaderId != 0)
                {
                    GL.DeleteShader(shaderId);
                }
                throw;
            }
        }
    }
}
