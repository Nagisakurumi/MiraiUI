using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Expends
{
    public static class ExpressionExpend
    {
        /// <summary>
        /// 获取lambda表达式表示的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetExpressionProperty<T>(this Expression<Func<T>> expression)
        {
            var lambda = (LambdaExpression)expression;
            MemberExpression memberExpr;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpr = (UnaryExpression)lambda.Body;
                memberExpr = (MemberExpression)unaryExpr.Operand;
            }
            else
            {
                memberExpr = (MemberExpression)lambda.Body;
            }
            return memberExpr.Member.Name;
        }
    }
}
