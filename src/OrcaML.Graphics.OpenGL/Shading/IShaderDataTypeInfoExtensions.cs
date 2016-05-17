using System;
using OrcaML.Graphics.Shading;

namespace OrcaML.Graphics.OpenGL.Shading
{
    public static class IShaderDataTypeInfoExtensions
    {
        public static GLSLDataType ToGLSLDataType(this IShaderDataTypeInfo dataTypeInfo)
        {
            if (dataTypeInfo == null)
            {
                return GLSLDataType.Void;
            }

            Type dataType = dataTypeInfo.BackingType;
            if (dataType == typeof(Sfloat)) return GLSLDataType.Float;
            if (dataType == typeof(Svec2)) return GLSLDataType.Vec2;
            if (dataType == typeof(Svec3)) return GLSLDataType.Vec3;
            if (dataType == typeof(Svec4)) return GLSLDataType.Vec4;
            throw new NotSupportedException();
        }
    }
}
