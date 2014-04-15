using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Newtonsoft.Json;

namespace CommandGeneratorWPF.NBT
{
	/// <summary>
	/// Why the double-tagging?
	/// </summary>
	public class ItemTagTag : IJsonable
	{
		#region components
		[Flags]
		public enum HideFlagsFormat : byte
		{
			none = 0,
			ench = 1,
			AttributeModifiers = 2,
			Unbreakable = 4,
			CanDestroy = 8,
			CanPlaceOn = 16,
			other = 32
		};

		enum FireworksStarType : byte
		{
			Small_Ball = 0,
			Large_Ball = 1,
			Star = 2,
			Creeper = 3,
			Burst = 4
		};

		public class EnchantmentTag
		{
			[JsonProperty]
			public short id
			{ get; set; }

			[JsonProperty]
			public short lvl
			{ get; set; }

			public EnchantmentTag(short enchID)
			{
				id = enchID;
				lvl = 1;
			}
			public EnchantmentTag(short enchID, short enchLvl)
			{
				id = enchID;
				lvl = enchLvl;
			}

			public override string ToString()
			{
				return EnchantmentFriendlyNames()[id] + " " + Extras.IntToRoman(lvl);
			}
		}
		public class DisplayTag
		{
			public int? color
			{ get; set; }

			[DefaultValue("")]
			public string Name
			{ get; set; }

			public List<string> Lore
			{ get; set; }

			public DisplayTag()
			{
				//color = (255 << 16) + (255 << 8) + 255; // #FFFFFF
				color = null;
				Name = "";
				Lore = null;
			}

			public bool IsDefault()
			{
				return (!color.HasValue) &&
						Name == "" && 
						Lore == null;
			}
		}
		public class CustomPotionEffectTag
		{
			[JsonProperty]
			public byte Id
			{ get; set; }

			[JsonProperty]
			public byte Amplifier
			{ get; set; }

			[JsonProperty]
			public int Duration
			{ get; set; }

			[DefaultValue(0)]
			public byte Ambient
			{ get; set; }

			[DefaultValue(1)]
			public byte ShowParticles
			{ get; set; }

			public CustomPotionEffectTag()
			{
				Id = 0;
				Amplifier = 0;
				Duration = 0;
				Ambient = 0;
				ShowParticles = 1;
			}
			public CustomPotionEffectTag(byte id, byte amp, int duration, 
				byte ambient, byte showParticles)
			{
				Id = id;
				Amplifier = amp;
				Duration = duration;
				Ambient = ambient;
				ShowParticles = showParticles;
			}

			public override string ToString()
			{
				string result = PotionEffectFriendlyNames()[Id] + " " + Extras.IntToRoman(Amplifier + 1);
				result += " for " + Duration.ToString() + " ticks";
				if (Ambient != 0)
				{
					result += " (ambient)";
				}
				if (ShowParticles == 0)
				{
					result += " (hidden)";
				}

				return result;
			}
		}
		public class FireworksStarTag
		{
			[DefaultValue(0)]
			public byte Flicker
			{ get; set; }

			[DefaultValue(0)]
			public byte Trail
			{ get; set; }

			[DefaultValue(-1)] //Always show
			public byte Type
			{ get; set; }

			public List<int> Colors
			{ get; set; }

			public List<int> FadeColors
			{ get; set; }

			public FireworksStarTag()
			{
				Colors = null;
				FadeColors = null;

				Flicker = 0;
				Trail = 0;
				Type = (byte)FireworksStarType.Small_Ball; // 0 (Default)
			}

			public override string ToString()
			{
				string result = ((FireworksStarType)Type).ToString();
				if (Flicker != 0)
				{
					result += " (flicker)";
				}

				if (Trail != 0)
				{
					result += " (trail)";
				}

				if (Colors == null)
				{
					result += " (no colors)";
				}

				if (FadeColors != null)
				{
					result += " (fade)";
				}

				return result;
			}

			public string GetTypeString()
			{
				string result = ((FireworksStarType)Type).ToString();
				result = result.Replace('_', ' ');
				return result;
			}
		}
		public class FireworksRocketTag
		{
			[DefaultValue(-25)] //always show this, ranges from -2 to 10 in UI
			public sbyte Flight
			{ get; set; }

			public List<FireworksStarTag> Explosions
			{ get; set; }

			public FireworksRocketTag()
			{
				Flight = 1;
				Explosions = new List<FireworksStarTag>();
				Explosions.Add(new FireworksStarTag());
			}
		}
		#endregion

		#region TagTags
		//General
		[DefaultValue(0)]
		public byte Unbreakable;
		public List<string> CanDestroy;

		public List<string> CanPlaceOn;

		public List<EnchantmentTag> ench;

		//Enchanted Books
		public List<EnchantmentTag> storedEnch;

		[DefaultValue(1)]
		public int RepairCost;

		public List<CustomPotionEffectTag> CustomPotionEffects;

		public DisplayTag display
		{ get; set; }

		[DefaultValue(HideFlagsFormat.none)]
		public HideFlagsFormat HideFlags;

		//Written Books
		[DefaultValue(0)]
		public int generation;
		[DefaultValue("")]
		public string author;
		[DefaultValue("")]
		public string title;
		public List<string> pages;

		//Skulls
		[DefaultValue("")]
		public string SkullOwner;

		public FireworksStarTag Explosion;
		public FireworksRocketTag Fireworks;

		//Map
		public byte? map_is_scaling
		{ get; set; }
		#endregion

		public ItemTagTag()
		{
			Unbreakable = 0;
			RepairCost = 1;
			HideFlags = HideFlagsFormat.none;
			generation = 0;
			author = "";
			title = "";
			SkullOwner = "";

			CanDestroy = null;
			CanPlaceOn = null;
			ench = null;
			storedEnch = null;
			CustomPotionEffects = null;
			display = null;
			pages = null;
			Explosion = new FireworksStarTag();
			Fireworks = new FireworksRocketTag();

			//canPlaceOn_hasValue = false;
			//ench_hasValue = false;
			//storedEnch_hasValue = false;
			//customPotion_hasValue = false;
			//display_hasValue = false;
			//bookData_hasValue = false;
			//Explosion_hasValue = false;
			//Fireworks_hasValue = false;
		}

		public TreeViewItem ToTree()
		{
			TreeViewItem tag = new TreeViewItem();
			tag.Header = Extras.TextImg("tag:", "doc_compound.png");

			#region general
			if (Unbreakable != 0)
			{
				TreeViewItem Unbreakable_ = new TreeViewItem();
				Unbreakable_.Header = Extras.TextImg("Unbreakable: " + 
					Unbreakable.ToString(), "doc_byte.png");
				tag.Items.Add(Unbreakable_);
			}

			if (CanDestroy != null)
			{
				TreeViewItem CanDestroy_ = new TreeViewItem();
				CanDestroy_.Header = Extras.TextImg("CanDestroy:", "doc_list.png");
				foreach (string s in CanDestroy)
				{
					TreeViewItem i = new TreeViewItem();
					i.Header = Extras.TextImg(s, "doc_string.png");
					CanDestroy_.Items.Add(i);
				}
				CanDestroy_.IsExpanded = true;
				tag.Items.Add(CanDestroy_);
			}

			if (CanPlaceOn != null)
			{
				TreeViewItem CanPlaceOn_ = new TreeViewItem();
				CanPlaceOn_.Header = Extras.TextImg("CanPlaceOn:", "doc_list.png");
				foreach (string s in CanPlaceOn)
				{
					TreeViewItem i = new TreeViewItem();
					i.Header = Extras.TextImg(s, "doc_string.png");
					CanPlaceOn_.Items.Add(i);
				}
				CanPlaceOn_.IsExpanded = true;
				tag.Items.Add(CanPlaceOn_);
			}
			#endregion

			#region display
			if (display != null)
			{
				TreeViewItem display_ = new TreeViewItem();
				display_.Header = Extras.TextImg("display:", "doc_compound.png");

				if (display.color.HasValue)
				{
					TreeViewItem color = new TreeViewItem();
					color.Header = Extras.TextImg("color: " + display.color.ToString(), 
						"doc_int.png");
					display_.Items.Add(color);
				}

				if (display.Name != "")
				{
					TreeViewItem name = new TreeViewItem();
					name.Header = Extras.TextImg("name: " + display.Name, 
						"doc_string.png");
					display_.Items.Add(name);
				}

				if (display.Lore != null)
				{
					TreeViewItem lore = new TreeViewItem();
					lore.Header = Extras.TextImg("lore:", "doc_list.png");

					foreach (string s in display.Lore)
					{
						TreeViewItem l = new TreeViewItem();
						l.Header = Extras.TextImg(s, "doc_string.png");
						lore.Items.Add(l);
					}
					lore.IsExpanded = true;
					display_.Items.Add(lore);
				}

				display_.IsExpanded = true;
				tag.Items.Add(display_);
			}
			#endregion

			#region ench
			if (ench != null)
			{
				TreeViewItem ench_ = new TreeViewItem();
				ench_.Header = Extras.TextImg("ench:", "doc_list.png");

				foreach (EnchantmentTag t in ench)
				{
					TreeViewItem e = new TreeViewItem();
					e.Header = Extras.TextImg("", "doc_compound.png");

					TreeViewItem id = new TreeViewItem();
					id.Header = Extras.TextImg("id: " + t.id.ToString(), 
						"doc_short.png");
					e.Items.Add(id);
					TreeViewItem lvl = new TreeViewItem();
					lvl.Header = Extras.TextImg("lvl: " + t.lvl.ToString(), 
						"doc_short.png");
					e.Items.Add(lvl);

					e.IsExpanded = true;
					ench_.Items.Add(e);
				}
				ench_.IsExpanded = true;
				tag.Items.Add(ench_);
			}

			if (storedEnch != null)
			{
				TreeViewItem storedEnch_ = new TreeViewItem();
				storedEnch_.Header = Extras.TextImg("storedEnch:", 
					"doc_list.png");

				foreach (EnchantmentTag t in storedEnch)
				{
					TreeViewItem e = new TreeViewItem();
					e.Header = Extras.TextImg("", "doc_compound.png");

					TreeViewItem id = new TreeViewItem();
					id.Header = Extras.TextImg("id: " + t.id.ToString(), 
						"doc_short.png");
					e.Items.Add(id);
					TreeViewItem lvl = new TreeViewItem();
					lvl.Header = Extras.TextImg("lvl: " + t.lvl.ToString(), 
						"doc_short.png");
					e.Items.Add(lvl);

					e.IsExpanded = true;
					storedEnch_.Items.Add(e);
				}
				storedEnch_.IsExpanded = true;
				tag.Items.Add(storedEnch_);
			}

			if (RepairCost != 1)
			{
				TreeViewItem repair = new TreeViewItem();
				repair.Header = Extras.TextImg("RepairCost: " +
					RepairCost.ToString(), "doc_int.png");
				tag.Items.Add(repair);
			}
			#endregion

			#region potion
			if (CustomPotionEffects != null)
			{
				TreeViewItem potion = new TreeViewItem();
				potion.Header = Extras.TextImg("CustomPotionEffects:", 
					"doc_list.png");

				foreach (CustomPotionEffectTag p in CustomPotionEffects)
				{
					TreeViewItem effect = new TreeViewItem();
					effect.Header = Extras.TextImg("", "doc_compound.png");

					TreeViewItem id = new TreeViewItem();
					id.Header = Extras.TextImg("Id: " + 
						p.Id.ToString(), "doc_byte.png");
					effect.Items.Add(id);

					TreeViewItem str = new TreeViewItem();
					str.Header = Extras.TextImg("Amplifier: " + 
						p.Amplifier.ToString(), "doc_byte.png");
					effect.Items.Add(str);

					TreeViewItem dur = new TreeViewItem();
					dur.Header = Extras.TextImg("Duration: " + 
						p.Duration.ToString(), "doc_int.png");
					effect.Items.Add(dur);

					if (p.Ambient != 0)
					{
						TreeViewItem amb = new TreeViewItem();
						amb.Header = Extras.TextImg("Ambient: " + 
							p.Ambient.ToString(), "doc_byte.png");
						effect.Items.Add(amb);
					}
					if (p.ShowParticles == 0)
					{
						TreeViewItem part = new TreeViewItem();
						part.Header = Extras.TextImg("ShowParticles: " + 
							p.ShowParticles.ToString(), "doc_byte.png");
						effect.Items.Add(part);
					}

					effect.IsExpanded = true;
					potion.Items.Add(effect);
				}

				potion.IsExpanded = true;
				tag.Items.Add(potion);
			}
			#endregion

			#region book
			if (title != "")
			{
				TreeViewItem title_ = new TreeViewItem();
				title_.Header = Extras.TextImg("title: " + title, 
					"doc_string.png");
				tag.Items.Add(title_);
			}

			if (author != "")
			{
				TreeViewItem author_ = new TreeViewItem();
				author_.Header = Extras.TextImg("author: " + author, 
					"doc_string.png");
				tag.Items.Add(author_);
			}

			if (generation != 0)
			{
				TreeViewItem gen = new TreeViewItem();
				gen.Header = Extras.TextImg("generation: " + 
					generation.ToString(), "doc_int.png");
				tag.Items.Add(gen);
			}

			if (pages != null)
			{
				if (pages.Count != 0)
				{
					TreeViewItem pages_ = new TreeViewItem();
					pages_.Header = Extras.TextImg("pages:", "doc_list.png");

					foreach (string p in pages)
					{
						TreeViewItem page = new TreeViewItem();
						string h = p.Substring(0, Math.Min(20, p.Length));
						if (h != p)
						{
							h += "...";
						}
						page.Header = Extras.TextImg(h, "doc_string.png");

						pages_.Items.Add(page);
					}

					pages_.IsExpanded = true;
					tag.Items.Add(pages_);
				}
			}
			#endregion

			#region FwStar
			if (Explosion != null)
			{
				TreeViewItem expl = new TreeViewItem();
				expl.Header = Extras.TextImg("Explosion:", "doc_compound.png");

				if (Explosion.Flicker != 0)
				{
					TreeViewItem fl = new TreeViewItem();
					fl.Header = Extras.TextImg("Flicker: " + 
						Explosion.Flicker.ToString(), "doc_byte.png");
					expl.Items.Add(fl);
				}

				if (Explosion.Trail != 0)
				{
					TreeViewItem tr = new TreeViewItem();
					tr.Header = Extras.TextImg("Trail: " +
						Explosion.Trail.ToString(), "doc_byte.png");
					expl.Items.Add(tr);
				}

				TreeViewItem ty = new TreeViewItem();
				ty.Header = Extras.TextImg("Type: " + Explosion.Type.ToString(), 
					"doc_byte.png");
				expl.Items.Add(ty);

				if (Explosion.Colors != null)
				{
					TreeViewItem colors = new TreeViewItem();
					colors.Header = Extras.TextImg("Colors:", "doc_array.png");

					foreach (int i in Explosion.Colors)
					{
						TreeViewItem t = new TreeViewItem();
						t.Header = Extras.TextImg(i.ToString(), "doc_int.png");
						colors.Items.Add(t);
					}

					colors.IsExpanded = true;
					expl.Items.Add(colors);
				}

				if (Explosion.FadeColors != null)
				{
					TreeViewItem fade = new TreeViewItem();
					fade.Header = Extras.TextImg("FadeColors:", "doc_array.png");

					foreach (int i in Explosion.FadeColors)
					{
						TreeViewItem t = new TreeViewItem();
						t.Header = Extras.TextImg(i.ToString(), "doc_int.png");
						fade.Items.Add(t);
					}

					fade.IsExpanded = true;
					expl.Items.Add(fade);
				}

				expl.IsExpanded = true;
				tag.Items.Add(expl);
			}
			#endregion

			#region FwRocket
			if (Fireworks != null)
			{
				TreeViewItem fw = new TreeViewItem();
				fw.Header = Extras.TextImg("Fireworks:", "doc_compound.png");

				TreeViewItem flight = new TreeViewItem();
				flight.Header = Extras.TextImg("Flight: " + Fireworks.Flight.ToString(),
					"doc_byte.png");
				fw.Items.Add(flight);

				if (Fireworks.Explosions != null)
				{
					TreeViewItem e = new TreeViewItem();
					e.Header = Extras.TextImg("Explosions:", "doc_list.png");

					foreach (FireworksStarTag star in Fireworks.Explosions)
					{
						TreeViewItem x = new TreeViewItem();
						x.Header = Extras.TextImg("", "doc_compound.png");

						if (star.Flicker != 0)
						{
							TreeViewItem flick = new TreeViewItem();
							flick.Header = Extras.TextImg("Flicker: " + star.Flicker,
								"doc_byte.png");
							x.Items.Add(flick);
						}

						if (star.Trail != 0)
						{
							TreeViewItem trail = new TreeViewItem();
							trail.Header = Extras.TextImg("Trail: " + star.Trail,
								"doc_byte.png");
							x.Items.Add(trail);
						}

						TreeViewItem type = new TreeViewItem();
						type.Header = Extras.TextImg("Type: " + star.Type.ToString(),
							"doc_byte.png");
						x.Items.Add(type);

						if (star.Colors != null)
						{
							TreeViewItem colors = new TreeViewItem();
							colors.Header = Extras.TextImg("Colors:", "doc_array.png");

							foreach (int i in star.Colors)
							{
								TreeViewItem c = new TreeViewItem();
								c.Header = Extras.TextImg(i.ToString(), "doc_int.png");
								colors.Items.Add(c);
							}
							colors.IsExpanded = true;
							x.Items.Add(colors);
						}

						if (star.FadeColors != null)
						{
							TreeViewItem fade = new TreeViewItem();
							fade.Header = Extras.TextImg("FadeColors:", "doc_array.png");

							foreach (int i in star.Colors)
							{
								TreeViewItem c = new TreeViewItem();
								c.Header = Extras.TextImg(i.ToString(), "doc_int.png");
								fade.Items.Add(c);
							}
							fade.IsExpanded = true;
							x.Items.Add(fade);
						}

						x.IsExpanded = true;
						e.Items.Add(x);
					}

					e.IsExpanded = true;
					fw.Items.Add(e);
				}

				fw.IsExpanded = true;
				tag.Items.Add(fw);
			}
			#endregion

			#region other
			if (SkullOwner != "")
			{
				TreeViewItem skull = new TreeViewItem();
				skull.Header = Extras.TextImg("SkullOwner: " + SkullOwner,
					"doc_string.png");
				tag.Items.Add(skull);
			}

			if (map_is_scaling.HasValue)
			{
				TreeViewItem map = new TreeViewItem();
				map.Header = Extras.TextImg("Map_is_scaling: "
					+ map_is_scaling.ToString(),"doc_byte.png");
				tag.Items.Add(map);
			}
			#endregion

			tag.IsExpanded = true;
			return tag;
		}

		public string ToJSON()
		{
			return Extras.Serialize(this);
		}

		public bool isEmpty()
		{
			return (ToJSON() == "{}" || ToJSON() == "");
		}

		public static List<EnchantmentTag> 
			AvailableEnchantments(EnchantmentType type)
		{
			List<EnchantmentTag> e = new List<EnchantmentTag>();

			switch (type)
			{
			case EnchantmentType.None:
				break;
			case EnchantmentType.Sword:
				for (short i = 16; i <= 21; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.Bow:
				e.Add(new EnchantmentTag(34));
				for (short i = 48; i <= 51; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				break;
			case EnchantmentType.PickShovel:
				e.Add(new EnchantmentTag(32));
				e.Add(new EnchantmentTag(33));
				e.Add(new EnchantmentTag(34));
				e.Add(new EnchantmentTag(35));
				break;
			case EnchantmentType.Axe:
				e.Add(new EnchantmentTag(32));
				e.Add(new EnchantmentTag(33));
				e.Add(new EnchantmentTag(34));
				e.Add(new EnchantmentTag(35));
				e.Add(new EnchantmentTag(16));
				e.Add(new EnchantmentTag(17));
				e.Add(new EnchantmentTag(18));
				break;
			case EnchantmentType.Armor:
				e.Add(new EnchantmentTag(0));
				e.Add(new EnchantmentTag(1));
				e.Add(new EnchantmentTag(3));
				e.Add(new EnchantmentTag(4));
				e.Add(new EnchantmentTag(7));
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.Helmet:
				e.Add(new EnchantmentTag(0));
				e.Add(new EnchantmentTag(1));
				e.Add(new EnchantmentTag(3));
				e.Add(new EnchantmentTag(4));
				e.Add(new EnchantmentTag(5));
				e.Add(new EnchantmentTag(6));
				e.Add(new EnchantmentTag(7));
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.Boots:
				e.Add(new EnchantmentTag(0));
				e.Add(new EnchantmentTag(1));
				e.Add(new EnchantmentTag(2));
				e.Add(new EnchantmentTag(3));
				e.Add(new EnchantmentTag(4));
				e.Add(new EnchantmentTag(7));
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.Rod:
				e.Add(new EnchantmentTag(61));
				e.Add(new EnchantmentTag(62));
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.DurableOnly:
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.Shears:
				e.Add(new EnchantmentTag(32));
				e.Add(new EnchantmentTag(33));
				e.Add(new EnchantmentTag(34));
				break;
			case EnchantmentType.All:
				for (short i = 0; i <= 7; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				for (short i = 16; i <= 21; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				for (short i = 32; i <= 35; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				for (short i = 48; i <= 51; i++)
				{
					e.Add(new EnchantmentTag(i));
				}
				e.Add(new EnchantmentTag(61));
				e.Add(new EnchantmentTag(62));
				break;
			default:
				break;
			}

			return e;
		}
		public static Dictionary<int, string> EnchantmentFriendlyNames()
		{
			Dictionary<int, string> e = new Dictionary<int, string>();

			e.Add(0, "Protection");
			e.Add(1, "Fire Protection");
			e.Add(2, "Feather Falling");
			e.Add(3, "Blast Protection");
			e.Add(4, "Projectile Protection");
			e.Add(5, "Respiration");
			e.Add(6, "Aqua Affinity");
			e.Add(7, "Thorns");

			e.Add(16, "Sharpness");
			e.Add(17, "Smite");
			e.Add(18, "Bane of Anthropods");
			e.Add(19, "Knockback");
			e.Add(20, "Fire Aspect");
			e.Add(21, "Looting");

			e.Add(32, "Efficiency");
			e.Add(33, "Silk Touch");
			e.Add(34, "Unbreaking"); //For everything
			e.Add(35, "Fortune");

			e.Add(48, "Power");
			e.Add(49, "Punch");
			e.Add(50, "Flame");
			e.Add(51, "Infinity");

			e.Add(61, "Luck of the Sea");
			e.Add(62, "Lure");

			return e;
		}

		public static List<byte> NormalPotionEffects()
		{
			List<byte> e = new List<byte>();

			for (byte i = 0; i <= 14; i++)
			{
				e.Add(i);
			}

			e.Remove(7);
			e.Remove(11);

			return e;
		}
		public static List<byte> AllPotionEffects()
		{
			List<byte> e = new List<byte>();

			for (byte i = 1; i <= 23; i++ )
			{
				e.Add(i);
			}

			return e;
		}
		public static Dictionary<byte, string> PotionEffectFriendlyNames()
		{
			Dictionary<byte, string> e = new Dictionary<byte, string>();

			e.Add(1, "Speed");
			e.Add(2, "Slowness");
			e.Add(3, "Haste");
			e.Add(4, "Mining Fatigue");
			e.Add(5, "Strength");
			e.Add(6, "Instant Health");
			e.Add(7, "Instant Damage");
			e.Add(8, "Jump Boost");
			e.Add(9, "Nausea");
			e.Add(10, "Regeneration");
			e.Add(11, "Resistance");
			e.Add(12, "Fire Resistance");
			e.Add(13, "Water Breathing");
			e.Add(14, "Invisibility");
			e.Add(15, "Blindness");
			e.Add(16, "Night Vision");
			e.Add(17, "Hunger");
			e.Add(18, "Weakness");
			e.Add(19, "Poison");
			e.Add(20, "Wither");
			e.Add(21, "Health Boost");
			e.Add(22, "Absorption");
			e.Add(23, "Saturation");

			return e;
		}
		public static Dictionary<byte, string> PotionDamageFriendlyNames()
		{
			Dictionary<byte, string> e = new Dictionary<byte, string>();

			e.Add(0, "No Effect (Water Bottle)");

			e.Add(1, "Regeneration");
			e.Add(2, "Swiftness");
			e.Add(3, "Fire Resistance");
			e.Add(4, "Poison");
			e.Add(5, "Healing");
			e.Add(6, "Night Vision");

			e.Add(8, "Weakness");
			e.Add(9, "Strength");
			e.Add(10, "Slowness");

			e.Add(12, "Harming");
			e.Add(13, "Water Breathing");
			e.Add(14, "Invisibility");

			return e;
		}
	}
}
