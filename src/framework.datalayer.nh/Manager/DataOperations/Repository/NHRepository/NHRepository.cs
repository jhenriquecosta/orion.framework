using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zeus.Domains;

namespace Zeus.NHibernate.Repository
{
    public  class NHRepository<TEntity> : INHRepository<TEntity> where TEntity : IAggregateRoot
    {
        private readonly ISession _session;


        protected internal NHRepository(ISession _session)
        {
            this._session = _session;
        }
        public TEntity Save(TEntity entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(entity);
                    _session.Flush();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            return entity;

        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    _session.Flush();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            return entity;

        }

        public TEntity Update(TEntity entity)
        {

            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(entity);
                    _session.Flush();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            return entity;
        }

        public void Delete(TEntity entity)
        {

            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(entity);
                    _session.Flush();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        public TEntity FirstOrDefault(object id)
        {
            return _session.Get<TEntity>(id);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {

            return _session.Query<TEntity>().FirstOrDefault(expression);

        }

        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null, int limit = 0)
        {
            var qry = _session.Query<TEntity>();
            if (predicate != null)
            {
                qry = qry.Where(predicate);
            }
            if (limit > 0) qry = qry.Take(limit);
            return qry.ToList();

        }

        public IList<T> HQLQuery<T>(string hqlQuery)
        {
            return _session.CreateQuery(hqlQuery).List<T>();
        }
        public dynamic SQLQuery(string sqlQuery)
        {
            return _session.CreateSQLQuery(sqlQuery).DynamicList();
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            await _session.SaveAsync(entity);
            return entity;

        }


        public async Task<TEntity> SaveOrUpdateAsync(TEntity entity)
        {
            await _session.SaveOrUpdateAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _session.UpdateAsync(entity);
            return entity;

        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            await _session.DeleteAsync(entity);
            return true;

        }


    }
}
