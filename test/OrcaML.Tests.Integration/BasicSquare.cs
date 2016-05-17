// #define USE_OPENGL

using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using OrcaML.Geometry;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.DirectX;
using OrcaML.Graphics.OpenGL;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Helpers;

namespace OrcaML.Tests.Integration
{
    public class BasicSquare : IDisposable
    {
        public static void Run()
        {
            using (var app = new BasicSquare())
            {
                app.Initialise();
                app.Loop();
            }
        }

        private IServiceProvider _serviceProvider;

        private IShaderProgram<MyShaderSpec, MyVertexSpec> _myShaderProgram;
        private IShaderProgramState<MyShaderSpec> _myShaderProgramProperties;
        private IVertexBuffer<MyVertexSpec> _myVertexBuffer;
        private int _time;

        public BasicSquare()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
#if USE_OPENGL
            services.AddOpenGL();
#else
            services.AddDirect3D();
#endif
        }

        public void Initialise()
        {
            var myShader = new MyShader();
            var myBuiltShader = myShader.Build();

            var shaderProgramFactory = _serviceProvider.GetService<IShaderProgramFactory>();
            _myShaderProgram = shaderProgramFactory.Create(new MyShader());
            
            var vertexBufferFactory = _serviceProvider.GetService<IVertexBufferFactory>();
            _myVertexBuffer = vertexBufferFactory.Create<MyVertexSpec>();
        }

        public void Dispose()
        {
            IDisposable disposable = _myShaderProgram as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public void Loop()
        {
            var graphicsService = _serviceProvider.GetService<IGraphicsService>();
            var backBuffer = graphicsService.BackBuffer;
#if USE_OPENGL
            var graphicsContext = _serviceProvider.GetService<OpenGLContext>();
#else
            var graphicsContext = _serviceProvider.GetService<Direct3DContext>();
#endif
            graphicsContext.RenderLoop(_serviceProvider, Render);
        }

        private void Render(IOutputBuffer buffer)
        {
            buffer.Clear(Colours.Black);

            var vertices = new MyVertexSpec[] {
                new MyVertexSpec() { Position = new Vector2f(32, 32), Colour = Colours.Red },
                new MyVertexSpec() { Position = new Vector2f(32, 64), Colour = Colours.Red },
                new MyVertexSpec() { Position = new Vector2f(64, 64), Colour = Colours.Red },
                new MyVertexSpec() { Position = new Vector2f(64, 32), Colour = Colours.Blue }
            };
            _myVertexBuffer.Set(vertices);

            float opacity = (float)((Math.Sin(_time / 180.0 * Math.PI) + 1.0) / 2.0);
            _myShaderProgram.SetUniform(new UniformFieldInfo("Opacity", 1, ShaderDataType.Float32), opacity);

            // buffer.Render(vertices, _myShaderProgram, _myShaderProgramProperties);
            buffer.Render(_myVertexBuffer, _myShaderProgram, _myShaderProgramProperties);

            _time++;
        }

        public class MyShader : IShader<MyShaderSpec, MyVertexSpec>
        {
            public ShaderTransfer<Svec4> TransferColour { get; set; }

            public void BuildVertex(IVertexShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                var localPos = builder.GetLocal<Svec2>();
                localPos.Value = builder.GetVertex(v => v.Position);
                localPos.Value = new Svec2(((localPos.Value.x / 640.0f) *  2.0f) - 1.0f,
                                           ((localPos.Value.y / 480.0f) * -2.0f) + 1.0f);
                builder.Position = new Svec4(localPos.Value.x, localPos.Value.y, 0.0f, 1.0f);

                TransferColour.Value = builder.GetVertex(v => v.Colour);
            }

            public void BuildFragment(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                var colour = builder.GetLocal<Svec4>();

                Sfloat opacity = builder.GetUniform(u => u.Opacity);
                colour.Value = TransferColour.Value * opacity;
                builder.Colour = new Svec4(colour.Value.rgb, opacity);
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
