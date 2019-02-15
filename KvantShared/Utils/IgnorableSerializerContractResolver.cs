using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KvantShared.Utils
{
    /// <summary>
    /// Special JsonConvert resolver that allows you to ignore properties.  See https://stackoverflow.com/a/13588192/1037948
    /// </summary>
    public class IgnorableSerializerContractResolver<T> : DefaultContractResolver
    {
        protected readonly HashSet<string> Ignores;

        public IgnorableSerializerContractResolver()
        {
            Ignores = new HashSet<string>();
        }

        public IgnorableSerializerContractResolver(params Expression<Func<object>>[] exclude)
        {
            Ignores = new HashSet<string>();
            Ignore(exclude.Select(e => e.GetPropertyName()).ToArray());
        }

        /// <summary>
        /// Explicitly ignore the given property(s) for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName">one or more properties to ignore.  Leave empty to ignore the type entirely.</param>
        public void Ignore(params string[] propertyName)
        {
            // start bucket if DNE
            foreach (var prop in propertyName)
                Ignores.Add(prop);
        }

        /// <summary>
        /// Is the given property for the given type ignored?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool IsIgnored(Type type, string propertyName)
        {
            // if no properties provided, ignore the type entirely
            //if (Ignores.Count == 0) return true;

            return Ignores.Contains(propertyName);
        }

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (IsIgnored(property.DeclaringType, property.PropertyName)
            // need to check BaseType as well for EF -- @per comment by user576838
                || IsIgnored(property.DeclaringType.BaseType, property.PropertyName))
            {
                property.ShouldSerialize = instance => false;
            }

            return property;
        }

        public IgnorableSerializerContractResolver<T> Ignore(Expression<Func<object>> selector)
        {
            var propertyName = selector.GetPropertyName();
            Ignore(propertyName);
            return this;
        }
    }
}
