using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderFunction : IShaderFunction
    {
        public IEnumerable<IShaderNode> Children => Statements;

        public string Name { get; }
        public IShaderDataTypeInfo ReturnDataType { get; }
        public IReadOnlyList<IShaderFunctionParameter> Parameters { get; }

        public string GeneratedName { get; }
        public IReadOnlyList<IShaderStatement> Statements { get; }

        public ShaderFunction(string name, IEnumerable<IShaderStatement> statements)
        {
            Name = name;
            Statements = statements.ToArray();
        }

        public override string ToString()
        {
            return $"Function: {ReturnDataType} {Name}()";
        }
    }
}
