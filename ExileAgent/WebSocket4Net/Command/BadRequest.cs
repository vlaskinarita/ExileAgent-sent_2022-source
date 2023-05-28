using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocket4Net.Command
{
	public sealed class BadRequest : WebSocketCommandBase
	{
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			Dictionary<string, object> valueContainer = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			string empty = string.Empty;
			commandInfo.Text.ParseMimeHeader(valueContainer, out empty);
			string value = valueContainer.GetValue(BadRequest.getString_0(107142758), string.Empty);
			if (!session.NotSpecifiedVersion)
			{
				if (string.IsNullOrEmpty(value))
				{
					session.FireError(new Exception(BadRequest.getString_0(107142729)));
				}
				else
				{
					session.FireError(new Exception(string.Format(BadRequest.getString_0(107142588), value)));
				}
				session.CloseWithoutHandshake();
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				session.FireError(new Exception(BadRequest.getString_0(107141983)));
				session.CloseWithoutHandshake();
				return;
			}
			string[] array = value.Split(BadRequest.m_ValueSeparator, StringSplitOptions.RemoveEmptyEntries);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				if (!int.TryParse(array[i], out num))
				{
					session.FireError(new Exception(BadRequest.getString_0(107141906)));
					session.CloseWithoutHandshake();
					return;
				}
				array2[i] = num;
			}
			if (!session.GetAvailableProcessor(array2))
			{
				session.FireError(new Exception(BadRequest.getString_0(107141983)));
				session.CloseWithoutHandshake();
				return;
			}
			session.ProtocolProcessor.SendHandshake(session);
		}

		public override string Name
		{
			get
			{
				return 400.ToString();
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static BadRequest()
		{
			Strings.CreateGetStringDelegate(typeof(BadRequest));
			BadRequest.m_ValueSeparator = new string[]
			{
				BadRequest.getString_0(107405166)
			};
		}

		private const string m_WebSocketVersion = "Sec-WebSocket-Version";

		private static readonly string[] m_ValueSeparator;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
