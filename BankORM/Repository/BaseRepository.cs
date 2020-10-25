using BankORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BankORM.Repository
{
	public abstract class BaseRepository<TDAL, TEntity, TKey> : IRepository<TDAL, DataRow, TEntity, TKey>
		where TDAL : IDataAccessLayer<TEntity, TKey>
		where TEntity : IEntity<TKey>
		where TKey : new()
	{
		public ILoggingService LoggingService { get; }

		public TDAL DataAccessLayer { get; }

		public IMapper<TEntity, DataRow> Mapper { get; }

		public BaseRepository(ILoggingService loggingService, TDAL dataAccessLayer, IMapper<TEntity, DataRow> mapper)
		{
			if (dataAccessLayer == null)
				throw new ArgumentNullException(nameof(dataAccessLayer));

			this.LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
			this.DataAccessLayer = dataAccessLayer;
			this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public IEnumerable<TEntity> Get()
		{
			return DataAccessLayer
									.Get()
									.Rows
									.Cast<DataRow>()
									.Select(dr => this.Mapper.Map(dr));
		}

		public TEntity Get(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			return DataAccessLayer
									.Get(key)
									.Rows
									.Cast<DataRow>()
									.Select(dr => this.Mapper.Map(dr))
									.Single();
		}

		// and
		public IEnumerable<TEntity> GetByFilter(TEntity filter)
		{
			if (filter == null)
				throw new ArgumentNullException(nameof(filter));

			return DataAccessLayer
									.GetByFilter(filter)
									.Rows
									.Cast<DataRow>()
									.Select(dr => this.Mapper.Map(dr));
		}

		public IEnumerable<TEntity> GetByFilter(IEnumerable<TEntity> orConditions)
		{
			if (orConditions == null)
				throw new ArgumentNullException(nameof(orConditions));

			var results = new List<TEntity>();
			foreach (var condition in orConditions)
			{
				var conditionResult = GetByFilter(condition);
				foreach (var item in conditionResult)
					if (!results.ContainsKey(item.Key))
						results.Add(item);
			}
			return results;
		}

		public void Delete(TKey key)
		{
			var item = Get(key);
			if (item == null)
				throw new KeyNotFoundException();

			DataAccessLayer.Delete(key);
		}

		public TKey Create(TEntity entity)
		{
			if (entity == null)
				throw new Exception("Input entity cannot be null.");

			return DataAccessLayer.Create(entity);
		}

		public void Update(TEntity entity)
		{
			var item = Get(entity.Key);
			if (item == null)
				throw new KeyNotFoundException();

			DataAccessLayer.Update(entity);
		}


		// this method is marked as virtual in order to let a developer implement
		// a more efficent massive update procedure
		public virtual void Update(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			foreach (var entity in entities)
				Update(entity);
		}
	}
}
