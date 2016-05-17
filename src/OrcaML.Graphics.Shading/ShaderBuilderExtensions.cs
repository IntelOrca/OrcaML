using System;
using System.Linq.Expressions;
using OrcaML.Geometry;

namespace OrcaML.Graphics.Shading
{
    public static class ShaderBuilderExtensions
    {
        public static Sfloat GetUniform<TProgramSpec, TVertexSpec>(this IShaderBuilder<TProgramSpec, TVertexSpec> builder,
                                                                   Expression<Func<TProgramSpec, float>> property)
            where TProgramSpec : class
            where TVertexSpec : struct
        {
            var internalBuilder = builder as IInternalShaderBuilder<TProgramSpec, TVertexSpec>;
            return (Sfloat)internalBuilder.GetUniform(property);
        }


        public static Sfloat GetVertex<TProgramSpec, TVertexSpec>(this IVertexShaderBuilder<TProgramSpec, TVertexSpec> builder,
                                                                  Expression<Func<TVertexSpec, float>> property)
            where TProgramSpec : class
            where TVertexSpec : struct
        {
            var internalBuilder = builder as IInternalShaderBuilder<TProgramSpec, TVertexSpec>;
            return (Sfloat)internalBuilder.GetVertex(property);
        }

        public static Svec2 GetVertex<TProgramSpec, TVertexSpec>(this IVertexShaderBuilder<TProgramSpec, TVertexSpec> builder,
                                                                 Expression<Func<TVertexSpec, Vector2f>> property)
            where TProgramSpec : class
            where TVertexSpec : struct
        {
            var internalBuilder = builder as IInternalShaderBuilder<TProgramSpec, TVertexSpec>;
            return (Svec2)internalBuilder.GetVertex(property);
        }

        public static Svec4 GetVertex<TProgramSpec, TVertexSpec>(this IVertexShaderBuilder<TProgramSpec, TVertexSpec> builder,
                                                                 Expression<Func<TVertexSpec, Vector4f>> property)
            where TProgramSpec : class
            where TVertexSpec : struct
        {
            var internalBuilder = builder as IInternalShaderBuilder<TProgramSpec, TVertexSpec>;
            return (Svec4)internalBuilder.GetVertex(property);
        }

        public static void For(this IShaderBuilder builder,
                               ShaderLocal<Sint> local,
                               Sint startValue,
                               Sint endValue,
                               Action block)
        {
            builder.For(() => local.Value = startValue, local.Value <= endValue, () => local.Value += 1, block);
        }

        public static void For(this IShaderBuilder builder,
                               Action startAction,
                               Sbool condition,
                               Action nextAction,
                               Action block)
        {
            // TODO cater for continue and break
            startAction.Invoke();
            builder.While(condition, () => {
                block();
                nextAction();
            });
        }
    }
}
