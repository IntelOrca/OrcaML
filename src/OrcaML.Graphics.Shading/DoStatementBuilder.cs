using System;
using System.Collections.Generic;
using OrcaML.Graphics.Shading.Dom;
using OrcaML.Graphics.Shading.Dom.Implementations;

namespace OrcaML.Graphics.Shading
{
    internal class DoStatementBuilder : IDoStatementBuilder
    {
        private readonly IInternalShaderBuilder _shaderBuilder;
        private readonly ShaderDoStatement _doStatement = new ShaderDoStatement();

        public ShaderDoStatement DoStatement => _doStatement;

        public DoStatementBuilder(IInternalShaderBuilder shaderBuilder, Action block)
        {
            _shaderBuilder = shaderBuilder;

            IEnumerable<IShaderStatement> statements = shaderBuilder.BuildStatements(block);
            _doStatement = new ShaderDoStatement();
            _doStatement.Block = new ShaderBlock(statements);
        }

        public void Until(Sbool condition)
        {
            throw new NotImplementedException();
        }

        public void While(Sbool condition)
        {
            _doStatement.WhileCondition = condition.Expression;
        }
    }
}
