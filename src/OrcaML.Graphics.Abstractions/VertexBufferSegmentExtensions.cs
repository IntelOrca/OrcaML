namespace OrcaML.Graphics.Abstractions
{
    public static class VertexBufferSegmentExtensions
    {
        public static void Set<TVertexSpec>(this IVertexBufferSegment<TVertexSpec> vertexBufferSegment, TVertexSpec[] source)
            where TVertexSpec : struct
        {
            vertexBufferSegment.Set(source, 0, source.Length);
        }

        public static void SetRange<TVertexSpec>(this IVertexBufferSegment<TVertexSpec> vertexBufferSegment,
                                                 int destinationIndex,
                                                 TVertexSpec[] source)
            where TVertexSpec : struct
        {
            vertexBufferSegment.SetRange(destinationIndex, source, 0, source.Length);
        }
    }
}
