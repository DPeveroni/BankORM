namespace BankORM.OM
{
	public class ArgumentNameValue
	{
		public string Key { get; set; }

		public object Value { get; set; }

		public ArgumentNameValue(string key, object value)
		{
			Key = key;
			Value = value;
		}
	}
}