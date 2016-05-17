using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public interface IShaderBuilder
    {
        IEnumerable<IShaderStatement> BuildStatements(Action block);
        IIfStatementBuilder If(Sbool condition, Action block);
        IDoStatementBuilder Do(Action block);
        void While(Sbool condition, Action block);

        void Discard();

        ShaderLocal<T> GetLocal<T>() where T : IShaderDataType, new();
    }

    public interface IShaderBuilder<TProgramSpec, TVertexSpec> : IShaderBuilder
        where TProgramSpec : class
        where TVertexSpec : struct
    {

    }
}
