using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Implementations
{
    internal class ShaderAssignmentStatement : IShaderAssignmentStatement
    {
        public IShaderStorageItem Destination { get; }
        public IShaderExpression Source { get; }

        public IEnumerable<IShaderNode> Children
        {
            get
            {
                yield return Source;
            }
        }

        public ShaderAssignmentStatement(IShaderStorageItem destination, IShaderExpression source)
        {
            Destination = destination;
            Source = source;
        }

        public override string ToString()
        {
            return $"Assignment: {Destination.Name} = {Source}";
        }
    }
}
