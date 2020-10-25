using BankORM.Interfaces;
using BankORM.OM;
using System.Data;

namespace BankORM.DAL
{
	public abstract class BaseDataAccessLayer<TEntity, TKey> : ObjectWithLogging, IDataAccessLayer<TEntity, TKey>
		where TEntity : IEntity<TKey>
		where TKey : new()
	{
		public BaseDataAccessLayer(ILoggingService loggingService)
		: base(loggingService)
		{
		}

		public DataTable Get(TKey key)
		{
			return this.TryCatch(
										() => this.BaseGet(key),
										() =>
											{
												var arg = new ArgumentNameValue(nameof(key), key);
												return new ArgumentNameValueCollection { arg };
											},
										new object[] { key });
		}

		public virtual DataTable Get()
		{
			return this.TryCatch(
									() => this.BaseGet(),
									null,
									new object[0]);
		}

		public DataTable GetByFilter(TEntity filter)
		{
			return this.TryCatch(
										() => this.BaseGetByFilter(filter),
										() =>
											{
												var arg = new ArgumentNameValue(nameof(filter), filter);
												return new ArgumentNameValueCollection { arg };
											},
										new object[] { filter });
		}

		public void Delete(TKey key)
		{
			this.TryCatch(
						() => this.BaseDelete(key),
						() =>
						{
							var arg = new ArgumentNameValue(nameof(key), key);
							return new ArgumentNameValueCollection { arg };
						},
						new object[] { key });
		}

		public TKey Create(TEntity entity)
		{
			return this.TryCatch(
						() => this.BaseCreate(entity),
						() =>
						{
							var arg = new ArgumentNameValue(nameof(entity), entity);
							return new ArgumentNameValueCollection { arg };
						},
						new object[] { entity });
		}

		public void Update(TEntity entity)
		{
			this.TryCatch(
						() => this.BaseUpdate(entity),
						() =>
						{
							var arg = new ArgumentNameValue(nameof(entity), entity);
							return new ArgumentNameValueCollection { arg };
						},
						new object[] { entity });
		}

		protected abstract TKey BaseCreate(TEntity entity);

		protected abstract void BaseDelete(TKey key);

		protected abstract DataTable BaseGetByFilter(TEntity filter);

		protected abstract void BaseUpdate(TEntity entity);

		protected abstract DataTable BaseGet(TKey key);

		protected abstract DataTable BaseGet();
	}
}
