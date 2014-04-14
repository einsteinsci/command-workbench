using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public partial class InventoryItem
	{
		public static InventoryItem BlockStone()
		{
			return new InventoryItem(1, "stone", 0, "Stone", 
				ImageSourceType.Blocks, "Stone.png");
		}
		public static InventoryItem BlockGrass()
		{
			return new InventoryItem(2, "grass", 0, "Grass", 
				ImageSourceType.Blocks, "Grass_Block.png");
		}
		public static InventoryItem BlockDirt()
		{
			return new InventoryItem(3, "dirt", 0, "Dirt",
				ImageSourceType.Blocks, "Dirt.png");
		}
		public static InventoryItem BlockDirtGrassless_Tech()
		{
			return new InventoryItem(3, "dirt", 1, true,
				"Grassless Dirt (tech)", "Dirt.png");
		}
		public static InventoryItem BlockPodzol()
		{
			return new InventoryItem(3, "dirt", 2, "Podzol",
				ImageSourceType.Blocks, "Podzol.png");
		}
		public static InventoryItem BlockCobblestone()
		{
			return new InventoryItem(4, "cobblestone", 0,
				"Cobblestone", ImageSourceType.Blocks, 
				"Cobblestone.png");
		}
		public static InventoryItem BlockWoodPlanks(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new InventoryItem(5, "planks", (short)type,
					"Oak Planks", ImageSourceType.Blocks,
					"Oak_Wood_Planks.png");
			case WoodType.Spruce:
				return new InventoryItem(5, "planks", (short)type,
					"Spruce Planks", ImageSourceType.Blocks,
					"Spruce_Wood_Planks.png");
			case WoodType.Birch:
				return new InventoryItem(5, "planks", (short)type,
					"Birch Planks", ImageSourceType.Blocks,
					"Birch_Wood_Planks.png");
			case WoodType.Jungle:
				return new InventoryItem(5, "planks", (short)type,
					"Jungle Wood Planks", ImageSourceType.Blocks,
					"Jungle_Wood_Planks.png");
			case WoodType.Acacia:
				return new InventoryItem(5, "planks", (short)type,
					"Acacia Planks", ImageSourceType.Blocks,
					"Acacia_Wood_Planks.png");
			case WoodType.Darkwood:
				return new InventoryItem(5, "planks", (short)type,
					"Dark_Oak Planks", ImageSourceType.Blocks,
					"Dark_Oak_Wood_Planks.png");
			default:
				return new InventoryItem(5, "planks", (short)type,
					"Oak Planks", ImageSourceType.Blocks,
					"Oak_Wood_Planks.png");
			}
		}
		public static InventoryItem BlockSapling(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new InventoryItem(5, "sapling", (short)type,
					"Oak Sapling", ImageSourceType.Blocks,
					"Oak_Sapling.png");
			case WoodType.Spruce:
				return new InventoryItem(5, "sapling", (short)type,
					"Spruce Sapling", ImageSourceType.Blocks,
					"Spruce_Sapling.png");
			case WoodType.Birch:
				return new InventoryItem(5, "sapling", (short)type,
					"Birch Sapling", ImageSourceType.Blocks,
					"Birch_Sapling.png");
			case WoodType.Jungle:
				return new InventoryItem(5, "sapling", (short)type,
					"Jungle Wood Sapling", ImageSourceType.Blocks,
					"Jungle_Sapling.png");
			case WoodType.Acacia:
				return new InventoryItem(5, "sapling", (short)type,
					"Acacia Sapling", ImageSourceType.Blocks,
					"Acacia_Sapling.png");
			case WoodType.Darkwood:
				return new InventoryItem(5, "sapling", (short)type,
					"Dark Oak Sapling", ImageSourceType.Blocks,
					"Dark_Oak_Sapling.png");
			default:
				return new InventoryItem(5, "sapling", (short)type,
					"Oak Sapling", ImageSourceType.Blocks,
					"Oak_Sapling.png");
			}
		}
		public static InventoryItem BlockBedrock()
		{
			return new InventoryItem(7, "bedrock", "Bedrock",
				ImageSourceType.Blocks, "Bedrock.png");
		}
		public static InventoryItem BlockWaterFlowing_Tech()
		{
			return new InventoryItem(8, "flowing_water", 0, true,
				"Flowing Water Block (tech)", "Grid_Water.png");
		}
		public static InventoryItem BlockWaterStationary_Tech()
		{
			return new InventoryItem(9, "water", 0, true, 
				"Stationary Water Block (tech)", "Grid_Water.png");
		}
		public static InventoryItem BlockLavaFlowing_Tech()
		{
			return new InventoryItem(10, "flowing_lava", 0, true,
				"Flowing Lava Block (tech)", "Grid_Lava.png");
		}
		public static InventoryItem BlockLavaStationary_Tech()
		{
			return new InventoryItem(11, "lava", 0, true,
				"Stationary Lava Block (tech)", "Grid_Lava.png");
		}
		public static InventoryItem BlockSand()
		{
			return new InventoryItem(12, "sand", "Sand",
				ImageSourceType.Blocks, "Sand.png");
		}
		public static InventoryItem BlockRedSand()
		{
			return new InventoryItem(12, "sand", 1, "Red Sand",
				ImageSourceType.Blocks, "Red_Sand.png");
		}
		public static InventoryItem BlockGravel()
		{
			return new InventoryItem(13, "gravel", "Gravel",
				ImageSourceType.Blocks, "Gravel.png");
		}
		public static InventoryItem BlockOreGold()
		{
			return new InventoryItem(14, "gold_ore", "Gold Ore",
				ImageSourceType.Blocks, "Gold_Ore.png");
		}
		public static InventoryItem BlockOreIron()
		{
			return new InventoryItem(15, "iron_ore", "Iron Ore",
				ImageSourceType.Blocks, "Iron_Ore.png");
		}
		public static InventoryItem BlockOreCoal()
		{
			return new InventoryItem(16, "coal_ore", "Coal Ore",
				ImageSourceType.Blocks, "Coal_Ore.png");
		}
		public static InventoryItem BlockLog(WoodType type)
		{
			if ((short)type >= (short)WoodType.Acacia)
			{
				switch (type)
				{
				case WoodType.Acacia:
					return new InventoryItem(162, "log2",
						(short)((short)type - (short)WoodType.Acacia),
						"Acacia Log", ImageSourceType.Blocks, 
						"Acacia_Wood.png");
				case WoodType.Darkwood:
					return new InventoryItem(162, "log2",
						(short)((short)type - (short)WoodType.Acacia),
						"Dark Oak Log", ImageSourceType.Blocks,
						"Dark_Oak_Wood.png");
				default:
					return new InventoryItem(162, "log2",
						(short)((short)type - (short)WoodType.Acacia),
						"Unknown Log", ImageSourceType.Blocks,
						"Acacia_Wood.png");
				}
			}
			else
			{
				switch (type)
				{
				case WoodType.Oak:
					return new InventoryItem(17, "log", (short)type,
						"Oak Log", ImageSourceType.Blocks, 
						"Oak_Wood.png");
				case WoodType.Spruce:
					return new InventoryItem(17, "log", (short)type,
						"Spruce Log", ImageSourceType.Blocks, 
						"Spruce_Wood.png");
				case WoodType.Birch:
					return new InventoryItem(17, "log", (short)type,
						"Birch Log", ImageSourceType.Blocks,
						"Birch_Wood.png");
				case WoodType.Jungle:
					return new InventoryItem(17, "log", (short)type,
						"Jungle Log", ImageSourceType.Blocks,
						"Jungle_Wood.png");
				default:
					return new InventoryItem(17, "log", (short)type,
						"Unknown Log", ImageSourceType.Blocks,
						"Oak_Wood.png");
				}
			}
		}
		public static InventoryItem BlockLog2(WoodType type)
		{
			return BlockLog(type);
		}
		public static InventoryItem BlockLeaves(WoodType type)
		{
			if ((int)type >= (int)WoodType.Acacia)
			{
				switch (type)
				{
				case WoodType.Acacia:
					return new InventoryItem(163, "leaves2",
						(short)((int)type - (int)WoodType.Acacia),
						"Acacia Leaves", ImageSourceType.Blocks,
						"Acacia_Leaves.png");
				case WoodType.Darkwood:
					return new InventoryItem(163, "leaves2",
						(short)((int)type - (int)WoodType.Acacia),
						"Dark Oak Leaves", ImageSourceType.Blocks,
						"Dark_Oak_Leaves.png");
				default:
					return new InventoryItem(163, "leaves2",
						(short)((int)type - (int)WoodType.Acacia),
						"Unknown Leaves", ImageSourceType.Blocks,
						"Acacia_Leaves.png");
				}
			}
			else
			{
				switch (type)
				{
				case WoodType.Oak:
					return new InventoryItem(18, "leaves", (short)type,
						"Oak Leaves", ImageSourceType.Blocks,
						"Oak_Leaves.png");
				case WoodType.Spruce:
					return new InventoryItem(18, "leaves", (short)type,
						"Spruce Leaves", ImageSourceType.Blocks,
						"Spruce_Leaves.png");
				case WoodType.Birch:
					return new InventoryItem(18, "leaves", (short)type,
						"Birch Leaves", ImageSourceType.Blocks,
						"Birch_Leaves.png");
				case WoodType.Jungle:
					return new InventoryItem(18, "leaves", (short)type,
						"Jungle Leaves", ImageSourceType.Blocks,
						"Jungle_Leaves.png");
				default:
					return new InventoryItem(18, "leaves", (short)type,
						"Unknown Leaves", ImageSourceType.Blocks,
						"Oak_Leaves.png");
				}
			}
		}
		public static InventoryItem BlockLeaves2(WoodType type)
		{
			return BlockLeaves(type);
		}
		public static InventoryItem BlockSponge()
		{
			return new InventoryItem(19, "sponge", "Sponge",
				ImageSourceType.Blocks, "Sponge.png");
		}
		public static InventoryItem BlockGlass()
		{
			return new InventoryItem(20, "glass", "Glass Block",
				ImageSourceType.Blocks, "Glass.png");
		}
		public static InventoryItem BlockOreLapis()
		{
			return new InventoryItem(21, "lapis_ore", "Lapis Lazuli Ore",
				ImageSourceType.Blocks, "Lapis_Lazuli_Ore.png");
		}
		public static InventoryItem BlockStorageLapis()
		{
			return new InventoryItem(22, "lapis_block", "Lapis Lazuli Block",
				ImageSourceType.Blocks, "Lapis_Lazuli_Block.png");
		}
		public static InventoryItem BlockDispenser()
		{
			return new InventoryItem(23, "dispenser", "Dispenser",
				ImageSourceType.Blocks, "Dispenser.png");
		}
		public static InventoryItem BlockSandstone()
		{
			return new InventoryItem(24, "sandstone", "Sandstone",
				ImageSourceType.Blocks, "Sandstone.png");
		}
		public static InventoryItem BlockSandstoneEtched()
		{
			return new InventoryItem(24, "sandstone", 1, "Creeper Sandstone",
				ImageSourceType.Blocks, "Chiseled_Sandstone.png");
		}
		public static InventoryItem BlockSandstoneSmooth()
		{
			return new InventoryItem(24, "sandstone", 2, "Smooth Sandstone",
				ImageSourceType.Blocks, "Smooth_Sandstone.png");
		}
		public static InventoryItem BlockNoteBlock()
		{
			return new InventoryItem(25, "noteblock", "Note Block",
				ImageSourceType.Blocks, "Note_Block.png");
		}
		// possibly unobtainable. Returns bed item when using strings
		public static InventoryItem BlockBed_Tech(short meta)
		{
			return new InventoryItem(26, "bed", meta, true, true, 
				"Bed Block (tech)", "Bed.png");
		}
		public static InventoryItem BlockRailGold()
		{
			return new InventoryItem(27, "golden_rail", "Powered Rail",
				ImageSourceType.Blocks, "Powered_Rail.png");
		}
		public static InventoryItem BlockRailDetector()
		{
			return new InventoryItem(28, "detector_rail", "Detector Rail",
				ImageSourceType.Blocks, "Detector_Rail.png");
		}
		public static InventoryItem BlockPistonSticky()
		{
			return new InventoryItem(29, "sticky_piston", "Sticky Piston",
				ImageSourceType.Blocks, "Sticky_Piston.png");
		}
		public static InventoryItem BlockWeb()
		{
			return new InventoryItem(30, "web", "Cobweb",
				ImageSourceType.Blocks, "Cobweb.png");
		}
		public static InventoryItem BlockTallGrass()
		{
			return new InventoryItem(31, "tallgrass", "Tall Grass",
				ImageSourceType.Blocks, "Grass.png");
		}
		public static InventoryItem BlockDeadBush()
		{
			return new InventoryItem(32, "deadbush", "Desert Shrub",
				ImageSourceType.Blocks, "Dead_Bush.png");
		}
		public static InventoryItem BlockPiston()
		{
			return new InventoryItem(33, "piston", "Piston",
				ImageSourceType.Blocks, "Piston.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockPistonHead_Tech()
		{
			return new InventoryItem(34, "piston_head", 0, true, 
				"Piston Head (tech)", "Block_34.png");
		}
		public static InventoryItem BlockWool(DyeType color)
		{
			switch (color)
			{
			case DyeType.White:
				return new InventoryItem(35, "wool", (short)color,
					"White Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Orange:
				return new InventoryItem(35, "wool", (short)color,
					"Orange Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Magenta:
				return new InventoryItem(35, "wool", (short)color,
					"Magenta Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.LightBlue:
				return new InventoryItem(35, "wool", (short)color,
					"Light Blue Wool", ImageSourceType.Blocks,
					"Light_Blue_Wool.png");
			case DyeType.Yellow:
				return new InventoryItem(35, "wool", (short)color,
					"Yellow Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Lime:
				return new InventoryItem(35, "wool", (short)color,
					"Lime Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Pink:
				return new InventoryItem(35, "wool", (short)color,
					"Pink Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Gray:
				return new InventoryItem(35, "wool", (short)color,
					"Gray Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Silver:
				return new InventoryItem(35, "wool", (short)color,
					"Light Gray Wool", ImageSourceType.Blocks,
					"Light_Gray_Wool.png");
			case DyeType.Cyan:
				return new InventoryItem(35, "wool", (short)color,
					"Cyan Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Purple:
				return new InventoryItem(35, "wool", (short)color,
					"Purple Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Blue:
				return new InventoryItem(35, "wool", (short)color,
					"Blue Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Brown:
				return new InventoryItem(35, "wool", (short)color,
					"Brown Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Green:
				return new InventoryItem(35, "wool", (short)color,
					"Green Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Red:
				return new InventoryItem(35, "wool", (short)color,
					"Red Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			case DyeType.Black:
				return new InventoryItem(35, "wool", (short)color,
					"Black Wool", ImageSourceType.Blocks,
					color.ToString() + "_Wool.png");
			default:
				return new InventoryItem(35, "wool", (short)color,
					"Unknown Color Wool", ImageSourceType.Blocks, 
					"White_Wool.png");
			}
		}
		// possibly unobtainable
		public static InventoryItem BlockPistonExtender_Tech()
		{
			return new InventoryItem(36, "piston_extension", 0, true, 
				"Piston Extension (tech)", "Block_34.png");
		}
		public static InventoryItem BlockFlowerYellow()
		{
			return new InventoryItem(37, "yellow_flower", "Dandelion",
				ImageSourceType.Blocks, "Dandelion.png");
		}
		public static InventoryItem BlockFlowerOther(FlowerType type)
		{
			switch (type)
			{
			case FlowerType.Poppy:
				return new InventoryItem(38, "red_flower", (short)type, 
					"Poppy", ImageSourceType.Blocks, "Poppy.png");
			case FlowerType.BlueOrchid:
				return new InventoryItem(38, "red_flower", (short)type, 
					"Blue Orchid", ImageSourceType.Blocks, "Blue_Orchid.png");
			case FlowerType.Allium:
				return new InventoryItem(38, "red_flower", (short)type,
					"Allium", ImageSourceType.Blocks, "Allium.png");
			case FlowerType.AzureBluet:
				return new InventoryItem(38, "red_flower", (short)type,
					"Azure Bluet", ImageSourceType.Blocks, "Azure_Bluet.png");
			case FlowerType.TulipRed:
				return new InventoryItem(38, "red_flower", (short)type,
					"Red Tulip", ImageSourceType.Blocks, "Red_Tulip.png");
			case FlowerType.TulipOrange:
				return new InventoryItem(38, "red_flower", (short)type,
					"Orange Tulip", ImageSourceType.Blocks, 
					"Orange_Tulip.png");
			case FlowerType.TulipWhite:
				return new InventoryItem(38, "red_flower", (short)type,
					"White Tulip", ImageSourceType.Blocks, "White_Tulip.png");
			case FlowerType.TulipPink:
				return new InventoryItem(38, "red_flower", (short)type,
					"Pink Tulip", ImageSourceType.Blocks, "Pink_Tulip.png");
			case FlowerType.OxeyeDaisy:
				return new InventoryItem(38, "red_flower", (short)type,
					"Oxeye Daisy", ImageSourceType.Blocks, "Oxeye_Daisy.png");
			default:
				return new InventoryItem(38, "red_flower", (short)type,
					"Unknown Flower", ImageSourceType.Blocks, "Poppy.png");
			}
		}
		public static InventoryItem BlockBrownMushroom()
		{
			return new InventoryItem(39, "brown_mushroom", "Brown Mushroom",
				ImageSourceType.Blocks, "Brown_Mushroom.png");
		}
		public static InventoryItem BlockRedMushroom()
		{
			return new InventoryItem(40, "red_mushroom", "Red Mushroom",
				ImageSourceType.Blocks, "Red_Mushroom.png");
		}
		public static InventoryItem BlockStorageGold()
		{
			return new InventoryItem(41, "gold_block", "Gold Block",
				ImageSourceType.Blocks, "Block_of_Gold.png");
		}
		public static InventoryItem BlockStorageIron()
		{
			return new InventoryItem(42, "iron_block", "Iron Block",
				ImageSourceType.Blocks, "Block_of_Iron.png");
		}
		// Anomaly: double_stone_slab has two extra forms of stone and sandstone,
		// with top face on all sides. Hence the int-dependent method as well.
		public static InventoryItem BlockDoubleStoneSlab_Tech(
			StoneSpecialType type)
		{
			return BlockDoubleStoneSlab_Tech((short)type);
		}
		public static InventoryItem BlockDoubleStoneSlab_Tech(short meta)
		{
			string friendly = ((StoneSpecialType)meta).ToString() + 
				" Double Slab (tech)";
			string img = "Double_Stone_Slab.png";

			switch (meta)
			{
			case (int)(StoneSpecialType.Stone):
				img = "Double_Stone_Slab.png";
				break;
			case (int)(StoneSpecialType.Sandstone):
				img = "Grid_Sandstone.png";
				break;
			case (int)(StoneSpecialType.Wood):
				img = "Grid_Oak_Wood_Planks.png";
				friendly = "Wood Stone Double Slab (tech)";
				break;
			case (int)(StoneSpecialType.Cobble):
				img = "Grid_Cobblestone.png";
				friendly = "Cobblestone Double Slab (tech)";
				break;
			case (int)(StoneSpecialType.Brick):
				img = "Grid_Bricks.png";
				break;
			case (int)(StoneSpecialType.StoneBrick):
				img = "Grid_Stone_Bricks.png";
				friendly = "Stone Brick Double Slab (tech)";
				break;
			case (int)(StoneSpecialType.NetherBrick):
				img = "Grid_Nether_Brick.png";
				friendly = "Nether Brick Double Slab (tech)";
				break;
			case (int)(StoneSpecialType.Quartz):
				img = "Grid_Block_of_Quartz.png";
				break;
			default:
				friendly = "Unknown Stone Double Slab (tech)";
				break;
			}

			return new InventoryItem(43, "double_stone_slab", meta, true, 
				friendly, img);
		}
		public static InventoryItem BlockStoneSlab(StoneSpecialType type)
		{
			switch (type)
			{
			case StoneSpecialType.Stone:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Stone Slab", ImageSourceType.Blocks,
					"Stone_Slab.png");
			case StoneSpecialType.Sandstone:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Sandstone Slab", ImageSourceType.Blocks,
					"Sandstone_Slab.png");
			case StoneSpecialType.Wood:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Wooden Stone Slab", ImageSourceType.Blocks,
					"Oak_Wood_Slab.png");
			case StoneSpecialType.Cobble:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Cobblestone Slab", ImageSourceType.Blocks,
					"Cobblestone_Slab.png");
			case StoneSpecialType.Brick:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Bricks Slab", ImageSourceType.Blocks,
					"Bricks_Slab.png");
			case StoneSpecialType.StoneBrick:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Stone Brick Slab", ImageSourceType.Blocks,
					"Stone_Bricks_Slab.png");
			case StoneSpecialType.NetherBrick:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Nether Brick Slab", ImageSourceType.Blocks,
					"Nether_Brick_Slab.png");
			case StoneSpecialType.Quartz:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Quartz Slab", ImageSourceType.Blocks,
					"Quartz_Slab.png");
			default:
				return new InventoryItem(44, "stone_slab", (short)type,
					"Unknown Slab", ImageSourceType.Blocks,
					"Stone_Slab.png");
			}
		}
		public static InventoryItem BlockBricks()
		{
			return new InventoryItem(45, "brick_block", "Bricks",
				ImageSourceType.Blocks, "Bricks.png");
		}
		public static InventoryItem BlockTNT()
		{
			return new InventoryItem(46, "tnt", "TNT",
				ImageSourceType.Blocks, "TNT.png");
		}
		public static InventoryItem BlockBookshelf()
		{
			return new InventoryItem(47, "bookshelf", "Bookshelf",
				ImageSourceType.Blocks, "Bookshelf.png");
		}
		public static InventoryItem BlockMossyCobblestone()
		{
			return new InventoryItem(48, "mossy_cobblestone", "Moss Stone",
				ImageSourceType.Blocks, "Moss_Stone.png");
		}
		public static InventoryItem BlockObsidian()
		{
			return new InventoryItem(49, "obsidian", "Obsidian",
				ImageSourceType.Blocks, "Obsidian.png");
		}
		public static InventoryItem BlockTorch()
		{
			return new InventoryItem(50, "torch", "Torch",
				ImageSourceType.Blocks, "Torch.png");
		}
		public static InventoryItem BlockFire()
		{
			return new InventoryItem(51, "fire", "Fire",
				ImageSourceType.Blocks, "Fire.gif");
		}
		// possibly unobtainable
		public static InventoryItem BlockSpawner()
		{
			return new InventoryItem(52, "mob_spawner", "Spawner",
				ImageSourceType.Blocks, "Monster_Spawner.png");
		}
		// separate from other wood types because of 1.2.5 and earlier,
		// though still used alongside new types
		public static InventoryItem BlockOakStairs()
		{
			return new InventoryItem(53, "oak_stairs", "Oak Stairs",
				ImageSourceType.Blocks, "Oak_Wood_Stairs.png");
		}
		public static InventoryItem BlockChest()
		{
			return new InventoryItem(54, "chest", "Chest",
				ImageSourceType.Blocks, "Chest.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockRedWire_Tech()
		{
			return new InventoryItem(55, "redstone_wire", 0, true, 
				"Redstone Wire (tech)", "Redstone_(Wire,Inventory).png");
		}
		public static InventoryItem BlockOreDiamond()
		{
			return new InventoryItem(56, "diamond_ore", "Diamond Ore",
				ImageSourceType.Blocks, "Diamond_ore.png");
		}
		public static InventoryItem BlockStorageDiamond()
		{
			return new InventoryItem(57, "diamond_block", "Diamond Block",
				ImageSourceType.Blocks, "Block_of_Diamond.png");
		}
		public static InventoryItem BlockWorkbench()
		{
			return new InventoryItem(58, "crafting_table", "Crafting Table",
				ImageSourceType.Blocks, "Crafting_Table.png");
		}
		// possibly onobtainable, returns wheat item when using strings
		public static InventoryItem BlockCropsWheat_Tech()
		{
			return new InventoryItem(59, "wheat", 7, true, true, 
				"Wheat Crops (tech)", "Wheat.gif");
		}
		public static InventoryItem BlockFarmland()
		{
			return new InventoryItem(60, "farmland", 0, true, "Farmland",
				"Grid_Farmland.png");
		}
		public static InventoryItem BlockFurnace()
		{
			return new InventoryItem(61, "furnace", "Furnace",
				ImageSourceType.Blocks, "Furnace.png");
		}
		public static InventoryItem BlockFurnaceLit_Tech()
		{
			return new InventoryItem(62, "lit_furnace", 0, true, 
				"Lit Furnace (tech)", "Furnace_(Active).png");
		}
		// possibly unobtainable
		public static InventoryItem BlockSignGround_Tech()
		{
			return new InventoryItem(63, "standing_sign", 0, true, 
				"Ground Sign Block (tech)", "Sign.png");
		}
		// possibly unobtainable, returns door item when using strings
		public static InventoryItem BlockWoodDoor_Tech()
		{
			return new InventoryItem(64, "wooden_door", 0, true, true, 
				"Wood Door Block (tech)", "Wooden_Door.png");
		}
		public static InventoryItem BlockLadder()
		{
			return new InventoryItem(65, "ladder", "Ladder",
				ImageSourceType.Blocks, "Ladder.png");
		}
		public static InventoryItem BlockRail()
		{
			return new InventoryItem(66, "rail", "Rail",
				ImageSourceType.Blocks, "Rail.png");
		}
		public static InventoryItem BlockStairsCobble()
		{
			return new InventoryItem(67, "stone_stairs",
				"Cobblestone Stairs",
				ImageSourceType.Blocks, "Cobblestone_Stairs.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockSignWall_Tech()
		{
			return new InventoryItem(68, "wall_sign", 0, true, 
				"Wall Sign Block (tech)", "Wall_Sign.png");
		}
		public static InventoryItem BlockLever()
		{
			return new InventoryItem(69, "lever", "Lever",
				ImageSourceType.Blocks, "Lever.png");
		}
		public static InventoryItem BlockStonePlate()
		{
			return new InventoryItem(70, "stone_pressure_plate",
				"Stone Pressure Plate",
				ImageSourceType.Blocks, "Stone_Pressure_Plate.png");
		}
		// possibly unobtainable, returns door item when using strings
		public static InventoryItem BlockIronDoor_Tech()
		{
			return new InventoryItem(71, "iron_door", 0, true, true, 
			"Door Block (tech)", "Iron_Door.png");
		}
		public static InventoryItem BlockWoodPlate()
		{
			return new InventoryItem(72, "wooden_pressure_plate",
				"Wood Pressure Plate",
				ImageSourceType.Blocks, "Wooden_Pressure_Plate.png");
		}
		public static InventoryItem BlockOreRedstone()
		{
			return new InventoryItem(73, "redstone_ore", "Redstone Ore",
				ImageSourceType.Blocks, "Redstone_Ore.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockOreRedstoneLit_Tech()
		{
			return new InventoryItem(74, "lit_redstone_ore", 0, true, 
				"Lit Redstone Ore (tech)", "Grid_Redstone_Ore.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockRedTorchOff_Tech()
		{
			return new InventoryItem(75, "unlit_redstone_torch", 0, true, 
				"Off Redstone Torch (tech)", 
				"Redstone_(Torch,_Inactive).png");
		}
		public static InventoryItem BlockRedTorch()
		{
			return new InventoryItem(76, "redstone_torch", "Redstone Torch",
				ImageSourceType.Blocks, "Redstone_Torch.png");
		}
		public static InventoryItem BlockButtonStone()
		{
			return new InventoryItem(77, "stone_button", "Stone Button",
				ImageSourceType.Blocks, "Stone_Button.png");
		}
		public static InventoryItem BlockSnowLayer()
		{
			return new InventoryItem(78, "snow_layer", "Layered Snow",
				ImageSourceType.Blocks, "Snow_(cover).png");
		}
		public static InventoryItem BlockIce()
		{
			return new InventoryItem(79, "ice", "Ice",
				ImageSourceType.Blocks, "Ice.png");
		}
		public static InventoryItem BlockSnow()
		{
			return new InventoryItem(80, "snow", "Snow Block",
				ImageSourceType.Blocks, "Snow.png");
		}
		public static InventoryItem BlockCactus()
		{
			return new InventoryItem(81, "cactus", "Cactus",
				ImageSourceType.Blocks, "Cactus.png");
		}
		public static InventoryItem BlockClay()
		{
			return new InventoryItem(82, "clay", "Clay",
				ImageSourceType.Blocks, "Clay_(block).png");
		}
		// possibly unobtainable, returns sugarcane item when using strings
		public static InventoryItem BlockReeds_Tech()
		{
			return new InventoryItem(83, "reeds", 0, true, true, 
				"Sugarcane Block (tech)", "Sugar_Canes.png");
		}
		public static InventoryItem BlockJukebox()
		{
			return new InventoryItem(84, "jukebox", "Jukebox",
				ImageSourceType.Blocks, "Jukebox.png");
		}
		public static InventoryItem BlockFence()
		{
			return new InventoryItem(85, "fence", "Fence",
				ImageSourceType.Blocks, "Fence.png");
		}
		public static InventoryItem BlockPumpkin()
		{
			return new InventoryItem(86, "punpkin", "Pumpkin",
				ImageSourceType.Blocks, "Pumpkin.png");
		}
		public static InventoryItem BlockNetherrack()
		{
			return new InventoryItem(87, "netherrack", "Netherrack",
				ImageSourceType.Blocks, "Netherrack.png");
		}
		public static InventoryItem BlockSoulSand()
		{
			return new InventoryItem(88, "soul_sand", "Soul Sand",
				ImageSourceType.Blocks, "Soul_Sand.png");
		}
		public static InventoryItem BlockGlowstone()
		{
			return new InventoryItem(89, "glowstone", "Glowstone",
				ImageSourceType.Blocks, "Glowstone.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockNetherPortal_Tech()
		{
			return new InventoryItem(90, "portal", 0, true, 
				"Nether Portal (tech)", "Portal.png");
		}
		public static InventoryItem BlockJackOLantern()
		{
			return new InventoryItem(91, "litpumpkin", "Jack O' Lantern",
				ImageSourceType.Blocks, "Jack_o'Lantern.png");
		}
		// probably unobtainable. returns cake item if strings are used
		public static InventoryItem BlockCake_Tech()
		{
			return new InventoryItem(92, "cake", 0, true, true, 
				"Lies (tech)", "Cake.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockRepeaterOff_Tech()
		{
			return new InventoryItem(93, "unpowered_repeater", 0, true, 
				"Off Redstone Repeater Block (tech)",
				"Redstone_(Repeater,_Inactive).gif");
		}
		// possibly unobtainable
		public static InventoryItem BlockRepeaterOn_Tech()
		{
			return new InventoryItem(94, "powered_repeater", 0, true, 
				"On Redstone Repeater Block (tech)",
				"Redstone_(Repeater,_Active).gif");
		}
		public static InventoryItem BlockGlassStained(DyeType color)
		{
			switch (color)
			{
			case DyeType.White:
				return new InventoryItem(95, "stained_glass", (short)color,
					"White Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Orange:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Orange Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Magenta:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Magenta Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.LightBlue:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Light Blue Stained Glass", ImageSourceType.Blocks,
					"Light_Blue_Stained_Glass.png");
			case DyeType.Yellow:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Yellow Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Lime:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Lime Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Pink:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Pink Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Gray:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Gray Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Silver:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Light Gray Stained Glass", ImageSourceType.Blocks,
					"Light_Gray_Stained_Glass.png");
			case DyeType.Cyan:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Cyan Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Purple:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Purple Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Blue:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Blue Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Brown:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Brown Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Green:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Green Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Red:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Red Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			case DyeType.Black:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Black Stained Glass", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass.png");
			default:
				return new InventoryItem(95, "stained_glass", (short)color,
					"Unknown Color Stained Glass", ImageSourceType.Blocks,
					"White_Stained_Glass.png");
			}
		}
		public static InventoryItem BlockTrapdoor()
		{
			return new InventoryItem(96, "trapdoor", "Trapdoor",
				ImageSourceType.Blocks, "Trapdoor.png");
		}
		public static InventoryItem BlockSilverfishStone()
		{
			return new InventoryItem(97, "monster_egg", 0,
				"Stone Silverfish Block",
				ImageSourceType.Blocks, "Stone.png");
		}
		public static InventoryItem BlockSilverfishCobble()
		{
			return new InventoryItem(97, "monster_egg", 1,
				"Cobblestone Silverfish Block",
				ImageSourceType.Blocks, "Cobblestone.png");
		}
		public static InventoryItem BlockSilverfishBrick()
		{
			return new InventoryItem(97, "monster_egg", 2,
				"Stone Silverfish Brick",
				ImageSourceType.Blocks, "Stone_Bricks.png");
		}
		public static InventoryItem BlockStoneBrick()
		{
			return new InventoryItem(98, "stonebrick", 0, "Stone Bricks",
				ImageSourceType.Blocks, "Stone_Bricks.png");
		}
		public static InventoryItem BlockMossyBrick()
		{
			return new InventoryItem(98, "stonebrick", 1,
				"Mossy Stone Bricks",
				ImageSourceType.Blocks, "Mossy_Stone_Bricks.png");
		}
		public static InventoryItem BlockCrackedBrick()
		{
			return new InventoryItem(98, "stonebrick", 2,
				"Cracked Stone Bricks",
				ImageSourceType.Blocks, "Cracked_Stone_Bricks.png");
		}
		public static InventoryItem BlockChiseledBrick()
		{
			return new InventoryItem(98, "stonebrick", 3,
				"Chiseled Stone Bricks",
				ImageSourceType.Blocks, "Chiseled_Stone_Bricks.png");
		}
		// technical, though obtainable in survival
		public static InventoryItem BlockBrownMushroomLarge_Tech(short orientation)
		{
			return new InventoryItem(99, "brown_mushroom_block",
				orientation, true, "Large Brown Mushroom Block (tech)",
				"150px-Huge_Brown_Mushroom.png");
		}
		// technical, though obtainable in survival
		public static InventoryItem BlockRedMushroomLarge_Tech(short orientation)
		{
			return new InventoryItem(100, "red_mushroom_block",
				orientation, true, "Large Red Mushroom Block (tech)",
				"150px-Huge_Red_Mushroom.png");
		}
		public static InventoryItem BlockIronBars()
		{
			return new InventoryItem(101, "iron_bars", "Iron Bars",
				ImageSourceType.Blocks, "Iron_Bars.png");
		}
		public static InventoryItem BlockGlassPane()
		{
			return new InventoryItem(102, "glass_pane", "Glass Pane",
				ImageSourceType.Blocks, "Glass_Pane.png");
		}
		public static InventoryItem BlockMelon()
		{
			return new InventoryItem(103, "melon_block", "Melon Block",
				ImageSourceType.Blocks, "Melon_(block).png");
		}
		// possibly unobtainable
		public static InventoryItem BlockStemPumpkin_Tech()
		{
			return new InventoryItem(104, "pumpkin_stem", 0, true, 
				"Pumpkin Stem", "Seed_Stem_Grown.png");
		}
		// possibly unobtainable
		public static InventoryItem BlockStemMelon_Tech()
		{
			return new InventoryItem(105, "melon_stem", 0, true,
				"Melon Stem", "Seed_Stem_Grown.png");
		}
		public static InventoryItem BlockVine()
		{
			return new InventoryItem(106, "vine", "Vines",
				ImageSourceType.Blocks, "Vines.png");
		}
		public static InventoryItem BlockFenceGate()
		{
			return new InventoryItem(107, "fence_gate", "Gate",
				ImageSourceType.Blocks, "Fence_Gate.png");
		}
		public static InventoryItem BlockStairsBrick()
		{
			return new InventoryItem(108, "brick_stairs", "Brick Stairs",
				ImageSourceType.Blocks, "Brick_Stairs.png");
		}
		public static InventoryItem BlockStairsStoneBrick()
		{
			return new InventoryItem(109, "stone_brick_stairs",
				"Stone Brick Stairs",
				ImageSourceType.Blocks, "Stone_Brick_Stairs.png");
		}
		public static InventoryItem BlockMycelium()
		{
			return new InventoryItem(110, "mycelium", "Mycelium",
				ImageSourceType.Blocks, "Mycelium.png");
		}
		public static InventoryItem BlockLilyPad()
		{
			return new InventoryItem(111, "waterlily", "Lily Pad",
				ImageSourceType.Blocks, "Lily_Pad.png");
		}
		public static InventoryItem BlockNetherBrick()
		{
			return new InventoryItem(112, "nether_brick", "Nether Brick",
				ImageSourceType.Blocks, "Nether_Brick.png");
		}
		public static InventoryItem BlockNetherBrickFence()
		{
			return new InventoryItem(113, "nether_brick_fence",
				"Nether Brick Fence",
				ImageSourceType.Blocks, "Nether_Brick_Fence.png");
		}
		public static InventoryItem BlockNetherBrickStairs()
		{
			return new InventoryItem(114, "nether_brick_stairs", "Nether Brick Stairs",
				ImageSourceType.Blocks, "Nether_Brick_Stairs.png");
		}
		// probably unobtainable; using strings gives nether wart item
		public static InventoryItem BlockNetherWart_Tech()
		{
			return new InventoryItem(115, "nether_wart", 0, true, true, "Nether Wart",
				"Nether_Wart.gif");
		}
		public static InventoryItem BlockEnchantingTable()
		{
			return new InventoryItem(116, "enchanting_table", "Enchanting Table",
				ImageSourceType.Blocks, "Enchantment_Table.png");
		}
		// probably unobtainable
		public static InventoryItem BlockBrewingStand_Tech()
		{
			return new InventoryItem(117, "brewing_stand", 0, true, true, "Brewing Stand",
				"Brewing_Stand.png");
		}
		// probably unobtainable. Using strings gives cauldron item
		public static InventoryItem BlockCauldron_Tech(short fill)
		{
			return new InventoryItem(118, "cauldron", fill, true, true, "Cauldron Block (tech)",
				"Cauldron.png");
		}
		// technical, though not unobtainable
		public static InventoryItem BlockEndPortal_Tech()
		{
			return new InventoryItem(119, "end_portal", 0, true, "End Portal (tech)",
				"End_Portal.png");
		}
		public static InventoryItem BlockEndPortalFrame()
		{
			return new InventoryItem(120, "end_portal_frame", "End Portal Frame",
				ImageSourceType.Blocks, "End_Portal_(block).png");
		}
		public static InventoryItem BlockEndStone()
		{
			return new InventoryItem(121, "end_stone", "End Stone",
				ImageSourceType.Blocks, "End_Stone.png");
		}
		public static InventoryItem BlockDragonEgg()
		{
			return new InventoryItem(122, "dragon_egg", "Dragon Egg",
				ImageSourceType.Blocks, "Dragon_Egg.png");
		}
		public static InventoryItem BlockRedstoneLampOff()
		{
			return new InventoryItem(123, "redstone_lamp", "Redstone Lamp",
				ImageSourceType.Blocks, "Redstone_Lamp.png");
		}
		public static InventoryItem BlockRedstoneLampOn_Tech()
		{
			return new InventoryItem(124, "lit_redstone_lamp", 0, true, 
				"On Redstone Lamp (tech)", "Redstone_Lamp_(Active).png");
		}
		// technical, though not unobtainable
		public static InventoryItem BlockDoubleWoodSlab_Tech(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Oak Double Slab (tech)",
					"Grid_Oak_Wood_Planks.png");
			case WoodType.Spruce:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Spruce Double Slab (tech)",
					"Grid_Spruce_Wood_Planks.png");
			case WoodType.Birch:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Birch Double Slab (tech)",
					"Grid_Birch_Wood_Planks.png");
			case WoodType.Jungle:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Jungle Double Slab (tech)",
					"Grid_Jungle_Wood_Planks.png");
			case WoodType.Acacia:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Acacia Double Slab (tech)",
					"Grid_Acacia_Wood_Planks.png");
			case WoodType.Darkwood:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Dark Oak Double Slab (tech)",
					"Grid_Dark_Oak_Wood_Planks.png");
			default:
				return new InventoryItem(125, "double_wooden_slab",
					(short)type, true, "Unknown Wood Double Slab (tech)",
					"Grid_Oak_Wood_Planks.png");
			}
		}
		public static InventoryItem BlockWoodSlab(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Oak Slab",
					ImageSourceType.Blocks,
					"Oak_Wood_Slab.png");
			case WoodType.Spruce:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Spruce Slab",
					ImageSourceType.Blocks,
					"Spruce_Wood_Slab.png");
			case WoodType.Birch:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Birch Slab",
					ImageSourceType.Blocks,
					"Birch_Wood_Slab.png");
			case WoodType.Jungle:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Jungle Slab",
					ImageSourceType.Blocks,
					"Jungle_Wood_Slab.png");
			case WoodType.Acacia:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Acacia Slab",
					ImageSourceType.Blocks,
					"Acacia_Wood_Slab.png");
			case WoodType.Darkwood:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Dark Oak Slab",
					ImageSourceType.Blocks,
					"Dark_Oak_Wood_Slab.png");
			default:
				return new InventoryItem(126, "wooden_slab",
					(short)type, "Unknown Wood Slab",
					ImageSourceType.Blocks,
					"Oak_Wood_Slab.png");
			}
		}
		// possibly unobtainable
		public static InventoryItem BlockCocoaPod_Tech(short growth)
		{
			return new InventoryItem(127, "cocoa", growth, true, "Cocoa Pod (tech)",
				"Cocoa_Plant.gif");
		}
		public static InventoryItem BlockSandstoneStairs()
		{
			return new InventoryItem(128, "sandstone_stairs", "Sandstone Stairs",
				ImageSourceType.Blocks, "Sandstone_Stairs.png");
		}
		public static InventoryItem BlockOreEmerald()
		{
			return new InventoryItem(129, "emerald_ore", "Emerald Ore",
				ImageSourceType.Blocks, "Emerald_Ore.png");
		}
		public static InventoryItem BlockEnderChest()
		{
			return new InventoryItem(130, "ender_chest", "Ender Chest",
				ImageSourceType.Blocks, "Ender_Chest.png");
		}
		public static InventoryItem BlockTripwireHook()
		{
			return new InventoryItem(131, "tripwire_hook", "Tripwire Hook",
				ImageSourceType.Blocks, "Tripwire_Hook.png");
		}
		// probably unobtainable
		public static InventoryItem BlockTripwire_Tech()
		{
			return new InventoryItem(132, "tripwire", 0, true, "Tripwire (tech)",
				"Tripwire.png");
		}
		public static InventoryItem BlockStorageEmerald()
		{
			return new InventoryItem(133, "emerald_block", "Emerald Block",
				ImageSourceType.Blocks, "Block_of_Emerald.png");
		}
		// oak stairs are above, these three were added later.
		public static InventoryItem BlockSpruceStairs()
		{
			return new InventoryItem(134, "spruce_stairs", "Spurce Stairs",
				ImageSourceType.Blocks, "Spruce_Wood_Stairs.png");
		}
		public static InventoryItem BlockBirchStairs()
		{
			return new InventoryItem(135, "birch_stairs", "Birch Stairs",
				ImageSourceType.Blocks, "Birch_Wood_Stairs.png");
		}
		public static InventoryItem BlockJungleStairs()
		{
			return new InventoryItem(136, "jungle_stairs", "Jungle Stairs",
				ImageSourceType.Blocks, "Jungle_Wood_Stairs.png");
		}
		// only from /give, but not a technical block
		public static InventoryItem BlockCommandBlock()
		{
			return new InventoryItem(137, "command_block", "Command Block",
				ImageSourceType.Blocks, "Command_Block.png");
		}
		public static InventoryItem BlockBeacon()
		{
			return new InventoryItem(138, "beacon", "Beacon",
				ImageSourceType.Blocks, "Beacon.png");
		}
		public static InventoryItem BlockCobbleWall()
		{
			return new InventoryItem(139, "cobblestone_wall", 0, "Cobblestone Wall",
				ImageSourceType.Blocks, "Cobblestone_Wall.png");
		}
		public static InventoryItem BlockCobbleWallMossy()
		{
			return new InventoryItem(139, "cobblestone_wall", 1, "Mossy Cobblestone Wall",
				ImageSourceType.Blocks, "Mossy_Cobblestone_Wall.png");
		}
		// probably unobtainable. Using strings gives flower pot item
		public static InventoryItem BlockFlowerPot_Tech()
		{
			return new InventoryItem(140, "flower_pot", 0, true, true, "Flower Pot (tech)",
				"Flower_Pot.png");
		}
		// technical, though not unobtainable
		public static InventoryItem BlockCarrotCrop_Tech(short growth)
		{
			return new InventoryItem(141, "carrots", growth, true, "Carrot Crops (tech)",
				"Carrot-animated.gif");
		}
		// technical, though not unobtainable
		public static InventoryItem BlockPotatoCrop_Tech(short growth)
		{
			return new InventoryItem(142, "potatoes", growth, true, "Potato Crops (tech)",
				"Potato-animated.gif");
		}
		public static InventoryItem BlockWoodButton()
		{
			return new InventoryItem(143, "wooden_button", "Wooden Button",
				ImageSourceType.Blocks, "Wooden_Button.png");
		}
		// probably unobtainable. Using strings gives skull item
		public static InventoryItem BlockSkull_Tech(short skullType)
		{
			string img = "Head.png";

			switch (skullType)
			{
			case 0:
				img = "Skeleton_Skull.png";
				break;
			case 1:
				img = "Wither_Skeleton_Skull.png";
				break;
			case 2:
				img = "Zombie_Head.png";
				break;
			case 3:
				img = "Head.png";
				break;
			case 4:
				img = "Creeper_Head.png";
				break;
			}

			return new InventoryItem(144, "skull", skullType, true, true, "Head", img);
		}
		public static InventoryItem BlockAnvil(short damage)
		{
			return new InventoryItem(145, "anvil", damage, "Anvil",
				ImageSourceType.Blocks, "Anvil.png");
		}
		public static InventoryItem BlockChestTrapped()
		{
			return new InventoryItem(146, "trapped_chest", "Trapped Chest",
				ImageSourceType.Blocks, "Trapped_Chest.png");
		}
		public static InventoryItem BlockLightPressurePlate()
		{
			return new InventoryItem(147, "light_weighted_pressure_plate",
				"Light Pressure Plate", ImageSourceType.Blocks,
				 "Weighted_Pressure_Plate_(Light).png");
		}
		public static InventoryItem BlockHeavyPressurePlate()
		{
			return new InventoryItem(148, "heavy_weighted_pressure_plate", 
				"Heavy Pressure Plate", ImageSourceType.Blocks,
				 "Weighted_Pressure_Plate_(Heavy).png");
		}
		// probably unobtainable
		public static InventoryItem BlockComparatorUnpowered_Tech()
		{
			return new InventoryItem(149, "unpowered_comparator", 0, true, 
				"Off Redstone Comparator Block (tech)",
				"Redstone_Comparator_(Inactive).gif");
		}
		public static InventoryItem BlockComparatorPowered_Tech()
		{
			return new InventoryItem(150, "powered_comparator", 0, true, 
				"On Redstone Comparator Block (tech)",
				"Redstone_Comparator_(Active).gif");
		}
		public static InventoryItem BlockSolarSensor()
		{
			return new InventoryItem(151, "daylight_detector", "Daylight Sensor",
				ImageSourceType.Blocks, "Daylight_Sensor.png");
		}
		public static InventoryItem BlockStorageRedstone()
		{
			return new InventoryItem(152, "redstone_block", "Redstone Block",
				ImageSourceType.Blocks, "Block_of_Redstone.png");
		}
		public static InventoryItem BlockOreQuartz()
		{
			return new InventoryItem(153, "quartz_ore", "Quartz Ore",
				ImageSourceType.Blocks, "Nether_Quartz_Ore.png");
		}
		// there is no item form. this is the actual hopper.
		public static InventoryItem BlockHopper()
		{
			return new InventoryItem(154, "hopper", "Hopper",
				ImageSourceType.Blocks, "Hopper.png");
		}
		public static InventoryItem BlockStorageQuartz()
		{
			return new InventoryItem(155, "quartz_block", 0, "Quartz Block",
				ImageSourceType.Blocks, "Block_of_Quartz.png");
		}
		public static InventoryItem BlockFancyQuartz()
		{
			return new InventoryItem(155, "quartz_block", 1,
				"Chiseled Quartz Block", ImageSourceType.Blocks, 
				"Chiseled_Quartz_Block.png");
		}
		public static InventoryItem BlockPillarQuartz()
		{
			return new InventoryItem(155, "quartz_block", 2, 
				"Pillar Quartz Block", ImageSourceType.Blocks,
				"Pillar_Quartz_Block.png");
		}
		public static InventoryItem BlockPillarQuartzNS_Tech()
		{
			return new InventoryItem(155, "quartz_block", 3, true,
				"North-South Pillar Quartz Block (tech)",
				"Pillar_Quartz_Block.png");
		}
		public static InventoryItem BlockPillarQuartzEW_Tech()
		{
			return new InventoryItem(155, "quartz_block", 4, true,
				"East-West Pillar Quartz Block (tech)",
				"Pillar_Quartz_Block.png");
		}
		public static InventoryItem BlockQuartzStairs()
		{
			return new InventoryItem(156, "quartz_stairs", "Quartz Stairs",
				ImageSourceType.Blocks, "Quartz_Stairs.png");
		}
		public static InventoryItem BlockActivatorRail()
		{
			return new InventoryItem(157, "activator_rail", "Activator Rail",
				ImageSourceType.Blocks, "Activator_Rail.png");
		}
		public static InventoryItem BlockDropper()
		{
			return new InventoryItem(158, "dropper", "Dropper",
				ImageSourceType.Blocks, "Dropper.png");
		}
		public static InventoryItem BlockStainedClay(DyeType color)
		{
			switch (color)
			{
			case DyeType.White:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"White Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Orange:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Orange Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Magenta:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Magenta Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.LightBlue:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Light Blue Stained Clay", ImageSourceType.Blocks,
					"Light_Blue_Stained_Clay.png");
			case DyeType.Yellow:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Yellow Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Lime:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Lime Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Pink:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Pink Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Gray:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Gray Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Silver:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Light Gray Stained Clay", ImageSourceType.Blocks,
					"Light_Gray_Stained_Clay.png");
			case DyeType.Cyan:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Cyan Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Purple:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Purple Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Blue:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Blue Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Brown:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Brown Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Green:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Green Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Red:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Red Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			case DyeType.Black:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Black Stained Clay", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Clay.png");
			default:
				return new InventoryItem(159, "stained_hardened_clay", (short)color,
					"Unknown Color Stained Clay", ImageSourceType.Blocks,
					"White_Stained_Clay.png");
			}
		}
		public static InventoryItem BlockGlassPaneStained(DyeType color)
		{
			switch (color)
			{
			case DyeType.White:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"White Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Orange:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Orange Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Magenta:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Magenta Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.LightBlue:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Light Blue Stained Glass Pane", ImageSourceType.Blocks,
					"Light_Blue_Stained_Glass_Pane.png");
			case DyeType.Yellow:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Yellow Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Lime:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Lime Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Pink:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Pink Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Gray:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Gray Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Silver:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Light Gray Stained Glass Pane", ImageSourceType.Blocks,
					"Light_Gray_Stained_Glass_Pane.png");
			case DyeType.Cyan:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Cyan Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Purple:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Purple Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Blue:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Blue Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Brown:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Brown Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Green:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Green Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Red:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Red Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			case DyeType.Black:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Black Stained Glass Pane", ImageSourceType.Blocks,
					color.ToString() + "_Stained_Glass_Pane.png");
			default:
				return new InventoryItem(160, "stained_glass_pane", (short)color,
					"Unknown Color Stained Glass Pane", ImageSourceType.Blocks,
					"White_Stained_Glass_Pane.png");
			}
		}
		public static InventoryItem BlockAcaciaStairs()
		{
			return new InventoryItem(163, "acacia_stairs", "Acacia Stairs",
				ImageSourceType.Blocks, "Acacia_Wood_Stairs.png");
		}
		public static InventoryItem BlockDarkOakStairs()
		{
			return new InventoryItem(164, "dark_oak_stairs", "Dark Oak Stairs",
				ImageSourceType.Blocks, "Dark_Oak_Wood_Stairs.png");
		}
		#region 1.8
		public static InventoryItem BlockSlime()
		{
			return new InventoryItem(165, "slime", "Slime Block (1.8)",
				ImageSourceType.Blocks, "Slime_Block.png");
		}
		public static InventoryItem BlockBarrier()
		{
			return new InventoryItem(166, "barrier", "Barrier (1.8)",
				ImageSourceType.Blocks, "Barrier.png");
		}
		public static InventoryItem BlockTrapdoorIron()
		{
			return new InventoryItem(167, "iron_trapdoor", "Iron Trapdoor (1.8)",
				ImageSourceType.Blocks, "Iron_Trapdoor.png");
		}
		#endregion
		public static InventoryItem BlockHayBale()
		{
			return new InventoryItem(170, "hay_block", "Hay Bale",
				ImageSourceType.Blocks, "Hay_Bale.png");
		}
		public static InventoryItem BlockCarpet(DyeType color)
		{
			switch (color)
			{
			case DyeType.White:
				return new InventoryItem(171, "carpet", (short)color,
					"White Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Orange:
				return new InventoryItem(171, "carpet", (short)color,
					"Orange Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Magenta:
				return new InventoryItem(171, "carpet", (short)color,
					"Magenta Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.LightBlue:
				return new InventoryItem(171, "carpet", (short)color,
					"Light Blue Carpet", ImageSourceType.Blocks,
					"Light_Blue_Carpet.png");
			case DyeType.Yellow:
				return new InventoryItem(171, "carpet", (short)color,
					"Yellow Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Lime:
				return new InventoryItem(171, "carpet", (short)color,
					"Lime Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Pink:
				return new InventoryItem(171, "carpet", (short)color,
					"Pink Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Gray:
				return new InventoryItem(171, "carpet", (short)color,
					"Gray Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Silver:
				return new InventoryItem(171, "carpet", (short)color,
					"Light Gray Carpet", ImageSourceType.Blocks,
					"Light_Gray_Carpet.png");
			case DyeType.Cyan:
				return new InventoryItem(171, "carpet", (short)color,
					"Cyan Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Purple:
				return new InventoryItem(171, "carpet", (short)color,
					"Purple Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Blue:
				return new InventoryItem(171, "carpet", (short)color,
					"Blue Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Brown:
				return new InventoryItem(171, "carpet", (short)color,
					"Brown Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Green:
				return new InventoryItem(171, "carpet", (short)color,
					"Green Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Red:
				return new InventoryItem(171, "carpet", (short)color,
					"Red Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			case DyeType.Black:
				return new InventoryItem(171, "carpet", (short)color,
					"Black Carpet", ImageSourceType.Blocks,
					color.ToString() + "_Carpet.png");
			default:
				return new InventoryItem(171, "carpet", (short)color,
					"Unknown Color Carpet", ImageSourceType.Blocks,
					"White_Carpet.png");
			}
		}
		// completely seperate ID from stained clay
		public static InventoryItem BlockHardenedClay()
		{
			return new InventoryItem(172, "hardened_clay", "Hardened Clay",
				ImageSourceType.Blocks, "Hardened_Clay.png");
		}
		public static InventoryItem BlockStorageCoal()
		{
			return new InventoryItem(173, "coal_block", "Coal Block",
				ImageSourceType.Blocks, "Block_of_Coal.png");
		}
		public static InventoryItem BlockIcePacked()
		{
			return new InventoryItem(174, "packed_ice", "Packed Ice",
				ImageSourceType.Blocks, "Packed_Ice.png");
		}
		public static InventoryItem BlockFlowerLargeSunflower()
		{
			return new InventoryItem(175, "double_plant", 0, "Sunflower",
				ImageSourceType.Blocks, "Sunflower.png");
		}
		public static InventoryItem BlockFlowerLargeLilac()
		{
			return new InventoryItem(175, "double_plant", 1, "Lilac",
				ImageSourceType.Blocks, "Lilac.png");
		}
		public static InventoryItem BlockFlowerLargeGrass()
		{
			return new InventoryItem(175, "double_plant", 2, "Really Tall Grass",
				ImageSourceType.Blocks, "Double_Tallgrass.png");
		}
		public static InventoryItem BlockFlowerLargeFern()
		{
			return new InventoryItem(175, "double_plant", 3, "Tall Fern",
				ImageSourceType.Blocks, "Large_Fern.png");
		}
		public static InventoryItem BlockFlowerLargeRose()
		{
			return new InventoryItem(175, "double_plant", 4, "Roses",
				ImageSourceType.Blocks, "Rose_Bush.png");
		}
		public static InventoryItem BlockFlowerLargePeony()
		{
			return new InventoryItem(175, "double_plant", 0, "Peony",
				ImageSourceType.Blocks, "Peony.png");
		}
	}
}
