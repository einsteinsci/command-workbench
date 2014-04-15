using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandGeneratorWPF;

namespace CommandGeneratorWPF.NBT
{
	#region enums

	/// <summary>
	/// Used for available enchantments
	/// </summary>
	public enum EnchantmentType : byte
	{
		None = 0,
		Sword,
		Bow,
		PickShovel,
		Axe,
		Armor,
		Helmet,
		Boots,
		Rod,
		DurableOnly,
		Shears,
		All,
	}

	/// <summary>
	/// Type of Wood. Applies to Leaves and saplings as well
	/// </summary>
	public enum WoodType : short
	{
		Oak = 0,
		Spruce,
		Birch,
		Jungle,
		Acacia,
		Darkwood,
	}

	/// <summary>
	/// Color. Applies to all color-based items
	/// </summary>
	public enum DyeType : byte
	{
		White = 0,
		Orange,
		Magenta,
		LightBlue,
		Yellow,
		Lime,
		Pink,
		Gray,
		Silver,
		Cyan,
		Purple,
		Blue,
		Brown,
		Green,
		Red,
		Black,
	}

	/// <summary>
	/// Type of Flower
	/// </summary>
	public enum FlowerType : byte
	{
		Poppy = 0,
		BlueOrchid,
		Allium,
		AzureBluet,
		TulipRed,
		TulipOrange,
		TulipWhite,
		TulipPink,
		OxeyeDaisy
	}

	/// <summary>
	/// Used because of Minecraft's odd way of putting different stone-like
	/// slabs together under a single ID
	/// </summary>
	public enum StoneSpecialType : byte
	{
		Stone = 0,
		Sandstone,
		Wood,
		Cobble,
		Brick,
		StoneBrick,
		NetherBrick,
		Quartz
	}

	/// <summary>
	/// To determine quickly which folder the item's inventory icon is in
	/// </summary>
	public enum ImageSourceType
	{
		Blocks,
		Items,
		Blocks_Tech
	};
	#endregion

	public partial class InventoryItem
	{
		#region constants

		public readonly string[] _tileEntityNames =
		{
			"dispenser",
			"noteblock",
			"mob_spawner",
			"chest",
			"furnace",
			"lit_furnace",
			"jukebox",
			"enchanting_table",
			"ender_chest",
			"command_block",
			"beacon",
			"trapped_chest",
			"daylight_detector",
			"hopper",
			"dropper",
		};

		public readonly string[] _colorStuffNames =
		{
			"wool",
			"stained_glass",
			"stained_hardened_clay",
			"stained_glass_pane",
			"dye",
		};

		#endregion

		public int ID
		{ get; set; }

		public short Metadata
		{ get; set; }

		public bool Technical
		{ get; private set; }
		public bool WrongName
		{ get; private set; }

		public string ShortName
		{ get; private set; }

		public string FriendlyName
		{ get; private set; }

		public EnchantmentType EnchantabilityType
		{ get; private set; }

		public string ImageSource
		{ get; private set; }

		#region Dependent Properties

		public string FullData
		{
			get
			{
				return ToString();
			}
		}
		public string ParentheticalID
		{
			get
			{
				return "(" + ID.ToString() + ")";
			}
		}

		public string OfficialName
		{
			get
			{
				return "minecraft:" + ShortName;
			}
		}

		public bool IsBlock
		{
			get
			{
				return ID < 256;
			}
		}

		public bool IsDurableAble
		{
			get
			{
				return DurabilityByName().ContainsKey(ShortName);
			}
		}
		public int? MaxDurability
		{
			get
			{
				if (!IsDurableAble)
				{
					return null;
				}
				else
				{
					return DurabilityByName()[ShortName];
				}
			}
		}

		public bool IsColorable
		{
			get
			{
				return _colorStuffNames.Contains(ShortName);
			}
		}

		public bool IsLeatherArmor
		{
			get
			{
				if (ShortName == "leather_helmet" ||
					ShortName == "leather_chestplate" ||
					ShortName == "leather_leggings" ||
					ShortName == "leather_boots")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool IsEnchantable
		{
			get
			{
				return ListEnchantables().Contains(this);
			}
		}
		public bool IsEnchantedBook
		{
			get
			{
				return ShortName == "enchanted_book";
			}
		}

		public bool IsPotion
		{
			get
			{
				return ShortName == "potion";
			}
		}
		public bool? IsSplashPotion
		{
			get
			{
				if (!IsPotion)
				{
					return null;
				}
				else
				{
					if (Metadata > 16000)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}

		public bool IsBook
		{
			get
			{
				return ShortName == "writable_book" || 
					ShortName == "written_book";
			}
		}

		public bool IsPlayerSkull
		{
			get
			{
				return (ShortName == "skull") &&
					Metadata == 3;
			}
		}

		public bool IsFireworksStar
		{
			get
			{
				return ShortName == "fireworks_charge";
			}
		}
		public bool IsFireworksRocket
		{
			get
			{
				return ShortName == "fireworks";
			}
		}

		public bool IsMap
		{
			get
			{
				return ShortName == "filled_map";
			}
		}

		public int MaxStackSize
		{
			get
			{
				return MaxStackSizeByName()[ShortName];
			}
		}

		#endregion

		public InventoryItem(int id, string name, string friendlyName, 
			ImageSourceType imgType, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = 0;
			Technical = false;
			WrongName = false;
			FriendlyName = friendlyName;
			EnchantabilityType = EnchantmentType.None;

			switch (imgType)
			{
			case ImageSourceType.Blocks:
				ImageSource = "assets/blocks/Grid_" + imgSource;
				break;
			case ImageSourceType.Items:
				imgSource = imgSource.ToLower();
				ImageSource = "assets/items/" + imgSource.ToLower();
				break;
			case ImageSourceType.Blocks_Tech:
				ImageSource = "assets/blocks/technical/" + imgSource;
				break;
			default:
				ImageSource = "assets/error.png";
				break;
			}
		}
		public InventoryItem(int id, string name, string friendlyname, 
			EnchantmentType twaType, ImageSourceType imgType, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = 0;
			Technical = false;
			WrongName = false;
			FriendlyName = friendlyname;
			EnchantabilityType = twaType;

			switch (imgType)
			{
			case ImageSourceType.Blocks:
				ImageSource = "assets/blocks/Grid_" + imgSource;
				break;
			case ImageSourceType.Items:
				imgSource = imgSource.ToLower();
				ImageSource = "assets/items/" + imgSource.ToLower();
				break;
			case ImageSourceType.Blocks_Tech:
				ImageSource = "assets/blocks/technical/" + imgSource;
				break;
			default:
				ImageSource = "assets/error.png";
				break;
			}
		}
		public InventoryItem(int id, string name, short meta, string friendlyName,
			ImageSourceType imgType, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = meta;
			Technical = false;
			WrongName = false;
			FriendlyName = friendlyName;
			EnchantabilityType = EnchantmentType.None;

			switch (imgType)
			{
			case ImageSourceType.Blocks:
				ImageSource = "assets/blocks/Grid_" + imgSource;
				break;
			case ImageSourceType.Items:
				ImageSource = "assets/items/" + imgSource.ToLower();
				break;
			case ImageSourceType.Blocks_Tech:
				ImageSource = "assets/blocks/technical/" + imgSource;
				break;
			default:
				ImageSource = "assets/error.png";
				break;
			}
		}
		public InventoryItem(int id, string name, short meta, bool tech, 
			string friendlyName, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = meta;
			Technical = tech;
			WrongName = false;
			FriendlyName = friendlyName;
			EnchantabilityType = EnchantmentType.None;
			ImageSource = "assets/blocks/technical/" + imgSource;
		}
		public InventoryItem(int id, string name, short meta, bool tech, bool funky, 
			string friendlyName, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = meta;
			Technical = tech;
			WrongName = funky;
			FriendlyName = friendlyName;
			EnchantabilityType = EnchantmentType.None;
			ImageSource = "assets/blocks/technical/" + imgSource;
		}
		public InventoryItem(int id, string name, short meta, bool tech, bool funky,
		   string friendlyName, EnchantmentType twaType, string imgSource)
		{
			ID = id;
			ShortName = name.ToLower();
			Metadata = meta;
			Technical = tech;
			WrongName = funky;
			FriendlyName = friendlyName;
			EnchantabilityType = twaType;
			ImageSource = "assets/blocks/technical/" + imgSource;
		}

		public override string ToString()
		{
			return ShortName + " (" + ID + ") : " + Metadata;
		}

		public static int Compare(InventoryItem a, InventoryItem b)
		{
			int delta = a.ID - b.ID;
			delta *= 64; // ID is worth 64x metadata

			if (delta == 0)
			{
				delta = a.Metadata - b.Metadata;
			}

			return delta;
		}

		//public override bool Equals(object obj)
		//{
		//	try
		//	{
		//		return Equals((InventoryItem)obj);
		//	}
		//	catch (InvalidCastException)
		//	{
		//		return false;
		//	}
		//}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public bool Equals(InventoryItem item)
		{
			if (base.Equals(null))
			{
				return false;
			}

			return (item.ShortName == this.ShortName) &&
				(item.Metadata == this.Metadata);
		}

		//public static bool operator ==(InventoryItem a, InventoryItem b)
		//{
		//	bool result = false;
		//	try
		//	{
		//		result = a.Equals(b);
		//	}
		//	catch (NullReferenceException)
		//	{
		//		return false;
		//	}

		//	return result;
		//}
		//public static bool operator !=(InventoryItem a, InventoryItem b)
		//{
		//	bool result = false;
		//	try
		//	{
		//		result = !a.Equals(b);
		//	}
		//	catch (NullReferenceException)
		//	{
		//		return false;
		//	}

		//	return result;
		//}
	}
}