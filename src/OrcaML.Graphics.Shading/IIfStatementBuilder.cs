using System;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.Shading
{
    public interface IIfStatementBuilder
    {
        IIfStatementBuilder ElseIf(Sbool condition, Action block);
        void Else(Action block);
    }
}
