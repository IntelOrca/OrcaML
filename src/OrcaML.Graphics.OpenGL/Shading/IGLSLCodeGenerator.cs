using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.OpenGL.Extensions;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.OpenGL.Shading
{
    [Flags]
    internal enum GLSLParameterQualifier
    {
        In    = (1 << 0),
        Out   = (1 << 1),
        InOut = In | Out,
    }

    internal interface IGLSLFunctionParameter : IShaderDataItem
    {
        GLSLParameterQualifier Qualifier { get; }
    }

    internal interface IGLSLCodeGenerator
    {
        void WriteUniform(IShaderStorageItem storageItem);
        void WriteInput(IShaderStorageItem storageItem, GLSLInterpolationMode interpolationMode = GLSLInterpolationMode.Smooth);
        void WriteOutput(IShaderStorageItem storageItem, GLSLInterpolationMode interpolationMode = GLSLInterpolationMode.Smooth);

        void BeginFunction(string name, GLSLDataType returnType, IEnumerable<IGLSLFunctionParameter> parameters);
        void EndFunction();

        void BeginReturn();
        void EndReturn();

        void WriteLocalDeclaration(IShaderStorageItem storageItem);
        void BeginAssignment(string target);
        void EndAssignment();

        void BeginCall(string functionName);
        void NextArgument();
        void EndCall();

        void BeginFieldAccessor();
        void EndFieldAccessor(string fieldName);

        void BeginExpression();
        void EndExpression();

        void WriteOperator(string op);

        void WriteGetDataItem(string name);

        void BeginIf();
        void BeginElse();

        void BeginBlock();
        void EndBlock();

        void BeginDo();
        void WriteDoWhile();
        void EndDo();

        void BeginWhile();
        void EndWhile();
    }

    internal interface IGLSLVertexCodeGenerator : IGLSLCodeGenerator
    {
    }

    internal interface IGLSLFragmentCodeGenerator : IGLSLCodeGenerator
    {
    }
}
