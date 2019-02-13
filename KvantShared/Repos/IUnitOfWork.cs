using System;
using System.Collections.Generic;
using System.Text;
using KvantShared.Model;

namespace KvantShared.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        bool DefaultCommit { get; set; }

        void Save();

        void Commit();

        void Rollback();

        IGenericRepo<T> Repo<T>() where T : class, IIdModel;
    }
}
