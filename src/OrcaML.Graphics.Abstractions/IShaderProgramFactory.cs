using OrcaML.Graphics.Shading;

namespace OrcaML.Graphics.Abstractions
{
    public interface IShaderProgramFactory
    {
        IShaderProgram<TProgramSpec, TVertexSpec> Create<TProgramSpec, TVertexSpec>(IShader<TProgramSpec, TVertexSpec> shader)
            where TProgramSpec : class, new()
            where TVertexSpec : struct;
    }
}
