using OrcaML.Graphics.Shading;

namespace OrcaML.Graphics.Shading
{
    /// <summary>
    /// Represents a shader that can be compiled and used to draw primitives.
    /// </summary>
    /// <typeparam name="TProgramSpec"></typeparam>
    /// <typeparam name="TVertexSpec"></typeparam>
    public interface IShader<TProgramSpec, TVertexSpec>
        where TProgramSpec : class, new()
        where TVertexSpec : struct
    {
        void BuildVertex(IVertexShaderBuilder<TProgramSpec, TVertexSpec> builder);
        void BuildFragment(IFragmentShaderBuilder<TProgramSpec, TVertexSpec> builder);
    }
}
