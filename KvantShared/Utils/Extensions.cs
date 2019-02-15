using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace KvantShared.Utils
{
    public static class Extensions
    {
        public static string GetPropertyName<TProperty>(this Expression<Func<TProperty>> propertyExpression)
        {
            return propertyExpression.Body.GetMemberExpression().GetPropertyName();
        }

        public static string GetPropertyName(this MemberExpression memberExpression)
        {
            if (memberExpression == null)
                return null;

            if (memberExpression.Member.MemberType != MemberTypes.Property)
                return null;

            var child = memberExpression.Member.Name;
            var parent = GetPropertyName(memberExpression.Expression.GetMemberExpression());

            if (parent == null)
                return child;
            return parent + "." + child;
        }

        public static MemberExpression GetMemberExpression(this Expression expression)
        {
            if (expression is MemberExpression memberExpression)
                return memberExpression;

            if (!(expression is UnaryExpression unaryExpression)) return null;

            memberExpression = (MemberExpression)unaryExpression.Operand;
            return memberExpression;
        }

        public static void ShouldEqual<T>(this T actual, T expected, string name)
        {
            if (!Equals(actual, expected))
                throw new Exception($"{name}: Expected <{expected}> Actual <{actual}>.");
        }

        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
                action(i);
        }
    }
}
