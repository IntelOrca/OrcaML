using System.Runtime.InteropServices;

namespace OrcaML.Geometry
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
    public struct Vector2f
    {
        [FieldOffset(0)] public float X;
        [FieldOffset(4)] public float Y;

        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
