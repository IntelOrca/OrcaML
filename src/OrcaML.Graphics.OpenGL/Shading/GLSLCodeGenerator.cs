using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.OpenGL.Extensions;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.OpenGL.Shading
{
    internal class GLSLCodeGenerator : IGLSLCodeGenerator
    {
        private readonly StringBuilder _sbUniforms = new StringBuilder();
        private readonly StringBuilder _sbInputs = new StringBuilder();
        private readonly StringBuilder _sbOutputs = new StringBuilder();
        private readonly StringBuilder _sbCode = new StringBuilder();

        private int _indentLevel = 0;

        public void WriteUniform(IShaderStorageItem storageItem)
        {
            _sbUniforms.Append("uniform ");
            AppendShaderDataItem(_sbUniforms, storageItem);
            AppendEndLine(_sbUniforms);
        }

        public void WriteInput(IShaderStorageItem storageItem, GLSLInterpolationMode interpolationMode)
        {
            AppendInterpolationMode(_sbInputs, interpolationMode);
            _sbInputs.Append("in ");
            AppendShaderDataItem(_sbInputs, storageItem);
            AppendEndLine(_sbInputs);
        }

        public void WriteOutput(IShaderStorageItem storageItem, GLSLInterpolationMode interpolationMode)
        {
            AppendInterpolationMode(_sbOutputs, interpolationMode);
            _sbOutputs.Append("out ");
            AppendShaderDataItem(_sbOutputs, storageItem);
            AppendEndLine(_sbOutputs);
        }

        public void BeginFunction(string name, GLSLDataType returnType, IEnumerable<IGLSLFunctionParameter> parameters)
        {
            _sbCode.Append(returnType.ToGLSL());
            _sbCode.Append(' ');
            _sbCode.Append(name);
            _sbCode.Append('(');

            bool anyParameters = false;
            foreach (IGLSLFunctionParameter parameter in parameters)
            {
                _sbCode.Append(parameter.Qualifier.ToGLSL());
                _sbCode.Append(' ');
                AppendShaderDataItem(_sbCode, null);
                _sbCode.Append(parameter.Name);

                _sbCode.Append(",");
                _sbCode.Append(" ");

                anyParameters = true;
            }

            if (anyParameters)
            {
                // Remove last parameter separator
                _sbCode.RemoveEnd(2);
            }

            _sbCode.Append(')');

            _sbCode.AppendLine();
            _sbCode.Append('{');
            _sbCode.AppendLine();

            _indentLevel++;
        }

        public void EndFunction()
        {
            _indentLevel--;

            _sbCode.Append('}');
            _sbCode.AppendLine();
        }

        public void BeginReturn()
        {
            AppendIndent(_sbCode);
            _sbCode.Append("return ");
        }

        public void EndReturn()
        {
            AppendEndLine(_sbCode);
        }

        public void WriteLocalDeclaration(IShaderStorageItem storageItem)
        {
            AppendIndent(_sbCode);
            AppendShaderDataItem(_sbCode, storageItem);
            AppendEndLine(_sbCode);
        }

        public void BeginAssignment(string target)
        {
            AppendIndent(_sbCode);
            _sbCode.Append(target);
            _sbCode.Append(" ");
            _sbCode.Append('=');
            _sbCode.Append(' ');
        }

        public void EndAssignment()
        {
            AppendEndLine(_sbCode);
        }

        public void BeginCall(string functionName)
        {
            _sbCode.Append(functionName);
            _sbCode.Append('(');
        }

        public void NextArgument()
        {
            _sbCode.Append(',');
            _sbCode.Append(' ');
        }

        public void EndCall()
        {
            _sbCode.Append(')');
        }

        public void BeginFieldAccessor()
        {
            
        }

        public void EndFieldAccessor(string fieldName)
        {
            _sbCode.Append('.');
            _sbCode.Append(fieldName);
        }

        public void BeginExpression()
        {
            _sbCode.Append('(');
        }

        public void EndExpression()
        {
            _sbCode.Append(')');
        }

        public void WriteOperator(string op)
        {
            _sbCode.Append(' ');
            _sbCode.Append(op);
            _sbCode.Append(' ');
        }

        public void WriteGetDataItem(string name)
        {
            _sbCode.Append(name);
        }

        public void BeginIf()
        {
            AppendIndent(_sbCode);
            _sbCode.Append("if ");
        }

        public void BeginElse()
        {
            AppendIndent(_sbCode);
            _sbCode.Append("else");
        }

        public void BeginDo()
        {
            AppendIndent(_sbCode);
            _sbCode.Append("do");
        }

        public void WriteDoWhile()
        {
            AppendIndent(_sbCode);
            _sbCode.Append("while ");
        }

        public void EndDo()
        {
            _sbCode.AppendLine(";");
        }

        public void BeginWhile()
        {
            _sbCode.Append("while ");
        }

        public void EndWhile()
        {
            _sbCode.AppendLine(";");
        }

        public void BeginBlock()
        {
            _sbCode.AppendLine();
            AppendIndent(_sbCode);
            _sbCode.AppendLine("{");

            _indentLevel++;
        }

        public void EndBlock()
        {
            _indentLevel--;

            AppendIndent(_sbCode);
            _sbCode.AppendLine("}");
        }

        protected void AppendInterpolationMode(StringBuilder sb, GLSLInterpolationMode interpolationMode)
        {
            if (interpolationMode != GLSLInterpolationMode.Smooth)
            {
                sb.Append(interpolationMode.ToGLSL());
                sb.Append(' ');
            }
        }

        protected void AppendShaderDataItem(StringBuilder sb, IShaderStorageItem storageItem)
        {
            string name = GLSLDataTypeHelpers.GetGLSLName(storageItem);

            AppendShaderDataType(sb, storageItem);
            sb.Append(' ');
            sb.Append(name);
        }

        private void AppendShaderDataType(StringBuilder sb, IShaderStorageItem storageItem)
        {
            GLSLDataType glslDataType = GLSLDataTypeHelpers.FromDataTypeInfo(storageItem.DataTypeInfo);
            sb.Append(glslDataType.ToGLSL());
        }

        protected void AppendEndLine(StringBuilder sb)
        {
            sb.Append(';');
            sb.AppendLine();
        }

        protected void AppendIndent(StringBuilder sb)
        {
            int numSpaces = _indentLevel * 4;
            for (int i = 0; i < numSpaces; i++)
            {
                sb.Append(' ');
            }
        }

        private void AppendVersion(StringBuilder sb, int versionNumber)
        {
            sb.AppendLine($"#version {versionNumber}");
        }

        private void AppendStringBuilder(StringBuilder destinationSB, StringBuilder sourceSB)
        {
            if (sourceSB.Length > 0)
            {
                destinationSB.AppendLine(sourceSB.ToString());
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendVersion(sb, 330);
            sb.AppendLine();

            AppendStringBuilder(sb, _sbUniforms);
            AppendStringBuilder(sb, _sbInputs);
            AppendStringBuilder(sb, _sbOutputs);
            AppendStringBuilder(sb, _sbCode);

            return sb.ToString();
        }
    }

    internal class GLSLVertexCodeGenerator : GLSLCodeGenerator, IGLSLVertexCodeGenerator
    {
    }

    internal class GLSLFragmentCodeGenerator : GLSLCodeGenerator, IGLSLFragmentCodeGenerator
    {
    }

    internal static class GLSLCodeGeneratorExtensions
    {
        public static void BeginMain(this IGLSLCodeGenerator cg)
        {
            cg.BeginFunction("main", GLSLDataType.Void, Enumerable.Empty<IGLSLFunctionParameter>());
        }

        public static void EndMain(this IGLSLCodeGenerator cg)
        {
            cg.EndFunction();
        }
    }
}
