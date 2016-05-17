namespace OrcaML.Graphics.Abstractions
{
    /// <summary>
    /// Represents an instance of a shader program. Wraps a shader program and one or more shaders in OpenGL.
    /// </summary>
    /// <typeparam name="TProgramSpec"></typeparam>
    /// <typeparam name="TVertexSpec"></typeparam>
    public interface IShaderProgram<TProgramSpec, TVertexSpec> where TProgramSpec : class, new()
                                                               where TVertexSpec : struct
    {
        void SetUniform<T>(UniformFieldInfo uniformFieldInfo, T value);
    }
}
