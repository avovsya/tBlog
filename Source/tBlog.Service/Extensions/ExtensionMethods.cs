using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace tBlog.Service.Extensions
{
    public static class ExtensionMethods
    {
        public static IList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            return source.Skip(pageNumber * (pageSize - 1)).Take(pageSize).ToList();
        }


        //TODO: remove
        //public static ObjectQuery<T> Include<T>(this ObjectQuery<T> source, Expression<Func<T, object>> exp)
        //{
        //    var path = GetPath(exp);
        //    return source.Include(path);
        //}

        //private static string GetPath(Expression exp)
        //{
        //    switch (exp.NodeType)
        //    {
        //        case ExpressionType.MemberAccess:
        //            var name = GetPath(((MemberExpression)exp).Expression) ?? "";

        //            if (name.Length > 0)
        //                name += ".";

        //            return name + ((MemberExpression)exp).Member.Name;

        //        case ExpressionType.Convert:
        //        case ExpressionType.Quote:
        //            return GetPath(((UnaryExpression)exp).Operand);

        //        case ExpressionType.Lambda:
        //            return GetPath(((LambdaExpression)exp).Body);

        //        default:
        //            return null;
        //    }
        //}
    }
}
