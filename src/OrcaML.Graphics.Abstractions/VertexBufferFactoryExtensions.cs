using OpenML.Common;
using OrcaML.Common;

namespace OrcaML.Graphics.Abstractions
{
    public static class VertexBufferFactoryExtensions
    {
        public static IVertexBuffer<TVertexSpec> Create<TVertexSpec>(this IVertexBufferFactory factory, TVertexSpec[] vertices)
            where TVertexSpec : struct
        {
            Guard.ArgumentNotNull(nameof(vertices), vertices);

            return Create(factory, vertices, 0, vertices.Length);
        }

        public static IVertexBuffer<TVertexSpec> Create<TVertexSpec>(this IVertexBufferFactory factory,
                                                                     TVertexSpec[] vertices,
                                                                     int index,
                                                                     int length)
            where TVertexSpec : struct
        {
            IVertexBuffer<TVertexSpec> buffer = null;
            try
            {
                buffer = factory.Create<TVertexSpec>();
                buffer.Set(vertices, index, length);
                return buffer;
            }
            catch
            {
                Disposer.Dispose(buffer);
                throw;
            }
        }
    }
}
