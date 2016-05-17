using System;
using Microsoft.Extensions.DependencyInjection;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OrcaML.Graphics.Abstractions;

namespace OrcaML.Graphics.OpenGL
{
    public class OpenGLContext
    {
        private GameWindow _gameWindow;

        public OpenGLContext()
        {
            _gameWindow = new GameWindow(640, 480);
            _gameWindow.WindowBorder = WindowBorder.Fixed;
        }

        public void RenderLoop(IServiceProvider serviceProvider, Action<IOutputBuffer> renderFunc)
        {
            var graphicsService = serviceProvider.GetService<IGraphicsService>();
            IOutputBuffer backBuffer = graphicsService.BackBuffer;

            var bounds = OpenTK.DisplayDevice.Default.Bounds;
            _gameWindow.Location = new System.Drawing.Point((bounds.Width - _gameWindow.Width) / 2,
                                                            (bounds.Height - _gameWindow.Height) / 2);
            _gameWindow.Visible = true;
            _gameWindow.Resize += (sender, e) => {
                GL.Viewport(0, 0, _gameWindow.Width, _gameWindow.Height);
            };
            _gameWindow.RenderFrame += (sender, e) => {
                renderFunc(backBuffer);
                _gameWindow.SwapBuffers();
            };
            _gameWindow.Run(60);
        }
    }
}
