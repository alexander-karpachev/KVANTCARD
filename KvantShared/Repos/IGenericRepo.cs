using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KvantShared.Model;

namespace KvantShared.Repos
{
    public interface IGenericRepo<T> where T : class, IIdModel
    {
        IGenericRepo<T> AsTracking(bool tracking);

        IGenericRepo<T> Include(params Expression<Func<T, object>>[] include);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> match);

        T GetById(int id);

        T Find(Expression<Func<T, bool>> match);

        IList<T> UpdateOrCreate(IList<T> items, params Expression<Func<T, object>>[] excludeFromUpdate);

        T UpdateOrCreate(T item, params Expression<Func<T, object>>[] excludeFromUpdate);

        T Create(T item);

        T Update(T item, params Expression<Func<T, object>>[] excludeFromUpdate);

        void UpdateSpecificFields(T item, params Expression<Func<T, object>>[] fields);

        bool Delete(int id);

        bool Delete(T item);

        IList<T> Delete(IList<T> items);

        IList<T> Delete(Expression<Func<T, bool>> match);

    }
}
