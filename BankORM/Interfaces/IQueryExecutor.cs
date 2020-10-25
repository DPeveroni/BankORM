using System.Collections;
using System.Data;

namespace BankORM.Interfaces
{
	public interface IQueryExecutor<TParameterCollection>
	 where TParameterCollection : IEnumerable
	{
		DataTable ExecQuery(string connectionString, string procedureName, TParameterCollection parameters, int retries);

		void ExecNonQuery(string connectionString, string procedureName, TParameterCollection parameters, int retries);
	}
}
