using System;
using System.Collections.Generic;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public interface IBuiltShader
    {
        IReadOnlyList<IShaderStorageItem> Uniforms { get; }
        IReadOnlyList<IShaderStorageItem> Outputs { get; }

        IShaderFunction VertexMain { get; }
        IShaderFunction FragmentMain { get; }
        IReadOnlyList<IShaderFunction> CustomFunctions { get; }
    }

    public class BuiltShader : IBuiltShader
    {
        public IReadOnlyList<IShaderStorageItem> Uniforms { get; }
        public IReadOnlyList<IShaderStorageItem> Outputs { get; }
        public IShaderFunction VertexMain { get; }
        public IShaderFunction FragmentMain { get; }
        public IReadOnlyList<IShaderFunction> CustomFunctions { get; }

        public BuiltShader(IShaderFunction vertex, IShaderFunction fragment)
        {
            VertexMain = vertex;
            FragmentMain = fragment;

            Outputs = new[] {
                ShaderStorageItem.CreateOutputColour()
            };
        }
    }
}
