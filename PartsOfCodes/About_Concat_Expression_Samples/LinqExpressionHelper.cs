using Bos.Tax.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Utilities
{
/*
Ref - 
\DevDocuments\Expression_Func_T, bool_拼接查询条件 - net5x - 博客园 (2022-04-06 5_05_45 PM).html
\DevDocuments\Linq系列(7)——表达式树之ExpressionVisitor - Edward.Zhan - 博客园 (2022-04-06 5_05_09 PM).html
 */
    public static class LinqExpressionHelper
    {
        public static Expression<Func<T, bool>> AppendExpress<T>(
            this Expression<Func<T, bool>> express,
            Expression<Func<T, bool>> appendExpress,
            EnConnectLogics? relations
            )
        {
            if (appendExpress == null || !relations.HasValue) return express;

            var parameter = Expression.Parameter(typeof(T));
            var visitor = new ModifyExVisitor(parameter);

            var left = visitor.Modify(express.Body);
            var right = visitor.Modify(appendExpress.Body);
            BinaryExpression binaryExpression;
            switch (relations)
            {
                case EnConnectLogics.And:
                    binaryExpression = Expression.AndAlso(left, right);
                    break;
                case EnConnectLogics.Or:
                    binaryExpression = Expression.OrElse(left, right);
                    break;
                default:
                    binaryExpression = Expression.AndAlso(left, right);
                    break;
            }
            var predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, visitor.parameter);
            return predicate;
        }
    }

    public class ModifyExVisitor
      : ExpressionVisitor
    {
        public ParameterExpression parameter { get; protected set; }
        public ModifyExVisitor(ParameterExpression parameter)
        {
            this.parameter = parameter;
        }

        public Expression Modify(Expression exp)
        {
            return Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return parameter;
        }
    }
}
