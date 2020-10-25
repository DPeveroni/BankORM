using BankORM.Interfaces;
using BankORM.OM;
using System;
using System.Collections;
using System.Data;

namespace BankORM.DAL
{
	public abstract class SqlDataAccessLayer<TParameterCollection, TEntity, TKey> : BaseDataAccessLayer<TEntity, TKey>
		where TParameterCollection : IEnumerable
		where TEntity : BaseOM<TKey>
		where TKey : new()
	{
		private string ConnectionString { get; }

		private IQueryExecutor<TParameterCollection> QueryExecutor { get; }

		public SqlDataAccessLayer(ILoggingService loggingService, IQueryExecutor<TParameterCollection> queryExecutor, string connectionString)
			: base(loggingService)
		{
			this.ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
			this.QueryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
		}

		protected DataTable ExecQuery(string procedureName, TParameterCollection parameters, int retries = 3)
		{
			return this.QueryExecutor.ExecQuery(this.ConnectionString, procedureName, parameters, retries);
		}

		protected void ExecNonQuery(string procedureName, TParameterCollection parameters, int retries = 3)
		{
			this.QueryExecutor.ExecNonQuery(this.ConnectionString, procedureName, parameters, retries);
		}
	}
}
