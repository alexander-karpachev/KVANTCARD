using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KvantShared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KvantShared.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, IIdModel
    {
        private readonly Db _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(Db db, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GenericRepo<T>>();
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbSet = _db.Set<T>();
            _tracking = false;
            _include = null;
        }

        private GenericRepo(GenericRepo<T> other)
        {
            _logger = other._logger;
            _db = other._db;
            _dbSet = other._dbSet;
            _tracking = other._tracking;
            _include = other._include;
        }

        private bool _tracking;
        private Expression<Func<T, object>>[] _include;
        private readonly ILogger<GenericRepo<T>> _logger;

        public IGenericRepo<T> AsTracking(bool tracking)
        {
            var ret = new GenericRepo<T>(this) { _tracking = tracking };
            return ret;
        }

        public IGenericRepo<T> Include(params Expression<Func<T, object>>[] include)
        {
            var ret = new GenericRepo<T>(this) { _include = include };
            return ret;
        }

        public IEnumerable<T> GetAll()
        {
            return RepoExtensions.Include(_dbSet, _include).AsTracking(_tracking).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> match)
        {
            return RepoExtensions.Include(_dbSet, _include).AsTracking(_tracking).Where(match).ToList();
        }

        public T GetById(int id)
        {
            return RepoExtensions.Include(_dbSet, _include).AsTracking(_tracking).FirstOrDefault(e => e.Id == id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return RepoExtensions.Include(_dbSet, _include).AsTracking(_tracking).FirstOrDefault(match);
        }

        public IList<T> UpdateOrCreate(IList<T> items, params Expression<Func<T, object>>[] excludeFromUpdate)
        {
            foreach (var item in items)
            {
                if (item.Id == 0)
                {
                    // Create
                    item.Updated = DateTime.UtcNow;
                    item.Created = item.Updated;
                    _dbSet.Add(item);
                }
                else
                {
                    // Update
                    _db.Entry(item).State = EntityState.Modified;
                    foreach (var expression in excludeFromUpdate)
                        _db.Entry(item).Property(expression).IsModified = false;
                    item.Updated = DateTime.UtcNow;
                }
            }

            return items;
        }

        public T UpdateOrCreate(T item, params Expression<Func<T, object>>[] excludeFromUpdate)
        {
            return item.Id == 0 ? Create(item) : Update(item, excludeFromUpdate);
        }

        public void UpdateSpecificFields(T item, params Expression<Func<T, object>>[] fields)
        {
            _dbSet.Attach(item);
            foreach (var expression in fields)
                _db.Entry(item).Property(expression).IsModified = true;
        }

        public T Create(T item)
        {
            return _dbSet.Add(item).Entity;
        }

        public T Update(T item, params Expression<Func<T, object>>[] excludeFromUpdate)
        {
            _db.Entry(item).State = EntityState.Modified;
            foreach (var expression in excludeFromUpdate)
                _db.Entry(item).Property(expression).IsModified = false;
            return item;
        }

        public bool Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item == null) return false;
            _dbSet.Remove(item);
            return true;
        }

        public bool Delete(T item)
        {
            if (item.Id == 0) return false;
            _dbSet.Remove(item);
            return true;
        }

        public IList<T> Delete(IList<T> items)
        {
            var outList = items.Where(e => e.Id != 0).ToList();
            _dbSet.RemoveRange(outList);
            return outList;
        }

        public IList<T> Delete(Expression<Func<T, bool>> match)
        {
            var outList = _dbSet.Where(match).ToList();
            _dbSet.RemoveRange(outList);
            return outList;
        }

    }
}