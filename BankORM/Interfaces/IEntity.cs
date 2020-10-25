namespace BankORM.Interfaces
{
	public interface IEntity<TKey>
		where TKey : new()
	{
		TKey Key { get; set; }

		bool EqualsByKey(object obj);
	}
}
