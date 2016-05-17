using System;

namespace OrcaML.Graphics.Abstractions
{
    public class VertexFieldInfo : IShaderDataItem
    {
        public Type CLRType { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the number of components there are. For example, 3 for a Vector3.
        /// </summary>
        public int Components { get; }

        /// <summary>
        /// Gets the internal representation of the value.
        /// </summary>
        public ShaderDataType Type { get; }

        /// <summary>
        /// Gets the offset in bytes from the start of the containing structure.
        /// </summary>
        public int Offset { get; }

        public VertexFieldInfo(Type clrType, string name, int components, ShaderDataType type, int offset)
        {
            CLRType = clrType;
            Name = name;
            Components = components;
            Type = type;
            Offset = offset;
        }

        public override string ToString()
        {
            return $"{Name}: {Type}*{Components} ({Offset})";
        }
    }
}
