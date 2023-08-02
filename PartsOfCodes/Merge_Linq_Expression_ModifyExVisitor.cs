using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Shutterfly.Core.Utilities
{
    public class ModifyExVisitor
      : ExpressionVisitor // ExpressionVisitor is a very important class
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

    public class DemoInvokeClass
    {
        public async Task<List<MessageLog>> GetMessageLogListAsync(SearchModel searcher)
        {
            var parameter = Expression.Parameter(typeof(MessageLog));
            var vistor = new ModifyExVisitor(parameter);

            Expression<Func<MessageLog, bool>> expr1 = log => log.IsSuccessful;
            Expression<Func<MessageLog, bool>> expr2 = log => log.OrderId.Contains("1479488");
            Expression<Func<MessageLog, bool>> expr3 = log => log.SourceController == "shutterfly.Api.Controllers.ShipmentsController";

            var predicate = MergeExpression(vistor, expr1, expr2);
            predicate = MergeExpression(vistor, predicate, expr3);

            return DB_context.MessageLogs.Where(predicate);
        }

        private Expression<Func<MessageLog, bool>> MergeExpression(
            ModifyExVisitor visitor,
            Expression<Func<MessageLog, bool>> expression1,
            Expression<Func<MessageLog, bool>> expression2
            )
        {
            var left = visitor.Modify(expression1.Body);
            var right = visitor.Modify(expression2.Body);
            var binaryExpression = Expression.AndAlso(left, right);
            var predicate = Expression.Lambda<Func<MessageLog, bool>>(binaryExpression, visitor.parameter);
            return predicate;
        }
    }
}
