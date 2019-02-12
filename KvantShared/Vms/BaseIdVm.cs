using System;
using System.Reflection;
using KvantShared.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KvantShared.Vms
{
    public class BaseIdVm : BaseVm, IIdModel, IEquatable<BaseIdVm>
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { SetProperty(ref _id, value, () => Id); }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseIdVm other) || GetType() != other.GetType())
                return false;
            if (other.Id == 0 && other.Id == 0)
                return ReferenceEquals(this, obj);
            return other.Id == Id;
        }

        public bool Equals(BaseIdVm other)
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

        public static bool operator ==(BaseIdVm left, BaseIdVm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseIdVm left, BaseIdVm right)
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
                    ContractResolver = BaseIdVm.SimpleResolver.Instance,
                    TypeNameHandling = TypeNameHandling.All
                });
            var str = $"DB Object with ID:{this.Id}:{this.GetType().FullName}\n{strRep}";
            return str;
        }

        public class SimpleResolver : DefaultContractResolver
        {
            public static readonly BaseIdVm.SimpleResolver Instance = new BaseIdVm.SimpleResolver();

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
