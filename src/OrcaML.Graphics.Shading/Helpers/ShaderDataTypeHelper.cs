using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaML.Geometry;

namespace OrcaML.Graphics.Shading
{
    public static class DataTypeHelper
    {
        public static IShaderDataTypeInfo GetDataTypeInfo<T>()
        {
            return GetDataTypeInfo(typeof(T));
        }

        public static IShaderDataTypeInfo GetDataTypeInfo(Type type)
        {
            if (type == typeof(float))    return Sfloat.StaticDataTypeInfo;
            if (type == typeof(Vector2f)) return Svec2.StaticDataTypeInfo;
            if (type == typeof(Vector3f)) return Svec3.StaticDataTypeInfo;
            if (type == typeof(Vector4f)) return Svec4.StaticDataTypeInfo;
            throw new NotImplementedException($"No corresponding shader data type for {type.Name}.");
        }
    }
}
