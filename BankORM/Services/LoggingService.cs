using BankORM.Interfaces;
using BankORM.OM;
using System;
using System.Linq;

namespace BankORM.Services
{
	public class LoggingService : ILoggingService
	{
		public void WriteError(string message, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteException(Exception exc, string message, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteException(Exception exc, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteInformation(string message, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteTrace(string message, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteWarning(string message, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void WriteMethodStart(string className, string method, ArgumentNameValueCollection args)
		{
			WriteTrace($"{GetMessage(className, method, args)} - START");
		}

		public void WriteMethodEnd(string className, string method, ArgumentNameValueCollection args, TimeSpan elapsed)
		{
			WriteTrace($"{GetMessage(className, method, args)} - END - Execution Time:{elapsed}");
		}

		public string GetExceptionMessage(string className, string method, Exception exc, ArgumentNameValueCollection args = null)
		{
			return $"{GetMessageHead(className, method)} - {nameof(exc.Message)}:{exc.Message}{GetArgumentString(args)}";
		}

		public string GetMessage(string className, string method, ArgumentNameValueCollection args = null)
		{
			return $"{GetMessageHead(className, method)}{GetArgumentString(args)}";
		}

		private static string GetMessageHead(string className, string method)
		{
			return $"[{className}] {method}";
		}

		private static string GetArgumentString(ArgumentNameValueCollection args)
		{
			string argumentsString = string.Empty;

			if (args != null)
			{
				var argsCollection = args.Select(kvp => $"{kvp.Key}:{kvp.Value}");
				argumentsString = $" - ({string.Join(", ", argsCollection)})";
			}

			return argumentsString;
		}
	}
}
