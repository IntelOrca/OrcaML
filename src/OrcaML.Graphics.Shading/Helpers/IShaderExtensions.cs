using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading.Helpers
{
    public static class IShaderExtensions
    {
        public static IBuiltShader Build<TProgramSpec, TVertexSpec>(this IShader<TProgramSpec, TVertexSpec> shader)
            where TProgramSpec : class, new()
            where TVertexSpec : struct
        {
            IShaderFunction vertexFunction;
            IShaderFunction fragmentFunction;

            var shaderBuilder = new ShaderBuilder<TProgramSpec, TVertexSpec>();

            var properties = shader.GetType()
                                   .GetProperties()
                                   .Where(IsShaderTransfer);
            int transferId = 1;
            foreach (PropertyInfo property in properties)
            {
                Type transferType = property.PropertyType;
                object transferObject = CreateTransfer(transferType, shaderBuilder, transferId);
                property.SetValue(shader, transferObject);
            }

            shaderBuilder.Initialise();
            shader.BuildVertex(shaderBuilder);
            vertexFunction = shaderBuilder.Function;

            shaderBuilder.Initialise();
            shader.BuildFragment(shaderBuilder);
            fragmentFunction = shaderBuilder.Function;

            var builtShader = new BuiltShader(vertexFunction, fragmentFunction);
            return builtShader;
        }

        private static object CreateTransfer(Type transferType, IInternalShaderBuilder shaderBuilder, int id)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            CultureInfo culture = null;
            object[] args = new object[] { shaderBuilder, id };
            object result = Activator.CreateInstance(transferType, flags, null, args, culture);
            return result;
        }

        private static bool IsShaderTransfer(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            Type genericTypeDefinition = propertyType.GetGenericTypeDefinition();
            return genericTypeDefinition == typeof(ShaderTransfer<>);
        }
    }
}
