using System.Collections.Generic;

namespace OrcaML.Graphics.Shading.Dom.Helpers
{
    public static class IShaderNodeExtensions
    {
        public static IEnumerable<IShaderNode> GetDescendants(this IShaderNode node)
        {
            IEnumerable<IShaderNode> children = node.Children;
            if (children == null)
            {
                yield break;
            }

            foreach (IShaderNode childA in children)
            {
                yield return childA;
                foreach (IShaderNode childB in childA.GetDescendants())
                {
                    yield return childB;
                }
            }
        }
    }
}
