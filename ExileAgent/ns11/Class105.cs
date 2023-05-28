using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ns1;
using ns12;
using ns14;
using ns36;
using ns6;
using PoEv2;
using PoEv2.Features.Expedition;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns11
{
	internal sealed class Class105
	{
		public Class105(MainForm mainForm_1)
		{
			this.mainForm_0 = mainForm_1;
		}

		public unsafe void method_0(int int_0 = 0)
		{
			void* ptr = stackalloc byte[6];
			*(byte*)ptr = ((!File.Exists(Class120.string_1)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_10();
			}
			else
			{
				string text = File.ReadAllText(Class120.string_1);
				JsonSerializerSettings jsonSerializerSettings = Util.smethod_12();
				jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
				this.dictionary_0 = JsonConvert.DeserializeObject<Dictionary<ConfigOptions, object>>(text, jsonSerializerSettings);
				((byte*)ptr)[1] = ((this.dictionary_0 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					if ((!text.Contains(Class105.getString_0(107351345)) || text.Contains(Class105.getString_0(107351296))) && int_0 <= 0)
					{
						text = text.Replace(Class105.getString_0(107351632), Class105.getString_0(107351619));
						text = text.Replace(Class105.getString_0(107351296), Class105.getString_0(107351602));
						File.WriteAllText(Class120.string_1, text);
						this.method_0(++int_0);
						return;
					}
					MessageBox.Show(Class105.getString_0(107351283));
					((byte*)ptr)[2] = (File.Exists(Class120.string_2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						DialogResult dialogResult = MessageBox.Show(Class105.getString_0(107351218), Class105.getString_0(107351617), MessageBoxButtons.YesNo);
						((byte*)ptr)[3] = ((dialogResult == DialogResult.Yes) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							string contents = File.ReadAllText(Class120.string_2);
							File.WriteAllText(Class120.string_1, contents);
							this.method_0(0);
							return;
						}
					}
					Process.GetCurrentProcess().Kill();
				}
			}
			foreach (object obj in Enum.GetValues(typeof(ConfigOptions)))
			{
				ConfigOptions configOptions = (ConfigOptions)obj;
				((byte*)ptr)[4] = ((!this.dictionary_0.ContainsKey(configOptions)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					this.dictionary_0.Add(configOptions, this.method_10()[configOptions]);
				}
				((byte*)ptr)[5] = ((configOptions == ConfigOptions.PricingCurrencyType) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0 && (this.dictionary_0[ConfigOptions.PricingCurrencyType].ToString() == Class105.getString_0(107351549) || this.dictionary_0[ConfigOptions.PricingCurrencyType].ToString().smethod_25()))
				{
					this.dictionary_0[ConfigOptions.PricingCurrencyType] = Class105.getString_0(107406421);
				}
			}
			this.mainForm_0.Invoke(new Action(this.method_12));
			this.method_1();
		}

		[DebuggerStepThrough]
		public Task method_1()
		{
			Class105.Class106 @class = new Class105.Class106();
			@class.class105_0 = this;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<Class105.Class106>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		public T method_2<T>(ConfigOptions configOptions_0)
		{
			T result;
			if (!this.dictionary_0.ContainsKey(configOptions_0))
			{
				result = default(T);
			}
			else
			{
				result = (T)((object)this.dictionary_0[configOptions_0]);
			}
			return result;
		}

		public string method_3(ConfigOptions configOptions_0)
		{
			string result;
			if (!this.dictionary_0.ContainsKey(configOptions_0))
			{
				result = string.Empty;
			}
			else
			{
				result = (((string)this.dictionary_0[configOptions_0]) ?? string.Empty);
			}
			return result;
		}

		public unsafe bool method_4(ConfigOptions configOptions_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (((bool)this.dictionary_0[configOptions_0]) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe int method_5(ConfigOptions configOptions_0)
		{
			void* ptr = stackalloc byte[9];
			((byte*)ptr)[8] = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)((byte*)ptr + 4) = 0;
			}
			else
			{
				int.TryParse(this.dictionary_0[configOptions_0].ToString(), out *(int*)ptr);
				*(int*)((byte*)ptr + 4) = *(int*)ptr;
			}
			return *(int*)((byte*)ptr + 4);
		}

		public unsafe decimal method_6(ConfigOptions configOptions_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			decimal result;
			if (*(sbyte*)ptr != 0)
			{
				result = 0m;
			}
			else
			{
				((byte*)ptr)[1] = ((this.dictionary_0[configOptions_0] is decimal) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = (decimal)this.dictionary_0[configOptions_0];
				}
				else
				{
					result = Convert.ToDecimal((double)this.dictionary_0[configOptions_0]);
				}
			}
			return result;
		}

		public unsafe int method_7(ConfigOptions configOptions_0)
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[4] = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = 0;
			}
			else
			{
				*(int*)ptr = (int)this.method_6(configOptions_0);
			}
			return *(int*)ptr;
		}

		public unsafe List<T> method_8<T>(ConfigOptions configOptions_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			List<T> result;
			if (*(sbyte*)ptr != 0)
			{
				result = new List<T>();
			}
			else
			{
				((byte*)ptr)[1] = ((!(this.dictionary_0[configOptions_0] is List<T>)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = new List<T>();
				}
				else
				{
					result = (((List<T>)this.dictionary_0[configOptions_0]) ?? new List<T>());
				}
			}
			return result;
		}

		public unsafe void method_9(ConfigOptions configOptions_0, object object_0, bool bool_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!this.dictionary_0.ContainsKey(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				this.dictionary_0[configOptions_0] = object_0;
				((byte*)ptr)[1] = (bool_0 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.method_1();
				}
			}
		}

		private Dictionary<ConfigOptions, object> method_10()
		{
			return new Dictionary<ConfigOptions, object>
			{
				{
					ConfigOptions.RunAsUser,
					string.Empty
				},
				{
					ConfigOptions.RunAsPassword,
					string.Empty
				},
				{
					ConfigOptions.FormLocation,
					Util.smethod_26(new Point(0, 0))
				},
				{
					ConfigOptions.FlipFormLocation,
					Util.smethod_26(new Point(0, 0))
				},
				{
					ConfigOptions.GameLocation,
					Util.smethod_26(new Point(0, 0))
				},
				{
					ConfigOptions.PopoutChatLocation,
					Util.smethod_26(new Point(0, 0))
				},
				{
					ConfigOptions.UserAgent,
					string.Empty
				},
				{
					ConfigOptions.CurrencyTabSize,
					Util.smethod_27(Class238.CurrencyStash.Size)
				},
				{
					ConfigOptions.CurrencyTabLocation,
					Util.smethod_26(new Point(0, 0))
				},
				{
					ConfigOptions.LastImportDirectory,
					Class395.SettingsPath
				},
				{
					ConfigOptions.GamePath,
					string.Empty
				},
				{
					ConfigOptions.GameClient,
					this.method_11()
				},
				{
					ConfigOptions.UsingSteam,
					false
				},
				{
					ConfigOptions.Resolution,
					0
				},
				{
					ConfigOptions.WindowMode,
					0
				},
				{
					ConfigOptions.LimitedUser,
					true
				},
				{
					ConfigOptions.ForceNewInstance,
					true
				},
				{
					ConfigOptions.ShowInGameChat,
					true
				},
				{
					ConfigOptions.ChatOpacity,
					7
				},
				{
					ConfigOptions.NetworkingMode,
					Class105.getString_0(107388370)
				},
				{
					ConfigOptions.LaunchArguments,
					Class105.getString_0(107351544)
				},
				{
					ConfigOptions.InstanceZone,
					0
				},
				{
					ConfigOptions.DisableAero,
					true
				},
				{
					ConfigOptions.AuthKey,
					string.Empty
				},
				{
					ConfigOptions.AccountName,
					string.Empty
				},
				{
					ConfigOptions.POESESSID,
					string.Empty
				},
				{
					ConfigOptions.League,
					string.Empty
				},
				{
					ConfigOptions.CharacterSelected,
					-1
				},
				{
					ConfigOptions.Email,
					string.Empty
				},
				{
					ConfigOptions.Password,
					string.Empty
				},
				{
					ConfigOptions.AutoLogin,
					false
				},
				{
					ConfigOptions.PreventAFK,
					true
				},
				{
					ConfigOptions.PredictServerLag,
					false
				},
				{
					ConfigOptions.ReconnectEnabled,
					false
				},
				{
					ConfigOptions.ReconnectAttempts,
					3m
				},
				{
					ConfigOptions.ReconnectDelay,
					30m
				},
				{
					ConfigOptions.KillHotkey,
					new List<string>
					{
						Class105.getString_0(107351519)
					}
				},
				{
					ConfigOptions.PauseHotkey,
					new List<string>
					{
						Class105.getString_0(107351514)
					}
				},
				{
					ConfigOptions.StopAfterTradesHotkey,
					new List<string>
					{
						Class105.getString_0(107351509)
					}
				},
				{
					ConfigOptions.JsonTradeLog,
					false
				},
				{
					ConfigOptions.AllowCleanInventory,
					false
				},
				{
					ConfigOptions.EnableGoAFK,
					true
				},
				{
					ConfigOptions.GoAFKMin,
					60m
				},
				{
					ConfigOptions.GoAFKMax,
					120m
				},
				{
					ConfigOptions.StayAFKMin,
					15m
				},
				{
					ConfigOptions.StayAFKMax,
					25m
				},
				{
					ConfigOptions.EnableLogout,
					false
				},
				{
					ConfigOptions.LogoutHours,
					12m
				},
				{
					ConfigOptions.LogoutMinutes,
					0m
				},
				{
					ConfigOptions.EnableLogin,
					false
				},
				{
					ConfigOptions.LoginHours,
					12m
				},
				{
					ConfigOptions.LoginMinutes,
					0m
				},
				{
					ConfigOptions.AFKMessagesList,
					new List<string>()
				},
				{
					ConfigOptions.CurrencyDumpTab,
					string.Empty
				},
				{
					ConfigOptions.MapsDumpTab,
					string.Empty
				},
				{
					ConfigOptions.FragmentsDumpTab,
					string.Empty
				},
				{
					ConfigOptions.CardDumpTab,
					string.Empty
				},
				{
					ConfigOptions.EssenceDumpTab,
					string.Empty
				},
				{
					ConfigOptions.DelveDumpTab,
					string.Empty
				},
				{
					ConfigOptions.OtherDumpTab,
					string.Empty
				},
				{
					ConfigOptions.UseDumpTabs,
					false
				},
				{
					ConfigOptions.ProcessRemoveOnlyTabs,
					false
				},
				{
					ConfigOptions.DumpTabList,
					new List<int>()
				},
				{
					ConfigOptions.HarvestDumpTab,
					string.Empty
				},
				{
					ConfigOptions.DeliriumDumpTab,
					string.Empty
				},
				{
					ConfigOptions.MetamorphDumpTab,
					string.Empty
				},
				{
					ConfigOptions.BlightDumpTab,
					string.Empty
				},
				{
					ConfigOptions.UltimatumDumpTab,
					string.Empty
				},
				{
					ConfigOptions.IncubatorDumpTab,
					string.Empty
				},
				{
					ConfigOptions.VialDumpTab,
					string.Empty
				},
				{
					ConfigOptions.StackedDeckDumpTab,
					string.Empty
				},
				{
					ConfigOptions.StashProfileSelected,
					0
				},
				{
					ConfigOptions.StashProfiles,
					new List<string>
					{
						Class105.getString_0(107351536)
					}
				},
				{
					ConfigOptions.StashProfileTabs,
					new Dictionary<int, List<int>>
					{
						{
							0,
							new List<int>()
						}
					}
				},
				{
					ConfigOptions.TradingPriority,
					new List<string>(Class120.list_3)
				},
				{
					ConfigOptions.AcceptOtherCurrency,
					true
				},
				{
					ConfigOptions.AcceptedCurrencyList,
					new List<string>(Class102.list_3)
				},
				{
					ConfigOptions.UnexpectedPercentageDiscount,
					0m
				},
				{
					ConfigOptions.CurrencyOnlyFromTab,
					false
				},
				{
					ConfigOptions.UseCustomExaltPrice,
					false
				},
				{
					ConfigOptions.ExaltedOrbValue,
					150m
				},
				{
					ConfigOptions.ScreenshotTrades,
					false
				},
				{
					ConfigOptions.UseTradeSafety,
					true
				},
				{
					ConfigOptions.DisableSameCurrency,
					true
				},
				{
					ConfigOptions.DisableSellingEngineer,
					true
				},
				{
					ConfigOptions.DisableSellingFacetors,
					true
				},
				{
					ConfigOptions.DisableSellingDecimalChaos,
					true
				},
				{
					ConfigOptions.DisableSellingCheapExalted,
					false
				},
				{
					ConfigOptions.CheapExaltedValue,
					100m
				},
				{
					ConfigOptions.DisableSellingCheapChaos,
					false
				},
				{
					ConfigOptions.CheapChaosList,
					new List<string>(Class102.string_9)
				},
				{
					ConfigOptions.ExcludedTabList,
					new List<int>()
				},
				{
					ConfigOptions.MessagesAfterTradeEnabled,
					false
				},
				{
					ConfigOptions.MessagesAfterTradeList,
					new List<string>(Class102.list_0)
				},
				{
					ConfigOptions.IgnoreListEnabled,
					false
				},
				{
					ConfigOptions.IgnoreList,
					new List<string>()
				},
				{
					ConfigOptions.IgnoreFailedTraders,
					false
				},
				{
					ConfigOptions.IgnoreFailedTradersLength,
					60m
				},
				{
					ConfigOptions.IgnoreScamTraders,
					false
				},
				{
					ConfigOptions.IgnoreScamTradersLength,
					60m
				},
				{
					ConfigOptions.SoldMessageEnabled,
					false
				},
				{
					ConfigOptions.SoldMessageList,
					new List<string>(Class102.list_1)
				},
				{
					ConfigOptions.MaxTradeAttempts,
					4m
				},
				{
					ConfigOptions.PutCurrencyBack,
					false
				},
				{
					ConfigOptions.MaxPendingTrades,
					4m
				},
				{
					ConfigOptions.MaxTimeBeforeOrderExpires,
					1m
				},
				{
					ConfigOptions.IgnoreRepeatPlayerEnabled,
					true
				},
				{
					ConfigOptions.IgnoreRepeatPlayerTime,
					10m
				},
				{
					ConfigOptions.HideoutLogoutEnabled,
					false
				},
				{
					ConfigOptions.HideoutLogoutTime,
					5m
				},
				{
					ConfigOptions.HideoutLogoutPlayerTime,
					10m
				},
				{
					ConfigOptions.SkipPerandusCoinListings,
					true
				},
				{
					ConfigOptions.StockLimitExcludePriced,
					true
				},
				{
					ConfigOptions.DecimalCurrencyList,
					new List<DecimalCurrencyListItem>(Class391.list_0)
				},
				{
					ConfigOptions.BuyingPriority,
					new List<string>(Class391.list_1)
				},
				{
					ConfigOptions.MaxBuyTradeAttempts,
					4m
				},
				{
					ConfigOptions.ClearIgnoreListMin,
					45m
				},
				{
					ConfigOptions.ClearIgnoreListMax,
					75m
				},
				{
					ConfigOptions.FlippingEnabled,
					false
				},
				{
					ConfigOptions.UpdateMinFlippingPrices,
					15m
				},
				{
					ConfigOptions.UpdateMaxFlippingPrices,
					20m
				},
				{
					ConfigOptions.FlippingTab,
					string.Empty
				},
				{
					ConfigOptions.FlippingList,
					new List<FlippingListItem>()
				},
				{
					ConfigOptions.ResetInstanceAfterUpdate,
					true
				},
				{
					ConfigOptions.IgnoreBotsWhileFlipping,
					true
				},
				{
					ConfigOptions.IgnorePrivateAccounts,
					false
				},
				{
					ConfigOptions.IgnoreNewAccounts,
					false
				},
				{
					ConfigOptions.IgnoreNewAccountDays,
					180m
				},
				{
					ConfigOptions.NextFlipUpdateTime,
					default(DateTime)
				},
				{
					ConfigOptions.EnableLiveSearch,
					true
				},
				{
					ConfigOptions.EnableItemBuying,
					true
				},
				{
					ConfigOptions.EnableBulkBuying,
					true
				},
				{
					ConfigOptions.LiveSearchList,
					new List<LiveSearchListItem>()
				},
				{
					ConfigOptions.BulkBuyingList,
					new List<BulkBuyingListItem>()
				},
				{
					ConfigOptions.ItemBuyingList,
					new List<ItemBuyingListItem>()
				},
				{
					ConfigOptions.ItemBuyingMinTime,
					20m
				},
				{
					ConfigOptions.ItemBuyingMaxTime,
					30m
				},
				{
					ConfigOptions.BulkBuyingMinTime,
					20m
				},
				{
					ConfigOptions.BulkBuyingMaxTime,
					30m
				},
				{
					ConfigOptions.PriceStashList,
					new List<int>()
				},
				{
					ConfigOptions.ForumThreadEnabled,
					false
				},
				{
					ConfigOptions.RepriceItems,
					false
				},
				{
					ConfigOptions.ForumThreadId,
					string.Empty
				},
				{
					ConfigOptions.StartAfterPricing,
					false
				},
				{
					ConfigOptions.PricingCurrencyType,
					Class105.getString_0(107406421)
				},
				{
					ConfigOptions.ListingsToSkip,
					3m
				},
				{
					ConfigOptions.ListingsToTake,
					5m
				},
				{
					ConfigOptions.UsePoePrices,
					false
				},
				{
					ConfigOptions.SkipPerandusCoins,
					true
				},
				{
					ConfigOptions.PriceWithDiscount,
					false
				},
				{
					ConfigOptions.DiscountAmount,
					0m
				},
				{
					ConfigOptions.DiscountCurrency,
					Class105.getString_0(107391542)
				},
				{
					ConfigOptions.BulkTypeList,
					new List<string>(Class102.list_4)
				},
				{
					ConfigOptions.PricingDND,
					true
				},
				{
					ConfigOptions.UniqueListings,
					true
				},
				{
					ConfigOptions.CraftSpeed,
					7
				},
				{
					ConfigOptions.UseCraftingSlot,
					true
				},
				{
					ConfigOptions.CraftCurrencyTab,
					string.Empty
				},
				{
					ConfigOptions.CraftMoreItems,
					false
				},
				{
					ConfigOptions.CraftMoreItemsTab,
					string.Empty
				},
				{
					ConfigOptions.StartAfterCrafting,
					false
				},
				{
					ConfigOptions.AltLimit,
					0m
				},
				{
					ConfigOptions.AltUseScourTransmute,
					true
				},
				{
					ConfigOptions.AltUseRegal,
					false
				},
				{
					ConfigOptions.AltUseAug,
					false
				},
				{
					ConfigOptions.AltAndOr,
					Class105.getString_0(107363504)
				},
				{
					ConfigOptions.RareUseScourAlch,
					true
				},
				{
					ConfigOptions.RareChaosLimit,
					0m
				},
				{
					ConfigOptions.RareCraftType,
					Class105.getString_0(107401447)
				},
				{
					ConfigOptions.RareAndOr,
					Class105.getString_0(107363504)
				},
				{
					ConfigOptions.ChanceChanceLimit,
					0m
				},
				{
					ConfigOptions.SocketJewellersLimit,
					0m
				},
				{
					ConfigOptions.SocketFusingLimit,
					0m
				},
				{
					ConfigOptions.SocketMinSockets,
					6m
				},
				{
					ConfigOptions.SocketMinLinks,
					6m
				},
				{
					ConfigOptions.MapScourLimit,
					0m
				},
				{
					ConfigOptions.MapAlchLimit,
					0m
				},
				{
					ConfigOptions.MapChaosLimit,
					0m
				},
				{
					ConfigOptions.MapMinQuality,
					20m
				},
				{
					ConfigOptions.MapMinQuantity,
					0m
				},
				{
					ConfigOptions.MapMinPacksize,
					0m
				},
				{
					ConfigOptions.MapPacksizeForced,
					false
				},
				{
					ConfigOptions.MapCraftChoice,
					Class105.getString_0(107401489)
				},
				{
					ConfigOptions.MapCorrupt,
					false
				},
				{
					ConfigOptions.MapCraftStashTab,
					string.Empty
				},
				{
					ConfigOptions.MapPreventedMods,
					new List<string>()
				},
				{
					ConfigOptions.MapForcedMods,
					new List<string>()
				},
				{
					ConfigOptions.MapForcedCount,
					Class105.getString_0(107402797)
				},
				{
					ConfigOptions.VaalCraftCurrency,
					Class105.getString_0(107397001)
				},
				{
					ConfigOptions.VaalCraftStash,
					string.Empty
				},
				{
					ConfigOptions.GwennenItemList,
					new List<string>(Gwennen.string_0)
				},
				{
					ConfigOptions.GwennenStashList,
					new List<int>()
				},
				{
					ConfigOptions.GwennenNonUniqueAction,
					0
				},
				{
					ConfigOptions.MuleStashList,
					new List<int>()
				},
				{
					ConfigOptions.BeastStashList,
					new List<int>()
				},
				{
					ConfigOptions.BeastStartAfterFinished,
					false
				},
				{
					ConfigOptions.BeastOrbLimit,
					0m
				},
				{
					ConfigOptions.BeastOpenBestiaryTiming,
					400m
				},
				{
					ConfigOptions.BeastInventoryClickTiming,
					300m
				},
				{
					ConfigOptions.StackedDeckList,
					new List<int>()
				},
				{
					ConfigOptions.OnWhisperReceived,
					false
				},
				{
					ConfigOptions.OnBotStopped,
					false
				},
				{
					ConfigOptions.OnTradeRequest,
					false
				},
				{
					ConfigOptions.OnDisconnect,
					false
				},
				{
					ConfigOptions.OnSuccessfulTrade,
					false
				},
				{
					ConfigOptions.OnFailedTrade,
					false
				},
				{
					ConfigOptions.OnScamAttempt,
					false
				},
				{
					ConfigOptions.OnBuyLivesearchHit,
					false
				},
				{
					ConfigOptions.OnBuyWhisper,
					false
				},
				{
					ConfigOptions.OnBuyPartyAccepted,
					false
				},
				{
					ConfigOptions.OnBuyScamAttempt,
					false
				},
				{
					ConfigOptions.OnCraftingItemComplete,
					false
				},
				{
					ConfigOptions.OnMuleInventoryEmpty,
					false
				},
				{
					ConfigOptions.OnMaxFailedNotification,
					false
				},
				{
					ConfigOptions.MaxFailedNotificationThreshold,
					1m
				},
				{
					ConfigOptions.PushBulletEnabled,
					false
				},
				{
					ConfigOptions.PushBulletToken,
					string.Empty
				},
				{
					ConfigOptions.DiscordEnabled,
					false
				},
				{
					ConfigOptions.DiscordWebHookUrl,
					string.Empty
				},
				{
					ConfigOptions.DiscordAvatarUrl,
					string.Empty
				},
				{
					ConfigOptions.DiscordUsername,
					string.Empty
				},
				{
					ConfigOptions.DiscordUsernameCheck,
					false
				},
				{
					ConfigOptions.SelectedPreset,
					Class105.getString_0(107392424)
				},
				{
					ConfigOptions.CustomSettings,
					new Dictionary<NumericUpDown, decimal>()
				},
				{
					ConfigOptions.ChangeStashTab,
					200m
				},
				{
					ConfigOptions.LoadStashTab,
					500m
				},
				{
					ConfigOptions.ClickItemFromStash,
					200m
				},
				{
					ConfigOptions.ClickItemFromInventory,
					200m
				},
				{
					ConfigOptions.ClickItemToTrade,
					0m
				},
				{
					ConfigOptions.ClipboardTiming,
					350m
				},
				{
					ConfigOptions.ScanItemInTrade,
					0m
				},
				{
					ConfigOptions.MouseMoveSpeed,
					7
				},
				{
					ConfigOptions.LaunchGameTiming,
					5m
				},
				{
					ConfigOptions.MaxGameLoadTime,
					2m
				},
				{
					ConfigOptions.TimeBeforeSaleExpires,
					60m
				},
				{
					ConfigOptions.MaxTradeTime,
					40m
				},
				{
					ConfigOptions.MaxTimeAcceptTrade,
					20m
				},
				{
					ConfigOptions.IntervalBetweenTrades,
					3m
				},
				{
					ConfigOptions.PartyInvite,
					0m
				},
				{
					ConfigOptions.PartyKick,
					200m
				},
				{
					ConfigOptions.AcceptDeclineRequest,
					200m
				},
				{
					ConfigOptions.Whisper,
					200m
				},
				{
					ConfigOptions.TimeBeforeBuyInviteExpires,
					20m
				},
				{
					ConfigOptions.BuyMaxTradeTime,
					50m
				},
				{
					ConfigOptions.BuyMaxTimeAcceptTrade,
					20m
				},
				{
					ConfigOptions.BuyAcceptDeclineRequest,
					200m
				},
				{
					ConfigOptions.TimeBeforePartyClosed,
					10m
				},
				{
					ConfigOptions.SetNote,
					450m
				},
				{
					ConfigOptions.CleanerClickSpeed,
					150m
				},
				{
					ConfigOptions.VendorRecipeStashList,
					new List<int>()
				},
				{
					ConfigOptions.VendorRecipeVendor,
					Class105.getString_0(107351523)
				},
				{
					ConfigOptions.VendorRecipeRecipeType,
					Class105.getString_0(107364718)
				},
				{
					ConfigOptions.VendorRecipeIdentifiedType,
					Class105.getString_0(107365348)
				},
				{
					ConfigOptions.VendorBelowPriceValue,
					1m
				},
				{
					ConfigOptions.VendorBelowPriceCurrency,
					Class105.getString_0(107391542)
				}
			};
		}

		private string method_11()
		{
			string result;
			if (this.method_4(ConfigOptions.UsingSteam))
			{
				result = Class105.getString_0(107394847);
			}
			else
			{
				result = Class105.getString_0(107395543);
			}
			return result;
		}

		[CompilerGenerated]
		private void method_12()
		{
			this.dictionary_0[ConfigOptions.RunAsUser] = this.method_3(ConfigOptions.RunAsUser);
			this.dictionary_0[ConfigOptions.RunAsPassword] = this.method_3(ConfigOptions.RunAsPassword);
			this.mainForm_0.Location = Util.smethod_18(this.method_3(ConfigOptions.FormLocation));
			this.dictionary_0[ConfigOptions.FlipFormLocation] = Util.smethod_18(this.method_3(ConfigOptions.FlipFormLocation));
			this.dictionary_0[ConfigOptions.GameLocation] = Util.smethod_18(this.method_3(ConfigOptions.GameLocation));
			this.dictionary_0[ConfigOptions.PopoutChatLocation] = Util.smethod_18(this.method_3(ConfigOptions.PopoutChatLocation));
			this.dictionary_0[ConfigOptions.UserAgent] = this.method_3(ConfigOptions.UserAgent);
			this.dictionary_0[ConfigOptions.CurrencyTabSize] = Util.smethod_19(this.method_3(ConfigOptions.CurrencyTabSize));
			this.dictionary_0[ConfigOptions.CurrencyTabLocation] = Util.smethod_18(this.method_3(ConfigOptions.CurrencyTabLocation));
			this.dictionary_0[ConfigOptions.LastImportDirectory] = this.method_3(ConfigOptions.LastImportDirectory);
			this.mainForm_0.textBox_8.Text = this.method_3(ConfigOptions.GamePath);
			this.dictionary_0[ConfigOptions.UsingSteam] = this.method_4(ConfigOptions.UsingSteam);
			this.mainForm_0.comboBox_66.SelectedItem = this.method_3(ConfigOptions.GameClient);
			this.mainForm_0.comboBox_11.SelectedIndex = this.method_5(ConfigOptions.Resolution);
			this.mainForm_0.comboBox_10.SelectedIndex = this.method_5(ConfigOptions.WindowMode);
			this.mainForm_0.checkBox_18.Checked = this.method_4(ConfigOptions.LimitedUser);
			this.mainForm_0.checkBox_15.Checked = this.method_4(ConfigOptions.ForceNewInstance);
			this.mainForm_0.checkBox_22.Checked = this.method_4(ConfigOptions.ShowInGameChat);
			this.mainForm_0.trackBar_2.Value = this.method_5(ConfigOptions.ChatOpacity);
			this.mainForm_0.comboBox_65.SelectedItem = this.method_3(ConfigOptions.NetworkingMode);
			this.mainForm_0.textBox_18.Text = this.method_3(ConfigOptions.LaunchArguments);
			this.mainForm_0.comboBox_70.SelectedIndex = this.method_5(ConfigOptions.InstanceZone);
			this.mainForm_0.checkBox_62.Checked = this.method_4(ConfigOptions.DisableAero);
			this.mainForm_0.textBox_3.Text = this.method_3(ConfigOptions.AuthKey);
			this.mainForm_0.textBox_5.Text = this.method_3(ConfigOptions.AccountName);
			this.mainForm_0.textBox_4.Text = this.method_3(ConfigOptions.POESESSID);
			this.dictionary_0[ConfigOptions.League] = this.method_3(ConfigOptions.League);
			this.mainForm_0.textBox_0.Text = this.method_3(ConfigOptions.Email);
			this.mainForm_0.textBox_1.Text = this.method_3(ConfigOptions.Password);
			this.mainForm_0.checkBox_4.Checked = this.method_4(ConfigOptions.AutoLogin);
			this.mainForm_0.checkBox_3.Checked = this.method_4(ConfigOptions.PreventAFK);
			this.mainForm_0.checkBox_2.Checked = this.method_4(ConfigOptions.PredictServerLag);
			this.mainForm_0.checkBox_5.Checked = this.method_4(ConfigOptions.ReconnectEnabled);
			this.mainForm_0.numericUpDown_9.smethod_2(this.method_6(ConfigOptions.ReconnectAttempts));
			this.mainForm_0.numericUpDown_8.smethod_2(this.method_6(ConfigOptions.ReconnectDelay));
			this.mainForm_0.textBox_2.Text = string.Join(Class105.getString_0(107351482), this.method_8<string>(ConfigOptions.KillHotkey));
			this.mainForm_0.textBox_15.Text = string.Join(Class105.getString_0(107351482), this.method_8<string>(ConfigOptions.StopAfterTradesHotkey));
			this.mainForm_0.textBox_16.Text = string.Join(Class105.getString_0(107351482), this.method_8<string>(ConfigOptions.PauseHotkey));
			this.mainForm_0.checkBox_49.Checked = this.method_4(ConfigOptions.JsonTradeLog);
			this.mainForm_0.checkBox_80.Checked = this.method_4(ConfigOptions.AllowCleanInventory);
			this.mainForm_0.checkBox_1.Checked = this.method_4(ConfigOptions.EnableGoAFK);
			this.mainForm_0.numericUpDown_4.smethod_2(this.method_6(ConfigOptions.GoAFKMin));
			this.mainForm_0.numericUpDown_5.smethod_2(this.method_6(ConfigOptions.GoAFKMax));
			this.mainForm_0.numericUpDown_7.smethod_2(this.method_6(ConfigOptions.StayAFKMin));
			this.mainForm_0.numericUpDown_6.smethod_2(this.method_6(ConfigOptions.StayAFKMax));
			this.mainForm_0.checkBox_0.Checked = this.method_4(ConfigOptions.EnableLogout);
			this.mainForm_0.numericUpDown_0.smethod_2(this.method_6(ConfigOptions.LogoutHours));
			this.mainForm_0.numericUpDown_1.smethod_2(this.method_6(ConfigOptions.LogoutMinutes));
			this.mainForm_0.checkBox_17.Checked = this.method_4(ConfigOptions.EnableLogin);
			this.mainForm_0.numericUpDown_3.smethod_2(this.method_6(ConfigOptions.LoginHours));
			this.mainForm_0.numericUpDown_2.smethod_2(this.method_6(ConfigOptions.LoginMinutes));
			this.mainForm_0.comboBox_5.SelectedItem = this.method_3(ConfigOptions.CurrencyDumpTab);
			this.mainForm_0.comboBox_9.SelectedItem = this.method_3(ConfigOptions.MapsDumpTab);
			this.mainForm_0.comboBox_8.SelectedItem = this.method_3(ConfigOptions.FragmentsDumpTab);
			this.mainForm_0.comboBox_3.SelectedItem = this.method_3(ConfigOptions.CardDumpTab);
			this.mainForm_0.comboBox_6.SelectedItem = this.method_3(ConfigOptions.EssenceDumpTab);
			this.mainForm_0.comboBox_7.SelectedItem = this.method_3(ConfigOptions.DelveDumpTab);
			this.mainForm_0.comboBox_4.SelectedItem = this.method_3(ConfigOptions.OtherDumpTab);
			this.mainForm_0.checkBox_6.Checked = this.method_4(ConfigOptions.UseDumpTabs);
			this.mainForm_0.checkBox_7.Checked = this.method_4(ConfigOptions.ProcessRemoveOnlyTabs);
			this.mainForm_0.comboBox_26.SelectedItem = this.method_3(ConfigOptions.HarvestDumpTab);
			this.mainForm_0.comboBox_23.SelectedItem = this.method_3(ConfigOptions.DeliriumDumpTab);
			this.mainForm_0.comboBox_22.SelectedItem = this.method_3(ConfigOptions.MetamorphDumpTab);
			this.mainForm_0.comboBox_21.SelectedItem = this.method_3(ConfigOptions.BlightDumpTab);
			this.mainForm_0.comboBox_64.SelectedItem = this.method_3(ConfigOptions.UltimatumDumpTab);
			this.mainForm_0.comboBox_16.SelectedItem = this.method_3(ConfigOptions.IncubatorDumpTab);
			this.mainForm_0.comboBox_15.SelectedItem = this.method_3(ConfigOptions.VialDumpTab);
			this.mainForm_0.comboBox_14.SelectedItem = this.method_3(ConfigOptions.StackedDeckDumpTab);
			this.dictionary_0[ConfigOptions.StashProfileSelected] = this.method_5(ConfigOptions.StashProfileSelected);
			this.dictionary_0[ConfigOptions.StashProfiles] = this.method_8<string>(ConfigOptions.StashProfiles);
			this.dictionary_0[ConfigOptions.StashProfileTabs] = this.method_2<Dictionary<int, List<int>>>(ConfigOptions.StashProfileTabs);
			ListBox.ObjectCollection items = this.mainForm_0.listBox_1.Items;
			object[] items2 = this.method_8<string>(ConfigOptions.TradingPriority).ToArray();
			items.AddRange(items2);
			this.mainForm_0.checkBox_52.Checked = this.method_4(ConfigOptions.AcceptOtherCurrency);
			this.mainForm_0.numericUpDown_62.smethod_2(this.method_6(ConfigOptions.UnexpectedPercentageDiscount));
			this.mainForm_0.checkBox_64.Checked = this.method_4(ConfigOptions.CurrencyOnlyFromTab);
			this.mainForm_0.checkBox_73.Checked = this.method_4(ConfigOptions.DisableSameCurrency);
			this.mainForm_0.checkBox_75.Checked = this.method_4(ConfigOptions.UseCustomExaltPrice);
			this.mainForm_0.numericUpDown_74.smethod_2(this.method_6(ConfigOptions.ExaltedOrbValue));
			this.mainForm_0.checkBox_78.Checked = this.method_4(ConfigOptions.ScreenshotTrades);
			this.mainForm_0.checkBox_72.Checked = this.method_4(ConfigOptions.UseTradeSafety);
			this.mainForm_0.checkBox_71.Checked = this.method_4(ConfigOptions.DisableSellingEngineer);
			this.mainForm_0.checkBox_70.Checked = this.method_4(ConfigOptions.DisableSellingFacetors);
			this.mainForm_0.checkBox_69.Checked = this.method_4(ConfigOptions.DisableSellingDecimalChaos);
			this.mainForm_0.checkBox_68.Checked = this.method_4(ConfigOptions.DisableSellingCheapExalted);
			this.mainForm_0.numericUpDown_72.smethod_2(this.method_6(ConfigOptions.CheapExaltedValue));
			this.mainForm_0.checkBox_67.Checked = this.method_4(ConfigOptions.DisableSellingCheapChaos);
			this.mainForm_0.checkBox_54.Checked = this.method_4(ConfigOptions.MessagesAfterTradeEnabled);
			this.mainForm_0.checkBox_55.Checked = this.method_4(ConfigOptions.IgnoreListEnabled);
			this.mainForm_0.checkBox_57.Checked = this.method_4(ConfigOptions.IgnoreFailedTraders);
			this.mainForm_0.numericUpDown_64.smethod_2(this.method_6(ConfigOptions.IgnoreFailedTradersLength));
			this.mainForm_0.checkBox_56.Checked = this.method_4(ConfigOptions.IgnoreScamTraders);
			this.mainForm_0.numericUpDown_63.smethod_2(this.method_6(ConfigOptions.IgnoreScamTradersLength));
			this.mainForm_0.checkBox_53.Checked = this.method_4(ConfigOptions.SoldMessageEnabled);
			this.mainForm_0.numericUpDown_70.smethod_2(this.method_6(ConfigOptions.MaxTradeAttempts));
			this.mainForm_0.checkBox_60.Checked = this.method_4(ConfigOptions.PutCurrencyBack);
			this.mainForm_0.numericUpDown_69.smethod_2(this.method_6(ConfigOptions.MaxPendingTrades));
			this.mainForm_0.numericUpDown_68.smethod_2(this.method_6(ConfigOptions.MaxTimeBeforeOrderExpires));
			this.mainForm_0.checkBox_59.Checked = this.method_4(ConfigOptions.IgnoreRepeatPlayerEnabled);
			this.mainForm_0.numericUpDown_67.smethod_2(this.method_6(ConfigOptions.IgnoreRepeatPlayerTime));
			this.mainForm_0.checkBox_58.Checked = this.method_4(ConfigOptions.HideoutLogoutEnabled);
			this.mainForm_0.numericUpDown_66.smethod_2(this.method_6(ConfigOptions.HideoutLogoutTime));
			this.mainForm_0.numericUpDown_65.smethod_2(this.method_6(ConfigOptions.HideoutLogoutPlayerTime));
			this.mainForm_0.checkBox_31.Checked = this.method_4(ConfigOptions.SkipPerandusCoinListings);
			this.mainForm_0.checkBox_76.Checked = this.method_4(ConfigOptions.StockLimitExcludePriced);
			this.dictionary_0[ConfigOptions.DecimalCurrencyList] = this.method_8<DecimalCurrencyListItem>(ConfigOptions.DecimalCurrencyList);
			ListBox.ObjectCollection items3 = this.mainForm_0.listBox_0.Items;
			items2 = this.method_8<string>(ConfigOptions.BuyingPriority).ToArray();
			items3.AddRange(items2);
			this.mainForm_0.numericUpDown_57.smethod_2(this.method_6(ConfigOptions.MaxBuyTradeAttempts));
			this.mainForm_0.numericUpDown_61.smethod_2(this.method_6(ConfigOptions.ClearIgnoreListMin));
			this.mainForm_0.numericUpDown_60.smethod_2(this.method_6(ConfigOptions.ClearIgnoreListMax));
			this.mainForm_0.checkBox_19.Checked = this.method_4(ConfigOptions.FlippingEnabled);
			this.mainForm_0.numericUpDown_29.smethod_2(this.method_6(ConfigOptions.UpdateMinFlippingPrices));
			this.mainForm_0.numericUpDown_30.smethod_2(this.method_6(ConfigOptions.UpdateMaxFlippingPrices));
			this.mainForm_0.comboBox_27.SelectedItem = this.method_3(ConfigOptions.FlippingTab);
			this.mainForm_0.checkBox_21.Checked = this.method_4(ConfigOptions.ResetInstanceAfterUpdate);
			this.mainForm_0.checkBox_27.Checked = this.method_4(ConfigOptions.IgnoreBotsWhileFlipping);
			this.mainForm_0.checkBox_29.Checked = this.method_4(ConfigOptions.IgnorePrivateAccounts);
			this.mainForm_0.checkBox_28.Checked = this.method_4(ConfigOptions.IgnoreNewAccounts);
			this.mainForm_0.numericUpDown_35.smethod_2(this.method_6(ConfigOptions.IgnoreNewAccountDays));
			this.mainForm_0.checkBox_46.Checked = this.method_4(ConfigOptions.EnableLiveSearch);
			this.mainForm_0.checkBox_45.Checked = this.method_4(ConfigOptions.EnableItemBuying);
			this.mainForm_0.checkBox_47.Checked = this.method_4(ConfigOptions.EnableBulkBuying);
			this.mainForm_0.numericUpDown_34.smethod_2(this.method_6(ConfigOptions.ItemBuyingMinTime));
			this.mainForm_0.numericUpDown_33.smethod_2(this.method_6(ConfigOptions.ItemBuyingMaxTime));
			this.mainForm_0.numericUpDown_32.smethod_2(this.method_6(ConfigOptions.BulkBuyingMinTime));
			this.mainForm_0.numericUpDown_31.smethod_2(this.method_6(ConfigOptions.BulkBuyingMaxTime));
			this.mainForm_0.checkBox_43.Checked = this.method_4(ConfigOptions.ForumThreadEnabled);
			this.mainForm_0.checkBox_41.Checked = this.method_4(ConfigOptions.RepriceItems);
			this.mainForm_0.textBox_14.Text = this.method_3(ConfigOptions.ForumThreadId);
			this.mainForm_0.checkBox_42.Checked = this.method_4(ConfigOptions.StartAfterPricing);
			this.mainForm_0.comboBox_50.SelectedItem = this.method_3(ConfigOptions.PricingCurrencyType);
			this.mainForm_0.numericUpDown_45.smethod_2(this.method_6(ConfigOptions.ListingsToSkip));
			this.mainForm_0.numericUpDown_44.smethod_2(this.method_6(ConfigOptions.ListingsToTake));
			this.mainForm_0.checkBox_40.Checked = this.method_4(ConfigOptions.UsePoePrices);
			this.mainForm_0.checkBox_39.Checked = this.method_4(ConfigOptions.SkipPerandusCoins);
			this.mainForm_0.checkBox_38.Checked = this.method_4(ConfigOptions.PriceWithDiscount);
			this.mainForm_0.numericUpDown_43.smethod_2(this.method_6(ConfigOptions.DiscountAmount));
			this.mainForm_0.comboBox_49.SelectedItem = this.method_3(ConfigOptions.DiscountCurrency);
			this.mainForm_0.checkBox_51.Checked = this.method_4(ConfigOptions.PricingDND);
			this.mainForm_0.checkBox_77.Checked = this.method_4(ConfigOptions.UniqueListings);
			this.mainForm_0.trackBar_1.Value = this.method_5(ConfigOptions.CraftSpeed);
			this.mainForm_0.checkBox_37.Checked = this.method_4(ConfigOptions.UseCraftingSlot);
			this.mainForm_0.comboBox_48.SelectedItem = this.method_3(ConfigOptions.CraftCurrencyTab);
			this.mainForm_0.checkBox_63.Checked = this.method_4(ConfigOptions.CraftMoreItems);
			this.mainForm_0.comboBox_71.SelectedItem = this.method_3(ConfigOptions.CraftMoreItemsTab);
			this.mainForm_0.checkBox_65.Checked = this.method_4(ConfigOptions.StartAfterCrafting);
			this.mainForm_0.numericUpDown_36.smethod_2(this.method_6(ConfigOptions.AltLimit));
			this.mainForm_0.checkBox_35.Checked = this.method_4(ConfigOptions.AltUseScourTransmute);
			this.mainForm_0.checkBox_34.Checked = this.method_4(ConfigOptions.AltUseRegal);
			this.mainForm_0.checkBox_33.Checked = this.method_4(ConfigOptions.AltUseAug);
			this.mainForm_0.comboBox_77.SelectedItem = this.method_3(ConfigOptions.AltAndOr);
			this.mainForm_0.checkBox_36.Checked = this.method_4(ConfigOptions.RareUseScourAlch);
			this.mainForm_0.numericUpDown_37.smethod_2(this.method_6(ConfigOptions.RareChaosLimit));
			this.mainForm_0.comboBox_76.SelectedItem = this.method_3(ConfigOptions.RareAndOr);
			this.mainForm_0.numericUpDown_38.smethod_2(this.method_6(ConfigOptions.ChanceChanceLimit));
			this.mainForm_0.numericUpDown_42.smethod_2(this.method_6(ConfigOptions.SocketJewellersLimit));
			this.mainForm_0.numericUpDown_41.smethod_2(this.method_6(ConfigOptions.SocketFusingLimit));
			this.mainForm_0.numericUpDown_40.smethod_2(this.method_6(ConfigOptions.SocketMinSockets));
			this.mainForm_0.numericUpDown_39.smethod_2(this.method_6(ConfigOptions.SocketMinLinks));
			this.mainForm_0.numericUpDown_49.smethod_2(this.method_6(ConfigOptions.MapScourLimit));
			this.mainForm_0.numericUpDown_51.smethod_2(this.method_6(ConfigOptions.MapAlchLimit));
			this.mainForm_0.numericUpDown_50.smethod_2(this.method_6(ConfigOptions.MapChaosLimit));
			this.mainForm_0.numericUpDown_46.smethod_2(this.method_6(ConfigOptions.MapMinQuality));
			this.mainForm_0.numericUpDown_47.smethod_2(this.method_6(ConfigOptions.MapMinQuantity));
			this.mainForm_0.numericUpDown_48.smethod_2(this.method_6(ConfigOptions.MapMinPacksize));
			this.mainForm_0.checkBox_48.Checked = this.method_4(ConfigOptions.MapPacksizeForced);
			this.mainForm_0.checkBox_44.Checked = this.method_4(ConfigOptions.MapCorrupt);
			this.mainForm_0.comboBox_17.SelectedItem = this.method_3(ConfigOptions.MapCraftStashTab);
			this.mainForm_0.comboBox_55.SelectedItem = this.method_3(ConfigOptions.MapForcedCount);
			this.mainForm_0.comboBox_62.SelectedItem = this.method_3(ConfigOptions.VaalCraftCurrency);
			this.mainForm_0.comboBox_63.SelectedItem = this.method_3(ConfigOptions.VaalCraftStash);
			this.mainForm_0.checkBox_50.Checked = this.method_4(ConfigOptions.BeastStartAfterFinished);
			this.mainForm_0.numericUpDown_53.smethod_2(this.method_6(ConfigOptions.BeastOrbLimit));
			this.mainForm_0.numericUpDown_55.smethod_2(this.method_6(ConfigOptions.BeastOpenBestiaryTiming));
			this.mainForm_0.numericUpDown_54.smethod_2(this.method_6(ConfigOptions.BeastInventoryClickTiming));
			this.mainForm_0.comboBox_75.SelectedIndex = this.method_5(ConfigOptions.GwennenNonUniqueAction);
			this.mainForm_0.checkBox_74.Checked = this.method_4(ConfigOptions.OnWhisperReceived);
			this.mainForm_0.checkBox_20.Checked = this.method_4(ConfigOptions.OnBotStopped);
			this.mainForm_0.checkBox_9.Checked = this.method_4(ConfigOptions.OnTradeRequest);
			this.mainForm_0.checkBox_8.Checked = this.method_4(ConfigOptions.OnDisconnect);
			this.mainForm_0.checkBox_12.Checked = this.method_4(ConfigOptions.OnSuccessfulTrade);
			this.mainForm_0.checkBox_11.Checked = this.method_4(ConfigOptions.OnFailedTrade);
			this.mainForm_0.checkBox_10.Checked = this.method_4(ConfigOptions.OnScamAttempt);
			this.mainForm_0.checkBox_25.Checked = this.method_4(ConfigOptions.OnBuyLivesearchHit);
			this.mainForm_0.checkBox_26.Checked = this.method_4(ConfigOptions.OnBuyWhisper);
			this.mainForm_0.checkBox_24.Checked = this.method_4(ConfigOptions.OnBuyPartyAccepted);
			this.mainForm_0.checkBox_23.Checked = this.method_4(ConfigOptions.OnBuyScamAttempt);
			this.mainForm_0.checkBox_32.Checked = this.method_4(ConfigOptions.OnCraftingItemComplete);
			this.mainForm_0.checkBox_66.Checked = this.method_4(ConfigOptions.OnMuleInventoryEmpty);
			this.mainForm_0.checkBox_79.Checked = this.method_4(ConfigOptions.OnMaxFailedNotification);
			this.mainForm_0.numericUpDown_75.smethod_2(this.method_6(ConfigOptions.MaxFailedNotificationThreshold));
			this.mainForm_0.checkBox_13.Checked = this.method_4(ConfigOptions.PushBulletEnabled);
			this.mainForm_0.textBox_6.Text = this.method_3(ConfigOptions.PushBulletToken);
			this.mainForm_0.checkBox_14.Checked = this.method_4(ConfigOptions.DiscordEnabled);
			this.mainForm_0.textBox_7.Text = this.method_3(ConfigOptions.DiscordWebHookUrl);
			this.mainForm_0.textBox_23.Text = this.method_3(ConfigOptions.DiscordAvatarUrl);
			this.mainForm_0.textBox_22.Text = this.method_3(ConfigOptions.DiscordUsername);
			this.mainForm_0.checkBox_61.Checked = this.method_4(ConfigOptions.DiscordUsernameCheck);
			this.mainForm_0.numericUpDown_14.smethod_2(this.method_6(ConfigOptions.ChangeStashTab));
			this.mainForm_0.numericUpDown_10.smethod_2(this.method_6(ConfigOptions.LoadStashTab));
			this.mainForm_0.numericUpDown_13.smethod_2(this.method_6(ConfigOptions.ClickItemFromStash));
			this.mainForm_0.numericUpDown_12.smethod_2(this.method_6(ConfigOptions.ClickItemFromInventory));
			this.mainForm_0.numericUpDown_52.smethod_2(this.method_6(ConfigOptions.ClickItemToTrade));
			this.mainForm_0.numericUpDown_73.smethod_2(this.method_6(ConfigOptions.ScanItemInTrade));
			this.mainForm_0.numericUpDown_11.smethod_2(this.method_6(ConfigOptions.ClipboardTiming));
			this.mainForm_0.trackBar_0.Value = this.method_5(ConfigOptions.MouseMoveSpeed);
			this.mainForm_0.numericUpDown_56.smethod_2(this.method_6(ConfigOptions.LaunchGameTiming));
			this.mainForm_0.numericUpDown_71.smethod_2(this.method_6(ConfigOptions.MaxGameLoadTime));
			this.mainForm_0.numericUpDown_22.smethod_2(this.method_6(ConfigOptions.TimeBeforeSaleExpires));
			this.mainForm_0.numericUpDown_20.smethod_2(this.method_6(ConfigOptions.MaxTradeTime));
			this.mainForm_0.numericUpDown_21.smethod_2(this.method_6(ConfigOptions.MaxTimeAcceptTrade));
			this.mainForm_0.numericUpDown_19.smethod_2(this.method_6(ConfigOptions.IntervalBetweenTrades));
			this.mainForm_0.numericUpDown_18.smethod_2(this.method_6(ConfigOptions.PartyInvite));
			this.mainForm_0.numericUpDown_17.smethod_2(this.method_6(ConfigOptions.PartyKick));
			this.mainForm_0.numericUpDown_16.smethod_2(this.method_6(ConfigOptions.AcceptDeclineRequest));
			this.mainForm_0.numericUpDown_15.smethod_2(this.method_6(ConfigOptions.Whisper));
			this.mainForm_0.numericUpDown_24.smethod_2(this.method_6(ConfigOptions.TimeBeforeBuyInviteExpires));
			this.mainForm_0.numericUpDown_27.smethod_2(this.method_6(ConfigOptions.BuyMaxTradeTime));
			this.mainForm_0.numericUpDown_28.smethod_2(this.method_6(ConfigOptions.BuyMaxTimeAcceptTrade));
			this.mainForm_0.numericUpDown_23.smethod_2(this.method_6(ConfigOptions.BuyAcceptDeclineRequest));
			this.mainForm_0.numericUpDown_58.smethod_2(this.method_6(ConfigOptions.TimeBeforePartyClosed));
			this.mainForm_0.numericUpDown_25.smethod_2(this.method_6(ConfigOptions.SetNote));
			this.mainForm_0.numericUpDown_26.smethod_2(this.method_6(ConfigOptions.CleanerClickSpeed));
			this.mainForm_0.comboBox_31.SelectedItem = this.method_3(ConfigOptions.VendorRecipeRecipeType);
			this.mainForm_0.comboBox_33.SelectedItem = this.method_3(ConfigOptions.VendorRecipeIdentifiedType);
			this.mainForm_0.comboBox_32.SelectedItem = this.method_3(ConfigOptions.VendorRecipeVendor);
			this.mainForm_0.numericUpDown_59.smethod_2(this.method_6(ConfigOptions.VendorBelowPriceValue));
			this.mainForm_0.comboBox_67.SelectedItem = this.method_3(ConfigOptions.VendorBelowPriceCurrency);
		}

		static Class105()
		{
			Strings.CreateGetStringDelegate(typeof(Class105));
		}

		private const string string_0 = "ExileAgent";

		private MainForm mainForm_0;

		private Dictionary<ConfigOptions, object> dictionary_0 = new Dictionary<ConfigOptions, object>();

		[NonSerialized]
		internal static GetString getString_0;
	}
}
