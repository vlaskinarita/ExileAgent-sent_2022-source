using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public static class KeyToStringHelper
	{
		public static void smethod_0()
		{
			KeyToStringHelper.dictionary_0 = new Dictionary<Key, string>();
			foreach (object obj in Enum.GetValues(typeof(Key)))
			{
				Key key = (Key)obj;
				string value = key.ToString();
				switch (key)
				{
				case Key.None:
				case Key.Cancel:
				case Key.Back:
				case Key.LineFeed:
				case Key.Clear:
				case Key.KanaMode:
				case Key.JunjaMode:
				case Key.FinalMode:
				case Key.HanjaMode:
				case Key.Escape:
				case Key.ImeConvert:
				case Key.ImeNonConvert:
				case Key.ImeAccept:
				case Key.ImeModeChange:
				case Key.Select:
				case Key.Print:
				case Key.Execute:
				case Key.Snapshot:
				case Key.Help:
				case Key.LWin:
				case Key.RWin:
				case Key.Apps:
				case Key.Sleep:
				case Key.Add:
				case Key.Separator:
				case Key.NumLock:
				case Key.BrowserBack:
				case Key.BrowserForward:
				case Key.BrowserRefresh:
				case Key.BrowserStop:
				case Key.BrowserSearch:
				case Key.BrowserFavorites:
				case Key.BrowserHome:
				case Key.VolumeMute:
				case Key.VolumeDown:
				case Key.VolumeUp:
				case Key.MediaNextTrack:
				case Key.MediaPreviousTrack:
				case Key.MediaStop:
				case Key.MediaPlayPause:
				case Key.LaunchMail:
				case Key.SelectMedia:
				case Key.LaunchApplication1:
				case Key.LaunchApplication2:
				case Key.Oem1:
				case Key.OemPlus:
				case Key.OemComma:
				case Key.OemMinus:
				case Key.OemPeriod:
				case Key.Oem2:
				case Key.Oem3:
				case Key.AbntC1:
				case Key.AbntC2:
				case Key.Oem4:
				case Key.Oem5:
				case Key.Oem6:
				case Key.Oem7:
				case Key.Oem8:
				case Key.Oem102:
				case Key.ImeProcessed:
				case Key.System:
				case Key.OemAttn:
				case Key.OemFinish:
				case Key.OemCopy:
				case Key.OemAuto:
				case Key.OemEnlw:
				case Key.OemBackTab:
				case Key.Attn:
				case Key.CrSel:
				case Key.ExSel:
				case Key.EraseEof:
				case Key.Play:
				case Key.Zoom:
				case Key.NoName:
				case Key.Pa1:
				case Key.OemClear:
				case Key.DeadCharProcessed:
					continue;
				case Key.Tab:
					value = KeyToStringHelper.getString_0(107441729);
					break;
				case Key.Return:
					value = KeyToStringHelper.getString_0(107441779);
					break;
				case Key.Pause:
					value = KeyToStringHelper.getString_0(107390875);
					break;
				case Key.Capital:
					value = KeyToStringHelper.getString_0(107441756);
					break;
				case Key.Space:
					value = KeyToStringHelper.getString_0(107441746);
					break;
				case Key.Prior:
					value = KeyToStringHelper.getString_0(107441829);
					break;
				case Key.Next:
					value = KeyToStringHelper.getString_0(107441788);
					break;
				case Key.End:
					value = KeyToStringHelper.getString_0(107441737);
					break;
				case Key.Home:
					value = KeyToStringHelper.getString_0(107441732);
					break;
				case Key.Left:
					value = KeyToStringHelper.getString_0(107442203);
					break;
				case Key.Up:
					value = KeyToStringHelper.getString_0(107442217);
					break;
				case Key.Right:
					value = KeyToStringHelper.getString_0(107442226);
					break;
				case Key.Down:
					value = KeyToStringHelper.getString_0(107442212);
					break;
				case Key.Insert:
					value = KeyToStringHelper.getString_0(107441724);
					break;
				case Key.Delete:
					value = KeyToStringHelper.getString_0(107441719);
					break;
				case Key.D0:
				case Key.D1:
				case Key.D2:
				case Key.D3:
				case Key.D4:
				case Key.D5:
				case Key.D6:
				case Key.D7:
				case Key.D8:
				case Key.D9:
					value = key.ToString().Replace(KeyToStringHelper.getString_0(107441834), string.Empty);
					break;
				case Key.NumPad0:
				case Key.NumPad1:
				case Key.NumPad2:
				case Key.NumPad3:
				case Key.NumPad4:
				case Key.NumPad5:
				case Key.NumPad6:
				case Key.NumPad7:
				case Key.NumPad8:
				case Key.NumPad9:
					value = key.ToString().Replace(KeyToStringHelper.getString_0(107441770), string.Empty);
					break;
				case Key.Multiply:
					value = KeyToStringHelper.getString_0(107442171);
					break;
				case Key.Subtract:
					value = KeyToStringHelper.getString_0(107369743);
					break;
				case Key.Decimal:
					value = KeyToStringHelper.getString_0(107369878);
					break;
				case Key.Divide:
					value = KeyToStringHelper.getString_0(107373096);
					break;
				case Key.Scroll:
					value = KeyToStringHelper.getString_0(107442166);
					break;
				case Key.LeftShift:
				case Key.RightShift:
					value = KeyToStringHelper.getString_0(107441811);
					break;
				case Key.LeftCtrl:
				case Key.RightCtrl:
					value = KeyToStringHelper.getString_0(107441802);
					break;
				case Key.LeftAlt:
				case Key.RightAlt:
					value = KeyToStringHelper.getString_0(107441761);
					break;
				}
				if (!KeyToStringHelper.dictionary_0.ContainsKey(key))
				{
					KeyToStringHelper.dictionary_0.Add(key, value);
				}
			}
		}

		public static bool smethod_1(Key key_0)
		{
			return KeyToStringHelper.dictionary_0.ContainsKey(key_0);
		}

		public static string smethod_2(Key key_0)
		{
			string result;
			KeyToStringHelper.dictionary_0.TryGetValue(key_0, out result);
			return result;
		}

		public static List<Key> smethod_3()
		{
			return KeyToStringHelper.dictionary_0.Keys.ToList<Key>();
		}

		public static List<Keys> smethod_4()
		{
			return KeyToStringHelper.smethod_3().Select(new Func<Key, Keys>(KeyToStringHelper.<>c.<>9.method_0)).ToList<Keys>();
		}

		public static bool smethod_5(Key key_0)
		{
			return key_0 == Key.Tab || key_0 == Key.Capital || key_0 - Key.LeftShift <= 5;
		}

		static KeyToStringHelper()
		{
			Strings.CreateGetStringDelegate(typeof(KeyToStringHelper));
		}

		private static Dictionary<Key, string> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
