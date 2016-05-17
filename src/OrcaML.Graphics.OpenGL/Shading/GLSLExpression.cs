using System;
using OrcaML.Graphics.Shading;
using OrcaML.Graphics.Shading.Dom;

namespace OrcaML.Graphics.OpenGL.Shading
{
    internal static class GLSLExpression
    {
        public static void WriteExpression(this IGLSLCodeGenerator cg, IShaderExpression expr)
        {
            if (expr is IShaderConstantExpression)
            {
                Evaluate(cg, expr as IShaderConstantExpression);
            }
            else if (expr is IShaderStorageItemExpression)
            {
                Evaluate(cg, expr as IShaderStorageItemExpression);
            }
            else if (expr is IShaderFieldExpression)
            {
                Evaluate(cg, expr as IShaderFieldExpression);
            }
            else if (expr is IShaderCallExpression)
            {
                Evaluate(cg, expr as IShaderCallExpression);
            }
            else if (expr is IShaderBinaryExpression)
            {
                Evaluate(cg, expr as IShaderBinaryExpression);
            }
            else if (expr is IShaderLocal)
            {
                Evaluate(cg, expr as IShaderLocal);
            }
            else
            {
                throw new NotSupportedException($"{expr.GetType().Name} is not supported");
            }
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderConstantExpression expr)
        {
            string strConstant = expr.ToString();
            codeGenerator.WriteGetDataItem(strConstant);
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderLocal expr)
        {
            string localName = "local_" + expr.Id;
            codeGenerator.WriteGetDataItem(localName);
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderStorageItemExpression expr)
        {
            string name = GLSLDataTypeHelpers.GetGLSLName(expr.StorageItem);
            codeGenerator.WriteGetDataItem(name);
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderFieldExpression expr)
        {
            codeGenerator.BeginFieldAccessor();
            WriteExpression(codeGenerator, expr.Child);
            codeGenerator.EndFieldAccessor(expr.FieldName);
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderCallExpression expr)
        {
            codeGenerator.BeginCall(expr.Function);
            for (int i = 0; i < expr.Arguments.Length; i++)
            {
                IShaderExpression argExpr = expr.Arguments[i];
                WriteExpression(codeGenerator, argExpr);
                if (i < expr.Arguments.Length - 1)
                {
                    codeGenerator.NextArgument();
                }
            }
            codeGenerator.EndCall();
        }

        private static void Evaluate(IGLSLCodeGenerator codeGenerator, IShaderBinaryExpression expr)
        {
            codeGenerator.BeginExpression();
            WriteExpression(codeGenerator, expr.Left);

            string op = GetOperatorString(expr.Operation);
            codeGenerator.WriteOperator(op);

            WriteExpression(codeGenerator, expr.Right);
            codeGenerator.EndExpression();
        }

        private static string GetOperatorString(ShaderBinaryExpressionOp op)
        {
            switch (op) {
            case ShaderBinaryExpressionOp.Add:                return "+";
            case ShaderBinaryExpressionOp.Subtract:           return "-";
            case ShaderBinaryExpressionOp.Multiply:           return "*";
            case ShaderBinaryExpressionOp.Divide:             return "/";
            case ShaderBinaryExpressionOp.LessThan:           return "<";
            case ShaderBinaryExpressionOp.GreaterThan:        return ">";
            case ShaderBinaryExpressionOp.LessThanOrEqual:    return "<=";
            case ShaderBinaryExpressionOp.GreaterThanOrEqual: return ">=";
            case ShaderBinaryExpressionOp.Equal:              return "==";
            case ShaderBinaryExpressionOp.NotEqual:           return "!=";
            default:
                throw new NotSupportedException($"Operator {op} not supported.");
            }
        }
    }
}
