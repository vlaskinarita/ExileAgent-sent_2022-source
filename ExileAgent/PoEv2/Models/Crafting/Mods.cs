using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PoEv2.Models.Crafting
{
	public sealed class Mods
	{
		[JsonProperty]
		public List<Mod> Normal { get; set; }

		[JsonProperty]
		private List<Mod> Master { get; set; }

		[JsonProperty]
		private List<Mod> Essence { get; set; }

		[JsonProperty]
		private List<Mod> Delve { get; set; }

		[JsonProperty]
		private List<Mod> Incursion { get; set; }

		[JsonProperty]
		private List<Mod> Elder { get; set; }

		[JsonProperty]
		private List<Mod> Shaper { get; set; }

		[JsonProperty]
		private List<Mod> Crusader { get; set; }

		[JsonProperty]
		private List<Mod> Redeemer { get; set; }

		[JsonProperty]
		private List<Mod> Hunter { get; set; }

		[JsonProperty]
		private List<Mod> Warlord { get; set; }

		public List<Mod> ModList { get; private set; }

		public unsafe void method_0(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[7];
			Mods.Class304 @class = new Mods.Class304();
			@class.jsonItem_0 = jsonItem_0;
			this.ModList = new List<Mod>();
			this.ModList.AddRange(this.Normal);
			*(byte*)ptr = ((@class.jsonItem_0.influences != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = (@class.jsonItem_0.influences.Crusader ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.ModList.AddRange(this.Crusader);
				}
				((byte*)ptr)[2] = (@class.jsonItem_0.influences.Elder ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					this.ModList.AddRange(this.Elder);
				}
				((byte*)ptr)[3] = (@class.jsonItem_0.influences.Shaper ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					this.ModList.AddRange(this.Shaper);
				}
				((byte*)ptr)[4] = (@class.jsonItem_0.influences.Redeemer ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					this.ModList.AddRange(this.Redeemer);
				}
				((byte*)ptr)[5] = (@class.jsonItem_0.influences.Hunter ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					this.ModList.AddRange(this.Hunter);
				}
				((byte*)ptr)[6] = (@class.jsonItem_0.influences.Warlord ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					this.ModList.AddRange(this.Warlord);
				}
			}
			this.ModList.RemoveAll(new Predicate<Mod>(@class.method_0));
			this.ModList.RemoveAll(new Predicate<Mod>(Mods.<>c.<>9.method_0));
		}

		public List<Mod> AllMods
		{
			get
			{
				List<Mod> result = new List<Mod>();
				result.smethod_23(new IEnumerable<Mod>[]
				{
					this.Normal,
					this.Master,
					this.Delve,
					this.Incursion,
					this.Elder,
					this.Shaper,
					this.Crusader,
					this.Redeemer,
					this.Hunter,
					this.Warlord
				});
				return result;
			}
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_8;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_9;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_10;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_11;

		public sealed class GClass21 : JsonConverter
		{
			public override bool CanConvert(Type t)
			{
				return t == typeof(List<Mod>);
			}

			public unsafe override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = ((reader.TokenType == JsonToken.Null) ? 1 : 0);
				object result;
				if (*(sbyte*)ptr != 0)
				{
					result = new List<Mod>();
				}
				else
				{
					((byte*)ptr)[1] = ((reader.TokenType == JsonToken.StartArray) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						JArray jarray = JArray.Load(reader);
						((byte*)ptr)[2] = ((jarray.Count == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							result = new List<Mod>();
						}
						else
						{
							result = jarray.ToObject<List<Mod>>();
						}
					}
					else
					{
						JObject jobject = JObject.Load(reader);
						result = jobject.ToObject<Dictionary<string, Mod>>().Values.ToList<Mod>();
					}
				}
				return result;
			}

			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}
		}

		[CompilerGenerated]
		private sealed class Class304
		{
			internal bool method_0(Mod mod_0)
			{
				return this.jsonItem_0.ilvl < int.Parse(mod_0.Level);
			}

			public JsonItem jsonItem_0;
		}
	}
}
