using System;
using System.Runtime.InteropServices;

namespace OrcaML.Geometry
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
    public struct Vector4f : IEquatable<Vector4f>
    {
        [FieldOffset( 0)] public float X;
        [FieldOffset( 4)] public float Y;
        [FieldOffset( 8)] public float Z;
        [FieldOffset(12)] public float W;

        public float R => X;
        public float G => Y;
        public float B => Z;
        public float A => W;

        public Vector4f(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            hash = (hash * 7) + Z.GetHashCode();
            hash = (hash * 7) + W.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector4f)
            {
                return Equals((Vector4f)obj);
            }
            return false;
        }

        public bool Equals(Vector4f other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z &&
                   A == other.A;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}, {W}";
        }

        public static bool operator ==(Vector4f left, Vector4f right)
        {
            return left.X == right.X &&
                   left.Y == right.Y &&
                   left.Z == right.Z &&
                   left.A == right.A;
        }

        public static bool operator !=(Vector4f left, Vector4f right)
        {
            return !(left == right);
        }
    }
}
