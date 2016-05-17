namespace OrcaML.Graphics.Abstractions
{
    public interface IVertexBufferFactory
    {
        IVertexBuffer<TVertexSpec> Create<TVertexSpec>() where TVertexSpec : struct;
    }
}
