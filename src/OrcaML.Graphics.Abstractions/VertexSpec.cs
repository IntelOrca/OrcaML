using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OrcaML.Graphics.Abstractions
{
    public static class VertexSpec
    {
        public static VertexFieldInfo[] GetVertexFieldInfo<TVertexSpec>()
        {
            return GetVertexFieldInfo(typeof(TVertexSpec));
        }

        public static VertexFieldInfo[] GetVertexFieldInfo(Type vertexSpec)
        {
            // TODO Check if struct layout is defined correctly for sequential or explicit

            FieldInfo[] fields = vertexSpec.GetFields();
            var vertexFields = new List<VertexFieldInfo>(capacity: fields.Length);
            int offset = 0;
            foreach (FieldInfo field in fields)
            {
                VertexFieldInfo vertexFieldInfo = GetVertexFieldInfo(field, offset);
                vertexFields.Add(vertexFieldInfo);

                offset += Marshal.SizeOf(field.FieldType);
            }

            return vertexFields.ToArray();
        }

        private static VertexFieldInfo GetVertexFieldInfo(FieldInfo field, int offset)
        {
            string fieldName = field.Name;
            Type fieldType = field.FieldType;

            var fieldOffsetAttribute = field.GetCustomAttribute<FieldOffsetAttribute>();
            if (fieldOffsetAttribute != null)
            {
                offset = fieldOffsetAttribute.Value;
            }

            int numComponents;
            ShaderDataType shaderDataType;
            if (!ShaderDataItemHelpers.TryGetDataItemType(fieldType, out numComponents, out shaderDataType))
            {
                throw new NotSupportedException($"Field {fieldName}: {fieldType} is not a supported Vertex field.");
            }

            return new VertexFieldInfo(fieldType, fieldName, numComponents, shaderDataType, offset);
        }
    }
}
