﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Dapper;

using DapperExtensions;

using Orion.Framework.DataLayer.Dapper.Extensions;
using Orion.Framework.DataLayer.Dapper.Filters.Action;
using Orion.Framework.DataLayer.Dapper.Filters.Query;
using Orion.Framework.DataLayer.Dapper.TableValueParameters;
using Orion.Framework.Domains;
using Orion.Framework.Exceptions;

namespace Orion.Framework.DataLayer.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : AbstractDapperRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;

            DapperQueryFilterExecuter = NullDapperQueryFilterExecuter.Instance;
            DapperActionFilterExecuter = NullDapperActionFilterExecuter.Instance;
        }

        public IDapperQueryFilterExecuter DapperQueryFilterExecuter { get; set; }

        public IDapperActionFilterExecuter DapperActionFilterExecuter { get; set; }

        public virtual DbConnection Connection => (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs.Empty);

        /// <summary>
        ///     Gets the active transaction. If Dapper is active then <see cref="IUnitOfWork" /> should be started before
        ///     and there must be an active transaction.
        /// </summary>
        /// <value>
        ///     The active transaction.
        /// </value>
        public virtual DbTransaction ActiveTransaction => (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs.Empty);

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).Single();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            if (item == null) { throw new EntityNotFoundException(typeof(TEntity), id); }

            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            PredicateGroup predicateGroup = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>();
            return Connection.GetList<TEntity>(predicateGroup, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, ActiveTransaction);
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return Connection.Query<TAny>(query, parameters, ActiveTransaction);
        }

        public override IEnumerable<TAny> Query<TAny>(string query, TableValueParameter tableValueParameter, CommandType commandType)
        {
            return Connection.Query<TAny>(query, tableValueParameter, ActiveTransaction, commandType: commandType);
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, TableValueParameter tableValueParameter, CommandType commandType)
        {
            return Connection.QueryAsync<TAny>(query, tableValueParameter, ActiveTransaction, commandType: commandType);
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Connection.QueryAsync<TEntity>(query, parameters, ActiveTransaction);
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Connection.QueryAsync<TAny>(query, parameters, ActiveTransaction);
        }

        public override int Execute(string query, TableValueParameter tableValueParameter, CommandType commandType)
        {
            return Connection.Execute(query, tableValueParameter, ActiveTransaction, commandType: commandType);
        }

        public override int Execute(string query, object parameters = null)
        {
            return Connection.Execute(query, parameters, ActiveTransaction);
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Connection.ExecuteAsync(query, parameters, ActiveTransaction);
        }

        public override Task<int> ExecuteAsync(string query, TableValueParameter tableValueParameter, CommandType commandType)
        {
            return Connection.ExecuteAsync(query, tableValueParameter, ActiveTransaction, commandType: commandType);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);

            return Connection.GetPage<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                pageNumber,
                itemsPerPage,
                ActiveTransaction);
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.Count<TEntity>(filteredPredicate, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                firstResult,
                maxResults,
                ActiveTransaction
            );
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(filteredPredicate, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetPage<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), pageNumber, itemsPerPage, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), firstResult, maxResults, ActiveTransaction);
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            DapperActionFilterExecuter.ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(entity);
            Connection.Update(entity, ActiveTransaction);
        }

        public override void Delete(TEntity entity)
        {
            if (entity is ISoftDelete)
            {
                DapperActionFilterExecuter.ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(entity);
                Connection.Update(entity, ActiveTransaction);
            }
            else
            {
                Connection.Delete(entity, ActiveTransaction);
            }
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = GetAll(predicate);
            foreach (TEntity entity in items)
            {
                Delete(entity);
            }
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            DapperActionFilterExecuter.ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(entity);
            TPrimaryKey primaryKey = Connection.Insert(entity, ActiveTransaction);
            return primaryKey;
        }
    }
}
