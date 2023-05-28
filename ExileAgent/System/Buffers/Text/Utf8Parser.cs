using System;
using System.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	public static class Utf8Parser
	{
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out bool value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat != '\0' && standardFormat != 'G' && standardFormat != 'l')
			{
				return ThrowHelper.TryParseThrowFormatException<bool>(out value, out bytesConsumed);
			}
			if (source.Length >= 4)
			{
				if ((*source[0] == 84 || *source[0] == 116) && (*source[1] == 82 || *source[1] == 114) && (*source[2] == 85 || *source[2] == 117))
				{
					if (*source[3] != 69)
					{
						if (*source[3] != 101)
						{
							goto IL_92;
						}
					}
					bytesConsumed = 4;
					value = true;
					return true;
				}
				IL_92:
				if (source.Length >= 5 && (*source[0] == 70 || *source[0] == 102) && (*source[1] == 65 || *source[1] == 97) && (*source[2] == 76 || *source[2] == 108) && (*source[3] == 83 || *source[3] == 115) && (*source[4] == 69 || *source[4] == 101))
				{
					bytesConsumed = 5;
					value = false;
					return true;
				}
			}
			bytesConsumed = 0;
			value = false;
			return false;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out DateTime value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0' || standardFormat == 'G')
				{
					DateTimeOffset dateTimeOffset;
					return Utf8Parser.TryParseDateTimeG(source, out value, out dateTimeOffset, out bytesConsumed);
				}
			}
			else if (standardFormat != 'O')
			{
				if (standardFormat != 'R')
				{
					if (standardFormat == 'l')
					{
						DateTimeOffset dateTimeOffset2;
						if (!Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out dateTimeOffset2, out bytesConsumed))
						{
							value = default(DateTime);
							return false;
						}
						value = dateTimeOffset2.DateTime;
						return true;
					}
				}
				else
				{
					DateTimeOffset dateTimeOffset3;
					if (!Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out dateTimeOffset3, out bytesConsumed))
					{
						value = default(DateTime);
						return false;
					}
					value = dateTimeOffset3.DateTime;
					return true;
				}
			}
			else
			{
				DateTimeOffset dateTimeOffset4;
				DateTimeKind dateTimeKind;
				if (!Utf8Parser.TryParseDateTimeOffsetO(source, out dateTimeOffset4, out bytesConsumed, out dateTimeKind))
				{
					value = default(DateTime);
					bytesConsumed = 0;
					return false;
				}
				if (dateTimeKind != DateTimeKind.Utc)
				{
					if (dateTimeKind == DateTimeKind.Local)
					{
						value = dateTimeOffset4.LocalDateTime;
					}
					else
					{
						value = dateTimeOffset4.DateTime;
					}
				}
				else
				{
					value = dateTimeOffset4.UtcDateTime;
				}
				return true;
			}
			return ThrowHelper.TryParseThrowFormatException<DateTime>(out value, out bytesConsumed);
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0')
				{
					return Utf8Parser.TryParseDateTimeOffsetDefault(source, out value, out bytesConsumed);
				}
				if (standardFormat == 'G')
				{
					DateTime dateTime;
					return Utf8Parser.TryParseDateTimeG(source, out dateTime, out value, out bytesConsumed);
				}
			}
			else
			{
				if (standardFormat == 'O')
				{
					DateTimeKind dateTimeKind;
					return Utf8Parser.TryParseDateTimeOffsetO(source, out value, out bytesConsumed, out dateTimeKind);
				}
				if (standardFormat == 'R')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out value, out bytesConsumed);
				}
				if (standardFormat == 'l')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out value, out bytesConsumed);
				}
			}
			return ThrowHelper.TryParseThrowFormatException<DateTimeOffset>(out value, out bytesConsumed);
		}

		private unsafe static bool TryParseDateTimeOffsetDefault(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed)
		{
			if (source.Length < 26)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			DateTimeOffset dateTimeOffset;
			int num;
			if (!Utf8Parser.TryParseDateTimeG(source, out dateTime, out dateTimeOffset, out num))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			if (*source[19] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			byte b = *source[20];
			if (b != 43 && b != 45)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num2 = (uint)(*source[21] - 48);
			uint num3 = (uint)(*source[22] - 48);
			if (num2 > 9U || num3 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int num4 = (int)(num2 * 10U + num3);
			if (*source[23] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num5 = (uint)(*source[24] - 48);
			uint num6 = (uint)(*source[25] - 48);
			if (num5 > 9U || num6 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int num7 = (int)(num5 * 10U + num6);
			TimeSpan t = new TimeSpan(num4, num7, 0);
			if (b == 45)
			{
				t = -t;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, b == 45, num4, num7, out value))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 26;
			return true;
		}

		private unsafe static bool TryParseDateTimeG(ReadOnlySpan<byte> source, out DateTime value, out DateTimeOffset valueAsOffset, out int bytesConsumed)
		{
			if (source.Length < 19)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			if (num > 9U || num2 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int month = (int)(num * 10U + num2);
			if (*source[2] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num3 = (uint)(*source[3] - 48);
			uint num4 = (uint)(*source[4] - 48);
			if (num3 > 9U || num4 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int day = (int)(num3 * 10U + num4);
			if (*source[5] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num5 = (uint)(*source[6] - 48);
			uint num6 = (uint)(*source[7] - 48);
			uint num7 = (uint)(*source[8] - 48);
			uint num8 = (uint)(*source[9] - 48);
			if (num5 > 9U || num6 > 9U || num7 > 9U || num8 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int year = (int)(num5 * 1000U + num6 * 100U + num7 * 10U + num8);
			if (*source[10] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num9 = (uint)(*source[11] - 48);
			uint num10 = (uint)(*source[12] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int hour = (int)(num9 * 10U + num10);
			if (*source[13] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num11 = (uint)(*source[14] - 48);
			uint num12 = (uint)(*source[15] - 48);
			if (num11 > 9U || num12 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int minute = (int)(num11 * 10U + num12);
			if (*source[16] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num13 = (uint)(*source[17] - 48);
			uint num14 = (uint)(*source[18] - 48);
			if (num13 > 9U || num14 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int second = (int)(num13 * 10U + num14);
			if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(year, month, day, hour, minute, second, 0, out valueAsOffset))
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 19;
			value = valueAsOffset.DateTime;
			return true;
		}

		private static bool TryCreateDateTimeOffset(DateTime dateTime, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			if (offsetHours > 14)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetMinutes > 59)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetHours == 14 && offsetMinutes != 0)
			{
				value = default(DateTimeOffset);
				return false;
			}
			long num = ((long)offsetHours * 3600L + (long)offsetMinutes * 60L) * 10000000L;
			if (offsetNegative)
			{
				num = -num;
			}
			bool result;
			try
			{
				value = new DateTimeOffset(dateTime.Ticks, new TimeSpan(num));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				result = false;
			}
			return result;
		}

		private static bool TryCreateDateTimeOffset(int year, int month, int day, int hour, int minute, int second, int fraction, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Unspecified, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, offsetNegative, offsetHours, offsetMinutes, out value))
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		private static bool TryCreateDateTimeOffsetInterpretingDataAsLocalTime(int year, int month, int day, int hour, int minute, int second, int fraction, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Local, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			bool result;
			try
			{
				value = new DateTimeOffset(dateTime);
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				result = false;
			}
			return result;
		}

		private static bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind, out DateTime value)
		{
			if (year == 0)
			{
				value = default(DateTime);
				return false;
			}
			if (month - 1 >= 12)
			{
				value = default(DateTime);
				return false;
			}
			uint num = (uint)(day - 1);
			if (num >= 28U && (ulong)num >= (ulong)((long)DateTime.DaysInMonth(year, month)))
			{
				value = default(DateTime);
				return false;
			}
			if (hour > 23)
			{
				value = default(DateTime);
				return false;
			}
			if (minute > 59)
			{
				value = default(DateTime);
				return false;
			}
			if (second > 59)
			{
				value = default(DateTime);
				return false;
			}
			int[] array = DateTime.IsLeapYear(year) ? Utf8Parser.s_daysToMonth366 : Utf8Parser.s_daysToMonth365;
			int num2 = year - 1;
			int num3 = num2 * 365 + num2 / 4 - num2 / 100 + num2 / 400 + array[month - 1] + day - 1;
			long num4 = (long)num3 * 864000000000L;
			int num5 = hour * 3600 + minute * 60 + second;
			num4 += (long)num5 * 10000000L;
			num4 += (long)fraction;
			value = new DateTime(num4, kind);
			return true;
		}

		private unsafe static bool TryParseDateTimeOffsetO(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, out DateTimeKind kind)
		{
			if (source.Length < 27)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			uint num3 = (uint)(*source[2] - 48);
			uint num4 = (uint)(*source[3] - 48);
			if (num > 9U || num2 > 9U || num3 > 9U || num4 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int year = (int)(num * 1000U + num2 * 100U + num3 * 10U + num4);
			if (*source[4] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num5 = (uint)(*source[5] - 48);
			uint num6 = (uint)(*source[6] - 48);
			if (num5 > 9U || num6 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int month = (int)(num5 * 10U + num6);
			if (*source[7] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num7 = (uint)(*source[8] - 48);
			uint num8 = (uint)(*source[9] - 48);
			if (num7 > 9U || num8 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int day = (int)(num7 * 10U + num8);
			if (*source[10] != 84)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num9 = (uint)(*source[11] - 48);
			uint num10 = (uint)(*source[12] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int hour = (int)(num9 * 10U + num10);
			if (*source[13] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num11 = (uint)(*source[14] - 48);
			uint num12 = (uint)(*source[15] - 48);
			if (num11 > 9U || num12 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int minute = (int)(num11 * 10U + num12);
			if (*source[16] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num13 = (uint)(*source[17] - 48);
			uint num14 = (uint)(*source[18] - 48);
			if (num13 > 9U || num14 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int second = (int)(num13 * 10U + num14);
			if (*source[19] != 46)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num15 = (uint)(*source[20] - 48);
			uint num16 = (uint)(*source[21] - 48);
			uint num17 = (uint)(*source[22] - 48);
			uint num18 = (uint)(*source[23] - 48);
			uint num19 = (uint)(*source[24] - 48);
			uint num20 = (uint)(*source[25] - 48);
			uint num21 = (uint)(*source[26] - 48);
			if (num15 > 9U || num16 > 9U || num17 > 9U || num18 > 9U || num19 > 9U || num20 > 9U || num21 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int fraction = (int)(num15 * 1000000U + num16 * 100000U + num17 * 10000U + num18 * 1000U + num19 * 100U + num20 * 10U + num21);
			byte b = (source.Length <= 27) ? 0 : (*source[27]);
			if (b != 90 && b != 43 && b != 45)
			{
				if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(year, month, day, hour, minute, second, fraction, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 27;
				kind = DateTimeKind.Unspecified;
				return true;
			}
			else if (b == 90)
			{
				if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, fraction, false, 0, 0, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 28;
				kind = DateTimeKind.Utc;
				return true;
			}
			else
			{
				if (source.Length < 33)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num22 = (uint)(*source[28] - 48);
				uint num23 = (uint)(*source[29] - 48);
				if (num22 > 9U || num23 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int offsetHours = (int)(num22 * 10U + num23);
				if (*source[30] != 58)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num24 = (uint)(*source[31] - 48);
				uint num25 = (uint)(*source[32] - 48);
				if (num24 > 9U || num25 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int offsetMinutes = (int)(num24 * 10U + num25);
				if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, fraction, b == 45, offsetHours, offsetMinutes, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 33;
				kind = DateTimeKind.Local;
				return true;
			}
		}

		private unsafe static bool TryParseDateTimeOffsetR(ReadOnlySpan<byte> source, uint caseFlipXorMask, out DateTimeOffset dateTimeOffset, out int bytesConsumed)
		{
			if (source.Length < 29)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0]) ^ caseFlipXorMask;
			uint num2 = (uint)(*source[1]);
			uint num3 = (uint)(*source[2]);
			uint num4 = (uint)(*source[3]);
			uint num5 = num << 24 | num2 << 16 | num3 << 8 | num4;
			DayOfWeek dayOfWeek;
			if (num5 <= 1398895660U)
			{
				if (num5 == 1181903148U)
				{
					dayOfWeek = DayOfWeek.Friday;
					goto IL_D5;
				}
				if (num5 == 1299148332U)
				{
					dayOfWeek = DayOfWeek.Monday;
					goto IL_D5;
				}
				if (num5 == 1398895660U)
				{
					dayOfWeek = DayOfWeek.Saturday;
					goto IL_D5;
				}
			}
			else if (num5 <= 1416131884U)
			{
				if (num5 == 1400204844U)
				{
					dayOfWeek = DayOfWeek.Sunday;
					goto IL_D5;
				}
				if (num5 == 1416131884U)
				{
					dayOfWeek = DayOfWeek.Thursday;
					goto IL_D5;
				}
			}
			else
			{
				if (num5 == 1416979756U)
				{
					dayOfWeek = DayOfWeek.Tuesday;
					goto IL_D5;
				}
				if (num5 == 1466262572U)
				{
					dayOfWeek = DayOfWeek.Wednesday;
					goto IL_D5;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_D5:
			if (*source[4] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num6 = (uint)(*source[5] - 48);
			uint num7 = (uint)(*source[6] - 48);
			if (num6 > 9U || num7 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int day = (int)(num6 * 10U + num7);
			if (*source[7] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num8 = (uint)(*source[8]) ^ caseFlipXorMask;
			uint num9 = (uint)(*source[9]);
			uint num10 = (uint)(*source[10]);
			uint num11 = (uint)(*source[11]);
			uint num12 = num8 << 24 | num9 << 16 | num10 << 8 | num11;
			int month;
			if (num12 <= 1249209376U)
			{
				if (num12 <= 1147495200U)
				{
					if (num12 == 1097888288U)
					{
						month = 4;
						goto IL_25B;
					}
					if (num12 == 1098213152U)
					{
						month = 8;
						goto IL_25B;
					}
					if (num12 == 1147495200U)
					{
						month = 12;
						goto IL_25B;
					}
				}
				else
				{
					if (num12 == 1181049376U)
					{
						month = 2;
						goto IL_25B;
					}
					if (num12 == 1247899168U)
					{
						month = 1;
						goto IL_25B;
					}
					if (num12 == 1249209376U)
					{
						month = 7;
						goto IL_25B;
					}
				}
			}
			else if (num12 <= 1298233632U)
			{
				if (num12 == 1249209888U)
				{
					month = 6;
					goto IL_25B;
				}
				if (num12 == 1298231840U)
				{
					month = 3;
					goto IL_25B;
				}
				if (num12 == 1298233632U)
				{
					month = 5;
					goto IL_25B;
				}
			}
			else
			{
				if (num12 == 1315927584U)
				{
					month = 11;
					goto IL_25B;
				}
				if (num12 == 1331917856U)
				{
					month = 10;
					goto IL_25B;
				}
				if (num12 == 1399156768U)
				{
					month = 9;
					goto IL_25B;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_25B:
			uint num13 = (uint)(*source[12] - 48);
			uint num14 = (uint)(*source[13] - 48);
			uint num15 = (uint)(*source[14] - 48);
			uint num16 = (uint)(*source[15] - 48);
			if (num13 > 9U || num14 > 9U || num15 > 9U || num16 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int year = (int)(num13 * 1000U + num14 * 100U + num15 * 10U + num16);
			if (*source[16] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num17 = (uint)(*source[17] - 48);
			uint num18 = (uint)(*source[18] - 48);
			if (num17 > 9U || num18 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int hour = (int)(num17 * 10U + num18);
			if (*source[19] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num19 = (uint)(*source[20] - 48);
			uint num20 = (uint)(*source[21] - 48);
			if (num19 > 9U || num20 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int minute = (int)(num19 * 10U + num20);
			if (*source[22] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num21 = (uint)(*source[23] - 48);
			uint num22 = (uint)(*source[24] - 48);
			if (num21 > 9U || num22 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int second = (int)(num21 * 10U + num22);
			uint num23 = (uint)(*source[25]);
			uint num24 = (uint)(*source[26]) ^ caseFlipXorMask;
			uint num25 = (uint)(*source[27]) ^ caseFlipXorMask;
			uint num26 = (uint)(*source[28]) ^ caseFlipXorMask;
			uint num27 = num23 << 24 | num24 << 16 | num25 << 8 | num26;
			if (num27 != 541543764U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, 0, false, 0, 0, out dateTimeOffset))
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (dayOfWeek != dateTimeOffset.DayOfWeek)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 29;
			return true;
		}

		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out decimal value, out int bytesConsumed, char standardFormat = '\0')
		{
			Utf8Parser.ParseNumberOptions options;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_39;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_39;
					case 'f':
						break;
					default:
						return ThrowHelper.TryParseThrowFormatException<decimal>(out value, out bytesConsumed);
					}
					break;
				}
				options = (Utf8Parser.ParseNumberOptions)0;
				goto IL_3B;
			}
			IL_39:
			options = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_3B:
			NumberBuffer numberBuffer = default(NumberBuffer);
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref numberBuffer, out bytesConsumed, options, out flag))
			{
				value = 0m;
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			if (*numberBuffer.Digits[0] == 0 && numberBuffer.Scale == 0)
			{
				numberBuffer.IsNegative = false;
			}
			value = 0m;
			if (!Number.NumberBufferToDecimal(ref numberBuffer, ref value))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out float value, out int bytesConsumed, char standardFormat = '\0')
		{
			double num;
			if (!Utf8Parser.TryParseNormalAsFloatingPoint(source, out num, out bytesConsumed, standardFormat))
			{
				return Utf8Parser.TryParseAsSpecialFloatingPoint<float>(source, float.PositiveInfinity, float.NegativeInfinity, float.NaN, out value, out bytesConsumed);
			}
			value = (float)num;
			if (float.IsInfinity(value))
			{
				value = 0f;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out double value, out int bytesConsumed, char standardFormat = '\0')
		{
			return Utf8Parser.TryParseNormalAsFloatingPoint(source, out value, out bytesConsumed, standardFormat) || Utf8Parser.TryParseAsSpecialFloatingPoint<double>(source, double.PositiveInfinity, double.NegativeInfinity, double.NaN, out value, out bytesConsumed);
		}

		private unsafe static bool TryParseNormalAsFloatingPoint(ReadOnlySpan<byte> source, out double value, out int bytesConsumed, char standardFormat)
		{
			Utf8Parser.ParseNumberOptions options;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_39;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_39;
					case 'f':
						break;
					default:
						return ThrowHelper.TryParseThrowFormatException<double>(out value, out bytesConsumed);
					}
					break;
				}
				options = (Utf8Parser.ParseNumberOptions)0;
				goto IL_3B;
			}
			IL_39:
			options = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_3B:
			NumberBuffer numberBuffer = default(NumberBuffer);
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref numberBuffer, out bytesConsumed, options, out flag))
			{
				value = 0.0;
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				value = 0.0;
				bytesConsumed = 0;
				return false;
			}
			if (*numberBuffer.Digits[0] == 0)
			{
				numberBuffer.IsNegative = false;
			}
			if (!Number.NumberBufferToDouble(ref numberBuffer, out value))
			{
				value = 0.0;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		private unsafe static bool TryParseAsSpecialFloatingPoint<T>(ReadOnlySpan<byte> source, T positiveInfinity, T negativeInfinity, T nan, out T value, out int bytesConsumed)
		{
			if (source.Length >= 8 && *source[0] == 73 && *source[1] == 110 && *source[2] == 102 && *source[3] == 105 && *source[4] == 110 && *source[5] == 105 && *source[6] == 116 && *source[7] == 121)
			{
				value = positiveInfinity;
				bytesConsumed = 8;
				return true;
			}
			if (source.Length >= 9 && *source[0] == 45 && *source[1] == 73 && *source[2] == 110 && *source[3] == 102 && *source[4] == 105 && *source[5] == 110 && *source[6] == 105 && *source[7] == 116 && *source[8] == 121)
			{
				value = negativeInfinity;
				bytesConsumed = 9;
				return true;
			}
			if (source.Length >= 3 && *source[0] == 78 && *source[1] == 97 && *source[2] == 78)
			{
				value = nan;
				bytesConsumed = 3;
				return true;
			}
			value = default(T);
			bytesConsumed = 0;
			return false;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out Guid value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'B')
			{
				if (standardFormat == '\0')
				{
					goto IL_4B;
				}
				if (standardFormat == 'B')
				{
					return Utf8Parser.TryParseGuidCore(source, true, '{', '}', out value, out bytesConsumed);
				}
			}
			else
			{
				if (standardFormat == 'D')
				{
					goto IL_4B;
				}
				if (standardFormat == 'N')
				{
					return Utf8Parser.TryParseGuidN(source, out value, out bytesConsumed);
				}
				if (standardFormat == 'P')
				{
					return Utf8Parser.TryParseGuidCore(source, true, '(', ')', out value, out bytesConsumed);
				}
			}
			return ThrowHelper.TryParseThrowFormatException<Guid>(out value, out bytesConsumed);
			IL_4B:
			return Utf8Parser.TryParseGuidCore(source, false, ' ', ' ', out value, out bytesConsumed);
		}

		private static bool TryParseGuidN(ReadOnlySpan<byte> text, out Guid value, out int bytesConsumed)
		{
			if (text.Length < 32)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			uint a;
			int num;
			if (Utf8Parser.TryParseUInt32X(text.Slice(0, 8), out a, out num))
			{
				if (num == 8)
				{
					ushort num2;
					if (Utf8Parser.TryParseUInt16X(text.Slice(8, 4), out num2, out num))
					{
						if (num == 4)
						{
							ushort num3;
							if (Utf8Parser.TryParseUInt16X(text.Slice(12, 4), out num3, out num))
							{
								if (num == 4)
								{
									ushort num4;
									if (Utf8Parser.TryParseUInt16X(text.Slice(16, 4), out num4, out num))
									{
										if (num == 4)
										{
											ulong num5;
											if (Utf8Parser.TryParseUInt64X(text.Slice(20), out num5, out num))
											{
												if (num == 12)
												{
													bytesConsumed = 32;
													value = new Guid((int)a, (short)num2, (short)num3, (byte)(num4 >> 8), (byte)num4, (byte)(num5 >> 40), (byte)(num5 >> 32), (byte)(num5 >> 24), (byte)(num5 >> 16), (byte)(num5 >> 8), (byte)num5);
													return true;
												}
											}
											value = default(Guid);
											bytesConsumed = 0;
											return false;
										}
									}
									value = default(Guid);
									bytesConsumed = 0;
									return false;
								}
							}
							value = default(Guid);
							bytesConsumed = 0;
							return false;
						}
					}
					value = default(Guid);
					bytesConsumed = 0;
					return false;
				}
			}
			value = default(Guid);
			bytesConsumed = 0;
			return false;
		}

		private unsafe static bool TryParseGuidCore(ReadOnlySpan<byte> source, bool ends, char begin, char end, out Guid value, out int bytesConsumed)
		{
			int num = 36 + (ends ? 2 : 0);
			if (source.Length < num)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends)
			{
				if ((char)(*source[0]) != begin)
				{
					value = default(Guid);
					bytesConsumed = 0;
					return false;
				}
				source = source.Slice(1);
			}
			uint a;
			int num2;
			if (!Utf8Parser.TryParseUInt32X(source, out a, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 8)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(9);
			ushort num3;
			if (!Utf8Parser.TryParseUInt16X(source, out num3, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num4;
			if (!Utf8Parser.TryParseUInt16X(source, out num4, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num5;
			if (!Utf8Parser.TryParseUInt16X(source, out num5, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ulong num6;
			if (!Utf8Parser.TryParseUInt64X(source, out num6, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 12)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends && (char)(*source[num2]) != end)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = num;
			value = new Guid((int)a, (short)num3, (short)num4, (byte)(num5 >> 8), (byte)num5, (byte)(num6 >> 40), (byte)(num6 >> 32), (byte)(num6 >> 24), (byte)(num6 >> 16), (byte)(num6 >> 8), (byte)num6);
			return true;
		}

		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_62;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_62;
					}
					if (standardFormat == 'n')
					{
						goto IL_59;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				value = 0;
				return Utf8Parser.TryParseByteX(source, Unsafe.As<sbyte, byte>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_62;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_62;
				}
				if (standardFormat == 'N')
				{
					goto IL_59;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<sbyte>(out value, out bytesConsumed);
			IL_59:
			return Utf8Parser.TryParseSByteN(source, out value, out bytesConsumed);
			IL_62:
			return Utf8Parser.TryParseSByteD(source, out value, out bytesConsumed);
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out short value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_62;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_62;
					}
					if (standardFormat == 'n')
					{
						goto IL_59;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				value = 0;
				return Utf8Parser.TryParseUInt16X(source, Unsafe.As<short, ushort>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_62;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_62;
				}
				if (standardFormat == 'N')
				{
					goto IL_59;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<short>(out value, out bytesConsumed);
			IL_59:
			return Utf8Parser.TryParseInt16N(source, out value, out bytesConsumed);
			IL_62:
			return Utf8Parser.TryParseInt16D(source, out value, out bytesConsumed);
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out int value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_62;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_62;
					}
					if (standardFormat == 'n')
					{
						goto IL_59;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				value = 0;
				return Utf8Parser.TryParseUInt32X(source, Unsafe.As<int, uint>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_62;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_62;
				}
				if (standardFormat == 'N')
				{
					goto IL_59;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<int>(out value, out bytesConsumed);
			IL_59:
			return Utf8Parser.TryParseInt32N(source, out value, out bytesConsumed);
			IL_62:
			return Utf8Parser.TryParseInt32D(source, out value, out bytesConsumed);
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out long value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_6A;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_6A;
					}
					if (standardFormat == 'n')
					{
						goto IL_61;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				value = 0L;
				return Utf8Parser.TryParseUInt64X(source, Unsafe.As<long, ulong>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_6A;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_6A;
				}
				if (standardFormat == 'N')
				{
					goto IL_61;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<long>(out value, out bytesConsumed);
			IL_61:
			return Utf8Parser.TryParseInt64N(source, out value, out bytesConsumed);
			IL_6A:
			return Utf8Parser.TryParseInt64D(source, out value, out bytesConsumed);
		}

		private unsafe static bool TryParseSByteD(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_13A;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_13A;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_12F;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_12F;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = num4 * 10 + num3 - 48;
									if ((ulong)num4 > (ulong)(127L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
									{
										goto IL_13A;
									}
								}
							}
						}
					}
					IL_12F:
					bytesConsumed = num2;
					value = (sbyte)(num4 * num);
					return true;
				}
			}
			IL_13A:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt16D(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_19A;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_19A;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_18F;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_18F;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = num4 * 10 + num3 - 48;
													if ((ulong)num4 > (ulong)(32767L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
													{
														goto IL_19A;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_18F:
					bytesConsumed = num2;
					value = (short)(num4 * num);
					return true;
				}
			}
			IL_19A:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt32D(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_294;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_294;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_28A;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_28A;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = 10 * num4 + num3 - 48;
													if (num2 < source.Length)
													{
														num3 = (int)(*source[num2]);
														if (ParserHelpers.IsDigit(num3))
														{
															num2++;
															num4 = 10 * num4 + num3 - 48;
															if (num2 < source.Length)
															{
																num3 = (int)(*source[num2]);
																if (ParserHelpers.IsDigit(num3))
																{
																	num2++;
																	num4 = 10 * num4 + num3 - 48;
																	if (num2 < source.Length)
																	{
																		num3 = (int)(*source[num2]);
																		if (ParserHelpers.IsDigit(num3))
																		{
																			num2++;
																			num4 = 10 * num4 + num3 - 48;
																			if (num2 < source.Length)
																			{
																				num3 = (int)(*source[num2]);
																				if (ParserHelpers.IsDigit(num3))
																				{
																					num2++;
																					num4 = 10 * num4 + num3 - 48;
																					if (num2 < source.Length)
																					{
																						num3 = (int)(*source[num2]);
																						if (ParserHelpers.IsDigit(num3))
																						{
																							num2++;
																							if (num4 > 214748364)
																							{
																								goto IL_294;
																							}
																							num4 = num4 * 10 + num3 - 48;
																							if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
																							{
																								goto IL_294;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_28A:
					bytesConsumed = num2;
					value = num4 * num;
					return true;
				}
			}
			IL_294:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt64D(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0L;
				return false;
			}
			int num = 0;
			int num2 = 1;
			if (*source[0] == 45)
			{
				num = 1;
				num2 = -1;
				if (source.Length <= 1)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			else if (*source[0] == 43)
			{
				num = 1;
				if (source.Length <= 1)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			int num3 = 19 + num;
			long num4 = (long)(*source[num] - 48);
			if (num4 >= 0L && num4 <= 9L)
			{
				ulong num5 = (ulong)num4;
				if (source.Length < num3)
				{
					for (int i = num + 1; i < source.Length; i++)
					{
						long num6 = (long)(*source[i] - 48);
						if (num6 < 0L || num6 > 9L)
						{
							bytesConsumed = i;
							value = (long)(num5 * (ulong)((long)num2));
							return true;
						}
						num5 = num5 * 10UL + (ulong)num6;
					}
				}
				else
				{
					for (int j = num + 1; j < num3 - 1; j++)
					{
						long num7 = (long)(*source[j] - 48);
						if (num7 < 0L || num7 > 9L)
						{
							bytesConsumed = j;
							value = (long)(num5 * (ulong)((long)num2));
							return true;
						}
						num5 = num5 * 10UL + (ulong)num7;
					}
					for (int k = num3 - 1; k < source.Length; k++)
					{
						long num8 = (long)(*source[k] - 48);
						if (num8 < 0L || num8 > 9L)
						{
							bytesConsumed = k;
							value = (long)(num5 * (ulong)((long)num2));
							return true;
						}
						bool flag = num2 > 0;
						bool flag2 = num8 > 8L || (flag && num8 > 7L);
						if (num5 > 922337203685477580UL || (num5 == 922337203685477580UL && flag2))
						{
							bytesConsumed = 0;
							value = 0L;
							return false;
						}
						num5 = num5 * 10UL + (ulong)num8;
					}
				}
				bytesConsumed = source.Length;
				value = (long)(num5 * (ulong)((long)num2));
				return true;
			}
			bytesConsumed = 0;
			value = 0L;
			return false;
		}

		private unsafe static bool TryParseSByteN(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10C;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10C;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (!ParserHelpers.IsDigit(num3))
					{
						goto IL_10C;
					}
					num4 = num3 - 48;
					for (;;)
					{
						num2++;
						if (num2 >= source.Length)
						{
							goto IL_101;
						}
						num3 = (int)(*source[num2]);
						if (num3 != 44)
						{
							if (num3 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num3))
							{
								goto IL_101;
							}
							num4 = num4 * 10 + num3 - 48;
							if (num4 > 127 + (-1 * num + 1) / 2)
							{
								goto IL_10C;
							}
						}
					}
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_10C;
					}
				}
				do
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_101;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_10C;
				}
				IL_101:
				bytesConsumed = num2;
				value = (sbyte)(num4 * num);
				return true;
			}
			IL_10C:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt16N(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10F;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10F;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (!ParserHelpers.IsDigit(num3))
					{
						goto IL_10F;
					}
					num4 = num3 - 48;
					for (;;)
					{
						num2++;
						if (num2 >= source.Length)
						{
							goto IL_104;
						}
						num3 = (int)(*source[num2]);
						if (num3 != 44)
						{
							if (num3 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num3))
							{
								goto IL_104;
							}
							num4 = num4 * 10 + num3 - 48;
							if (num4 > 32767 + (-1 * num + 1) / 2)
							{
								goto IL_10F;
							}
						}
					}
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_10F;
					}
				}
				do
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_104;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_10F;
				}
				IL_104:
				bytesConsumed = num2;
				value = (short)(num4 * num);
				return true;
			}
			IL_10F:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt32N(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_122;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_122;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (!ParserHelpers.IsDigit(num3))
					{
						goto IL_122;
					}
					num4 = num3 - 48;
					for (;;)
					{
						num2++;
						if (num2 >= source.Length)
						{
							goto IL_118;
						}
						num3 = (int)(*source[num2]);
						if (num3 != 44)
						{
							if (num3 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num3))
							{
								goto IL_118;
							}
							if (num4 > 214748364)
							{
								goto IL_122;
							}
							num4 = num4 * 10 + num3 - 48;
							if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)))
							{
								goto IL_122;
							}
						}
					}
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_122;
					}
				}
				do
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_118;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_122;
				}
				IL_118:
				bytesConsumed = num2;
				value = num4 * num;
				return true;
			}
			IL_122:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseInt64N(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[0]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_144;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_144;
					}
					num3 = (int)(*source[num2]);
				}
				long num4;
				if (num3 != 46)
				{
					if (!ParserHelpers.IsDigit(num3))
					{
						goto IL_144;
					}
					num4 = (long)(num3 - 48);
					for (;;)
					{
						num2++;
						if (num2 >= source.Length)
						{
							goto IL_139;
						}
						num3 = (int)(*source[num2]);
						if (num3 != 44)
						{
							if (num3 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num3))
							{
								goto IL_139;
							}
							if (num4 > 922337203685477580L)
							{
								goto IL_144;
							}
							num4 = num4 * 10L + (long)num3 - 48L;
							if (num4 > 9223372036854775807L + (long)((-1 * num + 1) / 2))
							{
								goto IL_144;
							}
						}
					}
				}
				else
				{
					num4 = 0L;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_144;
					}
				}
				do
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_139;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_144;
				}
				IL_139:
				bytesConsumed = num2;
				value = num4 * (long)num;
				return true;
			}
			IL_144:
			bytesConsumed = 0;
			value = 0L;
			return false;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_5A;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_5A;
					}
					if (standardFormat == 'n')
					{
						goto IL_51;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				return Utf8Parser.TryParseByteX(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_5A;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_5A;
				}
				if (standardFormat == 'N')
				{
					goto IL_51;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<byte>(out value, out bytesConsumed);
			IL_51:
			return Utf8Parser.TryParseByteN(source, out value, out bytesConsumed);
			IL_5A:
			return Utf8Parser.TryParseByteD(source, out value, out bytesConsumed);
		}

		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_5A;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_5A;
					}
					if (standardFormat == 'n')
					{
						goto IL_51;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				return Utf8Parser.TryParseUInt16X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_5A;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_5A;
				}
				if (standardFormat == 'N')
				{
					goto IL_51;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<ushort>(out value, out bytesConsumed);
			IL_51:
			return Utf8Parser.TryParseUInt16N(source, out value, out bytesConsumed);
			IL_5A:
			return Utf8Parser.TryParseUInt16D(source, out value, out bytesConsumed);
		}

		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_5A;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_5A;
					}
					if (standardFormat == 'n')
					{
						goto IL_51;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				return Utf8Parser.TryParseUInt32X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_5A;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_5A;
				}
				if (standardFormat == 'N')
				{
					goto IL_51;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<uint>(out value, out bytesConsumed);
			IL_51:
			return Utf8Parser.TryParseUInt32N(source, out value, out bytesConsumed);
			IL_5A:
			return Utf8Parser.TryParseUInt32D(source, out value, out bytesConsumed);
		}

		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_40;
						}
						goto IL_5A;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_5A;
					}
					if (standardFormat == 'n')
					{
						goto IL_51;
					}
					if (standardFormat != 'x')
					{
						goto IL_40;
					}
				}
				return Utf8Parser.TryParseUInt64X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat == '\0' || standardFormat == 'D')
				{
					goto IL_5A;
				}
			}
			else
			{
				if (standardFormat == 'G')
				{
					goto IL_5A;
				}
				if (standardFormat == 'N')
				{
					goto IL_51;
				}
			}
			IL_40:
			return ThrowHelper.TryParseThrowFormatException<ulong>(out value, out bytesConsumed);
			IL_51:
			return Utf8Parser.TryParseUInt64N(source, out value, out bytesConsumed);
			IL_5A:
			return Utf8Parser.TryParseUInt64D(source, out value, out bytesConsumed);
		}

		private unsafe static bool TryParseByteD(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_D8;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_D8;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = num3 * 10 + num2 - 48;
									if (num3 > 255 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
									{
										goto IL_E1;
									}
								}
							}
						}
					}
					IL_D8:
					bytesConsumed = num;
					value = (byte)num3;
					return true;
				}
			}
			IL_E1:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseUInt16D(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_13B;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_13B;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = num3 * 10 + num2 - 48;
													if (num3 > 65535 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
													{
														goto IL_144;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_13B:
					bytesConsumed = num;
					value = (ushort)num3;
					return true;
				}
			}
			IL_144:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseUInt32D(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_238;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_238;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = 10 * num3 + num2 - 48;
													if (num < source.Length)
													{
														num2 = (int)(*source[num]);
														if (ParserHelpers.IsDigit(num2))
														{
															num++;
															num3 = 10 * num3 + num2 - 48;
															if (num < source.Length)
															{
																num2 = (int)(*source[num]);
																if (ParserHelpers.IsDigit(num2))
																{
																	num++;
																	num3 = 10 * num3 + num2 - 48;
																	if (num < source.Length)
																	{
																		num2 = (int)(*source[num]);
																		if (ParserHelpers.IsDigit(num2))
																		{
																			num++;
																			num3 = 10 * num3 + num2 - 48;
																			if (num < source.Length)
																			{
																				num2 = (int)(*source[num]);
																				if (ParserHelpers.IsDigit(num2))
																				{
																					num++;
																					num3 = 10 * num3 + num2 - 48;
																					if (num < source.Length)
																					{
																						num2 = (int)(*source[num]);
																						if (ParserHelpers.IsDigit(num2))
																						{
																							num++;
																							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
																							{
																								goto IL_240;
																							}
																							num3 = num3 * 10 + num2 - 48;
																							if (num < source.Length && ParserHelpers.IsDigit((int)(*source[num])))
																							{
																								goto IL_240;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_238:
					bytesConsumed = num;
					value = (uint)num3;
					return true;
				}
			}
			IL_240:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		private unsafe static bool TryParseUInt64D(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)(*source[0] - 48);
			if (num > 9UL)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num2 = num;
			if (source.Length < 19)
			{
				for (int i = 1; i < source.Length; i++)
				{
					ulong num3 = (ulong)(*source[i] - 48);
					if (num3 > 9UL)
					{
						bytesConsumed = i;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num3;
				}
			}
			else
			{
				for (int j = 1; j < 18; j++)
				{
					ulong num4 = (ulong)(*source[j] - 48);
					if (num4 > 9UL)
					{
						bytesConsumed = j;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num4;
				}
				for (int k = 18; k < source.Length; k++)
				{
					ulong num5 = (ulong)(*source[k] - 48);
					if (num5 > 9UL)
					{
						bytesConsumed = k;
						value = num2;
						return true;
					}
					if (num2 > 1844674407370955161UL || (num2 == 1844674407370955161UL && num5 > 5UL))
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num2 = num2 * 10UL + num5;
				}
			}
			bytesConsumed = source.Length;
			value = num2;
			return true;
		}

		private unsafe static bool TryParseByteN(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_DF;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_DF;
					}
					num3 = num2 - 48;
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_D6;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_D6;
							}
							num3 = num3 * 10 + num2 - 48;
							if (num3 > 255)
							{
								goto IL_DF;
							}
						}
					}
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_DF;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_D6;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_DF;
				}
				IL_D6:
				bytesConsumed = num;
				value = (byte)num3;
				return true;
			}
			IL_DF:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseUInt16N(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_DF;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_DF;
					}
					num3 = num2 - 48;
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_D6;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_D6;
							}
							num3 = num3 * 10 + num2 - 48;
							if (num3 > 65535)
							{
								goto IL_DF;
							}
						}
					}
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_DF;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_D6;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_DF;
				}
				IL_D6:
				bytesConsumed = num;
				value = (ushort)num3;
				return true;
			}
			IL_DF:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		private unsafe static bool TryParseUInt32N(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_EB;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_EB;
					}
					num3 = num2 - 48;
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_E3;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_E3;
							}
							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
							{
								goto IL_EB;
							}
							num3 = num3 * 10 + num2 - 48;
						}
					}
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_EB;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_E3;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_EB;
				}
				IL_E3:
				bytesConsumed = num;
				value = (uint)num3;
				return true;
			}
			IL_EB:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		private unsafe static bool TryParseUInt64N(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[0]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_11A;
					}
					num2 = (int)(*source[num]);
				}
				long num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_11A;
					}
					num3 = (long)(num2 - 48);
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_112;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_112;
							}
							if (num3 > 1844674407370955161L || (num3 == 1844674407370955161L && num2 > 53))
							{
								goto IL_11A;
							}
							num3 = num3 * 10L + (long)num2 - 48L;
						}
					}
				}
				else
				{
					num3 = 0L;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_11A;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_112;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_11A;
				}
				IL_112:
				bytesConsumed = num;
				value = (ulong)num3;
				return true;
			}
			IL_11A:
			bytesConsumed = 0;
			value = 0UL;
			return false;
		}

		private unsafe static bool TryParseByteX(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 2)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 2; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 2; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = (byte)num;
						return true;
					}
					if (num > 15U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = (byte)num;
			return true;
		}

		private unsafe static bool TryParseUInt16X(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 4)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 4; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 4; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = (ushort)num;
						return true;
					}
					if (num > 4095U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = (ushort)num;
			return true;
		}

		private unsafe static bool TryParseUInt32X(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 8)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 8; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 8; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 268435455U)
					{
						bytesConsumed = 0;
						value = 0U;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		private unsafe static bool TryParseUInt64X(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)b2;
			if (source.Length <= 16)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b2;
				}
			}
			else
			{
				for (int j = 1; j < 16; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b2;
				}
				for (int k = 16; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 1152921504606846975UL)
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num = (num << 4) + (ulong)b2;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		private unsafe static bool TryParseNumber(ReadOnlySpan<byte> source, ref NumberBuffer number, out int bytesConsumed, Utf8Parser.ParseNumberOptions options, out bool textUsedExponentNotation)
		{
			textUsedExponentNotation = false;
			if (source.Length == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			Span<byte> digits = number.Digits;
			int num = 0;
			byte b = *source[0];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_55;
				}
				number.IsNegative = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_55:
			int num2 = num;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 48)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				*digits[0] = 0;
				number.Scale = 0;
				bytesConsumed = num;
				return true;
			}
			int num3 = num;
			while (num != source.Length)
			{
				b = *source[num];
				if (b - 48 > 9)
				{
					break;
				}
				num++;
			}
			int num4 = num - num2;
			int num5 = num - num3;
			int num6 = Math.Min(num5, 50);
			source.Slice(num3, num6).CopyTo(digits);
			int num7 = num6;
			number.Scale = num5;
			if (num == source.Length)
			{
				bytesConsumed = num;
				return true;
			}
			int num8 = 0;
			if (b == 46)
			{
				num++;
				int num9 = num;
				while (num != source.Length)
				{
					b = *source[num];
					if (b - 48 > 9)
					{
						break;
					}
					num++;
				}
				num8 = num - num9;
				int i = num9;
				if (num7 == 0)
				{
					while (i < num)
					{
						if (*source[i] != 48)
						{
							break;
						}
						number.Scale--;
						i++;
					}
				}
				int num10 = Math.Min(num - i, 51 - num7 - 1);
				source.Slice(i, num10).CopyTo(digits.Slice(num7));
				num7 += num10;
				if (num == source.Length)
				{
					if (num4 == 0 && num8 == 0)
					{
						bytesConsumed = 0;
						return false;
					}
					bytesConsumed = num;
					return true;
				}
			}
			if (num4 == 0 && num8 == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (((int)b & -33) != 69)
			{
				bytesConsumed = num;
				return true;
			}
			textUsedExponentNotation = true;
			num++;
			if ((options & Utf8Parser.ParseNumberOptions.AllowExponent) == (Utf8Parser.ParseNumberOptions)0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			bool flag = false;
			b = *source[num];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_22B;
				}
				flag = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_22B:
			uint num11;
			int num12;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num11, out num12))
			{
				bytesConsumed = 0;
				return false;
			}
			num += num12;
			if (flag)
			{
				if ((long)number.Scale < (long)(18446744071562067968UL + (ulong)num11))
				{
					number.Scale = int.MinValue;
				}
				else
				{
					number.Scale -= (int)num11;
				}
			}
			else
			{
				if ((long)number.Scale > (long)(2147483647UL - (ulong)num11))
				{
					bytesConsumed = 0;
					return false;
				}
				number.Scale += (int)num11;
			}
			bytesConsumed = num;
			return true;
		}

		private unsafe static bool TryParseTimeSpanBigG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			int num = 0;
			byte b = 0;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 32 && b != 9)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			bool isNegative = false;
			if (b == 45)
			{
				isNegative = true;
				num++;
				if (num == source.Length)
				{
					value = default(TimeSpan);
					bytesConsumed = 0;
					return false;
				}
			}
			uint days;
			int num2;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out days, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (num != source.Length)
			{
				if (*source[num++] == 58)
				{
					uint hours;
					if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out hours, out num2))
					{
						value = default(TimeSpan);
						bytesConsumed = 0;
						return false;
					}
					num += num2;
					if (num != source.Length)
					{
						if (*source[num++] == 58)
						{
							uint minutes;
							if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out minutes, out num2))
							{
								value = default(TimeSpan);
								bytesConsumed = 0;
								return false;
							}
							num += num2;
							if (num != source.Length)
							{
								if (*source[num++] == 58)
								{
									uint seconds;
									if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out seconds, out num2))
									{
										value = default(TimeSpan);
										bytesConsumed = 0;
										return false;
									}
									num += num2;
									if (num != source.Length)
									{
										if (*source[num++] == 46)
										{
											uint fraction;
											if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(num), out fraction, out num2))
											{
												value = default(TimeSpan);
												bytesConsumed = 0;
												return false;
											}
											num += num2;
											if (!Utf8Parser.TryCreateTimeSpan(isNegative, days, hours, minutes, seconds, fraction, out value))
											{
												value = default(TimeSpan);
												bytesConsumed = 0;
												return false;
											}
											if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
											{
												value = default(TimeSpan);
												bytesConsumed = 0;
												return false;
											}
											bytesConsumed = num;
											return true;
										}
									}
									value = default(TimeSpan);
									bytesConsumed = 0;
									return false;
								}
							}
							value = default(TimeSpan);
							bytesConsumed = 0;
							return false;
						}
					}
					value = default(TimeSpan);
					bytesConsumed = 0;
					return false;
				}
			}
			value = default(TimeSpan);
			bytesConsumed = 0;
			return false;
		}

		private static bool TryParseTimeSpanC(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, true, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_161;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_161;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_161;
				}
			}
			else if (separators <= 33619968U)
			{
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_161;
				}
				if (separators == 33619968U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, 0U, out value);
					goto IL_161;
				}
			}
			else
			{
				if (separators == 33620224U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_161;
				}
				if (separators == 33620226U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_161;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_161:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'T')
			{
				if (standardFormat == '\0')
				{
					goto IL_3F;
				}
				if (standardFormat == 'G')
				{
					return Utf8Parser.TryParseTimeSpanBigG(source, out value, out bytesConsumed);
				}
				if (standardFormat == 'T')
				{
					goto IL_3F;
				}
			}
			else
			{
				if (standardFormat == 'c')
				{
					goto IL_3F;
				}
				if (standardFormat == 'g')
				{
					return Utf8Parser.TryParseTimeSpanLittleG(source, out value, out bytesConsumed);
				}
				if (standardFormat == 't')
				{
					goto IL_3F;
				}
			}
			return ThrowHelper.TryParseThrowFormatException<TimeSpan>(out value, out bytesConsumed);
			IL_3F:
			return Utf8Parser.TryParseTimeSpanC(source, out value, out bytesConsumed);
		}

		private unsafe static bool TryParseTimeSpanFraction(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			int num = 0;
			if (0 == source.Length)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			uint num2 = (uint)(*source[num] - 48);
			if (num2 > 9U)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			num++;
			uint num3 = num2;
			int num4 = 1;
			while (num != source.Length)
			{
				num2 = (uint)(*source[num] - 48);
				if (num2 > 9U)
				{
					IL_71:
					switch (num4)
					{
					case 2:
						num3 *= 100000U;
						break;
					case 3:
						num3 *= 10000U;
						break;
					case 4:
						num3 *= 1000U;
						break;
					case 5:
						num3 *= 100U;
						break;
					case 6:
						num3 *= 10U;
						break;
					case 7:
						break;
					default:
						num3 *= 1000000U;
						break;
					}
					value = num3;
					bytesConsumed = num;
					return true;
				}
				num++;
				num4++;
				if (num4 > 7)
				{
					value = 0U;
					bytesConsumed = 0;
					return false;
				}
				num3 = 10U * num3 + num2;
			}
			goto IL_71;
		}

		private static bool TryCreateTimeSpan(bool isNegative, uint days, uint hours, uint minutes, uint seconds, uint fraction, out TimeSpan timeSpan)
		{
			if (hours <= 23U && minutes <= 59U && seconds <= 59U)
			{
				long num = (long)(((ulong)days * 3600UL * 24UL + (ulong)hours * 3600UL + (ulong)minutes * 60UL + (ulong)seconds) * 1000UL);
				long ticks;
				if (isNegative)
				{
					num = -num;
					if (num < -922337203685477L)
					{
						timeSpan = default(TimeSpan);
						return false;
					}
					long num2 = num * 10000L;
					if (num2 < (long)(9223372036854775808UL + (ulong)fraction))
					{
						timeSpan = default(TimeSpan);
						return false;
					}
					ticks = num2 - (long)((ulong)fraction);
				}
				else
				{
					if (num > 922337203685477L)
					{
						timeSpan = default(TimeSpan);
						return false;
					}
					long num3 = num * 10000L;
					if (num3 > (long)(9223372036854775807UL - (ulong)fraction))
					{
						timeSpan = default(TimeSpan);
						return false;
					}
					ticks = num3 + (long)((ulong)fraction);
				}
				timeSpan = new TimeSpan(ticks);
				return true;
			}
			timeSpan = default(TimeSpan);
			return false;
		}

		private static bool TryParseTimeSpanLittleG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, false, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_12B;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_12B;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_12B;
				}
			}
			else
			{
				if (separators == 16843008U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_12B;
				}
				if (separators == 16843010U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_12B;
				}
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_12B;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_12B:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		private const uint FlipCase = 32U;

		private const uint NoFlipCase = 0U;

		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};

		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};

		[Flags]
		private enum ParseNumberOptions
		{
			AllowExponent = 1
		}

		private enum ComponentParseResult : byte
		{
			NoMoreData,
			Colon,
			Period,
			ParseFailure
		}

		private struct TimeSpanSplitter
		{
			public unsafe bool TrySplitTimeSpan(ReadOnlySpan<byte> source, bool periodUsedToSeparateDay, out int bytesConsumed)
			{
				int num = 0;
				byte b = 0;
				while (num != source.Length)
				{
					b = *source[num];
					if (b != 32 && b != 9)
					{
						break;
					}
					num++;
				}
				if (num == source.Length)
				{
					bytesConsumed = 0;
					return false;
				}
				if (b == 45)
				{
					this.IsNegative = true;
					num++;
					if (num == source.Length)
					{
						bytesConsumed = 0;
						return false;
					}
				}
				int num2;
				if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out this.V1, out num2))
				{
					bytesConsumed = 0;
					return false;
				}
				num += num2;
				Utf8Parser.ComponentParseResult componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, periodUsedToSeparateDay, ref num, out this.V2);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 24);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V3);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 16);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V4);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 8);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V5);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)componentParseResult;
				if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
				{
					bytesConsumed = 0;
					return false;
				}
				bytesConsumed = num;
				return true;
			}

			private unsafe static Utf8Parser.ComponentParseResult ParseComponent(ReadOnlySpan<byte> source, bool neverParseAsFraction, ref int srcIndex, out uint value)
			{
				if (srcIndex == source.Length)
				{
					value = 0U;
					return Utf8Parser.ComponentParseResult.NoMoreData;
				}
				byte b = *source[srcIndex];
				if (b != 58 && (b != 46 || !neverParseAsFraction))
				{
					if (b != 46)
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.NoMoreData;
					}
					srcIndex++;
					int num;
					if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(srcIndex), out value, out num))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num;
					return Utf8Parser.ComponentParseResult.Period;
				}
				else
				{
					srcIndex++;
					int num2;
					if (!Utf8Parser.TryParseUInt32D(source.Slice(srcIndex), out value, out num2))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num2;
					if (b != 58)
					{
						return Utf8Parser.ComponentParseResult.Period;
					}
					return Utf8Parser.ComponentParseResult.Colon;
				}
			}

			public uint V1;

			public uint V2;

			public uint V3;

			public uint V4;

			public uint V5;

			public bool IsNegative;

			public uint Separators;
		}
	}
}
