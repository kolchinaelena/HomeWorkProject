using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerLibrary.Common
{
    public static class PropertyExtension
    {
        public static String GetPropertyName<T>(this Expression<Func<T, Object>> propertyFunc)
        {
            var memberExpr = propertyFunc.Body as MemberExpression;

            if (default(MemberExpression) == memberExpr)
            {
                var unaryExpr = propertyFunc.Body as UnaryExpression;

                if (default(UnaryExpression) != unaryExpr && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (default(MemberExpression) != memberExpr && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member.Name;
            }

            return string.Empty;
        }

        public static T Required<T>(this T? value, String message = null) where T : struct
        {
            if (value == null)
                throw new NullReferenceException(message ?? "Value is required");

            return value.Value;
        }
    }
}
