using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class TextMatchFilter : AbstractModelFilter
	{
		public static TextMatchFilter Regex(ObjectListView olv, params string[] texts)
		{
			return new TextMatchFilter(olv)
			{
				RegexStrings = texts
			};
		}

		public static TextMatchFilter Prefix(ObjectListView olv, params string[] texts)
		{
			return new TextMatchFilter(olv)
			{
				PrefixStrings = texts
			};
		}

		public static TextMatchFilter Contains(ObjectListView olv, params string[] texts)
		{
			return new TextMatchFilter(olv)
			{
				ContainsStrings = texts
			};
		}

		public TextMatchFilter(ObjectListView olv)
		{
			this.ListView = olv;
		}

		public TextMatchFilter(ObjectListView olv, string text)
		{
			this.ListView = olv;
			this.ContainsStrings = new string[]
			{
				text
			};
		}

		public TextMatchFilter(ObjectListView olv, string text, StringComparison comparison)
		{
			this.ListView = olv;
			this.ContainsStrings = new string[]
			{
				text
			};
			this.StringComparison = comparison;
		}

		public OLVColumn[] Columns
		{
			get
			{
				return this.columns;
			}
			set
			{
				this.columns = value;
			}
		}

		public OLVColumn[] AdditionalColumns
		{
			get
			{
				return this.additionalColumns;
			}
			set
			{
				this.additionalColumns = value;
			}
		}

		public IEnumerable<string> ContainsStrings
		{
			get
			{
				TextMatchFilter.<get_ContainsStrings>d__0 <get_ContainsStrings>d__ = new TextMatchFilter.<get_ContainsStrings>d__0(-2);
				<get_ContainsStrings>d__.<>4__this = this;
				return <get_ContainsStrings>d__;
			}
			set
			{
				this.MatchingStrategies = new List<TextMatchFilter.TextMatchingStrategy>();
				if (value != null)
				{
					foreach (string text in value)
					{
						this.MatchingStrategies.Add(new TextMatchFilter.TextContainsMatchingStrategy(this, text));
					}
				}
			}
		}

		public bool HasComponents
		{
			get
			{
				return this.MatchingStrategies.Count > 0;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				this.listView = value;
			}
		}

		public IEnumerable<string> PrefixStrings
		{
			get
			{
				TextMatchFilter.<get_PrefixStrings>d__6 <get_PrefixStrings>d__ = new TextMatchFilter.<get_PrefixStrings>d__6(-2);
				<get_PrefixStrings>d__.<>4__this = this;
				return <get_PrefixStrings>d__;
			}
			set
			{
				this.MatchingStrategies = new List<TextMatchFilter.TextMatchingStrategy>();
				if (value != null)
				{
					foreach (string text in value)
					{
						this.MatchingStrategies.Add(new TextMatchFilter.TextBeginsMatchingStrategy(this, text));
					}
				}
			}
		}

		public RegexOptions RegexOptions
		{
			get
			{
				if (this.regexOptions == null)
				{
					switch (this.StringComparison)
					{
					case StringComparison.CurrentCulture:
						this.regexOptions = new RegexOptions?(RegexOptions.None);
						break;
					case StringComparison.CurrentCultureIgnoreCase:
						this.regexOptions = new RegexOptions?(RegexOptions.IgnoreCase);
						break;
					case StringComparison.InvariantCulture:
					case StringComparison.Ordinal:
						this.regexOptions = new RegexOptions?(RegexOptions.CultureInvariant);
						break;
					case StringComparison.InvariantCultureIgnoreCase:
					case StringComparison.OrdinalIgnoreCase:
						this.regexOptions = new RegexOptions?(RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
						break;
					default:
						this.regexOptions = new RegexOptions?(RegexOptions.None);
						break;
					}
				}
				return this.regexOptions.Value;
			}
			set
			{
				this.regexOptions = new RegexOptions?(value);
			}
		}

		public IEnumerable<string> RegexStrings
		{
			get
			{
				TextMatchFilter.<get_RegexStrings>d__c <get_RegexStrings>d__c = new TextMatchFilter.<get_RegexStrings>d__c(-2);
				<get_RegexStrings>d__c.<>4__this = this;
				return <get_RegexStrings>d__c;
			}
			set
			{
				this.MatchingStrategies = new List<TextMatchFilter.TextMatchingStrategy>();
				if (value != null)
				{
					foreach (string text in value)
					{
						this.MatchingStrategies.Add(new TextMatchFilter.TextRegexMatchingStrategy(this, text));
					}
				}
			}
		}

		public StringComparison StringComparison
		{
			get
			{
				return this.stringComparison;
			}
			set
			{
				this.stringComparison = value;
			}
		}

		protected IEnumerable<OLVColumn> IterateColumns()
		{
			TextMatchFilter.<IterateColumns>d__12 <IterateColumns>d__ = new TextMatchFilter.<IterateColumns>d__12(-2);
			<IterateColumns>d__.<>4__this = this;
			return <IterateColumns>d__;
		}

		public override bool Filter(object modelObject)
		{
			if (this.ListView != null && this.HasComponents)
			{
				foreach (OLVColumn olvcolumn in this.IterateColumns())
				{
					if (olvcolumn.IsVisible && olvcolumn.Searchable)
					{
						string stringValue = olvcolumn.GetStringValue(modelObject);
						foreach (TextMatchFilter.TextMatchingStrategy textMatchingStrategy in this.MatchingStrategies)
						{
							if (string.IsNullOrEmpty(textMatchingStrategy.Text) || textMatchingStrategy.MatchesText(stringValue))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return true;
		}

		public IEnumerable<CharacterRange> FindAllMatchedRanges(string cellText)
		{
			List<CharacterRange> list = new List<CharacterRange>();
			foreach (TextMatchFilter.TextMatchingStrategy textMatchingStrategy in this.MatchingStrategies)
			{
				if (!string.IsNullOrEmpty(textMatchingStrategy.Text))
				{
					list.AddRange(textMatchingStrategy.FindAllMatchedRanges(cellText));
				}
			}
			return list;
		}

		public bool IsIncluded(OLVColumn column)
		{
			if (this.Columns == null)
			{
				return column.ListView == this.ListView;
			}
			foreach (OLVColumn olvcolumn in this.Columns)
			{
				if (olvcolumn == column)
				{
					return true;
				}
			}
			return false;
		}

		private OLVColumn[] columns;

		private OLVColumn[] additionalColumns;

		private ObjectListView listView;

		private RegexOptions? regexOptions;

		private StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;

		private List<TextMatchFilter.TextMatchingStrategy> MatchingStrategies = new List<TextMatchFilter.TextMatchingStrategy>();

		protected abstract class TextMatchingStrategy
		{
			public StringComparison StringComparison
			{
				get
				{
					return this.TextFilter.StringComparison;
				}
			}

			public TextMatchFilter TextFilter
			{
				get
				{
					return this.textFilter;
				}
				set
				{
					this.textFilter = value;
				}
			}

			public string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					this.text = value;
				}
			}

			public abstract IEnumerable<CharacterRange> FindAllMatchedRanges(string cellText);

			public abstract bool MatchesText(string cellText);

			private TextMatchFilter textFilter;

			private string text;
		}

		protected sealed class TextContainsMatchingStrategy : TextMatchFilter.TextMatchingStrategy
		{
			public TextContainsMatchingStrategy(TextMatchFilter filter, string text)
			{
				base.TextFilter = filter;
				base.Text = text;
			}

			public override bool MatchesText(string cellText)
			{
				return cellText.IndexOf(base.Text, base.StringComparison) != -1;
			}

			public override IEnumerable<CharacterRange> FindAllMatchedRanges(string cellText)
			{
				List<CharacterRange> list = new List<CharacterRange>();
				for (int num = cellText.IndexOf(base.Text, base.StringComparison); num != -1; num = cellText.IndexOf(base.Text, num + base.Text.Length, base.StringComparison))
				{
					list.Add(new CharacterRange(num, base.Text.Length));
				}
				return list;
			}
		}

		protected sealed class TextBeginsMatchingStrategy : TextMatchFilter.TextMatchingStrategy
		{
			public TextBeginsMatchingStrategy(TextMatchFilter filter, string text)
			{
				base.TextFilter = filter;
				base.Text = text;
			}

			public override bool MatchesText(string cellText)
			{
				return cellText.StartsWith(base.Text, base.StringComparison);
			}

			public override IEnumerable<CharacterRange> FindAllMatchedRanges(string cellText)
			{
				List<CharacterRange> list = new List<CharacterRange>();
				if (cellText.StartsWith(base.Text, base.StringComparison))
				{
					list.Add(new CharacterRange(0, base.Text.Length));
				}
				return list;
			}
		}

		protected sealed class TextRegexMatchingStrategy : TextMatchFilter.TextMatchingStrategy
		{
			public TextRegexMatchingStrategy(TextMatchFilter filter, string text)
			{
				base.TextFilter = filter;
				base.Text = text;
			}

			public RegexOptions RegexOptions
			{
				get
				{
					return base.TextFilter.RegexOptions;
				}
			}

			protected Regex Regex
			{
				get
				{
					if (this.regex == null)
					{
						try
						{
							this.regex = new Regex(base.Text, this.RegexOptions);
						}
						catch (ArgumentException)
						{
							this.regex = TextMatchFilter.TextRegexMatchingStrategy.InvalidRegexMarker;
						}
					}
					return this.regex;
				}
				set
				{
					this.regex = value;
				}
			}

			protected bool IsRegexInvalid
			{
				get
				{
					return this.Regex == TextMatchFilter.TextRegexMatchingStrategy.InvalidRegexMarker;
				}
			}

			public override bool MatchesText(string cellText)
			{
				return this.IsRegexInvalid || this.Regex.Match(cellText).Success;
			}

			public override IEnumerable<CharacterRange> FindAllMatchedRanges(string cellText)
			{
				List<CharacterRange> list = new List<CharacterRange>();
				if (!this.IsRegexInvalid)
				{
					foreach (object obj in this.Regex.Matches(cellText))
					{
						Match match = (Match)obj;
						if (match.Length > 0)
						{
							list.Add(new CharacterRange(match.Index, match.Length));
						}
					}
				}
				return list;
			}

			// Note: this type is marked as 'beforefieldinit'.
			static TextRegexMatchingStrategy()
			{
				Strings.CreateGetStringDelegate(typeof(TextMatchFilter.TextRegexMatchingStrategy));
				TextMatchFilter.TextRegexMatchingStrategy.InvalidRegexMarker = new Regex(TextMatchFilter.TextRegexMatchingStrategy.getString_0(107315161));
			}

			private Regex regex;

			private static Regex InvalidRegexMarker;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
