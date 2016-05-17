using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OrcaML.Graphics.Shading.Helpers
{
    public static class LinqExpressionExtensions
    {
        public static MemberInfo GetMemberInfo<T>(this Expression<T> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression != null)
            {
                var memberInfo = memberExpression.Member as MemberInfo;
                if (memberInfo != null)
                {
                    return memberInfo;
                }
            }

            throw new ArgumentException("Expression body is not a type member.");
        }

        public static FieldInfo GetFieldInfo<T>(this Expression<T> expression)
        {
            var memberInfo = expression.GetMemberInfo();
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo;
            }

            throw new ArgumentException("Expression body is not a property.");
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<T> expression)
        {
            var memberInfo = expression.GetMemberInfo();
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo;
            }

            throw new ArgumentException("Expression body is not a field.");
        }
    }
}
