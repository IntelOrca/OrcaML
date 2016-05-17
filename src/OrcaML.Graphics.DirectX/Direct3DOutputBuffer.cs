using OrcaML.Graphics.Abstractions;
using SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    internal class Direct3DOutputBuffer : IOutputBuffer
    {
        private readonly Direct3DGraphicsService _graphicsService;
        private readonly RenderTargetView _renderTargetView;

        public IGraphicsService GraphicsService => _graphicsService;
        public RenderTargetView RenderTargetView => _renderTargetView;

        public Direct3DOutputBuffer(Direct3DGraphicsService graphicsService, RenderTargetView renderTargetView)
        {
            _graphicsService = graphicsService;
            _renderTargetView = renderTargetView;
        }
    }
}