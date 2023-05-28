using System;

namespace HtmlAgilityPack
{
	internal sealed class Trace
	{
		internal static Trace Current
		{
			get
			{
				if (Trace._current == null)
				{
					Trace._current = new Trace();
				}
				return Trace._current;
			}
		}

		private void WriteLineIntern(string message, string category)
		{
		}

		public static void WriteLine(string message, string category)
		{
			Trace.Current.WriteLineIntern(message, category);
		}

		internal static Trace _current;
	}
}
