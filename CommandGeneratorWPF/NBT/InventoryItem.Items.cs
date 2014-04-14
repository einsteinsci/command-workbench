using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public partial class InventoryItem
	{
		public static InventoryItem ItemIronShovel()
		{
			return new InventoryItem(256, "iron_shovel", "Iron Shovel",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "iron_shovel.png");
		}
		public static InventoryItem ItemIronPick()
		{
			return new InventoryItem(257, "iron_pickaxe", "Iron Pickaxe",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "iron_pickaxe.png");
		}
		public static InventoryItem ItemIronAxe()
		{
			return new InventoryItem(258, "iron_axe", "Iron Axe",
				EnchantmentType.Axe,
				ImageSourceType.Items, "iron_axe.png");
		}
		public static InventoryItem ItemFlintSteel()
		{
			return new InventoryItem(259, "flint_and_steel", "Flint and Steel",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "flint_and_steel.png");
		}
		public static InventoryItem ItemApple()
		{
			return new InventoryItem(260, "apple", "Apple",
				ImageSourceType.Items, "apple.png");
		}
		public static InventoryItem ItemBow()
		{
			return new InventoryItem(261, "bow", "Bow",
				EnchantmentType.Bow,
				ImageSourceType.Items, "bow_standby.png");
		}
		public static InventoryItem ItemArrow()
		{
			return new InventoryItem(262, "arrow", "Arrow",
				ImageSourceType.Items, "arrow.png");
		}
		public static InventoryItem ItemCoal()
		{
			return new InventoryItem(263, "coal", "Coal",
				ImageSourceType.Items, "coal.png");
		}
		public static InventoryItem ItemDiamond()
		{
			return new InventoryItem(264, "diamond", "Diamond",
				ImageSourceType.Items, "diamond.png");
		}
		public static InventoryItem ItemIngotIron()
		{
			return new InventoryItem(265, "iron_ingot", "Iron Ingot",
				ImageSourceType.Items, "iron_ingot.png");
		}
		public static InventoryItem ItemIngotGold()
		{
			return new InventoryItem(266, "gold_ingot", "Gold Ingot",
				ImageSourceType.Items, "gold_ingot.png");
		}
		public static InventoryItem ItemIronSword()
		{
			return new InventoryItem(267, "iron_sword", "Iron Sword",
				EnchantmentType.Sword,
				ImageSourceType.Items, "iron_sword.png");
		}
		public static InventoryItem ItemWoodSword()
		{
			return new InventoryItem(268, "wooden_sword", "Wooden Sword",
				EnchantmentType.Sword,
				ImageSourceType.Items, "wood_sword.png");
		}
		public static InventoryItem ItemWoodShovel()
		{
			return new InventoryItem(269, "wooden_shovel", "Wooden Shovel",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "wood_shovel.png");
		}
		public static InventoryItem ItemWoodPick()
		{
			return new InventoryItem(270, "wooden_pickaxe", "Wooden Pickaxe",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "wood_pickaxe.png");
		}
		public static InventoryItem ItemWoodAxe()
		{
			return new InventoryItem(271, "wooden_axe", "Wooden Axe",
				EnchantmentType.Axe,
				ImageSourceType.Items, "wood_axe.png");
		}
		public static InventoryItem ItemStoneSword()
		{
			return new InventoryItem(272, "stone_sword", "Stone Sword",
				EnchantmentType.Sword,
				ImageSourceType.Items, "stone_sword.png");
		}
		public static InventoryItem ItemStoneShovel()
		{
			return new InventoryItem(273, "stone_shovel", "Stone Shovel",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "stone_shovel.png");
		}
		public static InventoryItem ItemStonePick()
		{
			return new InventoryItem(274, "stone_pickaxe", "Stone Pickaxe",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "stone_pickaxe.png");
		}
		public static InventoryItem ItemStoneAxe()
		{
			return new InventoryItem(275, "stone_axe", "Stone Axe",
				EnchantmentType.Axe,
				ImageSourceType.Items, "stone_axe.png");
		}
		public static InventoryItem ItemDiamondSword()
		{
			return new InventoryItem(276, "diamond_sword", "Diamond Sword",
				EnchantmentType.Sword,
				ImageSourceType.Items, "diamond_sword.png");
		}
		public static InventoryItem ItemDiamondShovel()
		{
			return new InventoryItem(277, "diamond_shovel", "Diamond Shovel",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "diamond_shovel.png");
		}
		public static InventoryItem ItemDiamondPick()
		{
			return new InventoryItem(278, "diamond_pickaxe", "Diamond Pickaxe",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "diamond_pickaxe.png");
		}
		public static InventoryItem ItemDiamondAxe()
		{
			return new InventoryItem(279, "diamond_axe", "Diamond Axe",
				EnchantmentType.Axe,
				ImageSourceType.Items, "diamond_axe.png");
		}
		public static InventoryItem ItemStick()
		{
			return new InventoryItem(280, "stick", "Stick",
				ImageSourceType.Items, "stick.png");
		}
		public static InventoryItem ItemBowl()
		{
			return new InventoryItem(281, "bowl", "Bowl",
				ImageSourceType.Items, "bowl.png");
		}
		public static InventoryItem ItemSoup()
		{
			return new InventoryItem(282, "mushroom_stew", "Mushroom Stew",
				ImageSourceType.Items, "mushroom_stew.png");
		}
		public static InventoryItem ItemGoldSword()
		{
			return new InventoryItem(283, "golden_sword", "Golden Sword",
				EnchantmentType.Sword,
				ImageSourceType.Items, "gold_sword.png");
		}
		public static InventoryItem ItemGoldShovel()
		{
			return new InventoryItem(284, "golden_shovel", "Golden Shovel",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "gold_shovel.png");
		}
		public static InventoryItem ItemGoldPick()
		{
			return new InventoryItem(285, "golden_pickaxe", "Golden Pickaxe",
				EnchantmentType.PickShovel,
				ImageSourceType.Items, "gold_pickaxe.png");
		}
		public static InventoryItem ItemGoldAxe()
		{
			return new InventoryItem(286, "golden_axe", "Golden Axe",
				EnchantmentType.Axe,
				ImageSourceType.Items, "gold_axe.png");
		}
		public static InventoryItem ItemString()
		{
			return new InventoryItem(287, "string", "String",
				ImageSourceType.Items, "string.png");
		}
		public static InventoryItem ItemFeather()
		{
			return new InventoryItem(288, "feather", "Feather",
				ImageSourceType.Items, "feather.png");
		}
		public static InventoryItem ItemGunpowder()
		{
			return new InventoryItem(289, "gunpowder", "Gunpowder",
				ImageSourceType.Items, "gunpowder.png");
		}
		public static InventoryItem ItemWoodHoe()
		{
			return new InventoryItem(290, "wooden_hoe", "Wooden Hoe",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "wood_hoe.png");
		}
		public static InventoryItem ItemStoneHoe()
		{
			return new InventoryItem(291, "stone_hoe", "Stone Hoe",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "stone_hoe.png");
		}
		public static InventoryItem ItemIronHoe()
		{
			return new InventoryItem(292, "iron_hoe", "Iron Hoe",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "iron_hoe.png");
		}
		public static InventoryItem ItemDiamondHoe()
		{
			return new InventoryItem(293, "diamond_hoe", "Diamond Hoe",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "diamond_hoe.png");
		}
		public static InventoryItem ItemGoldHoe()
		{
			return new InventoryItem(294, "golden_hoe", "Golden Hoe",
				EnchantmentType.DurableOnly,
				ImageSourceType.Items, "gold_hoe.png");
		}
		public static InventoryItem ItemSeeds()
		{
			return new InventoryItem(295, "wheat_seeds", "Seeds",
				ImageSourceType.Items, "seeds_wheat.png");
		}
		public static InventoryItem ItemWheat()
		{
			return new InventoryItem(296, "wheat", "Wheat",
				ImageSourceType.Items, "wheat.png");
		}
		public static InventoryItem ItemBread()
		{
			return new InventoryItem(297, "bread", "Bread",
				ImageSourceType.Items, "bread.png");
		}
		public static InventoryItem ItemLeatherHelmet()
		{
			return new InventoryItem(298, "leather_helmet", "Leather Cap",
				EnchantmentType.Armor,
				ImageSourceType.Items, "leather_helmet.png");
		}
		public static InventoryItem ItemLeatherChestplate()
		{
			return new InventoryItem(299, "leather_chestplate", "Leather Tunic",
				EnchantmentType.Armor,
				ImageSourceType.Items, "leather_chestplate.png");
		}
		public static InventoryItem ItemLeatherLeggings()
		{
			return new InventoryItem(300, "leather_leggings", "Leather Pants",
				EnchantmentType.Armor,
				ImageSourceType.Items, "leather_leggings.png");
		}
		public static InventoryItem ItemLeatherBoots()
		{
			return new InventoryItem(301, "leather_boots", "Leather Boots",
				EnchantmentType.Armor,
				ImageSourceType.Items, "leather_boots.png");
		}
		public static InventoryItem ItemChainHelmet()
		{
			return new InventoryItem(302, "chainmail_helmet", "Chain Hat",
				EnchantmentType.Armor,
				ImageSourceType.Items, "chainmail_helmet.png");
		}
		public static InventoryItem ItemChainChestplate()
		{
			return new InventoryItem(303, "chainmail_chestplate", "Chain Tunic",
				EnchantmentType.Armor,
				ImageSourceType.Items, "chainmail_chestplate.png");
		}
		public static InventoryItem ItemChainLeggings()
		{
			return new InventoryItem(304, "chainmail_leggings", "Chain Leggings",
				EnchantmentType.Armor,
				ImageSourceType.Items, "chainmail_leggings.png");
		}
		public static InventoryItem ItemChainBoots()
		{
			return new InventoryItem(305, "chainmail_boots", "Chain Boots",
				EnchantmentType.Armor,
				ImageSourceType.Items, "chainmail_boots.png");
		}
		public static InventoryItem ItemIronHelmet()
		{
			return new InventoryItem(306, "iron_helmet", "Iron Helmet",
				EnchantmentType.Armor,
				ImageSourceType.Items, "iron_helmet.png");
		}
		public static InventoryItem ItemIronChestplate()
		{
			return new InventoryItem(307, "iron_chestplate", "Iron Chestplate",
				EnchantmentType.Armor,
				ImageSourceType.Items, "iron_chestplate.png");
		}
		public static InventoryItem ItemIronLeggings()
		{
			return new InventoryItem(308, "iron_leggings", "Iron Leggings",
				EnchantmentType.Armor,
				ImageSourceType.Items, "iron_leggings.png");
		}
		public static InventoryItem ItemIronBoots()
		{
			return new InventoryItem(309, "iron_boots", "Iron Boots",
				EnchantmentType.Armor,
				ImageSourceType.Items, "iron_boots.png");
		}
		public static InventoryItem ItemDiamondHelmet()
		{
			return new InventoryItem(310, "diamond_helmet", "Diamond Helmet",
				EnchantmentType.Armor,
				ImageSourceType.Items, "diamond_helmet.png");
		}
		public static InventoryItem ItemDiamondChestplate()
		{
			return new InventoryItem(311, "diamond_chestplate", "Diamond Chestplate",
				EnchantmentType.Armor,
				ImageSourceType.Items, "diamond_chestplate.png");
		}
		public static InventoryItem ItemDiamondLeggings()
		{
			return new InventoryItem(312, "diamond_leggings", "Diamond Leggings",
				EnchantmentType.Armor,
				ImageSourceType.Items, "diamond_leggings.png");
		}
		public static InventoryItem ItemDiamondBoots()
		{
			return new InventoryItem(313, "diamond_boots", "Diamond Boots",
				EnchantmentType.Armor,
				ImageSourceType.Items, "diamond_boots.png");
		}
		public static InventoryItem ItemGoldHelmet()
		{
			return new InventoryItem(314, "golden_helmet", "Golden Helmet",
				EnchantmentType.Armor,
				ImageSourceType.Items, "gold_helmet.png");
		}
		public static InventoryItem ItemGoldChestplate()
		{
			return new InventoryItem(315, "golden_chestplate", "Golden Chestplate",
				EnchantmentType.Armor,
				ImageSourceType.Items, "gold_chestplate.png");
		}
		public static InventoryItem ItemGoldLeggings()
		{
			return new InventoryItem(316, "golden_leggings", "Golden Leggings",
				EnchantmentType.Armor,
				ImageSourceType.Items, "gold_leggings.png");
		}
		public static InventoryItem ItemGoldBoots()
		{
			return new InventoryItem(317, "golden_boots", "Golden Boots",
				EnchantmentType.Armor,
				ImageSourceType.Items, "gold_boots.png");
		}
		public static InventoryItem ItemFlint()
		{
			return new InventoryItem(318, "flint", "Flint",
				ImageSourceType.Items, "flint.png");
		}
		public static InventoryItem ItemBaconRaw()
		{
			return new InventoryItem(319, "porkchop", "Porkchop",
				ImageSourceType.Items, "porkchop_raw.png");
		}
		public static InventoryItem ItemBaconCooked()
		{
			return new InventoryItem(320, "cooked_porkchop", "Cooked Porkchop",
				ImageSourceType.Items, "porkchop_cooked.png");
		}
		public static InventoryItem ItemPainting()
		{
			return new InventoryItem(321, "painting", "Painting",
				ImageSourceType.Items, "painting.png");
		}
		public static InventoryItem ItemGapple()
		{
			return new InventoryItem(322, "golden_apple", "Golden Apple",
				ImageSourceType.Items, "apple_golden.png");
		}
		public static InventoryItem ItemNotchApple()
		{
			return new InventoryItem(322, "golden_apple", 1, "Enchanted Golden Apple",
				ImageSourceType.Items, "apple_golden.png");
		}
		public static InventoryItem ItemSign()
		{
			return new InventoryItem(323, "sign", "Sign",
				ImageSourceType.Items, "sign.png");
		}
		public static InventoryItem ItemDoorWood()
		{
			return new InventoryItem(324, "wooden_door", "Wooden Door",
				ImageSourceType.Items, "door_wood.png");
		}
		public static InventoryItem ItemBucketEmpty()
		{
			return new InventoryItem(325, "bucket", "Bucket",
				ImageSourceType.Items, "bucket_empty.png");
		}
		public static InventoryItem ItemBucketWater()
		{
			return new InventoryItem(326, "water_bucket", "Bucket of Water",
				ImageSourceType.Items, "bucket_water.png");
		}
		public static InventoryItem ItemBucketLava()
		{
			return new InventoryItem(327, "lava_bucket", "Bucket of Lava",
				ImageSourceType.Items, "bucket_lava.png");
		}
		public static InventoryItem ItemMinecart()
		{
			return new InventoryItem(328, "minecart", "Minecart",
				ImageSourceType.Items, "minecart_normal.png");
		}
		public static InventoryItem ItemSaddle()
		{
			return new InventoryItem(329, "saddle", "Saddle",
				ImageSourceType.Items, "saddle.png");
		}
		public static InventoryItem ItemDoorIron()
		{
			return new InventoryItem(330, "iron_door", "Iron Door",
				ImageSourceType.Items, "door_iron.png");
		}
		public static InventoryItem ItemRedstone()
		{
			return new InventoryItem(331, "redstone", "Redstone",
				ImageSourceType.Items, "redstone_dust.png");
		}
		public static InventoryItem ItemSnowball()
		{
			return new InventoryItem(332, "snowball", "Snowball",
				ImageSourceType.Items, "snowball.png");
		}
		public static InventoryItem ItemBoat()
		{
			return new InventoryItem(333, "boat", "Boat",
				ImageSourceType.Items, "boat.png");
		}
		public static InventoryItem ItemLeather()
		{
			return new InventoryItem(334, "leather", "Leather",
				ImageSourceType.Items, "leather.png");
		}
		public static InventoryItem ItemBucketMilk()
		{
			return new InventoryItem(335, "milk_bucket", "Milk Bucket",
				ImageSourceType.Items, "bucket_milk.png");
		}
		public static InventoryItem ItemBrick()
		{
			return new InventoryItem(336, "brick", "Brick",
				ImageSourceType.Items, "brick.png");
		}
		public static InventoryItem ItemClay()
		{
			return new InventoryItem(337, "clay_ball", "Clay Ball",
				ImageSourceType.Items, "clay_ball.png");
		}
		public static InventoryItem ItemReeds()
		{
			return new InventoryItem(338, "reeds", "Sugarcane",
				ImageSourceType.Items, "reeds.png");
		}
		public static InventoryItem ItemPaper()
		{
			return new InventoryItem(339, "paper", "Paper",
				ImageSourceType.Items, "paper.png");
		}
		public static InventoryItem ItemBook()
		{
			return new InventoryItem(340, "book", "Book",
				ImageSourceType.Items, "book_normal.png");
		}
		public static InventoryItem ItemSlimeball()
		{
			return new InventoryItem(341, "slime_ball", "Slimeball",
				ImageSourceType.Items, "slimeball.png");
		}
		public static InventoryItem ItemMinecartChest()
		{
			return new InventoryItem(342, "chest_minecart", "Chest Minecart",
				ImageSourceType.Items, "minecart_chest.png");
		}
		public static InventoryItem ItemMinecartFurnace()
		{
			return new InventoryItem(343, "furnace_minecart", "Minecart with Furnace",
				ImageSourceType.Items, "minecart_furnace.png");
		}
		public static InventoryItem ItemEgg()
		{
			return new InventoryItem(344, "egg", "Egg",
				ImageSourceType.Items, "egg.png");
		}
		public static InventoryItem ItemCompass()
		{
			return new InventoryItem(345, "compass", "Compass",
				ImageSourceType.Items, "compass.png");
		}
		public static InventoryItem ItemFishingRod()
		{
			return new InventoryItem(346, "fishing_rod", "Fishing Rod",
				ImageSourceType.Items, "fishing_rod_uncast.png");
		}
		public static InventoryItem ItemClock()
		{
			return new InventoryItem(347, "clock", "Clock",
				ImageSourceType.Items, "clock.png");
		}
		public static InventoryItem ItemGlowstoneDust()
		{
			return new InventoryItem(348, "glowstone_dust", "Glowstone Dust",
				ImageSourceType.Items, "glowstone_dust.png");
		}
		public static InventoryItem ItemFishCod()
		{
			return new InventoryItem(349, "fish", 0, "Fish",
				ImageSourceType.Items, "fish_cod_raw.png");
		}
		public static InventoryItem ItemFishSalmon()
		{
			return new InventoryItem(349, "fish", 1, "Salmon",
				ImageSourceType.Items, "fish_salmon_raw.png");
		}
		public static InventoryItem ItemFishNemo()
		{
			return new InventoryItem(349, "fish", 2, "Clownfish",
				ImageSourceType.Items, "fish_clownfish_raw.png");
		}
		public static InventoryItem ItemFishPuffer()
		{
			return new InventoryItem(349, "fish", 3, "Blowfish",
				ImageSourceType.Items, "fish_pufferfish_raw.png");
		}
		public static InventoryItem ItemCookedFishCod()
		{
			return new InventoryItem(350, "cooked_fish", 0, "Cooked Fish",
				ImageSourceType.Items, "fish_cod_cooked.png");
		}
		public static InventoryItem ItemCookedFishSalmon()
		{
			return new InventoryItem(350, "cooked_fish", 1, "Cooked Salmon",
				ImageSourceType.Items, "fish_salmon_cooked.png");
		}
		public static InventoryItem ItemDye(DyeType color)
		{
			string friendly = color.ToString() + " Dye";
			string img = "dye_powder_" + color.ToString() + ".png";
			if (color == DyeType.Black)
			{
				friendly = "Ink Sac";
			}
			if (color == DyeType.Blue)
			{
				friendly = "Lapis Lazuli";
			}
			if (color == DyeType.White)
			{
				friendly = "Bonemeal";
			}
			if (color == DyeType.Green)
			{
				friendly = "Cactus Wad";
			}
			if (color == DyeType.LightBlue)
			{
				friendly = "Light Blue Dye";
				img = "dye_powder_light_blue.png";
			}
			if (color == DyeType.Silver)
			{
				friendly = "Light Gray Dye";
			}

			return new InventoryItem(351, "dye", (short)color, friendly,
				ImageSourceType.Items, img);
		}
		public static InventoryItem ItemBone()
		{
			return new InventoryItem(352, "bone", "Bone",
				ImageSourceType.Items, "bone.png");
		}
		public static InventoryItem ItemSugar()
		{
			return new InventoryItem(353, "sugar", "Sugar",
				ImageSourceType.Items, "sugar.png");
		}
		public static InventoryItem ItemCake()
		{
			return new InventoryItem(354, "cake", "Cake",
				ImageSourceType.Items, "cake.png");
		}
		public static InventoryItem ItemBed()
		{
			return new InventoryItem(355, "bed", "Bed",
				ImageSourceType.Items, "bed.png");
		}
		public static InventoryItem ItemRepeater()
		{
			return new InventoryItem(356, "repeater", "Redstone Repeater",
				ImageSourceType.Items, "repeater.png");
		}
		public static InventoryItem ItemCookie()
		{
			return new InventoryItem(357, "cookie", "Cookie",
				ImageSourceType.Items, "cookie.png");
		}
		public static InventoryItem ItemMapFilled()
		{
			return new InventoryItem(358, "filled_map", "Map",
				ImageSourceType.Items, "map_filled.png");
		}
		public static InventoryItem ItemShears()
		{
			return new InventoryItem(359, "shears", "Shears",
				EnchantmentType.Shears,
				ImageSourceType.Items, "shears.png");
		}
		public static InventoryItem ItemMelon()
		{
			return new InventoryItem(360, "melon", "Melon",
				ImageSourceType.Items, "melon.png");
		}
		public static InventoryItem ItemSeedsPumpkin()
		{
			return new InventoryItem(361, "pumpkin_seeds", "Pumpkin Seeds",
				ImageSourceType.Items, "seeds_pumpkin.png");
		}
		public static InventoryItem ItemSeedsMelon()
		{
			return new InventoryItem(362, "melon_seeds", "Melon Seeds",
				ImageSourceType.Items, "seeds_melon.png");
		}
		public static InventoryItem ItemBeefRaw()
		{
			return new InventoryItem(363, "beef", "Raw Beef",
				ImageSourceType.Items, "beef_raw.png");
		}
		public static InventoryItem ItemSteak()
		{
			return new InventoryItem(364, "cooked_beef", "Steak",
				ImageSourceType.Items, "beef_cooked.png");
		}
		public static InventoryItem ItemChickenRaw()
		{
			return new InventoryItem(365, "chicken", "Raw Chicken",
				ImageSourceType.Items, "chicken_raw.png");
		}
		public static InventoryItem ItemChickenCooked()
		{
			return new InventoryItem(366, "cooked_chicken", "Cooked Chicken",
				ImageSourceType.Items, "chicken_cooked.png");
		}
		public static InventoryItem ItemRottenFlesh()
		{
			return new InventoryItem(367, "rotten_flesh", "Rotten Flesh",
				ImageSourceType.Items, "rotten_flesh.png");
		}
		public static InventoryItem ItemEnderPearl()
		{
			return new InventoryItem(368, "ender_pearl", "Ender Pearl",
				ImageSourceType.Items, "ender_pearl.png");
		}
		public static InventoryItem ItemBlazeRod()
		{
			return new InventoryItem(369, "blaze_rod", "Blaze Rod",
				ImageSourceType.Items, "blaze_rod.png");
		}
		public static InventoryItem ItemGhastTear()
		{
			return new InventoryItem(370, "ghast_tear", "Ghast Tear",
				ImageSourceType.Items, "ghast_tear.png");
		}
		public static InventoryItem ItemGoldNugget()
		{
			return new InventoryItem(371, "gold_nugget", "Gold Nugget",
				ImageSourceType.Items, "gold_nugget.png");
		}
		public static InventoryItem ItemNetherWart()
		{
			return new InventoryItem(372, "nether_wart", "Nether Wart",
				ImageSourceType.Items, "nether_wart.png");
		}
		public static InventoryItem ItemWaterBottle()
		{
			return new InventoryItem(373, "potion", 0, "Water Bottle",
				ImageSourceType.Items, "potion_bottle_water.png");
		}
		public static InventoryItem ItemPotion(short potionData)
		{
			return new InventoryItem(373, "potion", potionData, "Potion",
				ImageSourceType.Items, "potion_bottle_water.png");
		}
		public static InventoryItem ItemBottle()
		{
			return new InventoryItem(374, "glass_bottle", "Empty Bottle",
				ImageSourceType.Items, "potion_bottle_empty.png");
		}
		public static InventoryItem ItemSpiderEye()
		{
			return new InventoryItem(375, "spider_eye", "Spider Eye",
				ImageSourceType.Items, "spider_eye.png");
		}
		/// <summary>
		/// The wierdest item in the game: Fermented Spider Eye
		/// </summary>
		/// <returns>Fermented spider eye as InventoryItem</returns>
		public static InventoryItem ItemWeirdFunk()
		{
			return new InventoryItem(376, "fermented_spider_eye", "Fermented Spider Eye",
				ImageSourceType.Items, "spider_eye_fermented.png");
		}
		public static InventoryItem ItemBlazePowder()
		{
			return new InventoryItem(377, "blaze_powder", "Blaze powder",
				ImageSourceType.Items, "blaze_powder.png");
		}
		public static InventoryItem ItemMagmaCream()
		{
			return new InventoryItem(378, "magma_cream", "Magma Cream",
				ImageSourceType.Items, "magma_cream.png");
		}
		public static InventoryItem ItemBrewingStand()
		{
			return new InventoryItem(379, "brewing_stand", "Brewing Stand",
				ImageSourceType.Items, "brewing_stand.png");
		}
		public static InventoryItem ItemCauldron()
		{
			return new InventoryItem(380, "cauldron", "Cauldron",
				ImageSourceType.Items, "cauldron.png");
		}
		public static InventoryItem ItemEnderEye()
		{
			return new InventoryItem(381, "ender_eye", "Eye of Ender",
				ImageSourceType.Items, "ender_eye.png");
		}
		public static InventoryItem ItemSwagMelon()
		{
			return new InventoryItem(382, "speckled_melon", "Glistering Melon",
				ImageSourceType.Items, "melon_speckled.png");
		}
		public static InventoryItem ItemSpawnEgg(short entityID)
		{
			return new InventoryItem(383, "spawn_egg", entityID, "Spawn Egg",
				ImageSourceType.Items, "spawn_egg_combined.png");
		}
		public static InventoryItem ItemXPBottle()
		{
			return new InventoryItem(384, "experience_bottle", "Bottle O' Enchanting",
				ImageSourceType.Items, "experience_bottle.png");
		}
		public static InventoryItem ItemFireCharge()
		{
			return new InventoryItem(385, "fire_charge", "Fire Charge",
				ImageSourceType.Items, "fireball.png");
		}
		public static InventoryItem ItemBookOpen()
		{
			return new InventoryItem(386, "writable_book", "Book and Quill",
				ImageSourceType.Items, "book_writable.png");
		}
		public static InventoryItem ItemBookLiterature()
		{
			return new InventoryItem(387, "written_book", "Written Book",
				ImageSourceType.Items, "book_written.png");
		}
		public static InventoryItem ItemEmerald()
		{
			return new InventoryItem(388, "emerald", "Emerald",
				ImageSourceType.Items, "emerald.png");
		}
		public static InventoryItem ItemFrame()
		{
			return new InventoryItem(389, "item_frame", "Item Frame",
				ImageSourceType.Items, "item_frame.png");
		}
		public static InventoryItem ItemPot()
		{
			return new InventoryItem(390, "flower_pot", "Flower Pot",
				ImageSourceType.Items, "flower_pot.png");
		}
		public static InventoryItem ItemCarrot()
		{
			return new InventoryItem(391, "carrot", "Carrot",
				ImageSourceType.Items, "carrot.png");
		}
		public static InventoryItem ItemPotato()
		{
			return new InventoryItem(392, "potato", "Potato",
				ImageSourceType.Items, "potato.png");
		}
		public static InventoryItem ItemPotatoCooked()
		{
			return new InventoryItem(393, "baked_potato", "Baked Potato",
				ImageSourceType.Items, "potato_baked.png");
		}
		public static InventoryItem ItemPotatoRotten()
		{
			return new InventoryItem(394, "poisonous_potato", "Rotten Potato",
				ImageSourceType.Items, "potato_poisonous.png");
		}
		public static InventoryItem ItemMapEmpty()
		{
			return new InventoryItem(395, "map", "Empty Map",
				ImageSourceType.Items, "map_empty.png");
		}
		public static InventoryItem ItemSwagCarrot()
		{
			return new InventoryItem(396, "golden_carrot", "Golden Carrot",
				ImageSourceType.Items, "carrot_golden.png");
		}
		public static InventoryItem ItemSkullSkeleton()
		{
			return new InventoryItem(397, "skull", 0, "Skeleton Skull",
				ImageSourceType.Items, "skull_skeleton.png");
		}
		public static InventoryItem ItemSkullWither()
		{
			return new InventoryItem(397, "skull", 1, "Wither Skeleton Skull",
				ImageSourceType.Items, "skull_wither.png");
		}
		public static InventoryItem ItemSkullZombie()
		{
			return new InventoryItem(397, "skull", 2, "Zombie Head",
				ImageSourceType.Items, "skull_zombie.png");
		}
		public static InventoryItem ItemSkullPlayer()
		{
			return new InventoryItem(397, "skull", 3, "Player Head",
				ImageSourceType.Items, "skull_steve.png");
		}
		public static InventoryItem ItemSkullCreeper()
		{
			return new InventoryItem(397, "skull", 4, "Creeper Head",
				ImageSourceType.Items, "skull_creeper.png");
		}
		public static InventoryItem ItemCarrotOnAStick()
		{
			return new InventoryItem(398, "carrot_on_a_stick", "Carrot on a Stick",
				ImageSourceType.Items, "carrot_on_a_stick.png");
		}
		public static InventoryItem ItemNetherStar()
		{
			return new InventoryItem(399, "nether_star", "Nether Star",
				ImageSourceType.Items, "nether_star.png");
		}
		public static InventoryItem ItemPumpkinPie()
		{
			return new InventoryItem(400, "pumpkin_pie", "Pumpkin Pie",
				ImageSourceType.Items, "pumpkin_pie.png");
		}
		public static InventoryItem ItemFireworkRocket()
		{
			return new InventoryItem(401, "fireworks", "Fireworks Rocket",
				ImageSourceType.Items, "fireworks.png");
		}
		public static InventoryItem ItemFireworkStar()
		{
			return new InventoryItem(402, "fireworks_charge", "Fireworks Star",
				ImageSourceType.Items, "fireworks_charge.png");
		}
		public static InventoryItem ItemBookEnchanted()
		{
			return new InventoryItem(403, "enchanted_book", "Enchanted Book",
				EnchantmentType.None, //No normal enchantments can go in the book, only book enchantments
				ImageSourceType.Items, "book_enchanted.png");
		}
		public static InventoryItem ItemComparator()
		{
			return new InventoryItem(404, "comparator", "Redstone Comparator",
				ImageSourceType.Items, "comparator.png");
		}
		public static InventoryItem ItemBrickNether()
		{
			return new InventoryItem(405, "netherbrick", "Nether Brick",
				ImageSourceType.Items, "netherbrick.png");
		}
		public static InventoryItem ItemQuartz()
		{
			return new InventoryItem(406, "quartz", "Quartz",
				ImageSourceType.Items, "quartz.png");
		}
		public static InventoryItem ItemMinecartTNT()
		{
			return new InventoryItem(407, "tnt_minecart", "Minecart with TNT",
				ImageSourceType.Items, "minecart_tnt.png");
		}
		public static InventoryItem ItemMinecartHopper()
		{
			return new InventoryItem(408, "hopper_minecart", "Minecart with Hopper",
				ImageSourceType.Items, "minecart_hopper.png");
		}
		public static InventoryItem ItemIronBarding()
		{
			return new InventoryItem(417, "iron_horse_armor", "Iron Horse Armor",
				ImageSourceType.Items, "iron_horse_armor.png");
		}
		public static InventoryItem ItemGoldBarding()
		{
			return new InventoryItem(418, "golden_horse_armor", "Golden Horse Armor",
				ImageSourceType.Items, "gold_horse_armor.png");
		}
		public static InventoryItem ItemDiamondBarding()
		{
			return new InventoryItem(419, "diamond_horse_armor", "Diamond Horse Armor",
				ImageSourceType.Items, "diamond_horse_armor.png");
		}
		public static InventoryItem ItemLead()
		{
			return new InventoryItem(420, "lead", "Lead",
				ImageSourceType.Items, "lead.png");
		}
		public static InventoryItem ItemNameTag()
		{
			return new InventoryItem(421, "name_tag", "Nametag",
				ImageSourceType.Items, "name_tag.png");
		}
		public static InventoryItem ItemMinecartCommandBlock()
		{
			return new InventoryItem(422, "command_block_minecart",
				"Minecart with Command Block",
				ImageSourceType.Items, "minecart_command_block.png");
		}
		public static InventoryItem ItemRecord13()
		{
			return new InventoryItem(2256, "record_13", "Music Disc: 13",
				ImageSourceType.Items, "record_13.png");
		}
		public static InventoryItem ItemRecordCat()
		{
			return new InventoryItem(2257, "record_cat", "Music Disc: Cat",
				ImageSourceType.Items, "record_cat.png");
		}
		public static InventoryItem ItemRecordBlocks()
		{
			return new InventoryItem(2258, "record_blocks", "Music Disc: Blocks",
				ImageSourceType.Items, "record_blocks.png");
		}
		public static InventoryItem ItemRecordChirp()
		{
			return new InventoryItem(2259, "record_chirp", "Music Disc: Chirp",
				ImageSourceType.Items, "record_chirp.png");
		}
		public static InventoryItem ItemRecordFar()
		{
			return new InventoryItem(2260, "record_far", "Music Disc: Far",
				ImageSourceType.Items, "record_far.png");
		}
		public static InventoryItem ItemRecordMall()
		{
			return new InventoryItem(2261, "record_mall", "Music Disc: Mall",
				ImageSourceType.Items, "record_mall.png");
		}
		public static InventoryItem ItemRecordMellohi()
		{
			return new InventoryItem(2262, "record_mellohi", "Music Disc: Mellohi",
				ImageSourceType.Items, "record_mellohi.png");
		}
		public static InventoryItem ItemRecordStal()
		{
			return new InventoryItem(2263, "record_stal", "Music Disc: Stal",
				ImageSourceType.Items, "record_stal.png");
		}
		public static InventoryItem ItemRecordStrad()
		{
			return new InventoryItem(2264, "record_strad", "Music Disc: Strad",
				ImageSourceType.Items, "record_strad.png");
		}
		public static InventoryItem ItemRecordWard()
		{
			return new InventoryItem(2265, "record_ward", "Music Disc: Ward",
				ImageSourceType.Items, "record_ward.png");
		}
		public static InventoryItem ItemRecord11()
		{
			return new InventoryItem(2266, "record_11", "Music Disc: 11",
				ImageSourceType.Items, "record_11.png");
		}
		public static InventoryItem ItemRecordWait()
		{
			return new InventoryItem(2267, "record_wait", "Music Disc: Wait",
				ImageSourceType.Items, "record_wait.png");
		}
	}
}
