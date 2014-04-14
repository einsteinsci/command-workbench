using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using CommandGeneratorWPF.NBT;

namespace CommandGeneratorWPF
{
	/// <summary>
	/// Interaction logic for ItemCreationWindow.xaml
	/// </summary>
	public partial class ItemCreationWindow : Window
	{
		public ItemCreationWindow()
		{
			LoadUp();

			isCountless = false;
		}
		public ItemCreationWindow(bool countless)
		{
			LoadUp();

			isCountless = countless;

			if (isCountless)
			{
				ItemCountUpDown.IsEnabled = false;
				ItemCountUpDown.Value = null;
				ItemCountLbl.IsEnabled = false;

				SelectedItemStack.Count = 0;
			}
		}

		/// <summary>
		/// This is ALWAYS run in the constructor
		/// </summary>
		private void LoadUp()
		{
			isLoading = true;
			isCancel = true;

			GiveSelectedItem = InventoryItem.ItemDiamondSword();

			ResetItemSelector();
			ResetCanDestroySelector();
			ResetCanPlaceOnSelector();

			GiveItemTag_tag = new ItemTagTag();

			/////////////////////////
			InitializeComponent(); //
			/////////////////////////
			isLoading = false;

			fwIsLoading = false;
			syncingMeta = false;

			SelectItem(GiveSelectedItem);
			SelectItemBtn.DataContext = itemSelect.CurrentSelection;
			GiveEnableDisableNBT();

			CanDestroySelectedItem = InventoryItem.BlockGrass();
			CanDestroySelectBtn.DataContext = CanDestroySelectedItem;
			CanPlaceOnSelectedItem = InventoryItem.BlockWoodPlanks(WoodType.Oak);
			CanPlaceOnSelectBtn.DataContext = CanPlaceOnSelectedItem;

			UpdateEnchantmentList();
			PotionBaseCombo_SelectionChanged(null, null);
			UpdatePotionList();

			GiveRefreshNBT();

			ItemCountUpDown_ValueChanged(null, null);

			RefreshSkullImg();
			RefreshMaxDurabilityToolTip();

			RenderOptions.SetBitmapScalingMode(this,
				BitmapScalingMode.NearestNeighbor);
		}

		private bool isLoading;

		private const string _warningFunkyName = "Warning: " +
			"Using names in this command will not give the expected item.";

		private bool syncingMeta;

		private ItemSelectWindow itemSelect;

		private ItemSelectWindow canDestroySelect;
		private ItemSelectWindow canPlaceOnSelect;

		public bool isCancel;

		public bool isCountless;

		public InventoryItem GiveSelectedItem
		{
 			get
			{
				return _giveSelectedItem;
			}
			set
			{
				_giveSelectedItem = value;
				//System.Diagnostics.Debugger.Break(); //Do it because I said so.
			}
		}
		private InventoryItem _giveSelectedItem;

		public ItemFullTag SelectedItemStack
		{
			get
			{
				byte? count = ItemCountUpDown.Value;

				if (isCountless)
					count = null;

				return new ItemFullTag(GiveSelectedItem,
					count, null,
					GiveItemTag_tag);		// no slot
			}
		}

		public string SelectedItemString
		{
			get
			{
				return GiveCommandOutputBox.Text;
			}
		}

		public InventoryItem CanDestroySelectedItem
		{ get; private set; }
		public InventoryItem CanPlaceOnSelectedItem
		{ get; private set; }

		public ItemTagTag GiveItemTag_tag
		{ get; private set; }

		private void GiveRefreshCommand()
		{
			if (GiveCommandOutputBox == null)
			{
				return;
			}

			string json = SelectedItemStack.ToJSON();

			GiveCommandOutputBox.Text = json;
		}

		private void ResetItemSelector()
		{
			itemSelect = new ItemSelectWindow(GiveSelectedItem);
			itemSelect.Closed += ItemSelectWindow_Closed;

			RefreshMaxDurabilityToolTip();
		}

		private void RefreshMaxDurabilityToolTip()
		{
			if (GiveSelectedItem.IsDurableAble &&
						 GiveDamageUpDown != null)
			{
				string tip = "Max Durability: " +
					GiveSelectedItem.MaxDurability;

				GiveDamageTxt.ToolTip = tip;
				GiveDamageUpDown.ToolTip = tip;
			}
		}
		private void ItemSelectWindow_Closed(object sender, EventArgs e)
		{
			SelectItem(itemSelect.CurrentSelection);

			ResetItemSelector();
			GiveRefreshNBT();
		}
		private void SelectItem(InventoryItem selected)
		{
			SelectItemBtn.DataContext = selected;

			GiveSelectedItem = selected;
			syncingMeta = true;
			ItemMetaTxt.Text = selected.Metadata.ToString();
			GiveEnableDisableNBT();
			syncingMeta = false;
			SelectedItemIdTxt.Text = "(" + selected.ID.ToString() + ")";

			if (selected.WrongName)
			{
				WarningTxt.Text = _warningFunkyName;
				WarningTxt.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				WarningTxt.Visibility = System.Windows.Visibility.Hidden;
			}
		}

		private void ResetCanDestroySelector()
		{
			canDestroySelect = new ItemSelectWindow(true);
			canDestroySelect.Closed += CanDestroySelectWindow_Closed;
		}
		private void CanDestroySelectWindow_Closed(object sender, EventArgs e)
		{
			InventoryItem item = canDestroySelect.CurrentSelection;
			CanDestroySelectBtn.DataContext = item;
			CanDestroySelectedItem = item;

			ResetCanDestroySelector();
		}
		private void CanDestroySelectBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!canDestroySelect.IsLoaded)
			{
				canDestroySelect.Show();
			}

			canDestroySelect.Activate();
		}

		private void ResetCanPlaceOnSelector()
		{
			canPlaceOnSelect = new ItemSelectWindow(true);
			canPlaceOnSelect.Closed += CanPlaceOnSelectWindow_Closed;
		}
		private void CanPlaceOnSelectWindow_Closed(object sender, EventArgs e)
		{
			InventoryItem item = canPlaceOnSelect.CurrentSelection;
			CanPlaceOnSelectBtn.DataContext = item;
			CanPlaceOnSelectedItem = item;

			ResetCanPlaceOnSelector();
		}
		private void CanPlaceOnSelectBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!canPlaceOnSelect.IsLoaded)
			{
				canPlaceOnSelect.Show();
			}

			canPlaceOnSelect.Activate();
		}

		private void GiveRefreshNBT()
		{
			if (isLoading)
			{
				return;
			}

			GiveTree.Items.Clear();

			ItemTagTag tag = GiveItemTag_tag;

			if (tag == null)
			{
				tag = new ItemTagTag();
			}

			#region display
			if (DisplayTab.Visibility == Visibility.Visible)
			{
				tag.display = new ItemTagTag.DisplayTag();

				tag.display.Name = DisplayNameBox.Text;
				if (LoreList.HasItems)
				{
					tag.display.Lore = new List<string>();
					foreach (string s in LoreList.Items)
					{
						tag.display.Lore.Add(s);
					}
				}
				else
				{
					tag.display.Lore = null;
				}

				if (EnableCustomColorToggle.IsChecked ?? false)
				{
					tag.display.color = int.Parse(DisplayColorBox.Text);
				}

				if (tag.display.Name == "" &&
					tag.display.color == null &&
					tag.display.Lore == null)
				{
					tag.display = null;
				}
			}
			else
			{
				tag.display = null;
			}
			#endregion

			#region general
			if (GeneralTab.Visibility == Visibility.Visible)
			{
				tag.Unbreakable = (UnbreakableCheck.IsChecked ?? false).ToByte();
				if (CanPlaceOnItemsList.HasItems)
				{
					tag.CanPlaceOn = new List<string>();
					foreach (InventoryItem i in CanPlaceOnItemsList.Items)
					{
						tag.CanPlaceOn.Add(i.ShortName);
					}
				}
				else
				{
					tag.CanPlaceOn = null;
				}

				if (CanDestroyItemsList.HasItems)
				{
					tag.CanDestroy = new List<string>();
					foreach (InventoryItem i in CanDestroyItemsList.Items)
					{
						tag.CanDestroy.Add(i.ShortName);
					}
				}
				else
				{
					tag.CanDestroy = null;
				}
			}
			#endregion

			#region ench
			if (EnchList != null)
			{
				if (EnchList.HasItems)
				{
					if (EnchTypeBtn.Tag as string == "std")
					{
						tag.storedEnch = null;

						tag.ench = new List<ItemTagTag.EnchantmentTag>();
						foreach (ItemTagTag.EnchantmentTag e in EnchList.Items)
						{
							tag.ench.Add(e);
						}
					}
					else
					{
						tag.ench = null;

						tag.storedEnch = new List<ItemTagTag.EnchantmentTag>();
						foreach (ItemTagTag.EnchantmentTag e in EnchList.Items)
						{
							tag.storedEnch.Add(e);
						}
					}
				}
				else
				{
					tag.ench = null;
					tag.storedEnch = null;
				}
			}

			if (EnchRepairCost != null)
			{
				if (EnchRepairCost.Value != 1)
				{
					tag.RepairCost = EnchRepairCost.Value ?? 1;
				}
				else
				{
					tag.RepairCost = 1;
				}
			}
			#endregion

			#region potion
			if (PotionTab != null)
			{
				if (PotionTab.Visibility == System.Windows.Visibility.Visible)
				{
					if (PotionList.HasItems)
					{
						tag.CustomPotionEffects = new List<ItemTagTag.CustomPotionEffectTag>();
						foreach (ItemTagTag.CustomPotionEffectTag p in PotionList.Items)
						{
							tag.CustomPotionEffects.Add(p);
						}
					}
					else
					{
						tag.CustomPotionEffects = null;
					}
				}
				else
				{
					tag.CustomPotionEffects = null;
				}
			}
			#endregion

			#region book
			if (BookTab != null && BookPagesList != null)
			{
				if (BookTab.Visibility == System.Windows.Visibility.Visible)
				{
					tag.author = BookAuthorBox.Text;
					tag.title = BookTitleBox.Text;
					tag.generation = BookGenerationCombo.SelectedIndex; //Lines up perfectly

					tag.pages = new List<string>();
					foreach (ListBoxItem item in BookPagesList.Items)
					{
						if (item.Tag as string != "")
						{
							tag.pages.Add(item.Tag as string);
						}
					}
				}
				else
				{
					tag.author = "";
					tag.title = "";
					tag.generation = 0;
					tag.pages = null;
				}
			}
			#endregion

			#region FwStar
			if (FwStarTab != null && FwStarMainColorsList != null)
			{
				if (FwStarTab.Visibility == System.Windows.Visibility.Visible)
				{
					tag.Explosion = new ItemTagTag.FireworksStarTag();

					tag.Explosion.Flicker = (FwStarFlickerCheck.IsChecked ?? false).ToByte();
					tag.Explosion.Trail = (FwStarTrailCheck.IsChecked ?? false).ToByte();

					tag.Explosion.Type = (byte)FwStarShapeCombo.SelectedIndex;

					if (FwStarMainColorsList.HasItems)
					{
						tag.Explosion.Colors = new List<int>();
						foreach (ListBoxItem i in FwStarMainColorsList.Items)
						{
							tag.Explosion.Colors.Add((int)(i.Tag));
						}
					}
					else
					{
						tag.Explosion.Colors = null;
					}

					if (FwStarFadeColorsList.HasItems)
					{
						tag.Explosion.FadeColors = new List<int>();
						foreach (ListBoxItem i in FwStarFadeColorsList.Items)
						{
							tag.Explosion.FadeColors.Add((int)(i.Tag));
						}
					}
					else
					{
						tag.Explosion.FadeColors = null;
					}
				}
				else
				{
					tag.Explosion = null;
				}
			}
			#endregion

			#region FwRocket
			if (FwRocketTab != null && FwRocketExplosionsList != null)
			{
				if (FwRocketTab.Visibility == System.Windows.Visibility.Visible)
				{
					ItemTagTag.FireworksRocketTag rocket = new ItemTagTag.FireworksRocketTag();

					rocket.Flight = (sbyte)(FwRocketFlightUpDown.Value ?? 1);

					if (FwRocketExplosionsList.HasItems)
					{
						rocket.Explosions = new List<ItemTagTag.FireworksStarTag>();
						foreach (FireworksStarDisplayTag f in FwRocketExplosionsList.Items)
						{
							rocket.Explosions.Add(f.ToTag());
						}
					}
					else
					{
						rocket.Explosions = null;
					}

					tag.Fireworks = rocket;
				}
				else
				{
					tag.Fireworks = null;
				}
			}
			#endregion

			#region other
			if (OtherTab != null)
			{
				if (OtherTab.Visibility == System.Windows.Visibility.Visible)
				{
					if (SkullPlayerNameBox != null)
					{
						if (SkullPanel.IsEnabled)
						{
							tag.SkullOwner = SkullPlayerNameBox.Text;
						}
						else
						{
							tag.SkullOwner = "";
						}
					}

					if (MapIdUpDown != null)
					{
						if (MapPanel.IsEnabled)
						{
							tag.map_is_scaling = MapScaleCheck.IsChecked;
						}
						else
						{
							tag.map_is_scaling = null;
						}
					}
				}
				else
				{
					tag.SkullOwner = "";
					tag.map_is_scaling = null;
				}
			}
			#endregion

			GiveItemTag_tag = tag;

			if (GiveItemTag_tag.isEmpty())
			{
				GiveItemTag_tag = null;
			}

			GiveTree.Items.Add(SelectedItemStack.ToTreeView());

			GiveRefreshCommand();
		}

		private void GiveEnableDisableNBT()
		{
			InventoryItem item = GiveSelectedItem;

			#region display
			if (!GiveSelectedItem.IsLeatherArmor)
			{
				EnableCustomColorToggle.IsEnabled = false;
				EnableCustomColorToggle.IsChecked = false;
			}
			else
			{
				EnableCustomColorToggle.IsEnabled = true;
			}
			#endregion

			#region general
			if (item.IsDurableAble)
			{
				UnbreakableCheck.IsEnabled = true;
				GiveDamageUpDown.IsEnabled = true;
			}
			else
			{
				UnbreakableCheck.IsEnabled = false;
				UnbreakableCheck.IsChecked = false;

				GiveDamageUpDown.IsEnabled = false;

				syncingMeta = true;
				GiveDamageUpDown.Value = GiveSelectedItem.Metadata;
				syncingMeta = false;
			}

			if (item.IsBlock)
			{
				CanPlaceOnPanel.IsEnabled = true;
			}
			else
			{
				CanPlaceOnPanel.IsEnabled = false;
				CanPlaceOnItemsList.Items.Clear();
			}
			#endregion

			#region ench
			if (item.ShortName == "enchanted_book")
			{
				EnchTypeBtn.IsEnabled = true;
				if (EnchTypeBtn.Tag as string == "std")
				{
					EnchTypeBtn_Click(null, null);
				}
			}
			else
			{
				EnchTypeBtn.IsEnabled = false;
				if (EnchTypeBtn.Tag as string == "book")
				{
					EnchTypeBtn_Click(null, null);
				}
			}

			if (item.IsDurableAble)
			{
				EnchRepairCost.IsEnabled = true;
			}
			else
			{
				EnchRepairCost.IsEnabled = false;
			}
			#endregion

			#region potion
			if (item.IsPotion)
			{
				PotionTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				PotionTab.Visibility = System.Windows.Visibility.Collapsed;
			}
			#endregion

			#region book
			if (item.IsBook)
			{
				BookTab.Visibility = System.Windows.Visibility.Visible;

				if (item.ShortName == "written_book")
				{
					BookLitGrid.IsEnabled = true;
				}
				else
				{
					BookLitGrid.IsEnabled = false;
					BookAuthorBox.Text = "";
					BookTitleBox.Text = "";
				}
			}
			else
			{
				BookTab.Visibility = System.Windows.Visibility.Collapsed;
			}
			#endregion

			#region FwStar
			if (item.IsFireworksStar)
			{
				FwStarTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				FwStarTab.Visibility = System.Windows.Visibility.Collapsed;
			}
			#endregion

			#region FwRocket
			if (item.IsFireworksRocket)
			{
				FwRocketTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				FwRocketTab.Visibility = System.Windows.Visibility.Collapsed;
			}
			#endregion

			#region other
			if (item.IsPlayerSkull || item.IsMap)
			{
				OtherTab.Visibility = System.Windows.Visibility.Visible;

				if (item.IsPlayerSkull)
				{
					SkullPanel.IsEnabled = true;
				}
				else
				{
					SkullPanel.IsEnabled = false;
					SkullPlayerNameBox.Text = "";
				}

				if (item.IsMap)
				{
					MapPanel.IsEnabled = true;
				}
				else
				{
					MapPanel.IsEnabled = false;
					MapIdUpDown.Value = 0;
					MapScaleCheck.IsChecked = false;
				}
			}
			else
			{
				OtherTab.Visibility = System.Windows.Visibility.Collapsed;
			}
			#endregion

			if (!syncingMeta)
			{
				syncingMeta = true;
				UpdateEnchantmentList();
				UpdatePotionList();
				syncingMeta = false;
			}
		}

		private void UpdateEnchantmentList()
		{
			List<ItemTagTag.EnchantmentTag> tags;

			if ((EnchShowAllCheck.IsChecked ?? false) ||
				EnchTypeBtn.Tag as string == "book")
			{
				tags = ItemTagTag.AvailableEnchantments(
					EnchantmentType.All);
			}
			else
			{
				tags = ItemTagTag.AvailableEnchantments(
					GiveSelectedItem.EnchantabilityType);
			}

			EnchantmentCombo.Items.Clear();
			foreach (ItemTagTag.EnchantmentTag tag in tags)
			{
				EnchantmentCombo.Items.Add(Extras.CbItemFromEnchTag(tag));
			}

			EnchantmentCombo.SelectedIndex = EnchantmentCombo.HasItems ? 0 : -1;
		}

		private void UpdatePotionList()
		{
			List<byte> normal = ItemTagTag.NormalPotionEffects();
			List<byte> all = ItemTagTag.AllPotionEffects();

			PotionBaseCombo.Items.Clear();
			foreach (byte b in normal)
			{
				ComboBoxItem cbItem = new ComboBoxItem();
				cbItem.Content = ItemTagTag.PotionDamageFriendlyNames()[b];
				cbItem.Tag = b;
				PotionBaseCombo.Items.Add(cbItem);
			}
			PotionBaseCombo.SelectedIndex = 0;

			PotionCustomCombo.Items.Clear();
			foreach (byte b in all)
			{
				ComboBoxItem cbItem = new ComboBoxItem();
				cbItem.Content = ItemTagTag.PotionEffectFriendlyNames()[b];
				cbItem.Tag = b;
				PotionCustomCombo.Items.Add(cbItem);
			}
			PotionCustomCombo.SelectedIndex = 0;
		}

		private void SelectItemBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!itemSelect.IsLoaded)
			{
				itemSelect.Show();
			}

			itemSelect.Activate();
		}

		private void ItemCountUpDown_ValueChanged(object sender,
			RoutedPropertyChangedEventArgs<object> e)
		{
			if (GiveSelectedItem == null)
			{
				return;
			}

			if (GiveSelectedItem.MaxStackSize < ItemCountUpDown.Value)
			{
				ItemCountUpDown.Foreground = new SolidColorBrush(Colors.Red);
			}
			else
			{
				ItemCountUpDown.Foreground = new SolidColorBrush(Colors.Black);
			}

			GiveRefreshNBT();
		}

		private void ItemMetaTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!syncingMeta && GiveDamageUpDown != null)
			{
				syncingMeta = true;

				GiveDamageUpDown.Value = GiveSelectedItem.Metadata;
				PotionMetaBox.Text = GiveSelectedItem.Metadata.ToString();

				syncingMeta = false;

				GiveRefreshNBT();
			}
		}

		#region display
		private void LoreAddBtn_Click(object sender, RoutedEventArgs e)
		{
			LoreList.Items.Add(LoreLineBox.Text);
			LoreLineBox.Text = "";
			GiveRefreshNBT();
		}
		private void LoreRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			LoreList.Items.Remove(LoreList.SelectedItem);
			GiveRefreshNBT();
		}

		private void EnableCustomColorToggle_Checked(object sender, RoutedEventArgs e)
		{
			CustomColorStack.IsEnabled = true;
			GiveRefreshNBT();
		}
		private void EnableCustomColorToggle_Unchecked(object sender, RoutedEventArgs e)
		{
			CustomColorStack.IsEnabled = false;
			GiveRefreshNBT();
		}

		private void DisplayColorCanvas_SelectedColorChanged(object sender,
			RoutedPropertyChangedEventArgs<Color> e)
		{
			Color c = e.NewValue;

			int dec = (c.R << 16) + (c.G << 8) + c.B;

			DisplayColorBox.Text = dec.ToString();
			GiveRefreshNBT();
		}

		private void DisplayNameBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			GiveRefreshNBT();
		}
		#endregion

		#region general
		private void CanDestroyAddBtn_Click(object sender, RoutedEventArgs e)
		{
			CanDestroyItemsList.Items.Add(CanDestroySelectedItem);
			GiveRefreshNBT();
		}
		private void CanDestroyRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			CanDestroyItemsList.Items.Remove(CanDestroyItemsList.SelectedItem as InventoryItem);
			GiveRefreshNBT();
		}

		private void CanPlaceOnAddBtn_Click(object sender, RoutedEventArgs e)
		{
			CanPlaceOnItemsList.Items.Add(CanPlaceOnSelectedItem);
			GiveRefreshNBT();
		}
		private void CanPlaceOnRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			CanPlaceOnItemsList.Items.Remove(CanPlaceOnItemsList.SelectedItem as InventoryItem);
			GiveRefreshNBT();
		}

		private void UnbreakableCheck_Changed(object sender, RoutedEventArgs e)
		{
			GiveRefreshNBT();
		}

		private void GiveDamageUpDown_ValueChanged(object sender,
			RoutedPropertyChangedEventArgs<object> e)
		{
			if (!syncingMeta && PotionMetaBox != null)
			{
				syncingMeta = true;

				GiveSelectedItem.Metadata = (short)(GiveDamageUpDown.Value ?? 0);
				ItemMetaTxt.Text = GiveSelectedItem.Metadata.ToString();
				PotionMetaBox.Text = GiveSelectedItem.Metadata.ToString();

				syncingMeta = false;

				GiveRefreshNBT();
			}
		}
		#endregion

		#region ench
		private void EnchTypeBtn_Click(object sender, RoutedEventArgs e)
		{
			if (EnchTypeBtn.Tag as string == "std")
			{
				EnchTypeBtn.Tag = "book";
				EnchTypeBtn.Content = "Enchanting Type: Enchanted Books";
				EnchShowAllCheck.IsEnabled = false;
				EnchShowAllCheck.IsChecked = true;
			}
			else
			{
				EnchTypeBtn.Tag = "std";
				EnchTypeBtn.Content = "Enchanting Type: Standard";
				EnchShowAllCheck.IsEnabled = true;
			}

			EnchList.Items.Clear();
			GiveRefreshNBT();
		}

		private void EnchAddBtn_Click(object sender, RoutedEventArgs e)
		{
			short id = (short)((ComboBoxItem)EnchantmentCombo.SelectedItem).Tag;

			EnchList.Items.Add(new ItemTagTag.EnchantmentTag(id,
				(short)EnchLevelUpDn.Value.Value));

			GiveRefreshNBT();
		}
		private void EnchRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			EnchList.Items.Remove(EnchList.SelectedItem);
			GiveRefreshNBT();
		}

		private void EnchShowAllCheck_Changed(object sender, RoutedEventArgs e)
		{
			UpdateEnchantmentList();
		}

		private void EnchRepairCost_ValueChanged(object sender,
			RoutedPropertyChangedEventArgs<object> e)
		{
			GiveRefreshNBT();
		}
		#endregion

		#region potion
		private void PotionBaseStrBtn_Click(object sender, RoutedEventArgs e)
		{
			if (PotionBaseStrBtn.Content as string == "I")
			{
				PotionBaseStrBtn.Content = "II";
			}
			else
			{
				PotionBaseStrBtn.Content = "I";
			}

			PotionGenerateDamageValue();
		}

		private void PotionParticlesCheck_Checked(object sender, RoutedEventArgs e)
		{
			if (PotionAmbientCheck == null)
			{
				return;
			}

			PotionAmbientCheck.IsEnabled = true;
		}

		private void PotionParticlesCheck_Unchecked(object sender, RoutedEventArgs e)
		{
			PotionAmbientCheck.IsEnabled = false;
			PotionAmbientCheck.IsChecked = false;
		}

		private void PotionGenerateDamageValue()
		{
			byte effectByte = (byte)((PotionBaseCombo.SelectedItem as ComboBoxItem).Tag);
			int effect = effectByte;

			Bit strBit = (PotionBaseStrBtn.Content as string) == "II";
			int str = strBit;

			Bit extBit = PotionBaseExtendedCheck.IsChecked ?? false;
			int ext = extBit;

			int splash = 0;
			if (PotionBaseSplashCheck.IsChecked ?? false)
			{
				splash = 16384; //bit 14
			}
			else if (effect != 0) // no bit 13 for water
			{
				splash = 8192; //bit 13
			}

			int damage = splash + (ext << 6) + (str << 5) + effect;

			if (!syncingMeta)
			{
				syncingMeta = true;
				ItemMetaTxt.Text = damage.ToString();
				GiveSelectedItem.Metadata = (short)damage;
				PotionMetaBox.Text = damage.ToString();
				GiveDamageUpDown.Value = damage;
				syncingMeta = false;
			}

			GiveRefreshNBT();
		}

		private void PotionBaseCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			byte b = 0;

			try
			{
				b = (byte)((PotionBaseCombo.SelectedItem as ComboBoxItem).Tag);
			}
			catch (NullReferenceException)
			{
				return;
			}

			// All of these are only one strength through damage values:
			// Fire res,  nite-vis, slowness,  water br., invisible, water bottle
			if (b == 3 || b == 6 || b == 10 || b == 13 || b == 14 || b == 0)
			{
				PotionBaseStrBtn.IsEnabled = false;
				PotionBaseStrBtn.Content = "I";
			}
			else if (!PotionList.HasItems)
			{
				PotionBaseStrBtn.IsEnabled = true;
			}

			// Healing (5) and harming (6) cannot be extended through damage values.
			// Water bottles (0) have no effect to extend.
			if (b == 5 || b == 12 || b == 0)
			{
				PotionBaseExtendedCheck.IsEnabled = false;
				PotionBaseExtendedCheck.IsChecked = false;
			}
			else if (!PotionList.HasItems)
			{
				PotionBaseExtendedCheck.IsEnabled = true;
			}

			// no throwable water bottles
			if (b == 0)
			{
				PotionBaseSplashCheck.IsEnabled = false;
				PotionBaseSplashCheck.IsChecked = false;
			}
			else
			{
				PotionBaseSplashCheck.IsEnabled = true;
			}

			PotionGenerateDamageValue();
		}

		private void PotionRefreshHandler(object sender, RoutedEventArgs e)
		{
			PotionGenerateDamageValue();
		}

		private void PotionDurationUpDown_ValueChanged(object sender,
			RoutedPropertyChangedEventArgs<object> e)
		{
			float ticks = (PotionDurationUpDown.Value ?? 0.0f) * 20.0f;
			int iTicks = (int)ticks;

			float result = ((float)iTicks) / 20.0f;
			PotionDurationUpDown.Value = result;
		}

		private void PotionCustomAddBtn_Click(object sender, RoutedEventArgs e)
		{
			ItemTagTag.CustomPotionEffectTag tag = new ItemTagTag.CustomPotionEffectTag();
			tag.Id = (byte)((PotionCustomCombo.SelectedItem as ComboBoxItem).Tag);

			tag.Amplifier = (byte)((PotionCustomLvlUpDown.Value ?? 1) - 1);
			tag.Duration = (int)((PotionDurationUpDown.Value ?? 0.0f) * 20.0f);
			tag.Ambient = (PotionAmbientCheck.IsChecked ?? false).ToByte();
			tag.ShowParticles = (PotionParticlesCheck.IsChecked ?? true).ToByte();

			PotionList.Items.Add(tag);

			PotionBaseStrBtn.IsEnabled = false;
			PotionBaseExtendedCheck.IsEnabled = false;

			GiveRefreshNBT();
		}
		private void PotionCustomRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			PotionList.Items.Remove(PotionList.SelectedItem);

			if (!PotionList.HasItems)
			{
				PotionBaseStrBtn.IsEnabled = true;
				PotionBaseExtendedCheck.IsEnabled = true;
			}

			GiveRefreshNBT();
		}
		#endregion

		#region book
		private void BookPagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (BookPagesList.SelectedIndex == -1)
			{
				return;
			}

			string page = (BookPagesList.SelectedItem as ListBoxItem).Tag as string;

			BookCurrentPageBox.Text = page;

			BookPageNumberBox.Text = (BookPagesList.SelectedIndex + 1).ToString() +
				" of " + BookPagesList.Items.Count;
		}

		private void BookCurrentPageBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (BookPagesList == null)
			{
				return;
			}

			ListBoxItem item = BookPagesList.SelectedItem as ListBoxItem;

			string start = BookCurrentPageBox.Text.Substring(0, Math.Min(40, BookCurrentPageBox.Text.Length));
			if (start != BookCurrentPageBox.Text)
			{
				start += "...";
			}
			item.Content = (BookPagesList.SelectedIndex + 1).ToString() + ": " + start;

			item.Tag = BookCurrentPageBox.Text;

			BookPagesList.SelectedItem = item;

			GiveRefreshNBT();
		}

		private void BookNewPageBtn_Click(object sender, RoutedEventArgs e)
		{
			ListBoxItem item = new ListBoxItem();
			item.Content = BookPagesList.Items.Count.ToString() + ":";
			item.Tag = "";

			BookPagesList.Items.Add(item);
			BookPagesList.SelectedItem = item;

			BookCurrentPageBox.Text = "";

			GiveRefreshNBT();

			BookCurrentPageBox.Focus();
		}
		private void BookDeletePageBtn_Click(object sender, RoutedEventArgs e)
		{
			BookPagesList.Items.Remove(BookPagesList.SelectedItem);

			BookPagesList.SelectedIndex = BookPagesList.Items.Count - 1;

			if (BookPagesList.Items.Count == 0)
			{
				BookNewPageBtn_Click(sender, e);
			}

			GiveRefreshNBT();
		}

		private void BookTitleBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			GiveRefreshNBT();
		}
		private void BookAuthorBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			GiveRefreshNBT();
		}
		private void BookGenerationCombo_SelectionChanged(object sender,
			SelectionChangedEventArgs e)
		{
			GiveRefreshNBT();
		}
		#endregion

		#region FwStar
		private void FwStarMainColorsAddBtn_Click(object sender, RoutedEventArgs e)
		{
			Color c = Color.FromRgb(FwStarColorCanvas.R,
				FwStarColorCanvas.G, FwStarColorCanvas.B);
			int color = Extras.rgbToInt(c.R, c.G, c.B);

			ListBoxItem i = new ListBoxItem();
			i.Content = Extras.TextColor(color.ToString(), c);
			i.Tag = color;

			FwStarMainColorsList.Items.Add(i);

			GiveRefreshNBT();
		}
		private void FwStarMainColorsRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			FwStarMainColorsList.Items.Remove(
				FwStarMainColorsList.SelectedItem);

			GiveRefreshNBT();
		}

		private void FwStarFadeColorsAddBtn_Click(object sender, RoutedEventArgs e)
		{
			Color c = Color.FromRgb(FwStarColorCanvas.R,
				FwStarColorCanvas.G, FwStarColorCanvas.B);
			int color = Extras.rgbToInt(c.R, c.G, c.B);

			ListBoxItem i = new ListBoxItem();
			i.Content = Extras.TextColor(color.ToString(), c);
			i.Tag = color;

			FwStarFadeColorsList.Items.Add(i);

			GiveRefreshNBT();
		}

		private void FwStarFadeColorsRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			FwStarFadeColorsList.Items.Remove(
				FwStarFadeColorsList.SelectedItem);

			GiveRefreshNBT();
		}

		private void FwStarColorCanvas_SelectedColorChanged(object sender,
			RoutedPropertyChangedEventArgs<Color> e)
		{
			int col = Extras.rgbToInt(e.NewValue.R, e.NewValue.G, e.NewValue.B);

			FwStarColorBox.Text = col.ToString();
		}

		private void FwStarShapeCombo_SelectionChanged(object sender,
			SelectionChangedEventArgs e)
		{
			GiveRefreshNBT();
		}

		private void FwStarChecks_Changed(object sender, RoutedEventArgs e)
		{
			GiveRefreshNBT();
		}
		#endregion

		#region FwRocket

		//FLAG
		bool fwIsLoading;

		private void EnableDisableSelectedRocket()
		{
			if ((!FwRocketExplosionsList.HasItems) ||
				FwRocketExplosionsList.SelectedIndex == -1)
			{
				FwRocketSelectedExplosionStack.IsEnabled = false;
				FwRocketExplosionDeleteBtn.IsEnabled = false;
			}
			else
			{
				FwRocketSelectedExplosionStack.IsEnabled = true;
				FwRocketExplosionDeleteBtn.IsEnabled = true;
			}
		}

		private void RefreshSelectedExplosion()
		{
			if (FwRocketExplosionsList == null ||
				fwIsLoading)
			{
				return;
			}

			FireworksStarDisplayTag e =
				FwRocketExplosionsList.SelectedItem
				as FireworksStarDisplayTag;

			FireworksStarDisplayTag old = e;

			e.Flicker = FwRocketFlickerCheck.IsChecked ?? false;
			e.Trail = FwRocketTrailCheck.IsChecked ?? false;

			e.Shape = (byte)FwRocketShapeCombo.SelectedIndex;

			if (FwRocketMainColorsList.HasItems)
			{
				e.Main = new List<Color>();
				foreach (ListBoxItem i in FwRocketMainColorsList.Items)
				{
					e.Main.Add((Color)i.Tag);
				}
			}
			else
			{
				e.Main = new List<Color>();
			}

			if (FwRocketFadeColorsList.HasItems)
			{
				e.Fade = new List<Color>();
				foreach (ListBoxItem i in FwRocketFadeColorsList.Items)
				{
					e.Fade.Add((Color)i.Tag);
				}
			}
			else
			{
				e.Fade = new List<Color>();
			}

			int index = FwRocketExplosionsList.SelectedIndex;
			FwRocketExplosionsList.Items.Remove(old);
			FwRocketExplosionsList.Items.Insert(index, e);
			FwRocketExplosionsList.SelectedItem = e;

			GiveRefreshNBT();
		}

		private void FwRocketLoadExplosion(FireworksStarDisplayTag splodey)
		{
			fwIsLoading = true;

			FwRocketFlickerCheck.IsChecked = splodey.Flicker;
			FwRocketTrailCheck.IsChecked = splodey.Trail;
			FwRocketShapeCombo.SelectedIndex = splodey.Shape;

			FwRocketMainColorsList.Items.Clear();
			if (splodey.Main.Count != 0)
			{
				foreach (Color c in splodey.Main)
				{
					ListBoxItem i = new ListBoxItem();
					i.Content = Extras.TextColor(Extras.rgbToInt(
						c.R, c.G, c.B).ToString(), c);
					i.Tag = c;

					FwRocketMainColorsList.Items.Add(i);
				}
			}

			FwRocketFadeColorsList.Items.Clear();
			if (splodey.Fade.Count != 0)
			{
				foreach (Color c in splodey.Fade)
				{
					ListBoxItem i = new ListBoxItem();
					i.Content = Extras.TextColor(Extras.rgbToInt(
						c.R, c.G, c.B).ToString(), c);
					i.Tag = c;

					FwRocketFadeColorsList.Items.Add(i);
				}
			}

			fwIsLoading = false;
		}

		private void FwRocketMainColorsAddBtn_Click(object sender, RoutedEventArgs e)
		{
			Color c = FwRocketColorPicker.SelectedColor;
			int color = Extras.rgbToInt(c.R, c.G, c.B);

			ListBoxItem i = new ListBoxItem();
			i.Content = Extras.TextColor(color.ToString(), c);
			i.Tag = c;

			FwRocketMainColorsList.Items.Add(i);

			RefreshSelectedExplosion();
		}
		private void FwRocketMainColorsRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			FwRocketMainColorsList.Items.Remove(
				FwRocketMainColorsList.SelectedItem);

			RefreshSelectedExplosion();
		}

		private void FwRocketFadeColorsAddBtn_Click(object sender, RoutedEventArgs e)
		{
			Color c = FwRocketColorPicker.SelectedColor;
			int color = Extras.rgbToInt(c.R, c.G, c.B);

			ListBoxItem i = new ListBoxItem();
			i.Content = Extras.TextColor(color.ToString(), c);
			i.Tag = c;

			FwRocketFadeColorsList.Items.Add(i);

			RefreshSelectedExplosion();
		}
		private void FwRocketFadeColorsRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			FwRocketFadeColorsList.Items.Remove(
				FwRocketFadeColorsList.SelectedItem);

			RefreshSelectedExplosion();
		}

		private void FwRocketExplosionNewBtn_Click(object sender, RoutedEventArgs e)
		{
			FireworksStarDisplayTag splodey = new FireworksStarDisplayTag();
			FwRocketExplosionsList.Items.Add(splodey);

			FwRocketLoadExplosion(splodey);

			FwRocketExplosionsList.SelectedItem = splodey;

			EnableDisableSelectedRocket();

			GiveRefreshNBT();
		}
		private void FwRocketExplosionDeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			FwRocketExplosionsList.Items.Remove(
				FwRocketExplosionsList.SelectedItem);

			EnableDisableSelectedRocket();

			if (FwRocketExplosionsList.HasItems)
			{
				FwRocketExplosionsList.SelectedIndex = 0;
			}
			else
			{
				FwRocketExplosionsList.SelectedIndex = -1;
			}

			GiveRefreshNBT();
		}

		private void FwRocketExplosionsList_SelectionChanged(object sender,
			SelectionChangedEventArgs e)
		{
			if (FwRocketExplosionsList.SelectedIndex == -1)
			{
				EnableDisableSelectedRocket();
				return;
			}

			FireworksStarDisplayTag splodey =
				FwRocketExplosionsList.SelectedItem
				as FireworksStarDisplayTag;

			if (splodey == null)
			{
				System.Diagnostics.Debugger.Log(3, "FwRocket",
					"Explosion List selection could not " +
					"be converted into FireworksStarDisplayTag.\n");
				return;
			}

			FwRocketLoadExplosion(splodey);

			EnableDisableSelectedRocket();
		}

		private void FwRocket_Check_Changed(object sender, RoutedEventArgs e)
		{
			RefreshSelectedExplosion();
		}

		private void FwRocketShapeCombo_SelectionChanged(object sender,
			SelectionChangedEventArgs e)
		{
			RefreshSelectedExplosion();
		}

		private void FwRocketFlightUpDown_ValueChanged(object sender,
			RoutedPropertyChangedEventArgs<object> e)
		{
			GiveRefreshNBT();
		}

		#endregion

		#region other
		private void RefreshSkullImg()
		{
			if (SkullPlayerNameBox.Text == "")
			{
				return;
			}

			SkullProgress.Visibility = System.Windows.Visibility.Visible;

			BitmapImage bmp = new BitmapImage(new Uri(
						 "http://s3.amazonaws.com/MinecraftSkins/" +
						 SkullPlayerNameBox.Text + ".png"));
			Int32Rect face = new Int32Rect(8, 8, 8, 8);
			Int32Rect top = new Int32Rect(40, 8, 8, 8);

			bmp.DownloadCompleted += (obj, e) =>
			{
				CroppedBitmap crop = new CroppedBitmap(bmp, face);
				SkullPreviewImg.Source = crop;

				CroppedBitmap topCrop = new CroppedBitmap(bmp, top);
				SkullTopImg.Source = topCrop;

				SkullProgress.Visibility = System.Windows.Visibility.Collapsed;
			};
		}

		private void SkullPlayerNameBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			RefreshSkullImg();

			GiveRefreshNBT();
		}

		private void MapScaleCheck_Unchecked(object sender, RoutedEventArgs e)
		{
			GiveRefreshNBT();
		}

		private void MapScaleCheck_Checked(object sender, RoutedEventArgs e)
		{
			GiveRefreshNBT();
		}

		private void MapIdUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (GiveSelectedItem == null)
			{
				return;
			}

			GiveSelectedItem.Metadata = (short)(MapIdUpDown.Value ?? 0);
			ItemMetaTxt.Text = GiveSelectedItem.Metadata.ToString();
		}
		#endregion

		private void GiveCommandOutputBox_MouseEnter(object sender, MouseEventArgs e)
		{
			GiveCommandOutputBox.Focus();
			GiveCommandOutputBox.SelectAll();
		}

		private void GiveCommandOutputBox_MouseLeave(object sender, MouseEventArgs e)
		{
			GiveCommandOutputBox.Select(0, 0);
		}

		private void ConfirmItemBtn_Click(object sender, RoutedEventArgs e)
		{
			isCancel = false;

			this.Close();
		}
	}
}
