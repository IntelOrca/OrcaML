using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaML.Graphics.Abstractions
{
    public interface ITexture2D
    {
        int Width { get; }
        int Height { set; }
    }
}
