using System.Data;

namespace BankORM.Interfaces
{
	public interface IDataAccessLayer<TEntity, TKey>
		where TEntity : IEntity<TKey>
		where TKey : new()
	{
		DataTable Get(TKey key);

		DataTable Get();

		DataTable GetByFilter(TEntity filter);

		void Delete(TKey key);

		TKey Create(TEntity entity);

		void Update(TEntity entity);
	}
}
