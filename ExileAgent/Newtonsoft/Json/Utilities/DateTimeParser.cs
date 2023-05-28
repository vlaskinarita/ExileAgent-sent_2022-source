using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal struct DateTimeParser
	{
		static DateTimeParser()
		{
			Strings.CreateGetStringDelegate(typeof(DateTimeParser));
			DateTimeParser.Power10 = new int[]
			{
				-1,
				10,
				100,
				1000,
				10000,
				100000,
				1000000
			};
			DateTimeParser.Lzyyyy = DateTimeParser.getString_0(107345136).Length;
			DateTimeParser.Lzyyyy_ = DateTimeParser.getString_0(107345095).Length;
			DateTimeParser.Lzyyyy_MM = DateTimeParser.getString_0(107345086).Length;
			DateTimeParser.Lzyyyy_MM_ = DateTimeParser.getString_0(107345105).Length;
			DateTimeParser.Lzyyyy_MM_dd = DateTimeParser.getString_0(107345060).Length;
			DateTimeParser.Lzyyyy_MM_ddT = DateTimeParser.getString_0(107345075).Length;
			DateTimeParser.LzHH = DateTimeParser.getString_0(107345026).Length;
			DateTimeParser.LzHH_ = DateTimeParser.getString_0(107345021).Length;
			DateTimeParser.LzHH_mm = DateTimeParser.getString_0(107345048).Length;
			DateTimeParser.LzHH_mm_ = DateTimeParser.getString_0(107345039).Length;
			DateTimeParser.LzHH_mm_ss = DateTimeParser.getString_0(107344998).Length;
			DateTimeParser.Lz_ = DateTimeParser.getString_0(107373110).Length;
			DateTimeParser.Lz_zz = DateTimeParser.getString_0(107345017).Length;
		}

		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		private bool ParseTime(ref int start)
		{
			if (this.Parse2Digit(start, out this.Hour) && this.Hour <= 24 && this.ParseChar(start + DateTimeParser.LzHH, ':') && this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) && this.Minute < 60 && this.ParseChar(start + DateTimeParser.LzHH_mm, ':') && this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) && this.Second < 60 && (this.Hour != 24 || (this.Minute == 0 && this.Second == 0)))
			{
				start += DateTimeParser.LzHH_mm_ss;
				if (this.ParseChar(start, '.'))
				{
					this.Fraction = 0;
					int num = 0;
					for (;;)
					{
						int num2 = start + 1;
						start = num2;
						if (num2 >= this._end || num >= 7)
						{
							break;
						}
						int num3 = (int)(this._text[start] - '0');
						if (num3 < 0 || num3 > 9)
						{
							break;
						}
						this.Fraction = this.Fraction * 10 + num3;
						num++;
					}
					if (num < 7)
					{
						if (num == 0)
						{
							return false;
						}
						this.Fraction *= DateTimeParser.Power10[7 - num];
					}
					if (this.Hour == 24 && this.Fraction != 0)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c != 'Z')
				{
					if (c != 'z')
					{
						if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
						{
							if (c != '+')
							{
								if (c == '-')
								{
									this.Zone = ParserTimeZone.LocalWestOfUtc;
									start += DateTimeParser.Lz_zz;
								}
							}
							else
							{
								this.Zone = ParserTimeZone.LocalEastOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						if (start >= this._end)
						{
							goto IL_F8;
						}
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
								goto IL_F8;
							}
							goto IL_F8;
						}
						else
						{
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
								goto IL_F8;
							}
							goto IL_F8;
						}
					}
				}
				this.Zone = ParserTimeZone.Utc;
				start++;
			}
			IL_F8:
			return start == this._end;
		}

		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		public int Year;

		public int Month;

		public int Day;

		public int Hour;

		public int Minute;

		public int Second;

		public int Fraction;

		public int ZoneHour;

		public int ZoneMinute;

		public ParserTimeZone Zone;

		private char[] _text;

		private int _end;

		private static readonly int[] Power10;

		private static readonly int Lzyyyy;

		private static readonly int Lzyyyy_;

		private static readonly int Lzyyyy_MM;

		private static readonly int Lzyyyy_MM_;

		private static readonly int Lzyyyy_MM_dd;

		private static readonly int Lzyyyy_MM_ddT;

		private static readonly int LzHH;

		private static readonly int LzHH_;

		private static readonly int LzHH_mm;

		private static readonly int LzHH_mm_;

		private static readonly int LzHH_mm_ss;

		private static readonly int Lz_;

		private static readonly int Lz_zz;

		private const short MaxFractionDigits = 7;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
