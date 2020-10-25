using BankORM.Interfaces;
using System.Data;

namespace BankORM.OM
{
	public abstract class SqlMapper<TEntity> : IMapper<TEntity, DataRow>
	{
		public abstract DataRow Map(TEntity input);

		public abstract TEntity Map(DataRow input);
	}
}
