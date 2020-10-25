using System.Collections.Generic;

namespace BankORM.Interfaces
{
	public interface IRepository<TDAL, TStoreEntity, TEntity, TKey>
		where TDAL : IDataAccessLayer<TEntity, TKey>
		where TEntity : IEntity<TKey>
		where TKey : new()
	{
		TDAL DataAccessLayer { get; }

		IMapper<TEntity, TStoreEntity> Mapper { get; }

		IEnumerable<TEntity> Get();

		TEntity Get(TKey key);

		IEnumerable<TEntity> GetByFilter(TEntity filter);

		IEnumerable<TEntity> GetByFilter(IEnumerable<TEntity> orConditions);

		void Delete(TKey key);

		TKey Create(TEntity entity);

		void Update(TEntity entity);

		void Update(IEnumerable<TEntity> entities);
	}
}
