using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrcaML.Geometry;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.OpenGL;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Helpers;

namespace OrcaML.Tests.Integration
{
    public class BasicFill : IDisposable
    {
        public static void Run()
        {
            using (var app = new BasicFill())
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

        public BasicFill()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenGL();
        }

        public void Initialise()
        {
            var myShader = new MyShader();
            // var myBuiltShader = myShader.Build();

            var shaderProgramFactory = _serviceProvider.GetService<IShaderProgramFactory>();
            _myShaderProgram = shaderProgramFactory.Create(new Mandlebrot());

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
            var graphicsContext = _serviceProvider.GetService<OpenGLContext>();
            graphicsContext.RenderLoop(_serviceProvider, Render);
        }

        private void Render(IOutputBuffer buffer)
        {
            buffer.Clear(Colours.Black);

            var vertices = new MyVertexSpec[] {
                new MyVertexSpec() { Position = new Vector2f(-1, -1) },
                new MyVertexSpec() { Position = new Vector2f(-1,  1) },
                new MyVertexSpec() { Position = new Vector2f( 1,  1) },
                new MyVertexSpec() { Position = new Vector2f( 1, -1) }
            };
            _myVertexBuffer.Set(vertices);

            _myShaderProgram.SetUniform(new UniformFieldInfo("Time", 1, ShaderDataType.Float32), _time / 60.0f);
            buffer.Render(_myVertexBuffer, _myShaderProgram, _myShaderProgramProperties);

            _time++;
        }

        public class MyShader : IShader<MyShaderSpec, MyVertexSpec>
        {
            public ShaderTransfer<Svec2> TransferPosition { get; set; }

            public void BuildVertex(IVertexShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                Svec2 pos = builder.GetVertex(v => v.Position);
                builder.Position = new Svec4(pos.x, pos.y, 0.0f, 1.0f);

                pos = new Svec2((pos.x * 0.5f) + 0.5f, (pos.y * 0.5f) + 0.5f);
                TransferPosition.Value = pos;
            }

            public void BuildFragment(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                Svec4 a = new Svec4(0.2f, 0.4f, 1.0f, 1.0f);
                Svec4 b = new Svec4(0.85f, 0.9f, 1.0f, 1.0f);
                Svec2 uv = TransferPosition.Value;

                Sfloat v = ov(builder, uv * 10.0f);

                builder.Colour = SMath.mix(a, b, SMath.smoothstep(0.0f, 0.5f, v));
            }

            private static Sfloat ov(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder,
                                     Svec2 p)
            {
                var localP = builder.GetLocal<Svec2>();
                var localV = builder.GetLocal<Sfloat>();
                var localA = builder.GetLocal<Sfloat>();

                localP.Value = p;
                localV.Value = 0.0f;
                localA.Value = 0.4f;

                var localI = builder.GetLocal<Sint>();
                builder.For(localI, 0, 2, () =>
                {
                    localV.Value += voronoi(builder, localP.Value) * localA.Value;
                    localP.Value *= 2.0f;
                    localA.Value *= 0.5f;
                });
                return localV.Value;
            }

            private static Sfloat voronoi(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder, 
                                          Svec2 p)
            {
                var localN = builder.GetLocal<Svec2>();
                var localF = builder.GetLocal<Svec2>();
                var localMd = builder.GetLocal<Sfloat>();
                var localM = builder.GetLocal<Svec2>();

                Sfloat time = builder.GetUniform(u => u.Time);

                localN.Value = SMath.floor(p);
                localF.Value = SMath.fract(p);
                localMd.Value = 5.0f;
                localM.Value = new Svec2(0.0f);
                var localI = builder.GetLocal<Sint>();
                builder.For(localI, -1, 1, () =>
                {
                    var localJ = builder.GetLocal<Sint>();
                    builder.For(localJ, -1, 1, () =>
                    {
                        Svec2 g = new Svec2(localI.Value, localJ.Value);
                        Svec2 o = hash2(localN.Value + g);
                        o = 0.5f + 0.5f * SMath.sin(time + 5.038f * o);
                        Svec2 r = g + o - localF.Value;
                        Sfloat d = SMath.dot(r, r);
                        builder.If(d < localMd.Value, () => {
                            localMd.Value = d;
                            localM.Value = localN.Value + g + o;
                        });
                    });
                });
                return localMd.Value;
            }

            private static Svec2 hash2(Svec2 p)
            {
                return SMath.fract(
                    SMath.sin(
                        new Svec2(
                            SMath.dot(p, new Svec2(123.4f, 748.6f)),
                            SMath.dot(p, new Svec2(547.3f, 659.3f)))) * 5232.85324f);
            }
        }

        public class Mandlebrot : IShader<MyShaderSpec, MyVertexSpec>
        {
            public ShaderTransfer<Svec2> TransferPosition { get; set; }

            public void BuildVertex(IVertexShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                Svec2 pos = builder.GetVertex(v => v.Position);
                builder.Position = new Svec4(pos.x, pos.y, 0.0f, 1.0f);
                pos = new Svec2((pos.x * 0.5f) + 0.5f, (pos.y * 0.5f) + 0.5f);

                Sfloat time = builder.GetUniform(x => x.Time);
                time = 0.05f;
                Sfloat rx = 0.1f - time;
                Sfloat ry = 0.1f - time;

                Sfloat cx = 0.16125f;
                Sfloat cy = 0.638438f;
                Sfloat minX = cx - rx;
                Sfloat maxX = cx + rx;
                Sfloat minY = cy - ry;
                Sfloat maxY = cy + ry;

                // minX = -2.5f;
                // minY = -1;
                // maxX = 1;
                // maxY = 1;

                TransferPosition.Value = new Svec2(
                    SMath.mix(minX, maxX, pos.x),
                    SMath.mix(minY, maxY, pos.y)
                );
            }

            public void BuildFragment(IFragmentShaderBuilder<MyShaderSpec, MyVertexSpec> builder)
            {
                var x0y0 = TransferPosition.Value;
                var xy = builder.GetLocal<Svec2>();
                var iteration = builder.GetLocal<Sfloat>();
                var exitLoop = builder.GetLocal<Sint>();

                xy.Value = new Svec2(0);
                iteration.Value = 0;
                Sint maxIteration = 1000;

                builder.Do(() => {
                    var xxyy = builder.GetLocal<Svec2>();

                    Sfloat x = xy.Value.x;
                    Sfloat y = xy.Value.y;
                    xxyy.Value = new Svec2(x * x, y * y);
                    Sfloat xx = xxyy.Value.x;
                    Sfloat yy = xxyy.Value.y;

                    exitLoop.Value = 1;
                    builder.If(xx + yy < (2 * 2), () =>
                    {
                        builder.If(iteration.Value < maxIteration, () =>
                        {
                            xy.Value = new Svec2((xx - yy) + x0y0.x, 2 * x * y + x0y0.y);
                            iteration.Value = iteration.Value + 1;
                            exitLoop.Value = 0;
                        });
                    });
                })
                .While(exitLoop.Value == 0);

                Sfloat colourV = iteration.Value / maxIteration;

                var colour = builder.GetLocal<Svec4>();
                var mixer = builder.GetLocal<Sfloat>();
                builder.If(iteration.Value == maxIteration, () => {
                    colour.Value = new Svec4(0);
                })
                .Else(() =>
                {

                    builder.If(iteration.Value < maxIteration / 2, () => {
                        mixer.Value = SMath.mix(0.0f, 1.0f, colourV);
                        colour.Value = new Svec4(mixer.Value, 0, 0, 0);
                    })
                    .Else(() => {
                        mixer.Value = SMath.mix(0.0f, 1.0f, colourV);
                        colour.Value = new Svec4(0, 0, mixer.Value, 0);
                    });
                });
                builder.Colour = new Svec4(colour.Value.rgb, 1);
            }
        }

        public class MyShaderSpec
        {
            public float Time { get; set; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MyVertexSpec
        {
            public Vector2f Position;
        }
    }
}
