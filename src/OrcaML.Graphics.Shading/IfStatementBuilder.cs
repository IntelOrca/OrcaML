using System;
using System.Collections.Generic;
using System.Linq;
using OrcaML.Graphics.Shading.Dom;
using OrcaML.Graphics.Shading.Dom.Implementations;

namespace OrcaML.Graphics.Shading
{
    internal class IfStatementBuilder : IIfStatementBuilder
    {
        private readonly IInternalShaderBuilder _shaderBuilder;
        private readonly ShaderIfStatement _ifStatement = new ShaderIfStatement();

        public ShaderIfStatement IfStatement => _ifStatement;

        public IfStatementBuilder(IInternalShaderBuilder shaderBuilder)
        {
            _shaderBuilder = shaderBuilder;
        }

        public IIfStatementBuilder ElseIf(Sbool condition, Action block)
        {
            IEnumerable<IShaderStatement> statements = _shaderBuilder.BuildStatements(block);

            var ifBlock = new ShaderIfBlock();
            ifBlock.Condition = condition.Expression;
            ifBlock.Statements = statements.ToArray();

            _ifStatement.IfBlocks.Add(ifBlock);

            return this;
        }

        public void Else(Action block)
        {
            IEnumerable<IShaderStatement> statements = _shaderBuilder.BuildStatements(block);

            var elseBlock = new ShaderBlock(statements);
            _ifStatement.ElseBlock = elseBlock;
        }
    }
}
