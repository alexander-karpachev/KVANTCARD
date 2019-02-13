using System;
using System.Collections.Generic;
using System.Text;
using KvantShared.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace KvantShared.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceScope _scope;
        private Db _db;
        private IDbContextTransaction _trans;

        public UnitOfWork(IServiceScope scope)
        {
            _scope = scope;
            _db = _scope.ServiceProvider.GetRequiredService<Db>();
            _trans = _db.BeginTransaction();
            DefaultCommit = true;
        }

        public bool DefaultCommit { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _trans.Commit();
        }

        public void Rollback()
        {
            _trans.Rollback();
        }

        public IGenericRepo<T> Repo<T>() where T : class, IIdModel
        {
            return _scope.ServiceProvider.GetRequiredService<IGenericRepo<T>>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)  
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (DefaultCommit)
                Commit();
            else
                Rollback();
            _trans.Dispose();
            _db.Dispose();
            _scope.Dispose();
        }

    }
}
