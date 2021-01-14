using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Intercept;
using NHibernate.Persister.Entity;
using NHibernate.Tuple;
using NHibernate.Proxy;
using System.Data.Common;
using Orion.Framework.Domains;
using NHibernate.Criterion;

namespace Orion.Framework
{
	public static partial class SessionExtensions
	{
		public static ISession OpenSessionWithConnection(this ISession session, DbConnection connection)
		{
			return session.SessionWithOptions().Connection(connection).OpenSession();
		}

		public static ISession OpenSessionWithConnection(this ISessionFactory sessionFactory, DbConnection connection)
		{
			return sessionFactory.OpenSession().OpenSessionWithConnection(connection);
		}
		public static TResult Transact<TResult>(this ISession session, Func<ISession, TResult> func)
		{
			if (!session.Transaction.IsActive)
			{
				// Wrap in transaction
				TResult result;
				using (ITransaction tx = session.BeginTransaction())
				{
					result = func.Invoke(session);
					tx.Commit();
				}
				return result;
			}

			// Don't wrap;
			return func.Invoke(session);
		}

		public static void Transact(this ISession session, Action<ISession> action)
		{
			Transact(session, ses =>
			{
				action.Invoke(ses);
				return false;
			});
		}

		public static TResult Transact<TResult>(this IStatelessSession session, Func<IStatelessSession, TResult> func)
		{
			if (!session.Transaction.IsActive)
			{
				// Wrap in transaction
				TResult result;
				using (ITransaction tx = session.BeginTransaction())
				{
					result = func.Invoke(session);
					tx.Commit();
				}
				return result;
			}

			// Don't wrap;
			return func.Invoke(session);
		}

		public static void Transact(this IStatelessSession session, Action<IStatelessSession> action)
		{
			Transact(session, ses =>
			{
				action.Invoke(ses);
				return false;
			});
		}

		public static Boolean IsDirtyEntity(this ISession session, Object entity)
		{
			ISessionImplementor sessionImpl = session.GetSessionImplementation();
			IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;
			EntityEntry oldEntry = persistenceContext.GetEntry(entity);

			if ((oldEntry == null) && (entity is INHibernateProxy))
			{
				INHibernateProxy proxy = entity as INHibernateProxy;
				Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
				oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
			}

			if (oldEntry == null)
			{
				return false;
			}

			string className = oldEntry.EntityName;
			IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);

			Object[] oldState = oldEntry.LoadedState;
			if (oldState == null)
			{
				return false;
			}

			Object[] currentState = persister.GetPropertyValues(entity);
			Int32[] dirtyProps = oldState.Select((o, i) => ValuesAreEqual(oldState[i], currentState[i]) ? -1 : i).Where(x => x >= 0).ToArray();

			return (dirtyProps != null && dirtyProps.Length > 0);
		}

		/// <summary>
		/// Gets paged results.
		/// </summary>
		/// <typeparam name="T">The type of entity to get.</typeparam>
		/// <param name="session">The session.</param>
		/// <param name="searchCriterion">The search criterion.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="order">The order.</param>
		/// <param name="totalCount">The total count.</param>
		/// <returns>The list of entities in the page.</returns>
		public static IList<T> GetPagedResults<T>(this ISession session, ICriterion searchCriterion, int pageIndex, int pageSize, Order order, out int totalCount)
			where T : IEntity
		{
			var rowCount = session.CreateCriteria(typeof(T))
			.Add(searchCriterion)
			.SetProjection(Projections.RowCount()).FutureValue<int>();

			var criteria = session.CreateCriteria(typeof(T))
				.Add(searchCriterion);

			if (order != null)
			{
				criteria
					.AddOrder(order);
			}

			var results = criteria
				.SetFirstResult(pageIndex * pageSize)
				.SetMaxResults(pageSize)
				.Future<T>();

			totalCount = rowCount.Value;
			return results.ToList();
		}

		/// <summary>
		/// Checks, if values the are equal.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="currentValue">The current value.</param>
		/// <returns><c>true</c>, if values are equal, else <c>false</c></returns>
		private static bool ValuesAreEqual(object oldValue, object currentValue)
		{
			// If property is not loaded, it has no changes
			if (!NHibernateUtil.IsInitialized(oldValue))
			{
				return true;
			}

			if (oldValue == null)
			{
				return currentValue == null;
			}
			return oldValue.Equals(currentValue);
		}
		public static T Attach<T>(this ISession session, T entity, LockMode mode = null)
		{
			mode = mode ?? LockMode.None;

			IEntityPersister persister = session.GetSessionImplementation().GetEntityPersister(NHibernateProxyHelper.GuessClass(entity).FullName, entity);
			Object[] fields = persister.GetPropertyValues(entity);
			Object id = persister.GetIdentifier(entity);
			Object version = persister.GetVersion(entity);
			EntityEntry entry = session.GetSessionImplementation().PersistenceContext.AddEntry(entity, Status.Loaded, fields, null, id, version, LockMode.None, true, persister, true, false);
			
			return (entity);
		}

		public static Dictionary<String, Object> GetDirtyProperties<T>(this ISession session, T entity)
		{
			ISessionImplementor sessionImpl = session.GetSessionImplementation();
			IPersistenceContext context = sessionImpl.PersistenceContext;
			EntityEntry entry = context.GetEntry(context.Unproxy(entity));

			if ((entry == null) || (entry.RequiresDirtyCheck(entity) == false) || (entry.ExistsInDatabase == false) || (entry.LoadedState == null))
			{
				return (null);
			}

			IEntityPersister persister = entry.Persister;
			String[] propertyNames = persister.PropertyNames;
			Object[] currentState = persister.GetPropertyValues(entity);
			Object[] loadedState = entry.LoadedState;
			IEnumerable<StandardProperty> dp = (persister.EntityMetamodel.Properties.Where((property, i) => (LazyPropertyInitializer.UnfetchedProperty.Equals(loadedState[i]) == false) && (property.Type.IsDirty(loadedState[i], currentState[i], sessionImpl) == true))).ToArray();

			return (dp.ToDictionary(x => x.Name, x => currentState[Array.IndexOf(propertyNames, x.Name)]));
		}

		public static IEnumerable<T> Local<T>(this ISession session, Status status = Status.Loaded)
		{
			ISessionImplementor impl = session.GetSessionImplementation();
			IPersistenceContext pc = impl.PersistenceContext;

			foreach (T key in pc.EntityEntries.Keys.OfType<T>())
			{
				EntityEntry entry = pc.EntityEntries[key] as EntityEntry;

				if (entry.Status == status)
				{
					yield return (key);
				}
			}
		}

		public static EntityEntry Entry<T>(this ISession session, T entity)
		{
			ISessionImplementor impl = session.GetSessionImplementation();
			IPersistenceContext pc = impl.PersistenceContext;			
			EntityEntry entry = pc.EntityEntries[entity] as EntityEntry;

			return (entry);
		}
	}
}
