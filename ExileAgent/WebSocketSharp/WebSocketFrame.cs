using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp
{
	internal sealed class WebSocketFrame : IEnumerable, IEnumerable<byte>
	{
		private WebSocketFrame()
		{
		}

		internal WebSocketFrame(Opcode opcode, PayloadData payloadData, bool mask) : this(Fin.Final, opcode, payloadData, false, mask)
		{
		}

		internal WebSocketFrame(Fin fin, Opcode opcode, byte[] data, bool compressed, bool mask) : this(fin, opcode, new PayloadData(data), compressed, mask)
		{
		}

		internal WebSocketFrame(Fin fin, Opcode opcode, PayloadData payloadData, bool compressed, bool mask)
		{
			this._fin = fin;
			this._opcode = opcode;
			this._rsv1 = ((opcode.IsData() && compressed) ? Rsv.On : Rsv.Off);
			this._rsv2 = Rsv.Off;
			this._rsv3 = Rsv.Off;
			ulong length = payloadData.Length;
			if (length < 126UL)
			{
				this._payloadLength = (byte)length;
				this._extPayloadLength = WebSocket.EmptyBytes;
			}
			else if (length < 65536UL)
			{
				this._payloadLength = 126;
				this._extPayloadLength = ((ushort)length).InternalToByteArray(ByteOrder.Big);
			}
			else
			{
				this._payloadLength = 127;
				this._extPayloadLength = length.InternalToByteArray(ByteOrder.Big);
			}
			if (mask)
			{
				this._mask = Mask.On;
				this._maskingKey = WebSocketFrame.createMaskingKey();
				payloadData.Mask(this._maskingKey);
			}
			else
			{
				this._mask = Mask.Off;
				this._maskingKey = WebSocket.EmptyBytes;
			}
			this._payloadData = payloadData;
		}

		internal ulong ExactPayloadLength
		{
			get
			{
				return (this._payloadLength < 126) ? ((ulong)this._payloadLength) : ((this._payloadLength == 126) ? ((ulong)this._extPayloadLength.ToUInt16(ByteOrder.Big)) : this._extPayloadLength.ToUInt64(ByteOrder.Big));
			}
		}

		internal int ExtendedPayloadLengthWidth
		{
			get
			{
				return (this._payloadLength < 126) ? 0 : ((this._payloadLength == 126) ? 2 : 8);
			}
		}

		public byte[] ExtendedPayloadLength
		{
			get
			{
				return this._extPayloadLength;
			}
		}

		public Fin Fin
		{
			get
			{
				return this._fin;
			}
		}

		public bool IsBinary
		{
			get
			{
				return this._opcode == Opcode.Binary;
			}
		}

		public bool IsClose
		{
			get
			{
				return this._opcode == Opcode.Close;
			}
		}

		public bool IsCompressed
		{
			get
			{
				return this._rsv1 == Rsv.On;
			}
		}

		public bool IsContinuation
		{
			get
			{
				return this._opcode == Opcode.Cont;
			}
		}

		public bool IsControl
		{
			get
			{
				return this._opcode >= Opcode.Close;
			}
		}

		public bool IsData
		{
			get
			{
				return this._opcode == Opcode.Text || this._opcode == Opcode.Binary;
			}
		}

		public bool IsFinal
		{
			get
			{
				return this._fin == Fin.Final;
			}
		}

		public bool IsFragment
		{
			get
			{
				return this._fin == Fin.More || this._opcode == Opcode.Cont;
			}
		}

		public bool IsMasked
		{
			get
			{
				return this._mask == Mask.On;
			}
		}

		public bool IsPing
		{
			get
			{
				return this._opcode == Opcode.Ping;
			}
		}

		public bool IsPong
		{
			get
			{
				return this._opcode == Opcode.Pong;
			}
		}

		public bool IsText
		{
			get
			{
				return this._opcode == Opcode.Text;
			}
		}

		public ulong Length
		{
			get
			{
				return (ulong)(2L + (long)(this._extPayloadLength.Length + this._maskingKey.Length) + (long)this._payloadData.Length);
			}
		}

		public Mask Mask
		{
			get
			{
				return this._mask;
			}
		}

		public byte[] MaskingKey
		{
			get
			{
				return this._maskingKey;
			}
		}

		public Opcode Opcode
		{
			get
			{
				return this._opcode;
			}
		}

		public PayloadData PayloadData
		{
			get
			{
				return this._payloadData;
			}
		}

		public byte PayloadLength
		{
			get
			{
				return this._payloadLength;
			}
		}

		public Rsv Rsv1
		{
			get
			{
				return this._rsv1;
			}
		}

		public Rsv Rsv2
		{
			get
			{
				return this._rsv2;
			}
		}

		public Rsv Rsv3
		{
			get
			{
				return this._rsv3;
			}
		}

		private static byte[] createMaskingKey()
		{
			byte[] array = new byte[4];
			WebSocket.RandomNumber.GetBytes(array);
			return array;
		}

		private static string dump(WebSocketFrame frame)
		{
			ulong length = frame.Length;
			long num = (long)(length / 4UL);
			int num2 = (int)(length % 4UL);
			int num3;
			string arg;
			if (num < 10000L)
			{
				num3 = 4;
				arg = WebSocketFrame.getString_0(107134488);
			}
			else if (num < 65536L)
			{
				num3 = 4;
				arg = WebSocketFrame.getString_0(107134447);
			}
			else if (num < 4294967296L)
			{
				num3 = 8;
				arg = WebSocketFrame.getString_0(107134466);
			}
			else
			{
				num3 = 16;
				arg = WebSocketFrame.getString_0(107134421);
			}
			string arg2 = string.Format(WebSocketFrame.getString_0(107134408), num3);
			string format = string.Format(WebSocketFrame.getString_0(107134427), arg2);
			string lineFmt = string.Format(WebSocketFrame.getString_0(107134794), arg);
			string format2 = string.Format(WebSocketFrame.getString_0(107134741), arg2);
			StringBuilder buff = new StringBuilder(64);
			Func<Action<string, string, string, string>> func = delegate()
			{
				long lineCnt = 0L;
				return delegate(string arg1, string arg2, string arg3, string arg4)
				{
					StringBuilder buff = buff;
					string lineFmt = lineFmt;
					object[] array2 = new object[5];
					int num6 = 0;
					long num7 = lineCnt + 1L;
					lineCnt = num7;
					array2[num6] = num7;
					array2[1] = arg1;
					array2[2] = arg2;
					array2[3] = arg3;
					array2[4] = arg4;
					buff.AppendFormat(lineFmt, array2);
				};
			};
			Action<string, string, string, string> action = func();
			byte[] array = frame.ToArray();
			buff.AppendFormat(format, string.Empty);
			for (long num4 = 0L; num4 <= num; num4 += 1L)
			{
				long num5 = num4 * 4L;
				checked
				{
					if (num4 < num)
					{
						action(Convert.ToString(array[(int)((IntPtr)num5)], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 1L)))], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 2L)))], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 3L)))], 2).PadLeft(8, '0'));
					}
					else if (num2 > 0)
					{
						action(Convert.ToString(array[(int)((IntPtr)num5)], 2).PadLeft(8, '0'), (num2 >= 2) ? Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 1L)))], 2).PadLeft(8, '0') : string.Empty, (num2 == 3) ? Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 2L)))], 2).PadLeft(8, '0') : string.Empty, string.Empty);
					}
				}
			}
			buff.AppendFormat(format2, string.Empty);
			return buff.ToString();
		}

		private static string print(WebSocketFrame frame)
		{
			byte payloadLength = frame._payloadLength;
			string text = (payloadLength > 125) ? frame.ExactPayloadLength.ToString() : string.Empty;
			string text2 = BitConverter.ToString(frame._maskingKey);
			string text3 = (payloadLength == 0) ? string.Empty : ((payloadLength > 125) ? WebSocketFrame.getString_0(107134716) : ((!frame.IsText || frame.IsFragment || frame.IsMasked || frame.IsCompressed) ? frame._payloadData.ToString() : WebSocketFrame.utf8Decode(frame._payloadData.ApplicationData)));
			string format = WebSocketFrame.getString_0(107134711);
			return string.Format(format, new object[]
			{
				frame._fin,
				frame._rsv1,
				frame._rsv2,
				frame._rsv3,
				frame._opcode,
				frame._mask,
				payloadLength,
				text,
				text2,
				text3
			});
		}

		private static WebSocketFrame processHeader(byte[] header)
		{
			if (header.Length != 2)
			{
				string message = WebSocketFrame.getString_0(107134289);
				throw new WebSocketException(message);
			}
			Fin fin = ((header[0] & 128) == 128) ? Fin.Final : Fin.More;
			Rsv rsv = ((header[0] & 64) == 64) ? Rsv.On : Rsv.Off;
			Rsv rsv2 = ((header[0] & 32) == 32) ? Rsv.On : Rsv.Off;
			Rsv rsv3 = ((header[0] & 16) == 16) ? Rsv.On : Rsv.Off;
			byte opcode = header[0] & 15;
			Mask mask = ((header[1] & 128) == 128) ? Mask.On : Mask.Off;
			byte b = header[1] & 127;
			if (!opcode.IsSupported())
			{
				string message2 = WebSocketFrame.getString_0(107134228);
				throw new WebSocketException(CloseStatusCode.ProtocolError, message2);
			}
			if (!opcode.IsData() && rsv == Rsv.On)
			{
				string message3 = WebSocketFrame.getString_0(107134211);
				throw new WebSocketException(CloseStatusCode.ProtocolError, message3);
			}
			if (opcode.IsControl())
			{
				if (fin == Fin.More)
				{
					string message4 = WebSocketFrame.getString_0(107134134);
					throw new WebSocketException(CloseStatusCode.ProtocolError, message4);
				}
				if (b > 125)
				{
					string message5 = WebSocketFrame.getString_0(107134093);
					throw new WebSocketException(CloseStatusCode.ProtocolError, message5);
				}
			}
			return new WebSocketFrame
			{
				_fin = fin,
				_rsv1 = rsv,
				_rsv2 = rsv2,
				_rsv3 = rsv3,
				_opcode = (Opcode)opcode,
				_mask = mask,
				_payloadLength = b
			};
		}

		private static WebSocketFrame readExtendedPayloadLength(Stream stream, WebSocketFrame frame)
		{
			int extendedPayloadLengthWidth = frame.ExtendedPayloadLengthWidth;
			WebSocketFrame result;
			if (extendedPayloadLengthWidth == 0)
			{
				frame._extPayloadLength = WebSocket.EmptyBytes;
				result = frame;
			}
			else
			{
				byte[] array = stream.ReadBytes(extendedPayloadLengthWidth);
				if (array.Length != extendedPayloadLengthWidth)
				{
					string message = WebSocketFrame.getString_0(107133520);
					throw new WebSocketException(message);
				}
				frame._extPayloadLength = array;
				result = frame;
			}
			return result;
		}

		private static void readExtendedPayloadLengthAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			int len = frame.ExtendedPayloadLengthWidth;
			if (len == 0)
			{
				frame._extPayloadLength = WebSocket.EmptyBytes;
				completed(frame);
			}
			else
			{
				stream.ReadBytesAsync(len, delegate(byte[] bytes)
				{
					if (bytes.Length != len)
					{
						string message = WebSocketFrame.<>c__DisplayClass69_0.getString_0(107133528);
						throw new WebSocketException(message);
					}
					frame._extPayloadLength = bytes;
					completed(frame);
				}, error);
			}
		}

		private static WebSocketFrame readHeader(Stream stream)
		{
			return WebSocketFrame.processHeader(stream.ReadBytes(2));
		}

		private static void readHeaderAsync(Stream stream, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			stream.ReadBytesAsync(2, delegate(byte[] bytes)
			{
				completed(WebSocketFrame.processHeader(bytes));
			}, error);
		}

		private static WebSocketFrame readMaskingKey(Stream stream, WebSocketFrame frame)
		{
			WebSocketFrame result;
			if (!frame.IsMasked)
			{
				frame._maskingKey = WebSocket.EmptyBytes;
				result = frame;
			}
			else
			{
				byte[] array = stream.ReadBytes(4);
				if (array.Length != 4)
				{
					string message = WebSocketFrame.getString_0(107133475);
					throw new WebSocketException(message);
				}
				frame._maskingKey = array;
				result = frame;
			}
			return result;
		}

		private static void readMaskingKeyAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			if (!frame.IsMasked)
			{
				frame._maskingKey = WebSocket.EmptyBytes;
				completed(frame);
			}
			else
			{
				int len = 4;
				stream.ReadBytesAsync(len, delegate(byte[] bytes)
				{
					if (bytes.Length != len)
					{
						string message = WebSocketFrame.<>c__DisplayClass73_0.getString_0(107133488);
						throw new WebSocketException(message);
					}
					frame._maskingKey = bytes;
					completed(frame);
				}, error);
			}
		}

		private static WebSocketFrame readPayloadData(Stream stream, WebSocketFrame frame)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > PayloadData.MaxLength)
			{
				string message = WebSocketFrame.getString_0(107133414);
				throw new WebSocketException(CloseStatusCode.TooBig, message);
			}
			WebSocketFrame result;
			if (exactPayloadLength == 0UL)
			{
				frame._payloadData = PayloadData.Empty;
				result = frame;
			}
			else
			{
				long num = (long)exactPayloadLength;
				byte[] array = (frame._payloadLength < 127) ? stream.ReadBytes((int)exactPayloadLength) : stream.ReadBytes(num, 1024);
				if ((long)array.Length != num)
				{
					string message2 = WebSocketFrame.getString_0(107133333);
					throw new WebSocketException(message2);
				}
				frame._payloadData = new PayloadData(array, num);
				result = frame;
			}
			return result;
		}

		private static void readPayloadDataAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > PayloadData.MaxLength)
			{
				string message = WebSocketFrame.getString_0(107133414);
				throw new WebSocketException(CloseStatusCode.TooBig, message);
			}
			if (exactPayloadLength == 0UL)
			{
				frame._payloadData = PayloadData.Empty;
				completed(frame);
			}
			else
			{
				long len = (long)exactPayloadLength;
				Action<byte[]> completed2 = delegate(byte[] bytes)
				{
					if ((long)bytes.Length != len)
					{
						string message2 = WebSocketFrame.<>c__DisplayClass75_0.getString_0(107133350);
						throw new WebSocketException(message2);
					}
					frame._payloadData = new PayloadData(bytes, len);
					completed(frame);
				};
				if (frame._payloadLength < 127)
				{
					stream.ReadBytesAsync((int)exactPayloadLength, completed2, error);
				}
				else
				{
					stream.ReadBytesAsync(len, 1024, completed2, error);
				}
			}
		}

		private static string utf8Decode(byte[] bytes)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		internal static WebSocketFrame CreateCloseFrame(PayloadData payloadData, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Close, payloadData, false, mask);
		}

		internal static WebSocketFrame CreatePingFrame(bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, PayloadData.Empty, false, mask);
		}

		internal static WebSocketFrame CreatePingFrame(byte[] data, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, new PayloadData(data), false, mask);
		}

		internal static WebSocketFrame CreatePongFrame(PayloadData payloadData, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Pong, payloadData, false, mask);
		}

		internal static WebSocketFrame ReadFrame(Stream stream, bool unmask)
		{
			WebSocketFrame webSocketFrame = WebSocketFrame.readHeader(stream);
			WebSocketFrame.readExtendedPayloadLength(stream, webSocketFrame);
			WebSocketFrame.readMaskingKey(stream, webSocketFrame);
			WebSocketFrame.readPayloadData(stream, webSocketFrame);
			if (unmask)
			{
				webSocketFrame.Unmask();
			}
			return webSocketFrame;
		}

		internal static void ReadFrameAsync(Stream stream, bool unmask, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			Action<WebSocketFrame> <>9__3;
			Action<WebSocketFrame> <>9__2;
			Action<WebSocketFrame> <>9__1;
			WebSocketFrame.readHeaderAsync(stream, delegate(WebSocketFrame frame)
			{
				Stream stream2 = stream;
				Action<WebSocketFrame> completed2;
				if ((completed2 = <>9__1) == null)
				{
					completed2 = (<>9__1 = delegate(WebSocketFrame frame1)
					{
						Stream stream3 = stream;
						Action<WebSocketFrame> completed3;
						if ((completed3 = <>9__2) == null)
						{
							completed3 = (<>9__2 = delegate(WebSocketFrame frame2)
							{
								Stream stream4 = stream;
								Action<WebSocketFrame> completed4;
								if ((completed4 = <>9__3) == null)
								{
									completed4 = (<>9__3 = delegate(WebSocketFrame frame3)
									{
										if (unmask)
										{
											frame3.Unmask();
										}
										completed(frame3);
									});
								}
								WebSocketFrame.readPayloadDataAsync(stream4, frame2, completed4, error);
							});
						}
						WebSocketFrame.readMaskingKeyAsync(stream3, frame1, completed3, error);
					});
				}
				WebSocketFrame.readExtendedPayloadLengthAsync(stream2, frame, completed2, error);
			}, error);
		}

		internal void Unmask()
		{
			if (this._mask != Mask.Off)
			{
				this._mask = Mask.Off;
				this._payloadData.Mask(this._maskingKey);
				this._maskingKey = WebSocket.EmptyBytes;
			}
		}

		public IEnumerator<byte> GetEnumerator()
		{
			WebSocketFrame.<GetEnumerator>d__84 <GetEnumerator>d__ = new WebSocketFrame.<GetEnumerator>d__84(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		public void Print(bool dumped)
		{
			Console.WriteLine(dumped ? WebSocketFrame.dump(this) : WebSocketFrame.print(this));
		}

		public string PrintToString(bool dumped)
		{
			return dumped ? WebSocketFrame.dump(this) : WebSocketFrame.print(this);
		}

		public byte[] ToArray()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = (int)this._fin;
				num = (int)((byte)(num << 1) + this._rsv1);
				num = (int)((byte)(num << 1) + this._rsv2);
				num = (int)((byte)(num << 1) + this._rsv3);
				num = (int)((byte)(num << 4) + this._opcode);
				num = (int)((byte)(num << 1) + this._mask);
				num = (num << 7) + (int)this._payloadLength;
				memoryStream.Write(((ushort)num).InternalToByteArray(ByteOrder.Big), 0, 2);
				if (this._payloadLength > 125)
				{
					memoryStream.Write(this._extPayloadLength, 0, (this._payloadLength == 126) ? 2 : 8);
				}
				if (this._mask == Mask.On)
				{
					memoryStream.Write(this._maskingKey, 0, 4);
				}
				if (this._payloadLength > 0)
				{
					byte[] array = this._payloadData.ToArray();
					if (this._payloadLength < 127)
					{
						memoryStream.Write(array, 0, array.Length);
					}
					else
					{
						memoryStream.WriteBytes(array, 1024);
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		public override string ToString()
		{
			return BitConverter.ToString(this.ToArray());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		static WebSocketFrame()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocketFrame));
		}

		private byte[] _extPayloadLength;

		private Fin _fin;

		private Mask _mask;

		private byte[] _maskingKey;

		private Opcode _opcode;

		private PayloadData _payloadData;

		private byte _payloadLength;

		private Rsv _rsv1;

		private Rsv _rsv2;

		private Rsv _rsv3;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
