using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public static class Blocks
	{
		public static Block Stone()
		{
			return new Block(1, "stone", "Stone", "Stone");
		}
		public static Block Stone(RockType type)
		{
			string img = "Stone";
			string friendly = "Stone";

			switch (type)
			{
			case RockType.Stone:
				return Blocks.Stone();
			case RockType.Granite:
				friendly = "Granite";
				break;
			case RockType.GranitePolished:
				friendly = "Polished Granite";
				break;
			case RockType.Diorite:
				friendly = "Diorite";
				break;
			case RockType.DioritePolished:
				friendly = "Polished Diorite";
				break;
			case RockType.Andesite:
				friendly = "Andesite";
				break;
			case RockType.AndesitePolished:
				friendly = "Polished Andesite";
				break;
			default:
				friendly = "Unknown Stone Type";
				break;
			}

			return new Block(1, "stone", friendly, (byte)type, img);
		}

		public static Block Grass()
		{
			return new Block(2, "grass", "Grass", "Grass");
		}

		public static Block Dirt()
		{
			return new Block(3, "dirt", "Dirt", "Dirt");
		}
		public static Block DirtGrassless()
		{
			return new Block(3, "dirt", "Grassless Dirt", 1, "Dirt");
		}
		public static Block DirtPodzol()
		{
			return new Block(3, "dirt", "Podzol", 2, "Podzol");
		}

		public static Block Cobblestone()
		{
			return new Block(4, "cobblestone", "Cobblestone", "Cobblestone");
		}

		public static Block Planks()
		{
			return new Block(5, "planks", "(Oak) Wood Planks", "Oak_Wood_Planks");
		}
		public static Block Planks(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new Block(5, "planks", "Oak Wood Planks", 0, "Oak_Wood_Planks");
			case WoodType.Spruce:
				return new Block(5, "planks", "Spruce Wood Planks", 1, "Spruce_Wood_Planks");
			case WoodType.Birch:
				return new Block(5, "planks", "Birch Wood Planks", 2, "Birch_Wood_Planks");
			case WoodType.Jungle:
				return new Block(5, "planks", "Jungle Wood Planks", 3, "Jungle_Wood_Planks");
			case WoodType.Acacia:
				return new Block(5, "planks", "Acacia Wood Planks", 4, "Acacia_Wood_Planks");
			case WoodType.Darkwood:
				return new Block(5, "planks", "Dark Oak Wood Planks", 5, "Dark_Oak_Wood_Planks");
			default:
				return new Block(5, "planks", "Unknown Wood Planks", (byte)type, "Oak_Wood_Planks");
			}
		}

		public static Block Sapling()
		{
			return new Block(6, "sapling", "(Oak) Sapling", "Oak_Sapling");
		}
		public static Block Sapling(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new Block(6, "sapling", "Oak Sapling", 0, "Oak_Sapling");
			case WoodType.Spruce:
				return new Block(6, "sapling", "Spruce Sapling", 1, "Spruce_Sapling");
			case WoodType.Birch:
				return new Block(6, "sapling", "Birch Sapling", 2, "Birch_Sapling");
			case WoodType.Jungle:
				return new Block(6, "sapling", "Jungle Sapling", 3, "Jungle_Sapling");
			case WoodType.Acacia:
				return new Block(6, "sapling", "Acacia Sapling", 4, "Acacia_Sapling");
			case WoodType.Darkwood:
				return new Block(6, "sapling", "Dark Oak Sapling", 5, "Dark_Oak_Sapling");
			default:
				return new Block(6, "sapling", "Unknown Sapling", (byte)type, "Oak_Sapling");
			}
		}

		public static Block Bedrock()
		{
			return new Block(7, "bedrock", "Bedrock", "Bedrock");
		}

		public static Block WaterFlowing_Tech()
		{
			return new Block(8, "flowing_water", "Flowing Water", 0, true, "Grid_Water");
		}
		public static Block WaterStatic_Tech()
		{
			return new Block(9, "water", "Stationary Water", 0, true, "Grid_Water");
		}

		public static Block LavaFlowing_Tech()
		{
			return new Block(10, "flowing_lava", "Flowing Lava", 0, true, "Grid_Lava");
		}
		public static Block LavaStatic_Tech()
		{
			return new Block(10, "lava", "Stationary Lava", 0, true, "Grid_Lava");
		}

		public static Block Sand()
		{
			return new Block(12, "sand", "Sand", "Sand");
		}
		public static Block SandRed()
		{
			return new Block(12, "sand", "Red Sand", 1, "Red_Sand");
		}

		public static Block Gravel()
		{
			return new Block(13, "gravel", "Gravel", "Gravel");
		}

		public static Block OreGold()
		{
			return new Block(14, "gold_ore", "Gold Ore", "Gold_Ore");
		}
		public static Block OreIron()
		{
			return new Block(15, "iron_ore", "Iron Ore", "Iron_Ore");
		}
		public static Block OreCoal()
		{
			return new Block(16, "coal_ore", "Coal Ore", "Coal_Ore");
		}

		public static Block Log()
		{
			return new Block(17, "log", "Wood Log", "Oak_Wood");
		}
		public static Block Log(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new Block(17, "log", "Oak Log", 0, "Oak_Wood");
			case WoodType.Spruce:
				return new Block(17, "log", "Spruce Log", 1, "Spruce_Wood");
			case WoodType.Birch:
				return new Block(17, "log", "Birch Log", 2, "Birch_Wood");
			case WoodType.Jungle:
				return new Block(17, "log", "Jungle Log", 3, "Jungle_Wood");
			case WoodType.Acacia:
				return Log2(type);
			case WoodType.Darkwood:
				return Log2(type);
			default:
				return new Block(17, "log", "Unknown Log", (byte)type, "Oak_Wood");
			}
		}

		public static Block Leaves()
		{
			return new Block(18, "leaves", "(Oak) Leaves", "Oak_Leaves");
		}
		public static Block Leaves(WoodType type)
		{
			switch (type)
			{
			case WoodType.Oak:
				return new Block(18, "leaves", "Oak Leaves", 0, "Oak_Leaves");
			case WoodType.Spruce:
				return new Block(18, "leaves", "Spruce Leaves", 1, "Spruce_Leaves");
			case WoodType.Birch:
				return new Block(18, "leaves", "Birch Leaves", 2, "Birch_Leaves");
			case WoodType.Jungle:
				return new Block(18, "leaves", "Jungle Leaves", 3, "Jungle_Leaves");
			case WoodType.Acacia:
				return Leaves2(type);
			case WoodType.Darkwood:
				return Leaves2(type);
			default:
				return new Block(18, "leaves", "Unknown Leaves", (byte)type, "Oak_Leaves");
			}
		}

		public static Block Sponge()
		{
			return new Block(19, "sponge", "Sponge", "Sponge");
		}

		public static Block Glass()
		{
			return new Block(20, "glass", "Glass", "Glass");
		}

		public static Block OreLapis()
		{
			return new Block(21, "lapis_ore", "Lapis Lazuli Ore", "Lapis_Lazuli_Ore");
		}
		public static Block StorageLapis()
		{
			return new Block(22, "lapis_block", "Lapis Lazuli Block", "Lapis_Lazuli_Block");
		}
		public static Block Dispenser()
		{
			return new Block(23, "dispenser", "Dispenser", "Dispenser");
		}

		public static Block Log2(WoodType type);
		public static Block Leaves2(WoodType type);
	}
}
