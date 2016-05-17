using System;
using System.Drawing;
using OrcaML.Graphics.Abstractions;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using D3D11 = SharpDX.Direct3D11;

namespace OrcaML.Graphics.DirectX
{
    public class Direct3DContext
    {
        private readonly RenderForm _renderForm;

        #region DirectX

        private D3D11.Device _d3dDevice;
        private D3D11.DeviceContext _d3dDeviceContext;
        private D3D11.RenderTargetView _backBufferRenderTargetView;

        private SwapChain _swapChain;
        private ModeDescription _backBufferDesc;
        private RawViewportF _viewport;

        public D3D11.Device Device => _d3dDevice;
        public D3D11.DeviceContext DeviceContext => _d3dDeviceContext;
        public D3D11.RenderTargetView BackBufferRenderTargetView => _backBufferRenderTargetView;

        #endregion

        public Direct3DContext()
        {
            int width = 640;
            int height = 480;

            _renderForm = new RenderForm();
            _renderForm.ClientSize = new Size(width, height);
            _renderForm.AllowUserResizing = false;

            _backBufferDesc = new ModeDescription(width, height, new Rational(60, 1), Format.R8G8B8A8_UNorm);

            var swapChainDesc = new SwapChainDescription() {
                ModeDescription = _backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = _renderForm.Handle,
                IsWindowed = true
            };

            D3D11.Device.CreateWithSwapChain(DriverType.Hardware,
                                             D3D11.DeviceCreationFlags.None,
                                             swapChainDesc,
                                             out _d3dDevice,
                                             out _swapChain);

            _d3dDeviceContext = _d3dDevice.ImmediateContext;

            using (D3D11.Texture2D backBuffer = _swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                _backBufferRenderTargetView = new D3D11.RenderTargetView(_d3dDevice, backBuffer);
            }

            _d3dDeviceContext.OutputMerger.SetRenderTargets(_backBufferRenderTargetView);

            _viewport = new RawViewportF()
            {
                X = 0,
                Y = 0,
                Width = width,
                Height = height,
                MinDepth = 0,
                MaxDepth = 1
            };
            _d3dDeviceContext.Rasterizer.SetViewport(_viewport);
        }

        public void RenderLoop(IServiceProvider serviceProvider, Action<IOutputBuffer> renderFunc)
        {
            Direct3DGraphicsService graphicsService =
                serviceProvider.GetService(typeof(IGraphicsService)) as Direct3DGraphicsService;

            IOutputBuffer outputBuffer = graphicsService.BackBuffer;

            SharpDX.Windows.RenderLoop.Run(_renderForm, () => {
                renderFunc(outputBuffer);
                _swapChain.Present(1, PresentFlags.None);
            });
        }
    }
}