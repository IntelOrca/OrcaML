using OrcaML.Geometry;

namespace OrcaML.Graphics.Abstractions
{
    /// <summary>
    /// A collection of defined colours represented by <see cref="Vector4f"/>.
    /// </summary>
    public static class Colours
    {
        public static readonly Vector4f Transparent = new Vector4f(0, 0, 0, 0);
        public static readonly Vector4f Black = new Vector4f(0, 0, 0, 1);
        public static readonly Vector4f White = new Vector4f(1, 1, 1, 1);
        public static readonly Vector4f Red = new Vector4f(1, 0, 0, 1);
        public static readonly Vector4f Green = new Vector4f(0, 1, 0, 1);
        public static readonly Vector4f Blue = new Vector4f(0, 0, 1, 1);
        public static readonly Vector4f Yellow = new Vector4f(1, 1, 0, 1);
    }
}
