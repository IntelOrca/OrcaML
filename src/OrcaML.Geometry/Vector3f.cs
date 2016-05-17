using System;
using System.Runtime.InteropServices;

namespace OrcaML.Geometry
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
    public struct Vector3f : IEquatable<Vector3f>
    {
        [FieldOffset(0)] public float X;
        [FieldOffset(4)] public float Y;
        [FieldOffset(8)] public float Z;

        public float R => X;
        public float G => Y;
        public float B => Z;

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            hash = (hash * 7) + Z.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3f)
            {
                return Equals((Vector3f)obj);
            }
            return false;
        }

        public bool Equals(Vector3f other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        public static bool operator ==(Vector3f left, Vector3f right)
        {
            return left.X == right.X &&
                   left.Y == right.Y &&
                   left.Z == right.Z;
        }

        public static bool operator !=(Vector3f left, Vector3f right)
        {
            return !(left == right);
        }
    }
}
