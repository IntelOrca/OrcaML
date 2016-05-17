namespace OrcaML.Graphics.Shading
{
    public interface IFragmentShaderBuilder<TProgramSpec, TVertexSpec> : IShaderBuilder<TProgramSpec, TVertexSpec>
        where TProgramSpec : class
        where TVertexSpec : struct
    {
        Svec4 Colour { set; }
    }
}
