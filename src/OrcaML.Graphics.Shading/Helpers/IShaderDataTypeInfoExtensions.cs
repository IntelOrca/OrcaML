using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading.Helpers
{
    public static class IShaderDataTypeInfoExtensions
    {
        public static IShaderDataType Create(this IShaderDataTypeInfo dataTypeInfo, IShaderExpression expression)
        {
            return (IShaderDataType)Activator.CreateInstance(dataTypeInfo.BackingType, expression);
        }
    }
}
