using System.Runtime.InteropServices;
using OrcaML.Geometry;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Helpers;
using Xunit;

namespace OrcaML.Tests.Graphics.Shading
{
    public class ShaderBuilder
    {
        [Fact]
        public void TestDom()
        {
            var myShader = new MyShader();
            var myBuiltShader = myShader.Build();
        }

        public class MyShader : IShader<MyShaderSpec, MyVertexSpec>
        {
            public void BuildVertex(IVertexShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                var localPos = builder.GetLocal<Svec2>();
                localPos.Value = builder.GetVertex(v => v.Position);
                localPos.Value = new Svec2(((localPos.Value.x / 640.0f) * 2.0f) - 1.0f,
                                           ((localPos.Value.y / 480.0f) * 2.0f) - 1.0f);
                builder.Position = new Svec4(localPos.Value.x, localPos.Value.y, 0.0f, 1.0f);
            }

            public void BuildFragment(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                Sfloat opacity = builder.GetUniform(u => u.Opacity);
                builder.Colour = new Svec4(new Svec3(opacity), 1);
            }
        }

        public class MyShaderSpec
        {
            public float Opacity { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MyVertexSpec
        {
            public Vector2f Position;
            public Vector4f Colour;
        }
    }
}
