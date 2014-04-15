using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Converters;

namespace CommandGeneratorWPF.NBT
{
	public enum TextColor : byte
	{
		black = 0,
		dark_blue = 1,
		dark_green = 2,
		dark_aqua = 3,
		dark_red = 4,
		dark_purple = 5,
		gold = 6,
		gray = 7,
		dark_gray = 8,
		blue = 9,
		green = 10,
		aqua = 11,
		red = 12,
		light_purple = 13,
		yellow = 14,
		white = 15,
		reset = 16
	}

	public enum ClickCommand : byte
	{
		run_command = 0,
		suggest_command = 1,
		open_url = 2,
		NULL = 100,
	}
	public enum HoverCommand : byte
	{
		show_text = 0,
		show_item = 1,
		show_achievement = 2,
		show_entity = 3,		// 1.8
		NULL = 100,
	}

	public class TextRunTag : IJsonable
	{
		#region subclasses
		[JsonObject(MemberSerialization.OptOut)]
		public class ClickEvent
		{
			[JsonProperty]
			[JsonConverter(typeof(StringQuotelessEnumConverter))]
			[DefaultValue(ClickCommand.NULL)]
			public ClickCommand action
			{ get; set; }

			[DefaultValue("")]
			public string value
			{ get; set; }

			public ClickEvent(ClickCommand act, string val)
			{
				action = act;
				value = val;
			}
			public ClickEvent(ClickCommand cmd)
			{
				action = cmd;
				if (cmd == ClickCommand.run_command ||
					cmd == ClickCommand.suggest_command)
				{
					value = "/say YOLO";
				}
				else
				{
					value = "http://www.minecraftforum.net";
				}
			}
		}

		public class HoverEvent
		{
			[JsonProperty]
			[DefaultValue(HoverCommand.NULL)]
			[JsonConverter(typeof(StringQuotelessEnumConverter))]
			public HoverCommand action
			{ get; set; }

			[DefaultValue("")]
			public string value
			{ get; set; }

			[JsonIgnore]
			public ItemFullTag shownItem
			{ get; set; }

			public HoverEvent(HoverCommand act, string val)
			{
				action = act;
				value = val;
			}
			public HoverEvent(HoverCommand cmd)
			{
				action = cmd;
				switch (cmd)
				{
				case HoverCommand.show_text:
					value = "Hello World!";
					break;
				case HoverCommand.show_item:
					value = "{id:\"diamond\",tag:{display:{name:\"Diamonds For You!\"}}}";
					break;
				case HoverCommand.show_achievement:
					value = "achievement.openInventory";
					break;
				case HoverCommand.show_entity:
					value = "{type:\"Villager\",name:\"Helloo!\",id:VillagerNumber5}";
					break;
				default:
					value = "'ERROR: Unknown HoverCommand'";
					break;
				}
			}
		}

		public class TextScoreboard
		{
			/// <summary>
			/// Player Name. Can be viewer's.
			/// </summary>
			public string name
			{ get; set; }

			/// <summary>
			/// Objective Name
			/// </summary>
			[JsonConverter(typeof(QuotelessStringConverter))]
			public string objective
			{ get; set; }

			public TextScoreboard()
			{
				name = "einsteinsci";
				objective = "obj";
			}
			public TextScoreboard(string nom, string obj)
			{
				name = nom;
				objective = obj;
			}
		}
		#endregion

		[DefaultValue("")]
		public string text
		{ get; set; }

		[JsonConverter(typeof(StringQuotelessEnumConverter))]
		[DefaultValue(TextColor.white)]
		public TextColor color
		{ get; set; }

		public List<TextRunTag> extra
		{ get; set; }

		#region formatting
		public bool bold
		{ get; set; }

		public bool underline
		{ get; set; }

		public bool italic
		{ get; set; }

		public bool strikethrough
		{ get; set; }

		public bool obfuscated
		{ get; set; }
		#endregion

		[DefaultValue("")]
		public string insertion
		{ get; set; }

		public ClickEvent clickEvent
		{ get; set; }

		public HoverEvent hoverEvent
		{ get; set; }

		[DefaultValue("")]
		[JsonConverter(typeof(QuotelessStringConverter))]
		public string translate
		{
			get
			{
				return _translate;
			}
			set
			{
				_translate = value;
				if (_translate == "")
				{
					with = null;
				}
				else
				{
					if (with == null)
					{
						with = new List<string>();
					}
				}
			}
		}
		[JsonIgnore]
		private string _translate;
		
		public List<string> with
		{ get; set; }

		public TextScoreboard score
		{ get; set; }

		public TextRunTag()
		{
			text = "";
			extra = null;

			color = TextColor.white;
			bold = false;
			italic = false;
			underline = false;
			obfuscated = false;
			strikethrough = false;

			insertion = "";
			clickEvent = null;
			hoverEvent = null;
			translate = "";
			with = null;
			score = null;
		}

		public TextRunTag DuplicateNoExtras(TextRunTag input, TextRunTag reference)
		{
			TextRunTag output = input;
			output.extra = reference.extra;

			return output;
		}
		public TextRunTag DuplicateInherited()
		{
			TextRunTag result = new TextRunTag();

			result.color = color;
			
			result.bold = bold;
			result.italic = italic;
			result.underline = underline;
			result.strikethrough = strikethrough;
			result.obfuscated = obfuscated;

			result.clickEvent = clickEvent;
			result.hoverEvent = hoverEvent;

			return result;
		}

		public TreeViewItem ToTreeView()
		{
			TreeViewItem root = new TreeViewItem();
			root.Header = Extras.TextImg("(root)", "doc_compound.png");

			if (text != "")
			{
				TreeViewItem txt = new TreeViewItem();
				txt.Header = Extras.TextImg("text: " + text, "doc_string.png");
				root.Items.Add(txt);
			}
			if (color != TextColor.white)
			{
				TreeViewItem col = new TreeViewItem();
				col.Header = Extras.TextImg("color: " + color.ToString(), "doc_string.png");
				root.Items.Add(col);
			}

			#region formatting
			if (bold)
			{
				TreeViewItem l = new TreeViewItem();
				l.Header = Extras.TextImg("bold: " + bold.ToString().ToLower(), "doc_string.png");
				root.Items.Add(l);
			}
			if (italic)
			{
				TreeViewItem o = new TreeViewItem();
				o.Header = Extras.TextImg("italic: " + italic.ToString().ToLower(), "doc_string.png");
				root.Items.Add(o);
			}
			if (underline)
			{
				TreeViewItem n = new TreeViewItem();
				n.Header = Extras.TextImg("underline: " + underline.ToString().ToLower(), "doc_string.png");
				root.Items.Add(n);
			}
			if (strikethrough)
			{
				TreeViewItem m = new TreeViewItem();
				m.Header = Extras.TextImg("strikethrough: " + strikethrough.ToString().ToLower(), "doc_string.png");
				root.Items.Add(m);
			}
			if (obfuscated)
			{
				TreeViewItem k = new TreeViewItem();
				k.Header = Extras.TextImg("obfuscated: " + obfuscated.ToString().ToLower(), "doc_string.png");
				root.Items.Add(k);
			}
			#endregion

			if (insertion != "")
			{
				TreeViewItem ins = new TreeViewItem();
				ins.Header = Extras.TextImg("insertion: " + insertion, "doc_string.png");
				root.Items.Add(ins);
			}

			#region events
			if (clickEvent != null)
			{
				TreeViewItem click = new TreeViewItem();
				click.Header = Extras.TextImg("clickEvent", "doc_compound.png");

				TreeViewItem act = new TreeViewItem();
				act.Header = Extras.TextImg("action: " + clickEvent.action.ToString(), "doc_string.png");
				click.Items.Add(act);

				TreeViewItem val = new TreeViewItem();
				val.Header = Extras.TextImg("value: " + clickEvent.value.ToString(), "doc_string.png");
				click.Items.Add(val);

				click.IsExpanded = true;
				root.Items.Add(click);
			}

			if (hoverEvent != null)
			{
				TreeViewItem hover = new TreeViewItem();
				hover.Header = Extras.TextImg("hoverEvent", "doc_compound.png");

				TreeViewItem act = new TreeViewItem();
				act.Header = Extras.TextImg("action: " + hoverEvent.action.ToString(), "doc_string.png");
				hover.Items.Add(act);

				TreeViewItem val = new TreeViewItem();
				val.Header = Extras.TextImg("value: " + hoverEvent.value.ToString(), "doc_string.png");
				hover.Items.Add(val);

				hover.IsExpanded = true;
				root.Items.Add(hover);
			}
			#endregion

			#region translate
			if (translate != "")
			{
				TreeViewItem transl = new TreeViewItem();
				transl.Header = Extras.TextImg("translate: " + translate, "doc_string.png");
				root.Items.Add(transl);

				if (with != null)
				{
					if (with.Count != 0)
					{
						TreeViewItem w = new TreeViewItem();
						w.Header = Extras.TextImg("with:", "doc_list.png");

						foreach (string s in with)
						{
							TreeViewItem i = new TreeViewItem();
							i.Header = Extras.TextImg(s, "doc_string.png");
							w.Items.Add(i);
						}

						w.IsExpanded = true;
						root.Items.Add(w);
					}
				}
			}
			#endregion

			#region scoreboard
			if (score != null)
			{
				TreeViewItem sc = new TreeViewItem();
				sc.Header = Extras.TextImg("score:", "doc_compound.png");

				TreeViewItem p = new TreeViewItem();
				p.Header = Extras.TextImg("name: " + score.name, "doc_string.png");
				sc.Items.Add(p);

				TreeViewItem o = new TreeViewItem();
				o.Header = Extras.TextImg("objective: " + score.objective, "doc_string.png");
				sc.Items.Add(o);

				sc.IsExpanded = true;
				root.Items.Add(sc);
			}
			#endregion

			#region extras
			if (extra != null)
			{
				TreeViewItem extras = new TreeViewItem();
				extras.Header = Extras.TextImg("extra:", "doc_compound.png");

				foreach (TextRunTag tag in extra)
				{
					TreeViewItem currentExtra = new TreeViewItem();
					currentExtra.Header = Extras.TextImg("", "doc_compound.png");

					if (tag.text != "")
					{
						TreeViewItem txt = new TreeViewItem();
						txt.Header = Extras.TextImg("text: " + tag.text, "doc_string.png");
						currentExtra.Items.Add(txt);
					}
					if (tag.color != color)
					{
						TreeViewItem col = new TreeViewItem();
						col.Header = Extras.TextImg("color: " + tag.color.ToString(), "doc_string.png");
						currentExtra.Items.Add(col);
					}

					#region formatting
					if (tag.bold != bold)
					{
						TreeViewItem l = new TreeViewItem();
						l.Header = Extras.TextImg("bold: " + tag.bold.ToString().ToLower(), "doc_string.png");
						currentExtra.Items.Add(l);
					}
					if (tag.italic != italic)
					{
						TreeViewItem o = new TreeViewItem();
						o.Header = Extras.TextImg("italic: " + tag.italic.ToString().ToLower(), "doc_string.png");
						currentExtra.Items.Add(o);
					}
					if (tag.underline != underline)
					{
						TreeViewItem n = new TreeViewItem();
						n.Header = Extras.TextImg("underline: " + tag.underline.ToString().ToLower(), "doc_string.png");
						currentExtra.Items.Add(n);
					}
					if (tag.strikethrough != strikethrough)
					{
						TreeViewItem m = new TreeViewItem();
						m.Header = Extras.TextImg("bold: " + tag.strikethrough.ToString().ToLower(), "doc_string.png");
						currentExtra.Items.Add(m);
					}
					if (tag.obfuscated != obfuscated)
					{
						TreeViewItem k = new TreeViewItem();
						k.Header = Extras.TextImg("bold: " + tag.obfuscated.ToString().ToLower(), "doc_string.png");
						currentExtra.Items.Add(k);
					}
					#endregion

					if (tag.insertion != insertion)
					{
						TreeViewItem ins = new TreeViewItem();
						ins.Header = Extras.TextImg("insertion: " + insertion, "doc_string.png");
						currentExtra.Items.Add(ins);
					}

					#region events
					if (tag.clickEvent != clickEvent && tag.clickEvent != null)
					{
						TreeViewItem click = new TreeViewItem();
						click.Header = Extras.TextImg("clickEvent", "doc_compound.png");

						TreeViewItem act = new TreeViewItem();
						act.Header = Extras.TextImg("action: " + tag.clickEvent.action.ToString(), "doc_string.png");
						click.Items.Add(act);

						TreeViewItem val = new TreeViewItem();
						val.Header = Extras.TextImg("value: " + tag.clickEvent.value.ToString(), "doc_string.png");
						click.Items.Add(val);

						click.IsExpanded = true;
						currentExtra.Items.Add(click);
					}
					else if (tag.clickEvent == null)
					{
						TreeViewItem click = new TreeViewItem();
						click.Header = Extras.TextImg("clickEvent", "doc_compound.png");

						currentExtra.Items.Add(click);
					}

					if (tag.hoverEvent != hoverEvent && tag.hoverEvent != null)
					{
						TreeViewItem hover = new TreeViewItem();
						hover.Header = Extras.TextImg("hoverEvent", "doc_compound.png");

						TreeViewItem act = new TreeViewItem();
						act.Header = Extras.TextImg("action: " + tag.hoverEvent.action.ToString(), "doc_string.png");
						hover.Items.Add(act);

						TreeViewItem val = new TreeViewItem();
						val.Header = Extras.TextImg("value: " + tag.hoverEvent.value.ToString(), "doc_string.png");
						hover.Items.Add(val);

						hover.IsExpanded = true;
						currentExtra.Items.Add(hover);
					}
					#endregion

					#region translate
					if (tag.translate != "")
					{
						TreeViewItem transl = new TreeViewItem();
						transl.Header = Extras.TextImg("translate: " + tag.translate, "doc_string.png");
						currentExtra.Items.Add(transl);

						if (tag.with.Count != 0)
						{
							TreeViewItem w = new TreeViewItem();
							w.Header = Extras.TextImg("with:", "doc_list.png");

							foreach (string s in tag.with)
							{
								TreeViewItem i = new TreeViewItem();
								i.Header = Extras.TextImg(s, "doc_string.png");
								w.Items.Add(i);
							}

							w.IsExpanded = true;
							currentExtra.Items.Add(w);
						}
					}
					#endregion

					#region scoreboard
					if (tag.score != null)
					{
						TreeViewItem sc = new TreeViewItem();
						sc.Header = Extras.TextImg("score:", "doc_compound.png");

						TreeViewItem p = new TreeViewItem();
						p.Header = Extras.TextImg("name: " + tag.score.name, "doc_string.png");
						sc.Items.Add(p);

						TreeViewItem o = new TreeViewItem();
						o.Header = Extras.TextImg("objective: " + tag.score.objective, "doc_string.png");
						sc.Items.Add(o);

						sc.IsExpanded = true;
						currentExtra.Items.Add(sc);
					}
					#endregion

					currentExtra.IsExpanded = true;
					extras.Items.Add(currentExtra);
				}

				extras.IsExpanded = true;
				root.Items.Add(extras);
			}
			#endregion

			root.IsExpanded = true;
			return root;
		}

		public string ToJSON()
		{
			return Extras.Serialize(this);
		}
	}

	public static class TextPreviewer
	{
		public static SolidColorBrush ForegroundFromColor(TextColor color)
		{
			SolidColorBrush b;

			switch (color)
			{
			case TextColor.black:
				b = new SolidColorBrush(Colors.Black);
				break;
			case TextColor.dark_blue:
				b = new SolidColorBrush(Colors.Navy);
				break;
			case TextColor.dark_green:
				b = new SolidColorBrush(Colors.DarkGreen);
				break;
			case TextColor.dark_aqua:
				b = new SolidColorBrush(Colors.Teal);
				break;
			case TextColor.dark_red:
				b = new SolidColorBrush(Colors.DarkRed);
				break;
			case TextColor.dark_purple:
				b = new SolidColorBrush(Color.FromArgb(255, 100, 0, 110));
				break;
			case TextColor.gold:
				b = new SolidColorBrush(Colors.Goldenrod);
				break;
			case TextColor.gray:
				b = new SolidColorBrush(Colors.LightGray);
				break;
			case TextColor.dark_gray:
				b = new SolidColorBrush(Colors.DarkGray);
				break;
			case TextColor.blue:
				b = new SolidColorBrush(Color.FromArgb(255, 102, 178, 255));
				break;
			case TextColor.green:
				b = new SolidColorBrush(Colors.LightGreen);
				break;
			case TextColor.aqua:
				b = new SolidColorBrush(Colors.Cyan);
				break;
			case TextColor.red:
				b = new SolidColorBrush(Colors.Salmon);
				break;
			case TextColor.light_purple:
				b = new SolidColorBrush(Color.FromArgb(255, 255, 142, 250));
				break;
			case TextColor.yellow:
				b = new SolidColorBrush(Color.FromArgb(255, 255, 255, 138));
				break;
			case TextColor.white:
				b = new SolidColorBrush(Colors.White);
				break;
			case TextColor.reset:
				b = new SolidColorBrush(Colors.White);
				break;
			default:
				b = new SolidColorBrush(Colors.White);
				break;
			}

			return b;
		}

		public static TextBlock ExcludeExtras(TextRunTag tag, EventHandler onClicked)
		{
			TextBlock text = new TextBlock();

			text.Text = tag.text;

			#region formatting
			if (tag.bold && tag.color != TextColor.reset)
			{
				text.FontWeight = FontWeights.Bold;
			}

			if (tag.italic && tag.color != TextColor.reset)
			{
				text.FontStyle = FontStyles.Italic;
			}

			text.TextDecorations = new TextDecorationCollection();
			if (tag.underline && tag.color != TextColor.reset)
			{
				text.TextDecorations.Add(TextDecorations.Underline);
			}
			if (tag.strikethrough && tag.color != TextColor.reset)
			{
				text.TextDecorations.Add(TextDecorations.Strikethrough);
			}

			if (tag.obfuscated && tag.color != TextColor.reset)
			{
				text.FontFamily = new FontFamily("Webdings");
			}
			else
			{
				text.FontFamily = new FontFamily("Consolas");
			}

			text.Foreground = ForegroundFromColor(tag.color);
			#endregion

			bool hasTooltip = false;
			ToolTip tip = new ToolTip();
			StackPanel tipStuff = new StackPanel();

			RenderOptions.SetBitmapScalingMode(tipStuff,
				BitmapScalingMode.NearestNeighbor);

			#region ClickEvent
			if (tag.clickEvent != null)
			{
				hasTooltip = true;

				string action = tag.clickEvent.value;
				bool isCmd = action.StartsWith("/");

				string does = "";
				switch (tag.clickEvent.action)
				{
				case ClickCommand.run_command:
					if (isCmd)
					{
						does = "Runs: ";
					}
					else
					{
						does = "Chats: ";
					}
					break;
				case ClickCommand.suggest_command:
					does = "Suggests: ";
					break;
				case ClickCommand.open_url:
					does = "Opens Page: ";
					break;
				default:
					does = "Does (Something...): ";
					break;
				}

				TextBlock click = new TextBlock();
				click.Margin = new Thickness(0, 0, 0, 5);
				click.Text = does + "'" + tag.clickEvent.value + "'";
				tipStuff.Children.Add(click);
			}
			#endregion

			#region HoverEvent
			if (tag.hoverEvent != null)
			{
				hasTooltip = true;

				string action = tag.hoverEvent.value;
				bool isCmd = (action.StartsWith("/"));

				switch (tag.hoverEvent.action)
				{
				case HoverCommand.show_text:
					TextBlock show = new TextBlock();
					show.Text = action;
					show.Margin = new Thickness(0, 0, 0, 5);
					tipStuff.Children.Add(show);
					break;

				case HoverCommand.show_item:
					StackPanel itemInfo = tag.hoverEvent.shownItem.ToTooltip();
					tipStuff.Children.Add(itemInfo);
					break;

				case HoverCommand.show_achievement:
					TextBlock ach = new TextBlock();
					ach.Text = "Shows Achievement: " + tag.hoverEvent.value;
					ach.Margin = new Thickness(0, 0, 0, 5);
					tipStuff.Children.Add(ach);
					break;

				case HoverCommand.show_entity:
					TextBlock ent = new TextBlock();
					ent.Text = "Shows Entity: " + tag.hoverEvent.value;
					ent.Margin = new Thickness(0, 0, 0, 5);
					tipStuff.Children.Add(ent);
					break;

				default:
					TextBlock other = new TextBlock();
					other.Text = "Shows something...";
					other.Margin = new Thickness(0, 0, 0, 5);
					tipStuff.Children.Add(other);
					break;
				}
			}
			#endregion

			#region translate
			if (tag.translate != "")
			{
				hasTooltip = true;

				text.Text = "[Translate: " + tag.translate + "(";
				if (tag.with != null)
				{
					foreach (string s in tag.with)
					{
						text.Text += s + ", ";
					}
				}

				if (text.Text.EndsWith(", "))
				{
					text.Text = text.Text.Trim();
					text.Text = text.Text.TrimEnd(',');
				}

				text.Text += ")]";

				TextBlock trans = new TextBlock();
				trans.Text = "(translated)";
				tipStuff.Children.Add(trans);
			}
			#endregion

			#region scoreboard
			if (tag.score != null)
			{
				hasTooltip = true;

				text.Text = "[" + tag.score.name + 
					"." + tag.score.objective + "]";

				TextBlock obj = new TextBlock();
				obj.Text = tag.score.name + "'s score in " + tag.score.objective;
				tipStuff.Children.Add(obj);
			}
			#endregion

			tip.Content = tipStuff;
			if (hasTooltip)
			{
				text.ToolTip = tip;
			}

			//	text.Background = new SolidColorBrush(Colors.DarkBlue);

			text.MouseEnter += (sender, e) =>
				{
					//	if ((text.Background as SolidColorBrush).Color != Colors.DarkBlue)
					//	{
						text.Background = new SolidColorBrush(Colors.SkyBlue);
					//	}
				};

			text.MouseLeave += (sender, e) =>
				{
					//	if ((text.Background as SolidColorBrush).Color != Colors.DarkBlue)
					//	{
						text.Background = new SolidColorBrush(Colors.Transparent);
					//	}
				};

			text.MouseDown += (sender, e) => onClicked(sender, e);
			text.TouchDown += (sender, e) => onClicked(sender, e);
			text.StylusDown += (sender, e) => onClicked(sender, e);

			text.VerticalAlignment = VerticalAlignment.Center;

			return text;
		}

		public static StackPanel IncludeExtras(TextRunTag tag, EventHandler onClicked)
		{
			StackPanel panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;

			TextBlock root = ExcludeExtras(tag, onClicked);
			root.Tag = 0;
			panel.Children.Add(root);

			if (tag.extra != null)
			{
				foreach (TextRunTag t in tag.extra)
				{
					TextBlock extra = ExcludeExtras(t, onClicked);
					extra.Tag = tag.extra.IndexOf(t) + 1;

					panel.Children.Add(extra);
				}
			}

			panel.Margin = new Thickness(5);

			return panel;
		}

		public static TextColor ColorFromForeground(SolidColorBrush e)
		{
			if (e.Color == Colors.Black)
				return TextColor.black;
			if (e.Color == Colors.Navy)
				return TextColor.dark_blue;
			if (e.Color == Colors.DarkGreen)
				return TextColor.dark_green;
			if (e.Color == Colors.Teal)
				return TextColor.dark_aqua;
			if (e.Color == Colors.DarkRed)
				return TextColor.dark_red;
			if (e.Color == Color.FromArgb(255, 100, 0, 110))
				return TextColor.dark_purple;
			if (e.Color == Colors.Goldenrod)
				return TextColor.gold;
			if (e.Color == Colors.LightGray)
				return TextColor.gray;
			if (e.Color == Colors.DarkGray)
				return TextColor.dark_gray;
			if (e.Color == Color.FromArgb(255, 102, 178, 255))
				return TextColor.blue;
			if (e.Color == Colors.LightGreen)
				return TextColor.green;
			if (e.Color == Colors.Cyan)
				return TextColor.aqua;
			if (e.Color == Colors.Salmon)
				return TextColor.red;
			if (e.Color == Color.FromArgb(255, 255, 142, 250))
				return TextColor.light_purple;
			if (e.Color == Color.FromArgb(255, 255, 255, 138))
				return TextColor.yellow;
			if (e.Color == Colors.White)
				return TextColor.white;

			return TextColor.white;
		}

		// obsolete
		public static TextRunTag FromTextBlock(TextBlock block)
		{
			TextRunTag tag = new TextRunTag();
			tag.text = block.Text;

			tag.color = ColorFromForeground(block.Foreground as SolidColorBrush);

			tag.bold = (block.FontWeight == FontWeights.Bold);
			tag.italic = (block.FontStyle == FontStyles.Italic);
			tag.underline = block.TextDecorations.Contains(TextDecorations.Underline);
			tag.strikethrough = block.TextDecorations.Contains(TextDecorations.Strikethrough);
			tag.obfuscated = (block.FontFamily.Source == "Webdings");

			return tag;
		}
	}
}
