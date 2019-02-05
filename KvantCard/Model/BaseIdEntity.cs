using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KvantCard.Model
{
    public abstract class BaseIdEntity : BaseEntity, IIdModel, IEquatable<BaseIdEntity>
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseIdEntity other) || GetType() != other.GetType())
                return false;
            if (other.Id == 0 && other.Id == 0)
                return ReferenceEquals(this, obj);
            return other.Id == Id;
        }

        public bool Equals(BaseIdEntity other)
        {
            return Id == other?.Id;
        }


        private int _hash = 0;
        public override int GetHashCode()
        {
            if (_hash != 0)
                return _hash;
            if (Id != 0)
            {
                _hash = Id.GetHashCode();
                return _hash;
            }
            _hash = base.GetHashCode();
            return _hash;
        }

        public static bool operator ==(BaseIdEntity left, BaseIdEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseIdEntity left, BaseIdEntity right)
        {
            return !Equals(left, right);
        }

        //public override int GetHashCode()
        //{
        //    return Id != 0 ? Id.GetHashCode() : base.GetHashCode();
        //}

        public override string ToString()
        {
            if (Id == 0)
                return base.ToString();
            var strRep = JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = SimpleResolver.Instance,
                    TypeNameHandling = TypeNameHandling.All
                });
            var str = $"DB Object with ID:{this.Id}:{this.GetType().FullName}\n{strRep}";
            return str;
        }

        public class SimpleResolver : DefaultContractResolver
        {
            public static readonly SimpleResolver Instance = new SimpleResolver();

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                if (property.PropertyType != typeof(long)
                    && property.PropertyType != typeof(int)
                    && property.PropertyType != typeof(float)
                    && property.PropertyType != typeof(double)
                    && property.PropertyType != typeof(string)
                    && property.PropertyType != typeof(bool)
                    && property.PropertyType != typeof(DateTime)
                    && property.PropertyType != typeof(long?)
                    && property.PropertyType != typeof(int?)
                    && property.PropertyType != typeof(float?)
                    && property.PropertyType != typeof(double?)
                    && property.PropertyType != typeof(bool?)
                    && property.PropertyType != typeof(DateTime?)
                    )
                {
                    property.ShouldSerialize =
                        instance => false;
                }

                return property;
            }
        }
    }
}
