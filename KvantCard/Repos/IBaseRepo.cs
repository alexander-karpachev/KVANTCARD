using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantCard.Model;

namespace KvantCard.Repos
{
    public enum RepoOperation
    {
        BeforeCreate,
        AfterCreate,
        BeforeDelete,
        AfterDelete,
        BeforeUpdate,
        AfterUpdate,
        BeforeFlush,
        AfterFlush,
        BeforeInit,
        AfterInit
    }

    public class RepoCrudEventArgs<TVm> : EventArgs where TVm : class, IIdModel
    {
        public RepoCrudEventArgs(Db db, RepoOperation operation, long id, TVm input, TVm saved)
        {
            Db = db;
            Operation = operation;
            Id = id;
            Input = input;
            Saved = saved;
        }

        public Db Db { get; private set; }

        public RepoOperation Operation { get; private set; }

        public long Id { get; private set; }
        public TVm Input { get; private set; }
        public TVm Saved { get; private set; }
    }

    public interface IBaseRepo<out TModel, TVm> where TModel : IIdModel where TVm : class, IIdModel
    {
        event EventHandler<RepoCrudEventArgs<TVm>> Operation;

        IList<TVm> GetAll();

        IList<TVm> GetAllByModel(Func<TModel, bool> match);

        IList<TVm> GetAllByVm(Func<TVm, bool> match);

        TVm GetById(long id);

        TVm FindByModel(Func<TModel, bool> match);

        TVm FindByVm(Func<TVm, bool> match);

        IList<TVm> UpdateOrCreate(IList<TVm> items, bool updateSources);

        TVm UpdateOrCreate(long id, TVm item);

        TVm Create(TVm item);

        TVm Update(long id, TVm item);

        bool Delete(long id);
    }
}
