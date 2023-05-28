using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal static class HttpUtility
	{
		static HttpUtility()
		{
			Strings.CreateGetStringDelegate(typeof(HttpUtility));
			HttpUtility._hexChars = HttpUtility.getString_0(107144249).ToCharArray();
			HttpUtility._sync = new object();
		}

		private static Dictionary<string, char> getEntities()
		{
			object sync = HttpUtility._sync;
			Dictionary<string, char> entities;
			lock (sync)
			{
				if (HttpUtility._entities == null)
				{
					HttpUtility.initEntities();
				}
				entities = HttpUtility._entities;
			}
			return entities;
		}

		private static int getNumber(char c)
		{
			return (c < '0' || c > '9') ? ((c < 'A' || c > 'F') ? ((c < 'a' || c > 'f') ? -1 : ((int)(c - 'a' + '\n'))) : ((int)(c - 'A' + '\n'))) : ((int)(c - '0'));
		}

		private static int getNumber(byte[] bytes, int offset, int count)
		{
			int num = 0;
			int num2 = offset + count - 1;
			for (int i = offset; i <= num2; i++)
			{
				int number = HttpUtility.getNumber((char)bytes[i]);
				if (number == -1)
				{
					return -1;
				}
				num = (num << 4) + number;
			}
			return num;
		}

		private static int getNumber(string s, int offset, int count)
		{
			int num = 0;
			int num2 = offset + count - 1;
			for (int i = offset; i <= num2; i++)
			{
				int number = HttpUtility.getNumber(s[i]);
				if (number == -1)
				{
					return -1;
				}
				num = (num << 4) + number;
			}
			return num;
		}

		private static string htmlDecode(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			StringBuilder stringBuilder2 = new StringBuilder();
			int num2 = 0;
			foreach (char c in s)
			{
				if (num == 0)
				{
					if (c == '&')
					{
						stringBuilder2.Append('&');
						num = 1;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else if (c == '&')
				{
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
					stringBuilder2.Append('&');
					num = 1;
				}
				else
				{
					stringBuilder2.Append(c);
					if (num == 1)
					{
						if (c == ';')
						{
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
							num = 0;
						}
						else
						{
							num2 = 0;
							num = ((c == '#') ? 3 : 2);
						}
					}
					else if (num == 2)
					{
						if (c == ';')
						{
							string text = stringBuilder2.ToString();
							string key = text.Substring(1, text.Length - 2);
							Dictionary<string, char> entities = HttpUtility.getEntities();
							if (entities.ContainsKey(key))
							{
								stringBuilder.Append(entities[key]);
							}
							else
							{
								stringBuilder.Append(text);
							}
							stringBuilder2.Length = 0;
							num = 0;
						}
					}
					else if (num == 3)
					{
						if (c == ';')
						{
							if (stringBuilder2.Length > 3 && num2 < 65536)
							{
								stringBuilder.Append((char)num2);
							}
							else
							{
								stringBuilder.Append(stringBuilder2.ToString());
							}
							stringBuilder2.Length = 0;
							num = 0;
						}
						else if (c == 'x')
						{
							num = ((stringBuilder2.Length == 3) ? 4 : 2);
						}
						else if (!HttpUtility.isNumeric(c))
						{
							num = 2;
						}
						else
						{
							num2 = num2 * 10 + (int)(c - '0');
						}
					}
					else if (num == 4)
					{
						if (c == ';')
						{
							if (stringBuilder2.Length > 4 && num2 < 65536)
							{
								stringBuilder.Append((char)num2);
							}
							else
							{
								stringBuilder.Append(stringBuilder2.ToString());
							}
							stringBuilder2.Length = 0;
							num = 0;
						}
						else
						{
							int number = HttpUtility.getNumber(c);
							if (number == -1)
							{
								num = 2;
							}
							else
							{
								num2 = (num2 << 4) + number;
							}
						}
					}
				}
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.Append(stringBuilder2.ToString());
			}
			return stringBuilder.ToString();
		}

		private static string htmlEncode(string s, bool minimal)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				stringBuilder.Append((c == '"') ? HttpUtility.getString_0(107251346) : ((c == '&') ? HttpUtility.getString_0(107251378) : ((c == '<') ? HttpUtility.getString_0(107251396) : ((c == '>') ? HttpUtility.getString_0(107251355) : ((minimal || c <= '\u009f') ? c.ToString() : string.Format(HttpUtility.getString_0(107130426), (int)c))))));
			}
			return stringBuilder.ToString();
		}

		private static void initEntities()
		{
			HttpUtility._entities = new Dictionary<string, char>();
			HttpUtility._entities.Add(HttpUtility.getString_0(107250504), '\u00a0');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250495), '¡');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250454), '¢');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250445), '£');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250468), '¤');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250427), '¥');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250422), '¦');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250413), '§');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250436), '¨');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250431), '©');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250390), 'ª');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250381), '«');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250404), '¬');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250399), '­');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250362), '®');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250357), '¯');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250348), '°');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250375), '±');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250366), '²');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250325), '³');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250316), '´');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250339), 'µ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250298), '¶');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250289), '·');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250312), '¸');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250303), '¹');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250262), 'º');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250253), '»');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250276), '¼');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250747), '½');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250738), '¾');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250761), '¿');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250752), 'À');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250711), 'Á');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250702), 'Â');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250725), 'Ã');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250716), 'Ä');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250675), 'Å');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250698), 'Æ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250689), 'Ç');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250648), 'È');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250639), 'É');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250662), 'Ê');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250653), 'Ë');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250612), 'Ì');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250635), 'Í');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250626), 'Î');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250585), 'Ï');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250576), 'Ð');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250603), 'Ñ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250594), 'Ò');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250553), 'Ó');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250544), 'Ô');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250567), 'Õ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250558), 'Ö');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250517), '×');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250508), 'Ø');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250531), 'Ù');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249978), 'Ú');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249969), 'Û');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249992), 'Ü');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249983), 'Ý');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249942), 'Þ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249933), 'ß');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249956), 'à');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249915), 'á');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249906), 'â');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249929), 'ã');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249920), 'ä');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249879), 'å');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249870), 'æ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249893), 'ç');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249884), 'è');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249843), 'é');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249866), 'ê');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249857), 'ë');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249816), 'ì');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249807), 'í');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249830), 'î');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249821), 'ï');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249780), 'ð');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249775), 'ñ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249798), 'ò');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249789), 'ó');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249748), 'ô');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249771), 'õ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249762), 'ö');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250233), '÷');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250224), 'ø');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250247), 'ù');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250238), 'ú');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250197), 'û');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250188), 'ü');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250211), 'ý');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250170), 'þ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250161), 'ÿ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250184), 'ƒ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250175), 'Α');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250134), 'Β');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250125), 'Γ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250148), 'Δ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250107), 'Ε');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250094), 'Ζ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250117), 'Η');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250112), 'Θ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250071), 'Ι');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250062), 'Κ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250085), 'Λ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250076), 'Μ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250039), 'Ν');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250034), 'Ξ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250029), 'Ο');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250048), 'Π');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250011), 'Ρ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250006), 'Σ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249997), 'Τ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250024), 'Υ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249467), 'Φ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249462), 'Χ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249457), 'Ψ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249452), 'Ω');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249475), 'α');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249434), 'β');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249425), 'γ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249448), 'δ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249439), 'ε');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249394), 'ζ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249417), 'η');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249412), 'θ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249371), 'ι');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249362), 'κ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249385), 'λ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249376), 'μ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249339), 'ν');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249334), 'ξ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249329), 'ο');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249348), 'π');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249343), 'ρ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249306), 'ς');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249297), 'σ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249320), 'τ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249315), 'υ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249270), 'φ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249265), 'χ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249260), 'ψ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249287), 'ω');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249278), 'ϑ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249233), 'ϒ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249256), 'ϖ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249251), '•');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249722), '…');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249713), '′');
			HttpUtility._entities.Add(HttpUtility.getString_0(107460420), '″');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249736), '‾');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249727), '⁄');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249686), '℘');
			HttpUtility._entities.Add(HttpUtility.getString_0(107236387), 'ℑ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249677), 'ℜ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249700), '™');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249659), 'ℵ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249646), '←');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249669), '↑');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249660), '→');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249619), '↓');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249642), '↔');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249633), '↵');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249592), '⇐');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249583), '⇑');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249606), '⇒');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249597), '⇓');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249556), '⇔');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249579), '∀');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249570), '∂');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249529), '∃');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249520), '∅');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249543), '∇');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249534), '∈');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249493), '∉');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249484), '∋');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249511), '∏');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249502), '∑');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248953), '−');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248944), '∗');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248967), '√');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248958), '∝');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248917), '∞');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248908), '∠');
			HttpUtility._entities.Add(HttpUtility.getString_0(107394218), '∧');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248935), '∨');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248930), '∩');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248925), '∪');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248888), '∫');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248883), '∴');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248906), '∼');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248901), '≅');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248892), '≈');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248851), '≠');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248846), '≡');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248869), '≤');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248864), '≥');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248827), '⊂');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248822), '⊃');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248817), '⊄');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248840), '⊆');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248831), '⊇');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248790), '⊕');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248781), '⊗');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248804), '⊥');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248763), '⋅');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248754), '⌈');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248777), '⌉');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248768), '⌊');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248727), '⌋');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248718), '〈');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248741), '〉');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248732), '◊');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249207), '♠');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249198), '♣');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249221), '♥');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249212), '♦');
			HttpUtility._entities.Add(HttpUtility.getString_0(107251049), '"');
			HttpUtility._entities.Add(HttpUtility.getString_0(107251040), '&');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250482), '<');
			HttpUtility._entities.Add(HttpUtility.getString_0(107250477), '>');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249171), 'Œ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249194), 'œ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249185), 'Š');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249144), 'š');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249135), 'Ÿ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249158), 'ˆ');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249149), '˜');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249108), '\u2002');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249131), '\u2003');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249122), '\u2009');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249081), '‌');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249072), '‍');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249099), '‎');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249094), '‏');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249089), '–');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249048), '—');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249039), '‘');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249062), '’');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249053), '‚');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249012), '“');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249035), '”');
			HttpUtility._entities.Add(HttpUtility.getString_0(107249026), '„');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248985), '†');
			HttpUtility._entities.Add(HttpUtility.getString_0(107453693), '‡');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248976), '‰');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248999), '‹');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248990), '›');
			HttpUtility._entities.Add(HttpUtility.getString_0(107248437), '€');
		}

		private static bool isAlphabet(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		private static bool isNumeric(char c)
		{
			return c >= '0' && c <= '9';
		}

		private static bool isUnreserved(char c)
		{
			return c == '*' || c == '-' || c == '.' || c == '_';
		}

		private static bool isUnreservedInRfc2396(char c)
		{
			return c == '!' || c == '\'' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_' || c == '~';
		}

		private static bool isUnreservedInRfc3986(char c)
		{
			return c == '-' || c == '.' || c == '_' || c == '~';
		}

		private static byte[] urlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = offset + count - 1;
				for (int i = offset; i <= num; i++)
				{
					byte b = bytes[i];
					char c = (char)b;
					if (c == '%')
					{
						if (i > num - 2)
						{
							break;
						}
						int number = HttpUtility.getNumber(bytes, i + 1, 2);
						if (number == -1)
						{
							break;
						}
						memoryStream.WriteByte((byte)number);
						i += 2;
					}
					else if (c == '+')
					{
						memoryStream.WriteByte(32);
					}
					else
					{
						memoryStream.WriteByte(b);
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		private static void urlEncode(byte b, Stream output)
		{
			if (b > 31 && b < 127)
			{
				if (b == 32)
				{
					output.WriteByte(43);
					return;
				}
				if (HttpUtility.isNumeric((char)b))
				{
					output.WriteByte(b);
					return;
				}
				if (HttpUtility.isAlphabet((char)b))
				{
					output.WriteByte(b);
					return;
				}
				if (HttpUtility.isUnreserved((char)b))
				{
					output.WriteByte(b);
					return;
				}
			}
			byte[] buffer = new byte[]
			{
				37,
				(byte)HttpUtility._hexChars[b >> 4],
				(byte)HttpUtility._hexChars[(int)(b & 15)]
			};
			output.Write(buffer, 0, 3);
		}

		private static byte[] urlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = offset + count - 1;
				for (int i = offset; i <= num; i++)
				{
					HttpUtility.urlEncode(bytes[i], memoryStream);
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		internal static Uri CreateRequestUrl(string requestUri, string host, bool websocketRequest, bool secure)
		{
			Uri result;
			if (requestUri == null || requestUri.Length == 0)
			{
				result = null;
			}
			else if (host == null || host.Length == 0)
			{
				result = null;
			}
			else
			{
				string text = null;
				string arg = null;
				if (requestUri.IndexOf('/') == 0)
				{
					arg = requestUri;
				}
				else if (requestUri.MaybeUri())
				{
					Uri uri;
					if (!Uri.TryCreate(requestUri, UriKind.Absolute, out uri))
					{
						return null;
					}
					text = uri.Scheme;
					if (!(websocketRequest ? (text == HttpUtility.getString_0(107142121) || text == HttpUtility.getString_0(107363725)) : (text == HttpUtility.getString_0(107140421) || text == HttpUtility.getString_0(107363734))))
					{
						return null;
					}
					host = uri.Authority;
					arg = uri.PathAndQuery;
				}
				else if (!(requestUri == HttpUtility.getString_0(107449875)))
				{
					host = requestUri;
				}
				if (text == null)
				{
					text = (websocketRequest ? (secure ? HttpUtility.getString_0(107363725) : HttpUtility.getString_0(107142121)) : (secure ? HttpUtility.getString_0(107363734) : HttpUtility.getString_0(107140421)));
				}
				if (host.IndexOf(':') == -1)
				{
					host = string.Format(HttpUtility.getString_0(107135489), host, secure ? 443 : 80);
				}
				string uriString = string.Format(HttpUtility.getString_0(107130417), text, host, arg);
				Uri uri2;
				result = (Uri.TryCreate(uriString, UriKind.Absolute, out uri2) ? uri2 : null);
			}
			return result;
		}

		internal static IPrincipal CreateUser(string response, AuthenticationSchemes scheme, string realm, string method, Func<IIdentity, NetworkCredential> credentialsFinder)
		{
			IPrincipal result;
			if (response == null || response.Length == 0)
			{
				result = null;
			}
			else
			{
				if (scheme == AuthenticationSchemes.Digest)
				{
					if (realm == null || realm.Length == 0)
					{
						return null;
					}
					if (method == null || method.Length == 0)
					{
						return null;
					}
				}
				else if (scheme != AuthenticationSchemes.Basic)
				{
					return null;
				}
				if (credentialsFinder == null)
				{
					result = null;
				}
				else if (response.IndexOf(scheme.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
				{
					result = null;
				}
				else
				{
					AuthenticationResponse authenticationResponse = AuthenticationResponse.Parse(response);
					if (authenticationResponse == null)
					{
						result = null;
					}
					else
					{
						IIdentity identity = authenticationResponse.ToIdentity();
						if (identity == null)
						{
							result = null;
						}
						else
						{
							NetworkCredential networkCredential = null;
							try
							{
								networkCredential = credentialsFinder(identity);
							}
							catch
							{
							}
							if (networkCredential == null)
							{
								result = null;
							}
							else if (scheme == AuthenticationSchemes.Basic)
							{
								HttpBasicIdentity httpBasicIdentity = (HttpBasicIdentity)identity;
								result = ((httpBasicIdentity.Password == networkCredential.Password) ? new GenericPrincipal(identity, networkCredential.Roles) : null);
							}
							else
							{
								HttpDigestIdentity httpDigestIdentity = (HttpDigestIdentity)identity;
								result = (httpDigestIdentity.IsValid(networkCredential.Password, realm, method, null) ? new GenericPrincipal(identity, networkCredential.Roles) : null);
							}
						}
					}
				}
			}
			return result;
		}

		internal static Encoding GetEncoding(string contentType)
		{
			string value = HttpUtility.getString_0(107130076);
			StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
			foreach (string text in contentType.SplitHeaderValue(new char[]
			{
				';'
			}))
			{
				string text2 = text.Trim();
				if (text2.StartsWith(value, comparisonType))
				{
					string value2 = text2.GetValue('=', true);
					if (value2 == null || value2.Length == 0)
					{
						return null;
					}
					return Encoding.GetEncoding(value2);
				}
			}
			return null;
		}

		internal static bool TryGetEncoding(string contentType, out Encoding result)
		{
			result = null;
			try
			{
				result = HttpUtility.GetEncoding(contentType);
			}
			catch
			{
				return false;
			}
			return result != null;
		}

		public static string HtmlAttributeEncode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			return (s.Length > 0) ? HttpUtility.htmlEncode(s, true) : s;
		}

		public static void HtmlAttributeEncode(string s, TextWriter output)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			if (output == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130432));
			}
			if (s.Length != 0)
			{
				output.Write(HttpUtility.htmlEncode(s, true));
			}
		}

		public static string HtmlDecode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			return (s.Length > 0) ? HttpUtility.htmlDecode(s) : s;
		}

		public static void HtmlDecode(string s, TextWriter output)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			if (output == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130432));
			}
			if (s.Length != 0)
			{
				output.Write(HttpUtility.htmlDecode(s));
			}
		}

		public static string HtmlEncode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			return (s.Length > 0) ? HttpUtility.htmlEncode(s, false) : s;
		}

		public static void HtmlEncode(string s, TextWriter output)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			if (output == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130432));
			}
			if (s.Length != 0)
			{
				output.Write(HttpUtility.htmlEncode(s, false));
			}
		}

		public static string UrlDecode(string s)
		{
			return HttpUtility.UrlDecode(s, Encoding.UTF8);
		}

		public static string UrlDecode(byte[] bytes, Encoding encoding)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			return (num > 0) ? (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, 0, num)) : string.Empty;
		}

		public static string UrlDecode(string s, Encoding encoding)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			string result;
			if (s.Length == 0)
			{
				result = s;
			}
			else
			{
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				result = (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, 0, bytes.Length));
			}
			return result;
		}

		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding encoding)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			string result;
			if (num == 0)
			{
				if (offset != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = string.Empty;
			}
			else
			{
				if (offset < 0 || offset >= num)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = ((count > 0) ? (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, offset, count)) : string.Empty);
			}
			return result;
		}

		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			return (num > 0) ? HttpUtility.urlDecodeToBytes(bytes, 0, num) : bytes;
		}

		public static byte[] UrlDecodeToBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			byte[] result;
			if (s.Length == 0)
			{
				result = new byte[0];
			}
			else
			{
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				result = HttpUtility.urlDecodeToBytes(bytes, 0, bytes.Length);
			}
			return result;
		}

		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			byte[] result;
			if (num == 0)
			{
				if (offset != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = bytes;
			}
			else
			{
				if (offset < 0 || offset >= num)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = ((count > 0) ? HttpUtility.urlDecodeToBytes(bytes, offset, count) : new byte[0]);
			}
			return result;
		}

		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			return (num > 0) ? Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, 0, num)) : string.Empty;
		}

		public static string UrlEncode(string s)
		{
			return HttpUtility.UrlEncode(s, Encoding.UTF8);
		}

		public static string UrlEncode(string s, Encoding encoding)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			int length = s.Length;
			string result;
			if (length == 0)
			{
				result = s;
			}
			else
			{
				if (encoding == null)
				{
					encoding = Encoding.UTF8;
				}
				byte[] bytes = new byte[encoding.GetMaxByteCount(length)];
				int bytes2 = encoding.GetBytes(s, 0, length, bytes, 0);
				result = Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, 0, bytes2));
			}
			return result;
		}

		public static string UrlEncode(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			string result;
			if (num == 0)
			{
				if (offset != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = string.Empty;
			}
			else
			{
				if (offset < 0 || offset >= num)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = ((count > 0) ? Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, offset, count)) : string.Empty);
			}
			return result;
		}

		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			return (num > 0) ? HttpUtility.urlEncodeToBytes(bytes, 0, num) : bytes;
		}

		public static byte[] UrlEncodeToBytes(string s)
		{
			return HttpUtility.UrlEncodeToBytes(s, Encoding.UTF8);
		}

		public static byte[] UrlEncodeToBytes(string s, Encoding encoding)
		{
			if (s == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107404270));
			}
			byte[] result;
			if (s.Length == 0)
			{
				result = new byte[0];
			}
			else
			{
				byte[] bytes = (encoding ?? Encoding.UTF8).GetBytes(s);
				result = HttpUtility.urlEncodeToBytes(bytes, 0, bytes.Length);
			}
			return result;
		}

		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(HttpUtility.getString_0(107130391));
			}
			int num = bytes.Length;
			byte[] result;
			if (num == 0)
			{
				if (offset != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = bytes;
			}
			else
			{
				if (offset < 0 || offset >= num)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107145091));
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException(HttpUtility.getString_0(107350455));
				}
				result = ((count > 0) ? HttpUtility.urlEncodeToBytes(bytes, offset, count) : new byte[0]);
			}
			return result;
		}

		private static Dictionary<string, char> _entities;

		private static char[] _hexChars;

		private static object _sync;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
