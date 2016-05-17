using System;
using OrcaML.Geometry;

namespace OrcaML.Graphics.Abstractions
{
    public static class ShaderDataItemHelpers
    {
        public static bool TryGetDataItemType(Type clrType, out int numComponents, out ShaderDataType shaderType)
        {
            if (clrType == typeof(int))
            {
                numComponents = 1;
                shaderType = ShaderDataType.Integer32;
            }
            else if (clrType == typeof(float))
            {
                numComponents = 1;
                shaderType = ShaderDataType.Float32;
            }
            else if (clrType == typeof(Vector2f))
            {
                numComponents = 2;
                shaderType = ShaderDataType.Float32;
            }
            else if (clrType == typeof(Vector4f))
            {
                numComponents = 4;
                shaderType = ShaderDataType.Float32;
            }
            else
            {
                numComponents = default(int);
                shaderType = default(ShaderDataType);
                return false;
            }
            return true;
        }
    }
}
