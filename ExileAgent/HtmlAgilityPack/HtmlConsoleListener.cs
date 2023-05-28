using System;
using System.Diagnostics;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	internal sealed class HtmlConsoleListener : TraceListener
	{
		public override void Write(string Message)
		{
			this.Write(Message, HtmlConsoleListener.getString_0(107399760));
		}

		public override void Write(string Message, string Category)
		{
			Console.Write(HtmlConsoleListener.getString_0(107245739) + Category + HtmlConsoleListener.getString_0(107443329) + Message);
		}

		public override void WriteLine(string Message)
		{
			this.Write(Message + HtmlConsoleListener.getString_0(107398554));
		}

		public override void WriteLine(string Message, string Category)
		{
			this.Write(Message + HtmlConsoleListener.getString_0(107398554), Category);
		}

		static HtmlConsoleListener()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlConsoleListener));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
