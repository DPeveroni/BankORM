using BankORM.Interfaces;

namespace BankORM.OM
{
	public abstract class BaseOM<TKey> : IEntity<TKey>
		where TKey : new()
	{
		public TKey Key { get; set; }

		public override string ToString()
		{
			if (Key == null)
				return base.ToString();
			return $"{GetType().Name} - {nameof(Key)}:{Key}";
		}

		public bool EqualsByKey(object obj)
		{
			if (obj is BaseOM<TKey> other
					&& other != null)
			{
				// we consider equals two object with the same key
				// if the key is null we cannot use Equals method,
				// so we need to do an explicit check on null value.
				return other.Key == null && Key == null
					|| Key.Equals(other.Key);
			}
			return false;
		}
	}
}
