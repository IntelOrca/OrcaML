using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcaML.Graphics.Shading.Dom.Helpers
{
    public static class IShaderFunctionExtensions
    {
        public static IEnumerable<IShaderStorageItem> GetStorageItems(this IShaderFunction function)
        {
            IEnumerable<IShaderNode> nodes = function.GetDescendants();
            foreach (IShaderNode node in nodes)
            {
                IShaderStorageItem storageItem = null;
                if (node is IShaderAssignmentStatement)
                {
                    storageItem = ((IShaderAssignmentStatement)node).Destination;
                }
                else if (node is ShaderStorageItemExpression)
                {
                    storageItem = ((ShaderStorageItemExpression)node).StorageItem;
                }

                if (storageItem != null)
                {
                    yield return storageItem;
                }
            }
        }

        public static IEnumerable<IShaderStorageItem> GetDistinctStorageItems(this IShaderFunction function,
                                                                              StorageItemKind kind)
        {
            IEnumerable<IShaderStorageItem> results =
                function.GetStorageItems()
                        .Where(x => x.Kind == kind)
                        .Distinct();

            return results;
        }

        public static IEnumerable<IShaderStorageItem> GetUsedUniforms(this IShaderFunction function)
        {
            return function.GetDistinctStorageItems(StorageItemKind.Uniform);
        }

        public static IEnumerable<IShaderStorageItem> GetUsedVertexFields(this IShaderFunction function)
        {
            return function.GetDistinctStorageItems(StorageItemKind.Vertex);
        }

        public static IEnumerable<IShaderStorageItem> GetUsedInputs(this IShaderFunction function)
        {
            return function.GetDistinctStorageItems(StorageItemKind.Input);
        }

        public static IEnumerable<IShaderStorageItem> GetUsedOutputs(this IShaderFunction function)
        {
            return function.GetDistinctStorageItems(StorageItemKind.Output);
        }

        public static IEnumerable<IShaderStorageItem> GetUsedLocals(this IShaderFunction function)
        {
            return function.GetDistinctStorageItems(StorageItemKind.Local);
        }

        public static void VisitStatements(this IShaderFunction function, IShaderStatementVisitor visitor)
        {
            foreach (IShaderStatement statement in function.Statements)
            {
                statement.Visit(visitor);
            }
        }
    }
}
