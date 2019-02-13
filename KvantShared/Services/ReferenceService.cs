using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AutoMapper;
using KvantShared.Model;
using KvantShared.Repos;
using KvantShared.Utils;
using KvantShared.Vms;
using KvantShared.Vms.References;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Clauses;

namespace KvantShared.Services
{
    public class ReferenceService
    {
        private readonly UnitOfWorkFactory _workFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<ReferenceService> _logger;

        public ReferenceService(UnitOfWorkFactory workFactory,
            IMapper mapper, ILogger<ReferenceService> logger)
        {
            _workFactory = workFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<ReferenceVm> AllReferences()
        {
            using (var uow = _workFactory.Create())
            {
                var items = uow.Repo<Reference>().GetAll().Where(e => e.Deleted == null);
                var vms = items.Select(e => _mapper.Map<Reference, ReferenceVm>(e));
                return vms;
            }
        }

        public void Remove(ReferenceVm vm)
        {
            vm.Deleted = DateTime.Now;
            var item = _mapper.Map<Reference>(vm);
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Reference>().Update(item);
                //_referenceRepo.UpdateSpecificFields(item, r => r.Deleted);
                uow.Save();
                _mapper.Map(item, vm);
            }
        }

        public void Create(ReferenceVm vm)
        {
            if (vm.Id != 0)
                throw new Exception("Модель должна быть новой");
            var item = _mapper.Map<Reference>(vm);
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Reference>().Create(item);
                uow.Save();
                _mapper.Map(item, vm);
            }
        }

        public ReferenceVm Create(string code, string title)
        {
            var item = new Reference()
            {
                Complex = false,
                Code = code,
                Title = title
            };
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Reference>().Create(item);
                uow.Save();
            }
            var vm = _mapper.Map<ReferenceVm>(item);
            return vm;
        }

        public ReferenceVm Create<T>(string code, string title) where T : BaseRecordVm
        {
            var item = new Reference()
            {
                Complex = true,
                Code = code,
                Title = title,
                ItemClass = typeof(T).AssemblyQualifiedName
            };
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Reference>().Create(item);
                uow.Save();
            }
            var vm = _mapper.Map<ReferenceVm>(item);
            return vm;
        }

        public void Update(ReferenceVm vm)
        {
            if (vm.Id == 0)
                throw new Exception("Модель должна быть уже в базе");
            var item = _mapper.Map<Reference>(vm);
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Reference>().Update(item);
                uow.Save();
                _mapper.Map(item, vm);
            }
        }

        public ObservableCollection<BaseRecordVm> GetRecords(string referenceCode)
        {
            using (var uow = _workFactory.Create())
            {
                referenceCode = referenceCode.ToLowerInvariant();
                var reference = uow.Repo<Reference>().Find(e => e.Code.ToLowerInvariant() == referenceCode);
                var records = uow.Repo<Record>().GetAll(e => e.Reference.Id == reference.Id && e.Deleted == null);
                if (!reference.Complex)
                {
                    var ret = records.Select(e => _mapper.Map<SimpleRecordVm>(e));
                    return new ObservableCollection<BaseRecordVm>(ret);
                }
                else
                {
                    var type = Type.GetType(reference.ItemClass);
                    var ret = records.Select(e =>
                    {
                        var vm = (BaseRecordVm)JsonHelper.Deserialize(e.Content, type);
                        vm.Id = e.Id;
                        vm.Deleted = e.Deleted;
                        vm.Updated = e.Updated;
                        vm.Created = e.Created;
                        return vm;
                    });
                    return new ObservableCollection<BaseRecordVm>(ret);
                }
            }
        }

        public ObservableCollection<T> GetRecords<T>(string referenceCode) where T : BaseRecordVm
        {
            return new ObservableCollection<T>(GetRecords(referenceCode).Cast<T>());
        }

        public T CreateRecord<T>(string referenceCode, T vm) where T : BaseRecordVm
        {
            using (var uow = _workFactory.Create())
            {
                referenceCode = referenceCode.ToLowerInvariant();
                var reference = uow.Repo<Reference>().AsTracking(true).Find(e => e.Code.ToLowerInvariant() == referenceCode);
                var recType = vm.GetType().AssemblyQualifiedName;
                //if (vm.GetType() == typeof(SimpleRecordVm))
                //    throw new Exception("Класс записи не должен быть простым");
                if (reference.ItemClass != recType)
                    throw new Exception("Класс записи и словаря не совпадают");

                var content = reference.Complex ? JsonHelper.Serialize(vm) : (vm as SimpleRecordVm)?.Title;
                var rec = new Record
                {
                    Reference = reference,
                    Content = content
                };

                //reference.Records.Add(rec);
                rec = uow.Repo<Record>().Create(rec);
                uow.Save();

                vm.Id = rec.Id;
                vm.Deleted = rec.Deleted;
                vm.Updated = rec.Updated;
                vm.Created = rec.Created;
                return vm;
            }
        }

        public void RemoveRecord(BaseRecordVm vm)
        {
            if (vm.Id == 0)
                throw new Exception("Модель должна быть уже в базе");
            using (var uow = _workFactory.Create())
            {
                //reference.Records.Add(rec);
                var item = uow.Repo<Record>().GetById(vm.Id);
                item.Deleted = DateTime.Now;
                item = uow.Repo<Record>().Update(item);
                //_referenceRepo.UpdateSpecificFields(item, r => r.Deleted);

                uow.Save();
                vm.Deleted = item.Deleted;
                vm.Updated = item.Updated;
                vm.Created = item.Created;
            }
        }

        public void UpdateRecord<T>(T vm) where T : BaseRecordVm
        {
            if (vm.Id == 0)
                throw new Exception("Модель должна быть уже в базе");

            Record item;
            using (var uow = _workFactory.Create())
            {
                item = uow.Repo<Record>().AsTracking(false).Include(e => e.Reference).GetById(vm.Id);
                var content = item.Reference.Complex ? JsonHelper.Serialize(vm) : (vm as SimpleRecordVm)?.Title;
                item.Content = content;
                item = uow.Repo<Record>().Update(item);

                uow.Save();
            }
            vm.Deleted = item.Deleted;
            vm.Updated = item.Updated;
            vm.Created = item.Created;
        }
    }
}
