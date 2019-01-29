using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KvantCard.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace KvantCard.Repos
{
    public abstract class BaseRepo<TModel, TVm> : IBaseRepo<TModel, TVm> where TModel : BaseIdEntity where TVm : class, IIdModel
    {
        //private readonly object _lock = new object();

        protected IMapper Mapper { get; }
        protected Func<Db, DbSet<TModel>> DbSet { get; }
        protected ILogger<BaseRepo<TModel, TVm>> Logger { get; }

        protected DbSet<TModel> Set(Db db) => DbSet(db);
        protected IQueryable<TModel> Included(Db db) => AddInclude(Set(db));

        protected BaseRepo(Db db, ILoggerFactory loggerFactory, IMapper mapper, Func<Db, DbSet<TModel>> dbSet)
        {
            Db = db;
            Mapper = mapper;
            DbSet = dbSet;
            Logger = loggerFactory.CreateLogger<BaseRepo<TModel, TVm>>();
        }

        public event EventHandler<RepoCrudEventArgs<TVm>> Operation;

        protected void InvokeOperation(RepoCrudEventArgs<TVm> args)
        {
            try
            {
                Operation?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101, "CrudOperation"), ex, $"Exception on exceute CRUD operation {args.Operation}");
            }
        }

        protected Db Db { get; }

        public virtual IList<TVm> GetAll()
        {
            lock (Db)
            {
                return Included(Db).AsNoTracking().ToList().Select(ConvertToVm).ToList();
            }
        }

        public virtual IList<TVm> GetAllByModel(Func<TModel, bool> match)
        {
            lock (Db)
            {
                return Included(Db).Where(match).ToList().Select(ConvertToVm).ToList();
            }
        }

        public virtual IList<TVm> GetAllByVm(Func<TVm, bool> match)
        {
            lock (Db)
            {
                return Included(Db).AsNoTracking().ToList().Select(ConvertToVm).Where(match).ToList();
            }
        }

        public virtual TVm GetById(long id)
        {
            lock (Db)
            {
                var model = Included(Db).AsNoTracking().FirstOrDefault(e => e.Id == id);
                return model != null ? ConvertToVm(model) : null;
            }
        }

        public virtual TVm FindByModel(Func<TModel, bool> match)
        {
            lock (Db)
            {
                var model = Included(Db).AsNoTracking().FirstOrDefault(match);
                return model != null ? ConvertToVm(model) : null;
            }
        }

        public virtual TVm FindByVm(Func<TVm, bool> match)
        {
            lock (Db)
            {
                return Included(Db).AsNoTracking().ToList().Select(ConvertToVm).FirstOrDefault(match);
            }
        }

        public virtual IList<TVm> UpdateOrCreate(IList<TVm> items, bool updateSources)
        {
            IDictionary<TModel, TVm> updated = new Dictionary<TModel, TVm>();
            IDictionary<EntityEntry<TModel>, TVm> created = new Dictionary<EntityEntry<TModel>, TVm>();
            lock (Db)
            {
                using (var trans = Db.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        if (item.Id == 0)
                        {
                            // Create
                            var model = ConvertToModel(item);
                            model.Updated = DateTime.UtcNow;
                            model.Created = model.Updated;
                            InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.BeforeCreate, 0, item, null));
                            EntityEntry<TModel> m2 = Set(Db).Add(model);
                            created.Add(m2, item);
                        }
                        else
                        {
                            // Update
                            var original = Set(Db).First(x => x.Id == item.Id);
                            InvokeOperation(
                                new RepoCrudEventArgs<TVm>(Db, RepoOperation.BeforeUpdate, item.Id, item, null));
                            UpdateModelFromVm(item, original);
                            original.Updated = DateTime.UtcNow;
                            updated.Add(original, item);
                        }
                    }

                    Db.SaveChanges();
                    try
                    {
                        if (updateSources)
                        {
                            foreach (var pair in created)
                            {
                                UpdateVmFromModel(pair.Key.Entity, pair.Value);
                                InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterCreate,
                                    pair.Value.Id,
                                    pair.Value, pair.Value));
                            }

                            foreach (var pair in updated)
                            {
                                UpdateVmFromModel(pair.Key, pair.Value);
                                InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterUpdate, pair.Key.Id,
                                    pair.Value, pair.Value));
                            }

                            return items;
                        }
                        else
                        {
                            var outs = new List<TVm>();

                            foreach (var pair in created)
                            {
                                var vmOut = ConvertToVm(pair.Key.Entity);
                                InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterCreate,
                                    pair.Value.Id,
                                    pair.Value, vmOut));
                                outs.Add(vmOut);
                            }

                            foreach (var pair in updated)
                            {
                                var vmOut = ConvertToVm(pair.Key);
                                InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterUpdate, vmOut.Id,
                                    pair.Value, vmOut));
                                outs.Add(vmOut);
                            }

                            return outs;
                        }
                    }
                    finally
                    {
                        created.Clear();
                        updated.Clear();
                        trans.Commit();
                    }
                }
            }
        }

        public virtual TVm UpdateOrCreate(long id, TVm item)
        {
            if (id != item.Id) throw new Exception("Provided ID doesn't match object ID");
            return id == 0 ? Create(item) : Update(id, item);
        }

        public virtual TVm Create(TVm item)
        {
            lock (Db)
            {
                var model = ConvertToModel(item);
                model.Updated = DateTime.UtcNow;
                model.Created = model.Updated;
                using (var trans = Db.BeginTransaction())
                {
                    try
                    {
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.BeforeCreate, 0, item, null));
                        var m2 = Set(Db).Add(model);
                        Db.SaveChanges();
                        var vmOut = ConvertToVm(m2.Entity);
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterCreate, vmOut.Id, item, vmOut));

                        return vmOut;
                    }
                    finally
                    {
                        trans.Commit();
                    }
                }
            }
        }

        public virtual TVm Update(long id, TVm item)
        {
            lock (Db)
            {
                if (id != item.Id) throw new Exception("Provided ID doesn't match object ID");
                using (var trans = Db.BeginTransaction())
                {
                    try
                    {
                        var original = Set(Db).First(x => x.Id == id);
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.BeforeUpdate, id, item, null));
                        UpdateModelFromVm(item, original);
                        original.Updated = DateTime.UtcNow;
                        Db.SaveChanges();
                        var vmOut = ConvertToVm(original);
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterUpdate, vmOut.Id, item, vmOut));
                        return vmOut;
                    }
                    finally
                    {
                        trans.Commit();
                    }
                }
            }
        }

        public virtual bool Delete(long id)
        {
            lock (Db)
            {
                using (var trans = Db.BeginTransaction())
                {
                    try
                    {
                        var original = Set(Db).FirstOrDefault(x => x.Id == id);
                        if (original == null) return false;
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.BeforeDelete, id, null, null));
                        Set(Db).Remove(original);
                        Db.SaveChanges();
                        var vmOut = ConvertToVm(original);
                        InvokeOperation(new RepoCrudEventArgs<TVm>(Db, RepoOperation.AfterDelete, id, vmOut, null));
                        return true;
                    }
                    finally
                    {
                        trans.Commit();
                    }
                }
            }
        }

        protected virtual IQueryable<TModel> AddInclude(DbSet<TModel> dataSet)
        {
            return dataSet;
        }

        protected virtual TVm ConvertToVm(TModel model)
        {
            var o = Mapper.Map<TModel, TVm>(model);
            return o;
        }

        protected virtual TModel ConvertToModel(TVm vm)
        {
            var o = Mapper.Map<TVm, TModel>(vm);
            return o;
        }

        protected virtual void UpdateModelFromVm(TVm vm, TModel model)
        {
            Mapper.Map(vm, model);
        }

        protected virtual void UpdateVmFromModel(TModel model, TVm vm)
        {
            Mapper.Map(model, vm);
        }

    }
}
