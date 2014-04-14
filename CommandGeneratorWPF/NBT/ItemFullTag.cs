using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CommandGeneratorWPF.NBT
{
	public class ItemFullTag : IJsonable
	{
		public byte? Count
		{ get; set; }

		public byte? Slot
		{ get; set; }

		[JsonProperty]
		public string id
		{ get; set; }

		public short Damage
		{ get; set; }

		public ItemTagTag tag
		{ get; set; }

		[JsonIgnore]
		public InventoryItem Source
		{ get; set; }

		public ItemFullTag(InventoryItem src)
		{
			Count = 1;
			Slot = null;
			Damage = src.Metadata;
			id = src.ShortName;
			tag = null;

			Source = src;
		}
		public ItemFullTag(InventoryItem src, byte? count)
		{
			Count = count;
			Slot = null;
			Damage = src.Metadata;
			id = src.ShortName;
			tag = null;

			Source = src;
		}
		public ItemFullTag(InventoryItem src, byte? count, byte? slot)
		{
			Count = count;
			Slot = slot;
			id = src.ShortName;
			Damage = src.Metadata;
			tag = null;

			Source = src;
		}
		public ItemFullTag(InventoryItem src, byte? count, byte? slot, ItemTagTag tagTag)
		{
			Count = count;
			Slot = slot;
			id = src.ShortName;
			Damage = src.Metadata;
			tag = tagTag;

			Source = src;
		}

		public string ToJSON()
		{
			return Extras.Serialize(this);
		}

		public TreeViewItem ToTreeView()
		{
			TreeViewItem root = new TreeViewItem();
			root.Header = Extras.TextImg("(root)", "doc_compound.png");

			if (Count.HasValue)
			{
				TreeViewItem count = new TreeViewItem();
				count.Header = Extras.TextImg("Count: " + Count.ToString(), "doc_byte.png");
				root.Items.Add(count);
			}

			if (Slot.HasValue)
			{
				TreeViewItem slot = new TreeViewItem();
				slot.Header = Extras.TextImg("Slot: " + slot.ToString(), "doc_byte.png");
				root.Items.Add(slot);
			}

			TreeViewItem item = new TreeViewItem();
			item.Header = Extras.TextImg("id: " + id, "doc_string.png");
			root.Items.Add(item);

			TreeViewItem dmg = new TreeViewItem();
			dmg.Header = Extras.TextImg("Damage: " + Damage.ToString(), "doc_short.png");
			root.Items.Add(dmg);

			if (tag != null)
			{
				if (!tag.isEmpty())
				{
					TreeViewItem tagtag = tag.ToTree();
					root.Items.Add(tagtag);
				}
			}

			root.IsExpanded = true;
			return root;
		}

		public StackPanel ToTooltip()
		{
			StackPanel panel = new StackPanel();

			#region title
			string title = Source.FriendlyName;
			bool custom = false;
			try
			{
				if (tag.display.Name != "")
				{
					title = tag.display.Name;
					custom = true;
				}
			}
			catch (NullReferenceException)
			{
				// catch and release
			}
			try
			{
				if (tag.title != "")
				{
					title = tag.title;
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			TextBlock titleBlock = new TextBlock();
			titleBlock.Text = title;
			titleBlock.Foreground = new SolidColorBrush(Colors.Teal);
			if (custom)
			{
				titleBlock.FontStyle = FontStyles.Italic;
			}
			panel.Children.Add(titleBlock);
			#endregion

			#region nbt
			// Coming soon
			#endregion

			#region leatherDye
			try
			{
				if (tag.display.color != null)
				{
					TextBlock dye = new TextBlock();
					dye.Text = "Dyed";
					dye.FontStyle = FontStyles.Italic;
					dye.Foreground = new SolidColorBrush(Colors.LightGray);
					panel.Children.Add(dye);
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region potion
			List<string> potion = new List<string>();
			try
			{
				foreach (ItemTagTag.CustomPotionEffectTag p in 
					tag.CustomPotionEffects)
				{
					potion.Add(p.ToString());
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			foreach (string s in potion)
			{
				TextBlock b = new TextBlock();
				b.Text = s;
				b.Foreground = new SolidColorBrush(Colors.DarkGray);
				panel.Children.Add(b);
			}
			#endregion

			#region ench
			List<string> ench = new List<string>();
			try
			{
				foreach (ItemTagTag.EnchantmentTag e in
					tag.ench)
				{
					ench.Add(e.ToString());
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			try
			{
				foreach (ItemTagTag.EnchantmentTag e in
					tag.storedEnch)
				{
					ench.Add(e.ToString());
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			foreach (string s in ench)
			{
				TextBlock b = new TextBlock();
				b.Text = s;
				b.Foreground = new SolidColorBrush(Colors.DarkGray);
				panel.Children.Add(b);
			}
			#endregion

			#region lore
			try
			{
				if (tag.display.Lore != null)
				{
					foreach (string s in tag.display.Lore)
					{
						TextBlock l = new TextBlock();
						l.Text = s;
						l.Foreground = new SolidColorBrush(Colors.Purple);
						l.FontStyle = FontStyles.Italic;

						panel.Children.Add(l);
					}
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region unbreakable
			try
			{
				if (tag.Unbreakable != 0)
				{
					TextBlock newline = new TextBlock();
					newline.Text = "   ";
					newline.Foreground = new SolidColorBrush(Colors.Transparent);
					panel.Children.Add(newline);

					TextBlock u = new TextBlock();
					u.Text = "Unbreakable";
					u.Foreground = new SolidColorBrush(Colors.RoyalBlue);
					panel.Children.Add(u);
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region canBreak
			try
			{
				if (tag.CanDestroy != null)
				{
					List<string> dest = new List<string>();
					foreach (string s in tag.CanDestroy)
					{
						InventoryItem i = Extras.FindByShortName(s);
						dest.Add(i.FriendlyName);
					}

					if (dest.Count != 0)
					{
						TextBlock newline = new TextBlock();
						newline.Text = "   ";
						newline.Foreground = new SolidColorBrush(Colors.Transparent);
						panel.Children.Add(newline);

						TextBlock header = new TextBlock();
						header.Foreground = new SolidColorBrush(Colors.DarkGray);
						header.Text = "Can break:";
						panel.Children.Add(header);

						foreach (string s in dest)
						{
							TextBlock i = new TextBlock();
							i.Text = s;
							i.Foreground = new SolidColorBrush(Colors.LightGray);
							panel.Children.Add(i);
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region canBreak
			try
			{
				if (tag.CanPlaceOn != null)
				{
					List<string> place = new List<string>();
					foreach (string s in tag.CanPlaceOn)
					{
						InventoryItem i = Extras.FindByShortName(s);
						place.Add(i.FriendlyName);
					}

					if (place.Count != 0)
					{
						TextBlock newline = new TextBlock();
						newline.Text = "   ";
						newline.Foreground = new SolidColorBrush(Colors.Transparent);
						panel.Children.Add(newline);

						TextBlock header = new TextBlock();
						header.Foreground = new SolidColorBrush(Colors.DarkGray);
						header.Text = "Can be placed on:";
						panel.Children.Add(header);

						foreach (string s in place)
						{
							TextBlock i = new TextBlock();
							i.Text = s;
							i.Foreground = new SolidColorBrush(Colors.LightGray);
							panel.Children.Add(i);
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region FireworksStar
			try
			{
				if (tag.Explosion != null)
				{
					TextBlock type = new TextBlock();
					type.Text = tag.Explosion.GetTypeString();
					type.Foreground = new SolidColorBrush(Colors.DarkGray);
					panel.Children.Add(type);

					#region colors
					string col = "";
					foreach (int i in tag.Explosion.Colors)
					{
						col += i.ToString() + ", ";
					}
					col = col.Trim();
					col = col.TrimEnd(',');

					if (col != "")
					{
						TextBlock colors = new TextBlock();
						colors.Text = col;
						colors.Foreground = new SolidColorBrush(Colors.DarkGray);
						panel.Children.Add(colors);
					}
					#endregion

					#region fade
					string fade = "";
					foreach (int i in tag.Explosion.FadeColors)
					{
						fade += i.ToString() + ", ";
					}
					fade = fade.Trim();
					fade = fade.TrimEnd(',');

					if (fade != "")
					{
						TextBlock colors = new TextBlock();
						colors.Text = "Fade to " + fade;
						colors.Foreground = new SolidColorBrush(Colors.DarkGray);
						panel.Children.Add(colors);
					}
					#endregion

					#region effects
					if (tag.Explosion.Trail != 0)
					{
						TextBlock trail = new TextBlock();
						trail.Text = "Trail";
						trail.Foreground = new SolidColorBrush(Colors.DarkGray);
						panel.Children.Add(trail);
					}

					if (tag.Explosion.Flicker != 0)
					{
						TextBlock flicker = new TextBlock();
						flicker.Text = "Twinkle";
						flicker.Foreground = new SolidColorBrush(Colors.DarkGray);
						panel.Children.Add(flicker);
					}
					#endregion
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			#region FireworksRocket
			try
			{
				if (tag.Fireworks != null)
				{
					TextBlock dur = new TextBlock();
					dur.Text = "Flight Duration: " + tag.Fireworks.Flight.ToString();
					dur.Foreground = new SolidColorBrush(Colors.DarkGray);
					panel.Children.Add(dur);

					if (tag.Fireworks.Explosions != null)
					{
						foreach (ItemTagTag.FireworksStarTag star in
							tag.Fireworks.Explosions)
						{
							TextBlock type = new TextBlock();
							type.Text = tag.Explosion.GetTypeString();
							type.Foreground = new SolidColorBrush(Colors.DarkGray);
							panel.Children.Add(type);

							#region colors
							string col = "";
							foreach (int i in tag.Explosion.Colors)
							{
								col += i.ToString() + ", ";
							}
							col = col.Trim();
							col = col.TrimEnd(',');

							if (col != "")
							{
								TextBlock colors = new TextBlock();
								colors.Text = "\t" + col;
								colors.Foreground = new SolidColorBrush(Colors.DarkGray);
								panel.Children.Add(colors);
							}
							#endregion

							#region fade
							string fade = "";
							foreach (int i in tag.Explosion.FadeColors)
							{
								fade += i.ToString() + ", ";
							}
							fade = fade.Trim();
							fade = fade.TrimEnd(',');

							if (fade != "")
							{
								TextBlock colors = new TextBlock();
								colors.Text = "\tFade to " + fade;
								colors.Foreground = new SolidColorBrush(Colors.DarkGray);
								panel.Children.Add(colors);
							}
							#endregion

							#region effects
							if (tag.Explosion.Trail != 0)
							{
								TextBlock trail = new TextBlock();
								trail.Text = "\tTrail";
								trail.Foreground = new SolidColorBrush(Colors.DarkGray);
								panel.Children.Add(trail);
							}

							if (tag.Explosion.Flicker != 0)
							{
								TextBlock flicker = new TextBlock();
								flicker.Text = "\tTwinkle";
								flicker.Foreground = new SolidColorBrush(Colors.DarkGray);
								panel.Children.Add(flicker);
							}
							#endregion
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// and release
			}
			#endregion

			foreach (TextBlock txt in panel.Children)
			{
				txt.FontSize = 14;
				txt.FontFamily = new FontFamily("Consolas");
				txt.FontWeight = FontWeights.SemiBold;
			}

			return panel;
		}
	}
}
