using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class ShaderLocal<T> : IShaderLocal
        where T : IShaderDataType, new()
    {
        private readonly IInternalShaderBuilder _shaderBuilder;
        private readonly IShaderStorageItem _storageItem;
        private readonly ShaderStorageItemExpression _expression;

        public int Id { get; }
        public string Name { get; }

        internal ShaderLocal(IInternalShaderBuilder shaderBuilder, int id)
        {
            Id = id;
            Name = "local_" + Id;

            _shaderBuilder = shaderBuilder;
            _storageItem = ShaderStorageItem.CreateLocal(Name, (new T()).DataTypeInfo);
            _expression = new ShaderStorageItemExpression(_storageItem);
        }

        public T Value
        {
            get
            {
                return (T)Activator.CreateInstance(typeof(T), _expression);
            }
            set
            {
                _shaderBuilder.Assign(_storageItem, value.Expression);
            }
        }

        public static implicit operator T(ShaderLocal<T> local)
        {
            return local.Value;
        }

        public override string ToString()
        {
            return $"Local: {Name}";
        }
    }
}
