using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using OrcaML.Graphics.Shading.Dom;
using OrcaML.Graphics.Shading.Dom.Implementations;
using OrcaML.Graphics.Shading.Helpers;

namespace OrcaML.Graphics.Shading
{
    internal interface IInternalShaderBuilder
    {
        IEnumerable<IShaderStatement> BuildStatements(Action block);
        void Assign(IShaderStorageItem storageItem, IShaderExpression source);
    }

    internal interface IInternalShaderBuilder<TProgramSpec, TVertexSpec> : IInternalShaderBuilder
    {
        IShaderDataType GetUniform<TResult>(Expression<Func<TProgramSpec, TResult>> property);
        IShaderDataType GetVertex<TResult>(Expression<Func<TVertexSpec, TResult>> field);
    }

    internal class ShaderBuilder<TProgramSpec, TVertexSpec> : IShaderBuilder<TProgramSpec, TVertexSpec>,
                                                              IVertexShaderBuilder<TProgramSpec, TVertexSpec>,
                                                              IFragmentShaderBuilder<TProgramSpec, TVertexSpec>,
                                                              IInternalShaderBuilder<TProgramSpec, TVertexSpec>
        where TProgramSpec : class
        where TVertexSpec : struct
    {
        private IShaderStorageItem _outPosition = ShaderStorageItem.CreateOutputPosition();
        private IShaderStorageItem _outColour = ShaderStorageItem.CreateOutputColour();
        private List<IShaderStatement> _statementBuilder;
        private int _localIndex;

        private readonly Dictionary<PropertyInfo, IShaderStorageItem> _uniformStorageItems =
            new Dictionary<PropertyInfo, IShaderStorageItem>();
        private readonly Dictionary<FieldInfo, IShaderStorageItem> _vertexStorageItems =
            new Dictionary<FieldInfo, IShaderStorageItem>();

        public ShaderFunction Function
        {
            get
            {
                return new ShaderFunction("main", _statementBuilder);
            }
        }

        public Svec4 Position
        {
            set
            {
                Assign(_outPosition, value.Expression);
            }
        }

        public Svec4 Colour
        {
            set
            {
                Assign(_outColour, value.Expression);
            }
        }

        public void Initialise()
        {
            _statementBuilder = new List<IShaderStatement>();
        }

        public IEnumerable<IShaderStatement> BuildStatements(Action block)
        {
            List<IShaderStatement> parentStatementBuilder = _statementBuilder;
            List<IShaderStatement> childStatementBuilder = new List<IShaderStatement>();

            _statementBuilder = childStatementBuilder;
            block();
            _statementBuilder = parentStatementBuilder;

            return childStatementBuilder;
        }

        public IIfStatementBuilder If(Sbool condition, Action block)
        {
            var ifStatementBuilder = new IfStatementBuilder(this);

            _statementBuilder.Add(ifStatementBuilder.IfStatement);

            ifStatementBuilder.ElseIf(condition, block);
            return ifStatementBuilder;
        }

        public IDoStatementBuilder Do(Action block)
        {
            var doStatementBuilder = new DoStatementBuilder(this, block);

            _statementBuilder.Add(doStatementBuilder.DoStatement);

            return doStatementBuilder;
        }

        public void While(Sbool condition, Action block)
        {
            IEnumerable<IShaderStatement> statements = BuildStatements(block);

            var whileStatement = new ShaderWhileStatement();
            whileStatement.WhileCondition = condition.Expression;
            whileStatement.Block = new ShaderBlock(statements);

            _statementBuilder.Add(whileStatement);
        }

        public void Discard()
        {
            IShaderStatement statement = new ShaderDiscardStatement();
            _statementBuilder.Add(statement);
        }

        public void Assign(IShaderStorageItem storageItem, IShaderExpression source)
        {
            IShaderStatement statement = new ShaderAssignmentStatement(storageItem, source);
            _statementBuilder.Add(statement);
        }

        public ShaderLocal<T> GetLocal<T>() where T : IShaderDataType, new()
        {
            _localIndex++;
            return new ShaderLocal<T>(this, _localIndex);
        }

        public void SetColour(int outputBufferIndex, Svec4 value)
        {
            throw new NotImplementedException();
        }

        public IShaderDataType GetUniform<TResult>(Expression<Func<TProgramSpec, TResult>> property)
        {
            var propInfo = property.GetPropertyInfo();

            IShaderStorageItem storageItem;
            if (!_uniformStorageItems.TryGetValue(propInfo, out storageItem))
            {
                storageItem = GetUniformStorageItem(propInfo);
                _uniformStorageItems.Add(propInfo, storageItem);
            }

            IShaderDataTypeInfo dataTypeInfo = storageItem.DataTypeInfo;
            IShaderExpression expression = new ShaderStorageItemExpression(storageItem);
            return dataTypeInfo.Create(expression);
        }

        public IShaderDataType GetVertex<TResult>(Expression<Func<TVertexSpec, TResult>> field)
        {
            var fieldInfo = field.GetFieldInfo();
            Type fieldType = fieldInfo.FieldType;

            IShaderStorageItem storageItem;
            if (!_vertexStorageItems.TryGetValue(fieldInfo, out storageItem))
            {
                storageItem = GetVertexStorageItem(fieldInfo);
                _vertexStorageItems.Add(fieldInfo, storageItem);
            }

            IShaderDataTypeInfo dataTypeInfo = storageItem.DataTypeInfo;
            IShaderExpression expression = new ShaderStorageItemExpression(storageItem);
            return dataTypeInfo.Create(expression);
        }

        private IShaderStorageItem GetUniformStorageItem(PropertyInfo propertyInfo)
        {
            return ShaderStorageItem.CreateUniform(propertyInfo);
        }

        private IShaderStorageItem GetVertexStorageItem(FieldInfo fieldInfo)
        {
            return ShaderStorageItem.CreateVertex(fieldInfo);
        }
    }
}
