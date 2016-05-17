using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcaML.Graphics.Shading.Dom
{
    internal class ShaderCallExpression : IShaderCallExpression
    {
        public string Function { get; }
        public IShaderExpression[] Arguments { get; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                foreach (IShaderExpression node in Arguments)
                {
                    yield return node;
                }
            }
        }

        public ShaderCallExpression(string function, params IShaderDataType[] args)
        {
            Function = function;
            Arguments = args.Select(x => x.Expression)
                            .ToArray();
        }

        public override string ToString()
        {
            return Function + "(" + String.Join<IShaderExpression>(", ", Arguments) + ")";
        }
    }
}
