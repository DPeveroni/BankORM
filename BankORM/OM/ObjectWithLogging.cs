using BankORM.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BankORM.OM
{
	public abstract class ObjectWithLogging
	{
		protected ILoggingService LoggingService { get; }

		public ObjectWithLogging(ILoggingService loggingService)
		{
			LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
		}

		protected void TryCatch(Action methodFunc, Func<ArgumentNameValueCollection> getArgs, object[] args, [CallerMemberName] string callerMemberName = "")
		{
			TryCatch(
						() =>
						{
							methodFunc();
							return true;
						},
						getArgs,
						args,
						callerMemberName);
		}

		protected T TryCatch<T>(Func<T> methodFunc, Func<ArgumentNameValueCollection> getArgs, object[] args, [CallerMemberName] string callerMemberName = "")
		{
			Stopwatch sw = new Stopwatch();
			try
			{
				LoggingService.WriteMethodStart(GetType().Name, callerMemberName, getArgs());

				sw.Start();

				return methodFunc();
			}
			catch (Exception exc)
			{
				sw.Stop();
				var message = LoggingService.GetExceptionMessage(GetType().Name, callerMemberName, exc, getArgs());
				LoggingService.WriteException(exc, message, args);

				throw;
			}
			finally
			{
				if (sw.IsRunning)
					sw.Stop();
				LoggingService.WriteMethodEnd(GetType().Name, callerMemberName, getArgs(), sw.Elapsed);
			}
		}
	}
}