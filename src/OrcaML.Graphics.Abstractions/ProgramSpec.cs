using System;
using System.Collections.Generic;
using System.Reflection;

namespace OrcaML.Graphics.Abstractions
{
    public class ProgramSpec
    {
        public static UniformFieldInfo[] GetUniformInfo<TProgramSpec>()
        {
            return GetUniformInfo(typeof(TProgramSpec));
        }

        public static UniformFieldInfo[] GetUniformInfo(Type programSpec)
        {
            // TODO Check if struct layout is defined correctly for sequential or explicit

            PropertyInfo[] properties = programSpec.GetProperties();
            var uniformFields = new List<UniformFieldInfo>(capacity: properties.Length);
            foreach (PropertyInfo property in properties)
            {
                UniformFieldInfo uniformFieldInfo = GetUniformInfo(property);
                uniformFields.Add(uniformFieldInfo);
            }

            return uniformFields.ToArray();
        }

        private static UniformFieldInfo GetUniformInfo(PropertyInfo property)
        {
            string propertyName = property.Name;
            Type propertyType = property.PropertyType;

            int numComponents;
            ShaderDataType shaderDataType;
            if (!ShaderDataItemHelpers.TryGetDataItemType(propertyType, out numComponents, out shaderDataType))
            {
                throw new NotSupportedException($"Property {propertyName}: {propertyType} is not a supported Uniform property.");
            }

            return new UniformFieldInfo(propertyName, numComponents, shaderDataType);
        }
    }
}
