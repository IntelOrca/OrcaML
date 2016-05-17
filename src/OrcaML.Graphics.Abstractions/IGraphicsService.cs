using OrcaML.Geometry;

namespace OrcaML.Graphics.Abstractions
{
    /// <summary>
    /// Service
    /// </summary>
    public interface IGraphicsService
    {
        IOutputBuffer BackBuffer { get; }

        void ClearBuffer(IOutputBuffer buffer, Vector4f colour);
        void Render<TProgramSpec, TVertexSpec>(IOutputBuffer buffer,
                                               IVertexBufferSegment<TVertexSpec> vertices,
                                               IShaderProgram<TProgramSpec, TVertexSpec> program,
                                               IShaderProgramState<TProgramSpec> programState)
            where TProgramSpec : class, new()
            where TVertexSpec : struct;
    }
}
