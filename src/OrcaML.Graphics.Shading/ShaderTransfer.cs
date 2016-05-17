using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public class ShaderTransfer<T>
        where T : IShaderDataType, new()
    {
        private readonly IInternalShaderBuilder _shaderBuilder;
        private readonly int _id;
        private readonly string _name;
        private readonly IShaderStorageItem _inputStorageItem;
        private readonly IShaderStorageItem _outputStorageItem;
        private readonly IShaderDataTypeInfo _dataTypeInfo;

        private readonly IShaderExpression _getterExpression;
        private readonly IShaderExpression _setterExpression;

        internal ShaderTransfer(IInternalShaderBuilder shaderBuilder, int id)
        {
            _shaderBuilder = shaderBuilder;
            _id = id;
            _name = "transfer_" + _id;
            _dataTypeInfo = (new T()).DataTypeInfo;
            _inputStorageItem = ShaderStorageItem.CreateTransferInput(_name, _dataTypeInfo);
            _outputStorageItem = ShaderStorageItem.CreateTransferOutput(_name, _dataTypeInfo);
            _getterExpression = new ShaderStorageItemExpression(_inputStorageItem);
            _setterExpression = new ShaderStorageItemExpression(_outputStorageItem);
        }

        public T Value
        {
            get
            {
                return (T)Activator.CreateInstance(typeof(T), _getterExpression);
            }
            set
            {
                _shaderBuilder.Assign(_outputStorageItem, value.Expression);
            }
        }
    }
}
