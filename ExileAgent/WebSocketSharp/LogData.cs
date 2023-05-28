using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp
{
	public sealed class LogData
	{
		internal LogData(LogLevel level, StackFrame caller, string message)
		{
			this._level = level;
			this._caller = caller;
			this._message = (message ?? string.Empty);
			this._date = DateTime.Now;
		}

		public StackFrame Caller
		{
			get
			{
				return this._caller;
			}
		}

		public DateTime Date
		{
			get
			{
				return this._date;
			}
		}

		public LogLevel Level
		{
			get
			{
				return this._level;
			}
		}

		public string Message
		{
			get
			{
				return this._message;
			}
		}

		public override string ToString()
		{
			string text = string.Format(LogData.getString_0(107135219), this._date, this._level);
			MethodBase method = this._caller.GetMethod();
			Type declaringType = method.DeclaringType;
			int fileLineNumber = this._caller.GetFileLineNumber();
			string arg = string.Format(LogData.getString_0(107135234), new object[]
			{
				text,
				declaringType.Name,
				method.Name,
				fileLineNumber
			});
			string[] array = this._message.Replace(LogData.getString_0(107248446), LogData.getString_0(107404097)).TrimEnd(new char[]
			{
				'\n'
			}).Split(new char[]
			{
				'\n'
			});
			string result;
			if (array.Length <= 1)
			{
				result = string.Format(LogData.getString_0(107448537), arg, this._message);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(string.Format(LogData.getString_0(107135181), arg, array[0]), 64);
				string format = string.Format(LogData.getString_0(107135200), text.Length);
				for (int i = 1; i < array.Length; i++)
				{
					stringBuilder.AppendFormat(format, LogData.getString_0(107405303), array[i]);
				}
				StringBuilder stringBuilder2 = stringBuilder;
				int length = stringBuilder2.Length;
				stringBuilder2.Length = length - 1;
				result = stringBuilder.ToString();
			}
			return result;
		}

		static LogData()
		{
			Strings.CreateGetStringDelegate(typeof(LogData));
		}

		private StackFrame _caller;

		private DateTime _date;

		private LogLevel _level;

		private string _message;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
