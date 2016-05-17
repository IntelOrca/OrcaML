using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using OrcaML.Graphics.Abstractions;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Dom;
using OrcaML.Graphics.Shading.Dom.Helpers;
using OrcaML.Graphics.Shading.Helpers;

namespace OrcaML.Graphics.OpenGL.Shading
{
    internal class GLSLGenerator<TProgramSpec, TVertexSpec>
        where TProgramSpec : class, new()
        where TVertexSpec : struct
    {
        private const int GLSLVersion = 330;

        public string VertexSource { get; private set; }
        public string FragmentSource { get; private set; }

        public void CompileShader(IShader<TProgramSpec, TVertexSpec> shader)
        {
            IBuiltShader builtShader = shader.Build();
            VertexSource = GenerateVertexGLSL(builtShader);
            FragmentSource = GenerateFragmentGLSL(builtShader);
        }

        private string GenerateVertexGLSL(IBuiltShader builtShader)
        {
            IGLSLCodeGenerator cg = new GLSLCodeGenerator();
            IShaderFunction mainFunction = builtShader.VertexMain;

            var uniforms = mainFunction.GetUsedUniforms();
            foreach (var uniform in uniforms)
            {
                cg.WriteUniform(uniform);
            }

            var vertexFields = mainFunction.GetUsedVertexFields();
            foreach (var vertexField in vertexFields)
            {
                cg.WriteInput(vertexField);
            }

            var outputs = mainFunction.GetUsedOutputs();
            foreach (var output in outputs)
            {
                cg.WriteOutput(output);
            }

            var genVisitor = new GLSLStatementGenerator(cg);
            genVisitor.GenerateFunction(mainFunction);
            return cg.ToString();
        }

        private string GenerateFragmentGLSL(IBuiltShader builtShader)
        {
            IGLSLCodeGenerator cg = new GLSLCodeGenerator();
            IShaderFunction mainFunction = builtShader.FragmentMain;

            var uniforms = mainFunction.GetUsedUniforms();
            foreach (var uniform in uniforms)
            {
                cg.WriteUniform(uniform);
            }

            var inputs = mainFunction.GetUsedInputs();
            foreach (var input in inputs)
            {
                cg.WriteInput(input);
            }

            foreach (var output in builtShader.Outputs)
            {
                cg.WriteOutput(output);
            }

            var genVisitor = new GLSLStatementGenerator(cg);
            genVisitor.GenerateFunction(mainFunction);
            return cg.ToString();
        }

        private class GLSLStatementGenerator : IShaderStatementVisitor
        {
            private readonly IGLSLCodeGenerator _glslGenerator;

            public GLSLStatementGenerator(IGLSLCodeGenerator glslGenerator)
            {
                _glslGenerator = glslGenerator;
            }

            public void GenerateFunction(IShaderFunction function)
            {
                GLSLDataType returnType = function.ReturnDataType.ToGLSLDataType();
                var parameters = Enumerable.Empty<IGLSLFunctionParameter>();
                _glslGenerator.BeginFunction(function.Name, returnType, parameters);

                // Define local variables
                foreach (IShaderStorageItem local in function.GetUsedLocals())
                {
                    string name = GLSLDataTypeHelpers.GetGLSLName(local);
                    _glslGenerator.WriteLocalDeclaration(local);
                }

                function.VisitStatements(this);

                _glslGenerator.EndFunction();
            }

            public bool VisitAssignment(IShaderAssignmentStatement statement)
            {
                string destinationName = GLSLDataTypeHelpers.GetGLSLName(statement.Destination);

                _glslGenerator.BeginAssignment(destinationName);
                GLSLExpression.WriteExpression(_glslGenerator, statement.Source);
                _glslGenerator.EndAssignment();

                return true;
            }

            public bool VisitDiscard(IShaderDiscardStatement statement)
            {
                throw new NotImplementedException();
            }

            public bool VisitIf(IShaderIfStatement statement)
            {
                _glslGenerator.BeginIf();
                GLSLExpression.WriteExpression(_glslGenerator, statement.IfBlocks[0].Condition);
                _glslGenerator.BeginBlock();

                return true;
            }

            public void ExitIf(IShaderIfStatement statement)
            {
                _glslGenerator.EndBlock();
            }

            public bool VisitElse(IShaderIfStatement statement)
            {
                _glslGenerator.BeginElse();
                _glslGenerator.BeginBlock();

                return true;
            }

            public void ExitElse(IShaderIfStatement statement)
            {
                _glslGenerator.EndBlock();
            }

            public bool VisitDo(IShaderDoStatement statement)
            {
                _glslGenerator.BeginDo();
                _glslGenerator.BeginBlock();

                return true;
            }

            public bool VisitWhile(IShaderWhileStatement statement)
            {
                _glslGenerator.BeginWhile();
                GLSLExpression.WriteExpression(_glslGenerator, statement.WhileCondition);
                _glslGenerator.BeginBlock();

                return true;
            }

            public void ExitDo(IShaderDoStatement statement)
            {
                _glslGenerator.EndBlock();
                _glslGenerator.WriteDoWhile();
                GLSLExpression.WriteExpression(_glslGenerator, statement.WhileCondition);
                _glslGenerator.EndDo();
            }

            public void ExitWhile(IShaderWhileStatement statement)
            {
                _glslGenerator.EndBlock();
                _glslGenerator.EndWhile();
            }
        }
    }
}
