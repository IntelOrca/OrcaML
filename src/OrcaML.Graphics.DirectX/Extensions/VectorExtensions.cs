using OrcaML.Geometry;
using SharpDX.Mathematics.Interop;

namespace OrcaML.Graphics.DirectX.Extensions
{
    public static class VectorExtensions
    {
        public static RawColor4 ToRawColor4(this Vector4f vec)
        {
            return new RawColor4(vec.R, vec.G, vec.B, vec.A);
        }
    }
}
