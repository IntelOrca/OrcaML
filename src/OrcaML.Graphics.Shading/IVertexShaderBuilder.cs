using System;
using System.Linq.Expressions;

namespace OrcaML.Graphics.Shading
{
    public interface IVertexShaderBuilder<TProgramSpec, TVertexSpec> : IShaderBuilder<TProgramSpec, TVertexSpec>
        where TProgramSpec : class
        where TVertexSpec : struct
    {
        Svec4 Position { set; }
    }
}
