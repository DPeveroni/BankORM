using BankORM.OM;
using System;

namespace BankORM.Interfaces
{
	public interface ILoggingService
	{
		void WriteTrace(string message, params object[] args);

		void WriteInformation(string message, params object[] args);

		void WriteWarning(string message, params object[] args);

		void WriteError(string message, params object[] args);

		void WriteException(Exception exc, string message, params object[] args);

		void WriteException(Exception exc, params object[] args);

		string GetExceptionMessage(string className, string method, Exception exc, ArgumentNameValueCollection args = null);

		string GetMessage(string className, string method, ArgumentNameValueCollection args = null);

		void WriteMethodStart(string className, string method, ArgumentNameValueCollection args);

		void WriteMethodEnd(string className, string method, ArgumentNameValueCollection args, TimeSpan elapsed);
	}
}
