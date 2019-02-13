using System;
using AutoMapper;
using KvantShared.Model;
using KvantShared.Utils;
using KvantShared.Vms;
using KvantShared.Vms.References;

namespace KvantShared.Repos
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ReferenceVm, Reference>().IgnoreMember(o => o.ItemClass).AfterMap((v, i) => { i.ItemClass = v.ItemClass?.AssemblyQualifiedName; });
            CreateMap<Reference, ReferenceVm>().IgnoreMember(o => o.ItemClass).AfterMap((i, v) => { v.ItemClass = i.ItemClass != null ? Type.GetType(i.ItemClass) : null; });

            CreateMap<SimpleRecordVm, Record>().AfterMap((v, i) => { i.Content = v.Title; });
            CreateMap<Record, SimpleRecordVm>().AfterMap((i, v) => { v.Title = i.Content; });
        }
    }
}
