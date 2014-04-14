using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public partial class InventoryItem
	{
		public static Dictionary<string, int> DurabilityByName()
		{
			Dictionary<string, int> d = new Dictionary<string, int>();

			#region sword & tools
			d.Add("wooden_sword", 60);
			d.Add("golden_sword", 33);
			d.Add("stone_sword", 132);
			d.Add("iron_sword", 251);
			d.Add("diamond_sword", 1562);

			d.Add("wooden_shovel", 60);
			d.Add("golden_shovel", 33);
			d.Add("stone_shovel", 132);
			d.Add("iron_shovel", 251);
			d.Add("diamond_shovel", 1562);

			d.Add("wooden_pickaxe", 60);
			d.Add("golden_pickaxe", 33);
			d.Add("stone_pickaxe", 132);
			d.Add("iron_pickaxe", 251);
			d.Add("diamond_pickaxe", 1562);

			d.Add("wooden_axe", 60);
			d.Add("golden_axe", 33);
			d.Add("stone_axe", 132);
			d.Add("iron_axe", 251);
			d.Add("diamond_axe", 1562);

			d.Add("wooden_hoe", 60);
			d.Add("golden_hoe", 33);
			d.Add("stone_hoe", 132);
			d.Add("iron_hoe", 251);
			d.Add("diamond_hoe", 1562);
			#endregion

			d.Add("bow", 385);
			d.Add("flint_and_steel", 65);

			#region armor

			d.Add("leather_helmet", 56);
			d.Add("leather_chestplate", 81);
			d.Add("leather_leggings", 76);
			d.Add("leather_boots", 66);

			d.Add("golden_helmet", 78);
			d.Add("golden_chestplate", 113);
			d.Add("golden_leggings", 106);
			d.Add("golden_boots", 92);

			d.Add("chainmail_helmet", 166);
			d.Add("chainmail_chestplate", 241);
			d.Add("chainmail_leggings", 226);
			d.Add("chainmail_boots", 196);

			d.Add("iron_helmet", 166);
			d.Add("iron_chestplate", 241);
			d.Add("iron_leggings", 226);
			d.Add("iron_boots", 196);

			d.Add("diamond_helmet", 56);
			d.Add("diamond_chestplate", 81);
			d.Add("diamond_leggings", 76);
			d.Add("diamond_boots", 66);

			#endregion

			d.Add("fishing_rod", 65);
			d.Add("carrot_on_a_stick", 26);

			return d;
		}

		public static Dictionary<string, int> MaxStackSizeByName()
		{
			Dictionary<string, int> d = new Dictionary<string, int>();

			ObservableCollection<InventoryItem> all = ListBlocksAllID();
			all.AddRange(ListItemsAllIdOnly());

			foreach (InventoryItem i in all)
			{
				if (i.IsDurableAble ||
					i.IsPotion ||
					i.IsMap ||
					i.IsEnchantable ||
					i.IsPotion ||
					i.IsBook ||
					i == ItemDoorWood() ||
					i == ItemDoorIron() ||
					i == ItemBoat() ||
					i == ItemIronBarding() ||
					i == ItemGoldBarding() ||
					i == ItemDiamondBarding() ||
					i == ItemSoup() ||
					i == ItemSaddle() ||
					i == ItemBucketLava() ||
					i == ItemBucketWater() || 
					i == ItemBucketMilk() ||
					i == ItemBed() ||
					i == ItemBookEnchanted() ||
					i == ItemBookLiterature() ||
					i == ItemBookOpen() ||
					i.ShortName.Contains("record") ||
					i.ShortName.Contains("minecart"))
				{
					d.Add(i.ShortName, 1);
				}
				else if (i == ItemEnderPearl() ||
					i == ItemEgg() ||
					i == ItemBowl() ||
					i == ItemSign() ||
					i == ItemBucketEmpty() ||
					i == ItemSnowball())
				{
					d.Add(i.ShortName, 16);
				}
				else if (!i.WrongName)
				{
					d.Add(i.ShortName, 64);
				}
			}

			return d;
		}

		public static ObservableCollection<InventoryItem> ListTileEntities()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(BlockDispenser());
			e.Add(BlockSpawner());
			e.Add(BlockChest());
			e.Add(BlockFurnace());
			e.Add(BlockFurnaceLit_Tech());
			e.Add(BlockJukebox());
			e.Add(BlockEnchantingTable());
			e.Add(BlockEnderChest());
			e.Add(BlockCommandBlock());
			e.Add(BlockBeacon());
			e.Add(BlockChestTrapped());
			e.Add(BlockSolarSensor());
			e.Add(BlockHopper());
			e.Add(BlockDropper());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListEnchantables()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemBow());

			#region normalTools
			e.Add(ItemIronSword());
			e.Add(ItemIronShovel());
			e.Add(ItemIronPick());
			e.Add(ItemIronAxe());

			e.Add(ItemWoodSword());
			e.Add(ItemWoodShovel());
			e.Add(ItemWoodPick());
			e.Add(ItemWoodAxe());

			e.Add(ItemStoneSword());
			e.Add(ItemStoneShovel());
			e.Add(ItemStonePick());
			e.Add(ItemStoneAxe());
			
			e.Add(ItemDiamondSword());
			e.Add(ItemDiamondShovel());
			e.Add(ItemDiamondPick());
			e.Add(ItemDiamondAxe());
			
			e.Add(ItemGoldSword());
			e.Add(ItemGoldShovel());
			e.Add(ItemGoldPick());
			e.Add(ItemGoldAxe());
			#endregion

			e.Add(ItemWoodHoe());
			e.Add(ItemStoneHoe());
			e.Add(ItemIronHoe());
			e.Add(ItemDiamondHoe());
			e.Add(ItemGoldHoe());

			#region armor
			e.Add(ItemLeatherHelmet());
			e.Add(ItemLeatherChestplate());
			e.Add(ItemLeatherLeggings());
			e.Add(ItemLeatherBoots());
			
			e.Add(ItemChainHelmet());
			e.Add(ItemChainChestplate());
			e.Add(ItemChainLeggings());
			e.Add(ItemChainBoots());
			
			e.Add(ItemIronHelmet());
			e.Add(ItemIronChestplate());
			e.Add(ItemIronLeggings());
			e.Add(ItemIronBoots());
			
			e.Add(ItemDiamondHelmet());
			e.Add(ItemDiamondChestplate());
			e.Add(ItemDiamondLeggings());
			e.Add(ItemDiamondBoots());
			
			e.Add(ItemGoldHelmet());
			e.Add(ItemGoldChestplate());
			e.Add(ItemGoldLeggings());
			e.Add(ItemGoldBoots());
			#endregion

			e.Add(ItemShears());
			e.Add(ItemFlintSteel());
			e.Add(ItemFishingRod());
			e.Add(ItemCarrotOnAStick());
			e.Add(ItemBookEnchanted());

			return e;
		}

		public static ObservableCollection<InventoryItem> ListDye()
		{
			ObservableCollection<InventoryItem> d = new ObservableCollection<InventoryItem>();
			
			d.Add(ItemDye(DyeType.White));
			d.Add(ItemDye(DyeType.Orange));
			d.Add(ItemDye(DyeType.Magenta));
			d.Add(ItemDye(DyeType.LightBlue));
			d.Add(ItemDye(DyeType.Yellow));
			d.Add(ItemDye(DyeType.Lime));
			d.Add(ItemDye(DyeType.Pink));
			d.Add(ItemDye(DyeType.Gray));
			d.Add(ItemDye(DyeType.Silver));
			d.Add(ItemDye(DyeType.Cyan));
			d.Add(ItemDye(DyeType.Purple));
			d.Add(ItemDye(DyeType.Blue));
			d.Add(ItemDye(DyeType.Brown));
			d.Add(ItemDye(DyeType.Green));
			d.Add(ItemDye(DyeType.Red));
			d.Add(ItemDye(DyeType.Black));

			return d;
		}

		// Components

		public static ObservableCollection<InventoryItem> ListTools()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemIronShovel());
			e.Add(ItemIronPick());
			e.Add(ItemIronAxe());

			e.Add(ItemWoodShovel());
			e.Add(ItemWoodPick());
			e.Add(ItemWoodAxe());

			e.Add(ItemStoneShovel());
			e.Add(ItemStonePick());
			e.Add(ItemStoneAxe());

			e.Add(ItemDiamondShovel());
			e.Add(ItemDiamondPick());
			e.Add(ItemDiamondAxe());

			e.Add(ItemGoldShovel());
			e.Add(ItemGoldPick());
			e.Add(ItemGoldAxe());

			e.Add(ItemWoodHoe());
			e.Add(ItemStoneHoe());
			e.Add(ItemIronHoe());
			e.Add(ItemDiamondHoe());
			e.Add(ItemGoldHoe());

			e.Add(ItemShears());
			e.Add(ItemFlintSteel());
			e.Add(ItemFishingRod());
			e.Add(ItemCarrotOnAStick());
			e.Add(ItemClock());
			e.Add(ItemCompass());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListWeapons()
		{
			ObservableCollection<InventoryItem> w = new ObservableCollection<InventoryItem>();

			w.Add(ItemBow());
			w.Add(ItemArrow());

			w.Add(ItemIronSword());
			w.Add(ItemWoodSword());
			w.Add(ItemStoneSword());
			w.Add(ItemDiamondSword());
			w.Add(ItemGoldSword());

			return w;
		}
		public static ObservableCollection<InventoryItem> ListToolsWeapons()
		{
			ObservableCollection<InventoryItem> t = ListTools();
			
			foreach (InventoryItem i in ListWeapons())
			{
				t.Add(i);
			}

			return t;
		}
		public static ObservableCollection<InventoryItem> ListArmor()
		{
			ObservableCollection<InventoryItem> a = new ObservableCollection<InventoryItem>();

			a.Add(ItemLeatherHelmet());
			a.Add(ItemLeatherChestplate());
			a.Add(ItemLeatherLeggings());
			a.Add(ItemLeatherBoots());

			a.Add(ItemChainHelmet());
			a.Add(ItemChainChestplate());
			a.Add(ItemChainLeggings());
			a.Add(ItemChainBoots());

			a.Add(ItemIronHelmet());
			a.Add(ItemIronChestplate());
			a.Add(ItemIronLeggings());
			a.Add(ItemIronBoots());

			a.Add(ItemDiamondHelmet());
			a.Add(ItemDiamondChestplate());
			a.Add(ItemDiamondLeggings());
			a.Add(ItemDiamondBoots());

			a.Add(ItemGoldHelmet());
			a.Add(ItemGoldChestplate());
			a.Add(ItemGoldLeggings());
			a.Add(ItemGoldBoots());

			return a;
		}

		public static ObservableCollection<InventoryItem> ListFoodIdOnly()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemApple());
			e.Add(ItemSoup());
			e.Add(ItemBread());
			e.Add(ItemBaconRaw());
			e.Add(ItemBaconCooked());
			e.Add(ItemGapple());
			e.Add(ItemBucketMilk());
			e.Add(ItemFishCod());
			e.Add(ItemCookedFishCod());
			e.Add(ItemCookie());
			e.Add(ItemMelon());
			e.Add(ItemBeefRaw());
			e.Add(ItemSteak());
			e.Add(ItemChickenRaw());
			e.Add(ItemChickenCooked());
			e.Add(ItemRottenFlesh());
			e.Add(ItemCarrot());
			e.Add(ItemPotato());
			e.Add(ItemPotatoCooked());
			e.Add(ItemPotatoRotten());
			e.Add(ItemPumpkinPie());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListFood()
		{
			ObservableCollection<InventoryItem> e = ListFoodIdOnly();

			e.Add(ItemNotchApple());
			e.Add(ItemFishSalmon());
			e.Add(ItemFishNemo());
			e.Add(ItemFishPuffer());
			e.Add(ItemCookedFishSalmon());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListMaterialsIdOnly()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemCoal());
			e.Add(ItemDiamond());
			e.Add(ItemIngotIron());
			e.Add(ItemIngotGold());
			e.Add(ItemStick());
			e.Add(ItemString());
			e.Add(ItemFeather());
			e.Add(ItemGunpowder());
			e.Add(ItemSeeds());
			e.Add(ItemWheat());
			e.Add(ItemFlint());
			e.Add(ItemRedstone());
			e.Add(ItemSnowball());
			e.Add(ItemLeather());
			e.Add(ItemBrick());
			e.Add(ItemClay());
			e.Add(ItemReeds());
			e.Add(ItemPaper());
			e.Add(ItemSlimeball());
			e.Add(ItemEgg());
			e.Add(ItemGlowstoneDust());
			e.Add(ItemDye(DyeType.Black));
			e.Add(ItemBone());
			e.Add(ItemSugar());
			e.Add(ItemSeedsPumpkin());
			e.Add(ItemSeedsMelon());
			e.Add(ItemEnderPearl());
			e.Add(ItemBlazeRod());
			e.Add(ItemGhastTear());
			e.Add(ItemGoldNugget());
			e.Add(ItemNetherWart());
			e.Add(ItemSpiderEye());
			e.Add(ItemBlazePowder());
			e.Add(ItemMagmaCream());
			e.Add(ItemEmerald());
			e.Add(ItemSkullSkeleton());
			e.Add(ItemQuartz());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListMaterials()
		{
			ObservableCollection<InventoryItem> e = ListMaterialsIdOnly();

			e.Add(ItemDye(DyeType.White));
			e.Add(ItemDye(DyeType.Orange));
			e.Add(ItemDye(DyeType.Magenta));
			e.Add(ItemDye(DyeType.LightBlue));
			e.Add(ItemDye(DyeType.Yellow));
			e.Add(ItemDye(DyeType.Lime));
			e.Add(ItemDye(DyeType.Pink));
			e.Add(ItemDye(DyeType.Gray));
			e.Add(ItemDye(DyeType.Silver));
			e.Add(ItemDye(DyeType.Cyan));
			e.Add(ItemDye(DyeType.Purple));
			e.Add(ItemDye(DyeType.Blue));
			e.Add(ItemDye(DyeType.Brown));
			e.Add(ItemDye(DyeType.Green));
			e.Add(ItemDye(DyeType.Red));

			e.Add(ItemSkullWither());
			e.Add(ItemSkullZombie());
			e.Add(ItemSkullPlayer());
			e.Add(ItemSkullCreeper());

			e.Sort(Compare);

			return e;
		}
		public static ObservableCollection<InventoryItem> ListPlaceables()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemPainting());
			e.Add(ItemSign());
			e.Add(ItemDoorWood());
			e.Add(ItemMinecart());
			e.Add(ItemDoorIron());
			e.Add(ItemBoat());
			e.Add(ItemMinecartChest());
			e.Add(ItemMinecartFurnace());
			e.Add(ItemCake());
			e.Add(ItemBed());
			e.Add(ItemRepeater());
			e.Add(ItemBrewingStand());
			e.Add(ItemCauldron());
			e.Add(ItemFrame());
			e.Add(ItemPot());
			e.Add(ItemComparator());
			e.Add(ItemMinecartTNT());
			e.Add(ItemMinecartHopper());
			e.Add(ItemMinecartCommandBlock());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListMusic()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemRecord13());
			e.Add(ItemRecordCat());
			e.Add(ItemRecordBlocks());
			e.Add(ItemRecordChirp());
			e.Add(ItemRecordFar());
			e.Add(ItemRecordMall());
			e.Add(ItemRecordMellohi());
			e.Add(ItemRecordStal());
			e.Add(ItemRecordStrad());
			e.Add(ItemRecordWard());
			e.Add(ItemRecord11());
			e.Add(ItemRecordWait());

			return e;
		}
		public static ObservableCollection<InventoryItem> ListMisc()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(ItemBowl());
			e.Add(ItemBucketEmpty());
			e.Add(ItemBucketWater());
			e.Add(ItemBucketLava());
			e.Add(ItemSaddle());
			e.Add(ItemBook());
			e.Add(ItemMapFilled());
			e.Add(ItemPotion(0));
			e.Add(ItemBottle());
			e.Add(ItemWeirdFunk());
			e.Add(ItemEnderEye());
			e.Add(ItemSwagMelon());
			e.Add(ItemSpawnEgg(99)); // MEOW
			e.Add(ItemXPBottle());
			e.Add(ItemFireCharge());
			e.Add(ItemBookOpen());
			e.Add(ItemBookLiterature());
			e.Add(ItemMapEmpty());
			e.Add(ItemNetherStar());
			e.Add(ItemFireworkRocket());
			e.Add(ItemFireworkStar());
			e.Add(ItemBookEnchanted());
			e.Add(ItemBrickNether());
			e.Add(ItemIronBarding());
			e.Add(ItemGoldBarding());
			e.Add(ItemDiamondBarding());
			e.Add(ItemLead());
			e.Add(ItemNameTag());

			return e;
		}

		public static ObservableCollection<InventoryItem> ListSnapshots()
		{
			ObservableCollection<InventoryItem> e = new ObservableCollection<InventoryItem>();

			e.Add(BlockBarrier());
			e.Add(BlockSlime());
			e.Add(BlockTrapdoorIron());

			return e;
		}

		public static ObservableCollection<InventoryItem> ListBlocksAll()
		{
			ObservableCollection<InventoryItem> a = ListBlocksNonTechnical_AllVariants();
			
			a.AddRange(ListBlocksTechnical());

			a.Sort(InventoryItem.Compare);

			return a;
		}
		public static ObservableCollection<InventoryItem> ListBlocksAllID()
		{
			ObservableCollection<InventoryItem> a =
				ListBlocksNonTechnical_IdOnly();
			a.AddRange(ListBlocksTechnicalIdOnly());
			a.AddRange(ListSnapshots());

			a.Sort(InventoryItem.Compare);
			return a;
		}
		public static ObservableCollection<InventoryItem> ListItemsAllIdOnly()
		{
			ObservableCollection<InventoryItem> a = ListToolsWeapons();

			a.AddRange(ListArmor());
			a.AddRange(ListFoodIdOnly());
			a.AddRange(ListMaterialsIdOnly());
			a.AddRange(ListPlaceables());
			a.AddRange(ListMusic());
			a.AddRange(ListMisc());

			a.Sort(InventoryItem.Compare);

			return a;
		}
		public static ObservableCollection<InventoryItem> ListItemsAll()
		{
			ObservableCollection<InventoryItem> a = ListToolsWeapons();

			a.AddRange(ListArmor());
			a.AddRange(ListFood());
			a.AddRange(ListMaterials());
			a.AddRange(ListPlaceables());
			a.AddRange(ListMusic());
			a.AddRange(ListMisc());

			a.Sort(InventoryItem.Compare);

			return a;
		}

		public static ObservableCollection<InventoryItem> ListALLTheThings()
		{
			ObservableCollection<InventoryItem> all = ListBlocksAll();
			all.AddRange(ListItemsAll());
			all.AddRange(ListSnapshots());

			all.Sort(InventoryItem.Compare);

			return all;
		}
		public static ObservableCollection<InventoryItem> ListAllNonTech()
		{
			ObservableCollection<InventoryItem> n = 
				ListBlocksNonTechnical_AllVariants();
			n.AddRange(ListItemsAll());
			n.AddRange(ListSnapshots());

			n.Sort(InventoryItem.Compare);
			return n;
		}
		public static ObservableCollection<InventoryItem> ListAllIDIncludeTech()
		{
			ObservableCollection<InventoryItem> a =
				ListBlocksAllID();
			a.AddRange(ListItemsAll());

			a.Sort(InventoryItem.Compare);
			return a;
		}

		public static ObservableCollection<InventoryItem> ListBlocksNonTechnical_IdOnly()
		{
			ObservableCollection<InventoryItem> n = new ObservableCollection<InventoryItem>();

			#region 0-15
			n.Add(BlockStone());
			n.Add(BlockGrass());
			n.Add(BlockDirt());
			n.Add(BlockCobblestone());
			n.Add(BlockWoodPlanks(WoodType.Oak));		//*
			n.Add(BlockSapling(WoodType.Oak));			//*
			n.Add(BlockBedrock());
			n.Add(BlockSand());
			n.Add(BlockGravel());
			n.Add(BlockOreGold());
			n.Add(BlockOreIron());
			#endregion

			#region 16-31
			n.Add(BlockOreCoal());
			n.Add(BlockLog(WoodType.Oak));				//*
			n.Add(BlockLeaves(WoodType.Oak));			//*
			n.Add(BlockSponge());
			n.Add(BlockGlass());
			n.Add(BlockOreLapis());
			n.Add(BlockStorageLapis());
			n.Add(BlockDispenser());
			n.Add(BlockSandstone());
			n.Add(BlockNoteBlock());
			n.Add(BlockRailGold());
			n.Add(BlockRailDetector());
			n.Add(BlockPistonSticky());
			n.Add(BlockWeb());
			n.Add(BlockTallGrass());
			#endregion

			#region 32-47
			n.Add(BlockDeadBush());
			n.Add(BlockPiston());
			n.Add(BlockWool(DyeType.White));				//*
			n.Add(BlockFlowerYellow());
			n.Add(BlockFlowerOther(FlowerType.Poppy));	//*
			n.Add(BlockBrownMushroom());
			n.Add(BlockRedMushroom());
			n.Add(BlockStorageGold());
			n.Add(BlockStorageIron());
			n.Add(BlockStoneSlab(StoneSpecialType.Stone));//*
			n.Add(BlockBricks());
			n.Add(BlockTNT());
			n.Add(BlockBookshelf());
			#endregion

			#region 48-63
			n.Add(BlockMossyCobblestone());
			n.Add(BlockObsidian());
			n.Add(BlockTorch());
			n.Add(BlockFire());
			n.Add(BlockSpawner());
			n.Add(BlockOakStairs());
			n.Add(BlockChest());
			n.Add(BlockOreDiamond());
			n.Add(BlockStorageDiamond());
			n.Add(BlockWorkbench());
			n.Add(BlockFurnace());
			#endregion

			#region 64-79
			n.Add(BlockLadder());
			n.Add(BlockRail());
			n.Add(BlockStairsCobble());
			n.Add(BlockStonePlate());
			n.Add(BlockWoodPlate());
			n.Add(BlockOreRedstone());
			n.Add(BlockRedTorch());
			n.Add(BlockButtonStone());
			n.Add(BlockSnowLayer());
			n.Add(BlockIce());
			#endregion

			#region 80-95
			n.Add(BlockSnow());
			n.Add(BlockCactus());
			n.Add(BlockClay());
			n.Add(BlockJukebox());
			n.Add(BlockFence());
			n.Add(BlockPumpkin());
			n.Add(BlockNetherrack());
			n.Add(BlockSoulSand());
			n.Add(BlockGlowstone());
			n.Add(BlockJackOLantern());
			n.Add(BlockGlassStained(DyeType.White));	//*
			#endregion

			#region 96-111
			n.Add(BlockTrapdoor());
			n.Add(BlockSilverfishStone());				//*
			n.Add(BlockStoneBrick());
			n.Add(BlockIronBars());
			n.Add(BlockGlassPane());
			n.Add(BlockMelon());
			n.Add(BlockVine());
			n.Add(BlockFenceGate());
			n.Add(BlockStairsBrick());
			n.Add(BlockStairsStoneBrick());
			n.Add(BlockMycelium());
			n.Add(BlockLilyPad());
			#endregion

			#region 112-127
			n.Add(BlockNetherBrick());
			n.Add(BlockNetherBrickFence());
			n.Add(BlockNetherBrickStairs());
			n.Add(BlockEnchantingTable());
			n.Add(BlockEndPortalFrame());
			n.Add(BlockEndStone());
			n.Add(BlockDragonEgg());
			n.Add(BlockRedstoneLampOff());
			n.Add(BlockWoodSlab(WoodType.Oak));			//*
			#endregion

			#region 128-143
			n.Add(BlockSandstoneStairs());
			n.Add(BlockOreEmerald());
			n.Add(BlockEnderChest());
			n.Add(BlockTripwireHook());
			n.Add(BlockStorageEmerald());
			n.Add(BlockSpruceStairs());
			n.Add(BlockBirchStairs());
			n.Add(BlockJungleStairs());
			n.Add(BlockCommandBlock());
			n.Add(BlockBeacon());
			n.Add(BlockCobbleWall());					//*
			n.Add(BlockWoodButton());
			#endregion

			#region 144-159
			n.Add(BlockAnvil(0));
			n.Add(BlockChestTrapped());
			n.Add(BlockLightPressurePlate());
			n.Add(BlockHeavyPressurePlate());
			n.Add(BlockSolarSensor());
			n.Add(BlockStorageRedstone());
			n.Add(BlockOreQuartz());
			n.Add(BlockHopper());
			n.Add(BlockStorageQuartz());
			n.Add(BlockQuartzStairs());
			n.Add(BlockActivatorRail());
			n.Add(BlockDropper());
			n.Add(BlockStainedClay(DyeType.White));	//*
			#endregion

			#region 160-175
			n.Add(BlockGlassPaneStained(DyeType.White)); //*
			n.Add(BlockAcaciaStairs());
			n.Add(BlockDarkOakStairs());
			n.Add(BlockHayBale());
			n.Add(BlockCarpet(DyeType.White));			//*
			n.Add(BlockHardenedClay());
			n.Add(BlockStorageCoal());
			n.Add(BlockIcePacked());
			n.Add(BlockFlowerLargeSunflower());			//*
			#endregion

			return n;
		}
		public static ObservableCollection<InventoryItem> ListBlocksNonTechnical_AllVariants()
		{
			ObservableCollection<InventoryItem> n = ListBlocksNonTechnical_IdOnly();

			#region wood
			//Planks
			n.Add(BlockWoodPlanks(WoodType.Birch));
			n.Add(BlockWoodPlanks(WoodType.Spruce));
			n.Add(BlockWoodPlanks(WoodType.Jungle));
			n.Add(BlockWoodPlanks(WoodType.Acacia));
			n.Add(BlockWoodPlanks(WoodType.Darkwood));

			//Saplings
			n.Add(BlockSapling(WoodType.Birch));
			n.Add(BlockSapling(WoodType.Spruce));
			n.Add(BlockSapling(WoodType.Jungle));

			//Logs
			n.Add(BlockLog(WoodType.Birch));
			n.Add(BlockLog(WoodType.Spruce));
			n.Add(BlockLog(WoodType.Jungle));

			//Leaves
			n.Add(BlockLeaves(WoodType.Birch));
			n.Add(BlockLeaves(WoodType.Spruce));
			n.Add(BlockLeaves(WoodType.Jungle));

			//Slab
			n.Add(BlockWoodSlab(WoodType.Birch));
			n.Add(BlockWoodSlab(WoodType.Spruce));
			n.Add(BlockWoodSlab(WoodType.Jungle));
			n.Add(BlockWoodSlab(WoodType.Acacia));
			n.Add(BlockWoodSlab(WoodType.Darkwood));
			#endregion

			#region colorable
			//e.Add(BlockWool(DyeType.White));
			n.Add(BlockWool(DyeType.Orange));
			n.Add(BlockWool(DyeType.Magenta));
			n.Add(BlockWool(DyeType.LightBlue));
			n.Add(BlockWool(DyeType.Yellow));
			n.Add(BlockWool(DyeType.Lime));
			n.Add(BlockWool(DyeType.Pink));
			n.Add(BlockWool(DyeType.Gray));
			n.Add(BlockWool(DyeType.Silver));
			n.Add(BlockWool(DyeType.Cyan));
			n.Add(BlockWool(DyeType.Purple));
			n.Add(BlockWool(DyeType.Blue));
			n.Add(BlockWool(DyeType.Brown));
			n.Add(BlockWool(DyeType.Green));
			n.Add(BlockWool(DyeType.Red));
			n.Add(BlockWool(DyeType.Black));

			//e.Add(BlockGlassStained(DyeType.White));
			n.Add(BlockGlassStained(DyeType.Orange));
			n.Add(BlockGlassStained(DyeType.Magenta));
			n.Add(BlockGlassStained(DyeType.LightBlue));
			n.Add(BlockGlassStained(DyeType.Yellow));
			n.Add(BlockGlassStained(DyeType.Lime));
			n.Add(BlockGlassStained(DyeType.Pink));
			n.Add(BlockGlassStained(DyeType.Gray));
			n.Add(BlockGlassStained(DyeType.Silver));
			n.Add(BlockGlassStained(DyeType.Cyan));
			n.Add(BlockGlassStained(DyeType.Purple));
			n.Add(BlockGlassStained(DyeType.Blue));
			n.Add(BlockGlassStained(DyeType.Brown));
			n.Add(BlockGlassStained(DyeType.Green));
			n.Add(BlockGlassStained(DyeType.Red));
			n.Add(BlockGlassStained(DyeType.Black));

			//e.Add(BlockStainedClay(DyeType.White));
			n.Add(BlockStainedClay(DyeType.Orange));
			n.Add(BlockStainedClay(DyeType.Magenta));
			n.Add(BlockStainedClay(DyeType.LightBlue));
			n.Add(BlockStainedClay(DyeType.Yellow));
			n.Add(BlockStainedClay(DyeType.Lime));
			n.Add(BlockStainedClay(DyeType.Pink));
			n.Add(BlockStainedClay(DyeType.Gray));
			n.Add(BlockStainedClay(DyeType.Silver));
			n.Add(BlockStainedClay(DyeType.Cyan));
			n.Add(BlockStainedClay(DyeType.Purple));
			n.Add(BlockStainedClay(DyeType.Blue));
			n.Add(BlockStainedClay(DyeType.Brown));
			n.Add(BlockStainedClay(DyeType.Green));
			n.Add(BlockStainedClay(DyeType.Red));
			n.Add(BlockStainedClay(DyeType.Black));

			//e.Add(BlockGlassPaneStained(DyeType.White));
			n.Add(BlockGlassPaneStained(DyeType.Orange));
			n.Add(BlockGlassPaneStained(DyeType.Magenta));
			n.Add(BlockGlassPaneStained(DyeType.LightBlue));
			n.Add(BlockGlassPaneStained(DyeType.Yellow));
			n.Add(BlockGlassPaneStained(DyeType.Lime));
			n.Add(BlockGlassPaneStained(DyeType.Pink));
			n.Add(BlockGlassPaneStained(DyeType.Gray));
			n.Add(BlockGlassPaneStained(DyeType.Silver));
			n.Add(BlockGlassPaneStained(DyeType.Cyan));
			n.Add(BlockGlassPaneStained(DyeType.Purple));
			n.Add(BlockGlassPaneStained(DyeType.Blue));
			n.Add(BlockGlassPaneStained(DyeType.Brown));
			n.Add(BlockGlassPaneStained(DyeType.Green));
			n.Add(BlockGlassPaneStained(DyeType.Red));
			n.Add(BlockGlassPaneStained(DyeType.Black));

			//e.Add(BlockCarpet(DyeType.White));
			n.Add(BlockCarpet(DyeType.Orange));
			n.Add(BlockCarpet(DyeType.Magenta));
			n.Add(BlockCarpet(DyeType.LightBlue));
			n.Add(BlockCarpet(DyeType.Yellow));
			n.Add(BlockCarpet(DyeType.Lime));
			n.Add(BlockCarpet(DyeType.Pink));
			n.Add(BlockCarpet(DyeType.Gray));
			n.Add(BlockCarpet(DyeType.Silver));
			n.Add(BlockCarpet(DyeType.Cyan));
			n.Add(BlockCarpet(DyeType.Purple));
			n.Add(BlockCarpet(DyeType.Blue));
			n.Add(BlockCarpet(DyeType.Brown));
			n.Add(BlockCarpet(DyeType.Green));
			n.Add(BlockCarpet(DyeType.Red));
			n.Add(BlockCarpet(DyeType.Black));
			#endregion

			#region stone
			n.Add(BlockStoneSlab(StoneSpecialType.Sandstone));
			n.Add(BlockStoneSlab(StoneSpecialType.Cobble));
			n.Add(BlockStoneSlab(StoneSpecialType.Brick));
			n.Add(BlockStoneSlab(StoneSpecialType.StoneBrick));
			n.Add(BlockStoneSlab(StoneSpecialType.NetherBrick));
			n.Add(BlockStoneSlab(StoneSpecialType.Quartz));

			n.Add(BlockCobbleWallMossy());
			#endregion

			#region flowers
			n.Add(BlockFlowerOther(FlowerType.BlueOrchid));
			n.Add(BlockFlowerOther(FlowerType.Allium));
			n.Add(BlockFlowerOther(FlowerType.AzureBluet));
			n.Add(BlockFlowerOther(FlowerType.TulipRed));
			n.Add(BlockFlowerOther(FlowerType.TulipOrange));
			n.Add(BlockFlowerOther(FlowerType.TulipWhite));
			n.Add(BlockFlowerOther(FlowerType.TulipPink));
			n.Add(BlockFlowerOther(FlowerType.OxeyeDaisy));

			n.Add(BlockFlowerLargeLilac());
			n.Add(BlockFlowerLargeGrass());
			n.Add(BlockFlowerLargeFern());
			n.Add(BlockFlowerLargeRose());
			n.Add(BlockFlowerLargePeony());
			#endregion

			n.Sort(InventoryItem.Compare);

			return n;
		}

		public static ObservableCollection<InventoryItem> ListBlocksTechnical()
		{
			ObservableCollection<InventoryItem> t = new ObservableCollection<InventoryItem>();

			t.Add(BlockWaterFlowing_Tech());
			t.Add(BlockWaterStationary_Tech());
			t.Add(BlockLavaFlowing_Tech());
			t.Add(BlockLavaStationary_Tech());

			t.Add(BlockBed_Tech(0));
			t.Add(BlockPistonHead_Tech());
			t.Add(BlockPistonExtender_Tech());
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Stone));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Sandstone));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Cobble));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Brick));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.StoneBrick));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.NetherBrick));
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Quartz));

			t.Add(BlockRedWire_Tech());
			t.Add(BlockCropsWheat_Tech());
			t.Add(BlockFarmland());
			t.Add(BlockFurnaceLit_Tech());
			t.Add(BlockWoodDoor_Tech());
			t.Add(BlockSignWall_Tech());
			t.Add(BlockIronDoor_Tech());
			t.Add(BlockOreRedstoneLit_Tech());
			t.Add(BlockRedTorchOff_Tech());

			t.Add(BlockReeds_Tech());
			t.Add(BlockNetherPortal_Tech());
			t.Add(BlockCake_Tech());
			t.Add(BlockRepeaterOff_Tech());
			t.Add(BlockRepeaterOn_Tech());
			t.Add(BlockBrownMushroomLarge_Tech(0));
			t.Add(BlockRedMushroomLarge_Tech(0));
			t.Add(BlockStemPumpkin_Tech());
			t.Add(BlockStemMelon_Tech());
			t.Add(BlockNetherWart_Tech());
			t.Add(BlockBrewingStand_Tech());
			t.Add(BlockCauldron_Tech(0));
			t.Add(BlockEndPortal_Tech());
			t.Add(BlockRedstoneLampOn_Tech());

			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Oak));
			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Birch));
			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Spruce));
			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Jungle));
			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Acacia));
			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Darkwood));

			t.Add(BlockCocoaPod_Tech(8));
			t.Add(BlockTripwire_Tech());
			t.Add(BlockFlowerPot_Tech());
			t.Add(BlockCarrotCrop_Tech(7));
			t.Add(BlockPotatoCrop_Tech(7));
			t.Add(BlockSkull_Tech(0));
			t.Add(BlockSkull_Tech(1));
			t.Add(BlockSkull_Tech(2));
			t.Add(BlockSkull_Tech(3));
			t.Add(BlockSkull_Tech(4));
			t.Add(BlockComparatorUnpowered_Tech());
			t.Add(BlockComparatorPowered_Tech());

			return t;
		}
		public static ObservableCollection<InventoryItem> ListBlocksTechnicalIdOnly()
		{
			ObservableCollection<InventoryItem> t = new ObservableCollection<InventoryItem>();

			t.Add(BlockWaterFlowing_Tech());
			t.Add(BlockWaterStationary_Tech());
			t.Add(BlockLavaFlowing_Tech());
			t.Add(BlockLavaStationary_Tech());

			t.Add(BlockBed_Tech(0));
			t.Add(BlockPistonHead_Tech());
			t.Add(BlockPistonExtender_Tech());
			t.Add(BlockDoubleStoneSlab_Tech(StoneSpecialType.Stone));

			t.Add(BlockRedWire_Tech());
			t.Add(BlockCropsWheat_Tech());
			t.Add(BlockFarmland());
			t.Add(BlockFurnaceLit_Tech());
			t.Add(BlockWoodDoor_Tech());
			t.Add(BlockSignWall_Tech());
			t.Add(BlockIronDoor_Tech());
			t.Add(BlockOreRedstoneLit_Tech());
			t.Add(BlockRedTorchOff_Tech());

			t.Add(BlockReeds_Tech());
			t.Add(BlockNetherPortal_Tech());
			t.Add(BlockCake_Tech());
			t.Add(BlockRepeaterOff_Tech());
			t.Add(BlockRepeaterOn_Tech());
			t.Add(BlockBrownMushroomLarge_Tech(0));
			t.Add(BlockRedMushroomLarge_Tech(0));
			t.Add(BlockStemPumpkin_Tech());
			t.Add(BlockStemMelon_Tech());
			t.Add(BlockNetherWart_Tech());
			t.Add(BlockBrewingStand_Tech());
			t.Add(BlockCauldron_Tech(0));
			t.Add(BlockEndPortal_Tech());
			t.Add(BlockRedstoneLampOn_Tech());

			t.Add(BlockDoubleWoodSlab_Tech(WoodType.Oak));

			t.Add(BlockCocoaPod_Tech(8));
			t.Add(BlockTripwire_Tech());
			t.Add(BlockFlowerPot_Tech());
			t.Add(BlockCarrotCrop_Tech(7));
			t.Add(BlockPotatoCrop_Tech(7));
			t.Add(BlockSkull_Tech(0));
			t.Add(BlockComparatorUnpowered_Tech());
			t.Add(BlockComparatorPowered_Tech());

			return t;
		}

		// Creative Tabs
		// 404
	}
}