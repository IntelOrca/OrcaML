using System.Collections.Generic;
using OrcaML.Geometry;

namespace OrcaML.Graphics.Abstractions
{
    public static class OutputBufferExtensions
    {
        public static void Clear(this IOutputBuffer buffer, Vector4f colour)
        {
            buffer.GraphicsService.ClearBuffer(buffer, colour);
        }

        public static void Render<TP, TV>(this IOutputBuffer buffer,
                                          TV[] vertices,
                                          IShaderProgram<TP, TV> program,
                                          IShaderProgramState<TP> programState = null)
            where TP : class, new()
            where TV : struct
        {
            
        }

        public static void Render<TP, TV>(this IOutputBuffer buffer,
                                          IReadOnlyCollection<TV> vertices,
                                          IShaderProgram<TP, TV> program,
                                          IShaderProgramState<TP> programState = null)
            where TP : class, new()
            where TV : struct
        {

        }

        public static void Render<TP, TV>(this IOutputBuffer buffer,
                                          IVertexBufferSegment<TV> vertices,
                                          IShaderProgram<TP, TV> program,
                                          IShaderProgramState<TP> programState = null)
            where TP : class, new()
            where TV : struct
        {
            buffer.GraphicsService.Render(buffer, vertices, program, programState);
        }
    }
}
