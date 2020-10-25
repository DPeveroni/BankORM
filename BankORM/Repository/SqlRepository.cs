using BankORM.DAL;
using BankORM.Interfaces;
using BankORM.OM;
using System.Collections;

namespace BankORM.Repository
{
	public abstract class SqlRepository<TSqlDAL, TParameterCollection, TEntity, TKey> : BaseRepository<TSqlDAL, TEntity, TKey>
		where TSqlDAL : SqlDataAccessLayer<TParameterCollection, TEntity, TKey>
		where TParameterCollection : IEnumerable
		where TEntity : BaseOM<TKey>
		where TKey : new()
	{
		public SqlRepository(ILoggingService loggingService, TSqlDAL sqlDataAccessLayer, SqlMapper<TEntity> sqlMapper)
		: base(loggingService, sqlDataAccessLayer, sqlMapper)
		{
		}
	}
}
