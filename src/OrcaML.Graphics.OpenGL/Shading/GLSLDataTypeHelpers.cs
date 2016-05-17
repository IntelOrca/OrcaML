using System;
using OpenML.Common;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.OpenGL.Shading
{
    internal static class GLSLDataTypeHelpers
    {
        public static GLSLDataType FromDataTypeInfo(IShaderDataTypeInfo typeInfo)
        {
            Type type = typeInfo.BackingType;

            if (type == typeof(Sint)) return GLSLDataType.Int;
            if (type == typeof(Sfloat)) return GLSLDataType.Float;
            if (type == typeof(Svec2)) return GLSLDataType.Vec2;
            if (type == typeof(Svec3)) return GLSLDataType.Vec3;
            if (type == typeof(Svec4)) return GLSLDataType.Vec4;

            throw new NotSupportedException();
        }

        public static GLSLDataType FromShaderDataItem(IShaderDataItem dataItem)
        {
            if (dataItem.Components < 1 || dataItem.Components > 4)
            {
                throw new NotSupportedException($"Shader data type component size of {dataItem.Components} is not supported.");
            }

            Guard.ArgumentInRange(nameof(dataItem.Components), dataItem.Components, 1, 4);

            // Translate abstract shader type to GLSL type
            GLSLDataType glslDataType;
            switch (dataItem.Type) {
            case ShaderDataType.Integer32:
                glslDataType = GLSLDataType.Int;
                break;
            case ShaderDataType.Float32:
                glslDataType = GLSLDataType.Float;
                break;
            default:
                throw new NotSupportedException($"ShaderDataType {dataItem.Type} not supported.");
            }
            
            // OR with component size to get from e.g. float to vec3
            glslDataType |= (GLSLDataType)((dataItem.Components - 1) << 0);

            return glslDataType;
        }

        public static string ToGLSL(this GLSLDataType dataType)
        {
            return dataType.ToString().ToLower();
        }

        public static string ToGLSL(this GLSLInterpolationMode interpolationMode)
        {
            return interpolationMode.ToString().ToLower();
        }

        public static string ToGLSL(this GLSLParameterQualifier parameterQualifier)
        {
            return parameterQualifier.ToString().ToLower();
        }

        public static string GetGLSLName(IShaderStorageItem storageItem)
        {
            string name = storageItem.GeneratedName;
            string prefix;

            switch (storageItem.Kind) {
            case StorageItemKind.Special:
                return GetSpecialGLSLName(storageItem.Name);

            case StorageItemKind.Uniform:
                prefix = GLSL.PrefixUniform;
                break;
            case StorageItemKind.Vertex:
                prefix = GLSL.PrefixAttribute;
                break;
            case StorageItemKind.Local:
                prefix = GLSL.PrefixLocal;
                break;
            case StorageItemKind.Input:
                prefix = GLSL.PrefixTransfer;
                break;
            case StorageItemKind.Output:
                prefix = GLSL.PrefixTransfer;
                break;

            default:
                throw new NotImplementedException();
            }

            return prefix + name;
        }

        private static string GetSpecialGLSLName(string specialName)
        {
            switch (specialName) {
            case SpecialStorageItems.Position:
                return "gl_Position";
            }

            throw new NotSupportedException();
        }
    }
}
