using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PartTest
{
    public class ExpressionTest
    {
        public static async Task Test1Async()
        {
            //Console.WriteLine(Calculate());
            Console.WriteLine(Sum(1, 2, 3, 4));
        }
        private static T Sum<T>(params T[] list) where T : struct
        {
            Expression exp = Expression.Constant(list[0]);
            foreach (var item in list.Skip(1)) {
                exp = Expression.Add(exp, Expression.Constant(item));
            }
            Expression<Func<T>> lambda = Expression.Lambda<Func<T>>(
                Expression.Convert(exp, typeof(T)));
            Func<T> compiled = lambda.Compile();
            return compiled();
        }
        private static int Calculate()
        {
            Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(
                Expression.Convert(
                    Expression.Add(
                        Expression.Multiply(
                            Expression.Constant(3),
                            Expression.Constant(10)
                        ),
                        Expression.Constant(5)
                    ),
                    typeof(int)
                )
            );

            Func<int> compiled = lambda.Compile();
            return compiled();
        }
    }
}
