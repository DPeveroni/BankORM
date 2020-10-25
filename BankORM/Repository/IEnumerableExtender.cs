using BankORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankORM.Repository
{
	internal static class IEnumerableExtender
	{
		internal static bool ContainsKey<T, K>(this IEnumerable<T> collection, K key)
		where T : IEntity<K>
		where K : new()
		{
			if (key == null)
				throw new ArgumentException(nameof(key));

			return collection.Any(i => i.Key.Equals(key));
		}
	}
}
