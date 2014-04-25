using CommandGeneratorWPF.NBT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Utils;
using Xceed.Wpf;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Core;

namespace CommandGeneratorWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool isLoading = true;

		public MainWindow()
		{
			isLoading = true;

			#region /give

			GiveSelectedItem = InventoryItem.ItemDiamondSword();

			ResetItemSelector();
			ResetSelectorWindow();
			ResetCanDestroySelector();
			ResetCanPlaceOnSelector();

			GiveItemTag_tag = new ItemTagTag();

			#endregion /give

			#region /tellraw

			tellrawSelector = Extras.GenerateSelector(tellrawSelector_onClosed);
			tellrawHoverItemCreator = Extras.GenerateItemCreator(true,
				tellrawHoverItemCreator_onClosed);
			InitTellrawFields();

			#endregion /tellraw

			//////////////////////////////
			InitializeComponent();		//
			//////////////////////////////
			isLoading = false;

			#region /give

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

			#endregion /give

			#region /tellraw

			TellrawClickEventEnableCheck_Click(null, null);
			TellrawHoverEventEnableCheck_Click(null, null);
			TellrawTranslateEnableCheck_Click(null, null);
			TellrawScoreboardEnableCheck_Click(null, null);

			TellrawClickEventEnableCheck.ToolTip = tellrawEventTooltipWarning;
			TellrawHoverEventEnableCheck.ToolTip = tellrawEventTooltipWarning;

			TellrawPreviewScroll.Content = TextPreviewer.IncludeExtras(
				tellrawTextTag, TellrawTextItem_OnSelected);

			TellrawRefreshNBT();

			#endregion /tellraw

			RenderOptions.SetBitmapScalingMode(this,
				BitmapScalingMode.NearestNeighbor);
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			System.Diagnostics.Debugger.Log(0, "*", "Closing...\n");
			itemSelect.Close();
			selectorWindow.Close();
			Environment.Exit(0); //Manual exit as a workaround for bug
		}

		#region /give

		private const string _warningFunkyName = "Warning: " +
			"Using names in this command will not give the expected item.";

		private bool syncingMeta;

		private ItemSelectWindow itemSelect;
		private SelectorWindow selectorWindow;

		private ItemSelectWindow canDestroySelect;
		private ItemSelectWindow canPlaceOnSelect;

		public InventoryItem GiveSelectedItem
		{
			get
			{
				return _giveSelectedItem;
			}
			set
			{
				_giveSelectedItem = value;
				//System.Diagnostics.Debugger.Break(); // Do it. Now.
			}
		}

		private InventoryItem _giveSelectedItem;

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

			string cmd = "/give " + PlayerBox.Text + " " +
				GiveSelectedItem.ShortName + " " +
				ItemCountUpDown.Value.ToString() + " " +
				GiveSelectedItem.Metadata.ToString() + " ";

			string json = Extras.Serialize(GiveItemTag_tag);

			GiveCommandOutputBox.Text = cmd + json;
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
			syncingMeta = true;
			SelectItemBtn.DataContext = selected;
			GiveSelectedItem = selected;
			ItemMetaTxt.Text = selected.Metadata.ToString();
			SelectedItemIdTxt.Text = "(" + selected.ID.ToString() + ")";
			GiveEnableDisableNBT();
			syncingMeta = false;

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

		private void ResetSelectorWindow()
		{
			selectorWindow = new SelectorWindow();
			selectorWindow.Closed += selectorWindow_Closed;
		}

		private void selectorWindow_Closed(object sender, EventArgs e)
		{
			if (selectorWindow.Result != "")
			{
				PlayerBox.Text = selectorWindow.Result;
			}

			ResetSelectorWindow();
		}

		private void GiveRefreshNBT()
		{
			GiveTree.Items.Clear();

			ItemTagTag tag = GiveItemTag_tag;

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

			#endregion display

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

			#endregion general

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

			#endregion ench

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

			#endregion potion

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

			#endregion book

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

			#endregion FwStar

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

			#endregion FwRocket

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
							tag.map_is_scaling = MapScaleCheck.IsChecked.ToByte();
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

			#endregion other

			GiveItemTag_tag = tag;

			GiveTree.Items.Add(tag.ToTree());

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

			#endregion display

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
				if (!syncingMeta)
				{
					syncingMeta = true;
					GiveDamageUpDown.Value = GiveSelectedItem.Metadata;
					syncingMeta = false;
				}
				else
				{
					GiveDamageUpDown.Value = GiveSelectedItem.Metadata;
				}
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

			#endregion general

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

			#endregion ench

			#region potion

			if (item.IsPotion)
			{
				PotionTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				PotionTab.Visibility = System.Windows.Visibility.Collapsed;
			}

			#endregion potion

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

			#endregion book

			#region FwStar

			if (item.IsFireworksStar)
			{
				FwStarTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				FwStarTab.Visibility = System.Windows.Visibility.Collapsed;
			}

			#endregion FwStar

			#region FwRocket

			if (item.IsFireworksRocket)
			{
				FwRocketTab.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				FwRocketTab.Visibility = System.Windows.Visibility.Collapsed;
			}

			#endregion FwRocket

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

			#endregion other

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

		private void SelectorBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!selectorWindow.IsLoaded)
			{
				selectorWindow.Show();
			}

			selectorWindow.Activate();
		}

		private void PlayerBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			GiveRefreshCommand();
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

			GiveRefreshCommand();
		}

		private void ItemMetaTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!syncingMeta && GiveDamageUpDown != null)
			{
				syncingMeta = true;

				GiveDamageUpDown.Value = GiveSelectedItem.Metadata;
				PotionMetaBox.Text = GiveSelectedItem.Metadata.ToString();

				syncingMeta = false;

				GiveRefreshCommand();
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

		#endregion display

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

				GiveRefreshCommand();
			}
		}

		#endregion general

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

		#endregion ench

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

			byte strBit = ((PotionBaseStrBtn.Content as string) == "II").ToByte();
			int str = strBit;

			byte extBit = (PotionBaseExtendedCheck.IsChecked ?? false).ToByte();
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

			syncingMeta = true;
			ItemMetaTxt.Text = damage.ToString();
			GiveSelectedItem.Metadata = (short)damage;
			PotionMetaBox.Text = damage.ToString();
			GiveDamageUpDown.Value = damage;
			syncingMeta = false;

			GiveRefreshCommand();
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

			// All of these are only one strength through damage values: Fire res,
			// nite-vis, slowness, water br., invisible, water bottle
			if (b == 3 || b == 6 || b == 10 || b == 13 || b == 14 || b == 0)
			{
				PotionBaseStrBtn.IsEnabled = false;
				PotionBaseStrBtn.Content = "I";
			}
			else if (!PotionList.HasItems)
			{
				PotionBaseStrBtn.IsEnabled = true;
			}

			// Healing (5) and harming (6) cannot be extended through damage values. Water
			// bottles (0) have no effect to extend.
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

		#endregion potion

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

		#endregion book

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

		#endregion FwStar

		#region FwRocket

		//FLAG
		private bool fwIsLoading;

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

		#endregion FwRocket

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

		#endregion other

		private void GiveCommandOutputBox_MouseEnter(object sender, MouseEventArgs e)
		{
			GiveCommandOutputBox.Focus();
			GiveCommandOutputBox.SelectAll();
		}

		private void GiveCommandOutputBox_MouseLeave(object sender, MouseEventArgs e)
		{
			GiveCommandOutputBox.Select(0, 0);
		}

		private void GiveCommandOutputBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Clipboard.SetData(DataFormats.Text, GiveCommandOutputBox.Text);
		}

		#endregion /give

		#region /tellraw

		private SelectorWindow tellrawSelector;

		private TextRunTag tellrawTextTag;
		private int tellrawSelectedIndex;

		private bool tellrawIsLoading;

		private ItemFullTag tellrawSelectedHoverItem;

		// const string tellrawChatTooltip = "Commands must be prefixed with a slash, " +
		// "otherwise this will merely be entered as chat.";
		private const string tellrawEventTooltipWarning =
			"Warning: Events in first text will be inherited " +
			"by all child (non-first) texts.\n To prevent this, put the " +
			"event(s) in a second text, with blank text as the first.";

		private ItemCreationWindow tellrawHoverItemCreator;

		private void InitTellrawFields()
		{
			tellrawTextTag = new TextRunTag();
			tellrawTextTag.text = "Text";

			tellrawSelectedIndex = 0;
			tellrawIsLoading = false;

			tellrawSelectedHoverItem = new ItemFullTag(InventoryItem.BlockStorageDiamond());
		}

		private void TellrawRefreshNBT()
		{
			if (isLoading || tellrawIsLoading)
			{
				return;
			}

			if (tellrawSelectedIndex == 0)
			{
				if (TellrawTextBox.IsEnabled)
				{
					tellrawTextTag.text = TellrawTextBox.Text;
				}
				else
				{
					tellrawTextTag.text = "";
				}
				tellrawTextTag.color = (TextColor)TellrawTextColorCombo.SelectedIndex;

				#region formatting

				tellrawTextTag.bold = TellrawBoldToggle.IsChecked ?? false;
				tellrawTextTag.italic = TellrawItalicToggle.IsChecked ?? false;
				tellrawTextTag.underline = TellrawUnderlineToggle.IsChecked ?? false;
				tellrawTextTag.strikethrough = TellrawStrikethroughToggle.IsChecked ?? false;
				tellrawTextTag.obfuscated = TellrawObfuscatedToggle.IsChecked ?? false;

				#endregion formatting

				#region clickEvent

				if (TellrawClickEventGroup.IsEnabled)
				{
					tellrawTextTag.clickEvent = new TextRunTag.ClickEvent(
						(ClickCommand)TellrawClickActionCombo.SelectedIndex,
						TellrawClickActionBox.Text);
				}
				else
				{
					tellrawTextTag.clickEvent = null;
				}

				#endregion clickEvent

				#region hoverEvent

				if (TellrawHoverEventGroup.IsEnabled)
				{
					tellrawTextTag.hoverEvent = new TextRunTag.HoverEvent(
						(HoverCommand)TellrawHoverActionCombo.SelectedIndex,
						TellrawHoverActionTextBox.Text);

					tellrawTextTag.hoverEvent.shownItem =
						tellrawSelectedHoverItem;
				}
				else
				{
					tellrawTextTag.hoverEvent = null;
				}

				#endregion hoverEvent

				#region translate

				if (TellrawTranslateGroup.IsEnabled)
				{
					tellrawTextTag.translate = TellrawTranslateIdBox.Text;

					tellrawTextTag.with = new List<string>();
					foreach (string s in TellrawTranslateArgListBox.Items)
					{
						tellrawTextTag.with.Add(s);
					}

					if (tellrawTextTag.with.Count == 0)
					{
						tellrawTextTag.with = null;
					}
				}
				else
				{
					tellrawTextTag.translate = "";
					tellrawTextTag.with = null;
				}

				#endregion translate

				#region scoreboard

				if (TellrawScoreboardGroup.IsEnabled)
				{
					TextRunTag.TextScoreboard score = new TextRunTag.TextScoreboard();

					score.name = TellrawScoreboardPlayerBox.Text;
					score.objective = TellrawScoreboardObjBox.Text;

					tellrawTextTag.score = score;
				}
				else
				{
					tellrawTextTag.score = null;
				}

				#endregion scoreboard
			}
			else
			{
				if (TellrawTextBox.IsEnabled)
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].text =
						TellrawTextBox.Text;
				}
				else
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].text = "";
				}
				tellrawTextTag.extra[tellrawSelectedIndex - 1].color =
					(TextColor)TellrawTextColorCombo.SelectedIndex;

				#region formatting

				tellrawTextTag.extra[tellrawSelectedIndex - 1].bold =
					TellrawBoldToggle.IsChecked ?? false;
				tellrawTextTag.extra[tellrawSelectedIndex - 1].italic =
					TellrawItalicToggle.IsChecked ?? false;
				tellrawTextTag.extra[tellrawSelectedIndex - 1].underline =
					TellrawUnderlineToggle.IsChecked ?? false;
				tellrawTextTag.extra[tellrawSelectedIndex - 1].strikethrough =
					TellrawStrikethroughToggle.IsChecked ?? false;
				tellrawTextTag.extra[tellrawSelectedIndex - 1].obfuscated =
					TellrawObfuscatedToggle.IsChecked ?? false;

				#endregion formatting

				#region clickEvent

				if (TellrawClickEventGroup.IsEnabled)
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].clickEvent =
						new TextRunTag.ClickEvent((ClickCommand)
						TellrawClickActionCombo.SelectedIndex,
						TellrawClickActionBox.Text);
				}
				else
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].clickEvent = null;
				}

				#endregion clickEvent

				#region hoverEvent

				if (TellrawHoverEventGroup.IsEnabled)
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].hoverEvent =
						new TextRunTag.HoverEvent((HoverCommand)
						TellrawHoverActionCombo.SelectedIndex,
						TellrawHoverActionTextBox.Text);

					tellrawTextTag.extra[tellrawSelectedIndex - 1].
						hoverEvent.shownItem = tellrawSelectedHoverItem;
				}
				else
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].hoverEvent = null;
				}

				#endregion hoverEvent

				#region translate

				if (TellrawTranslateGroup.IsEnabled)
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].translate =
						TellrawTranslateIdBox.Text;

					tellrawTextTag.extra[tellrawSelectedIndex - 1].with =
						new List<string>();
					foreach (string s in TellrawTranslateArgListBox.Items)
					{
						tellrawTextTag.extra[tellrawSelectedIndex - 1].with.Add(s);
					}

					if (tellrawTextTag.extra[tellrawSelectedIndex - 1].with.Count == 0)
					{
						tellrawTextTag.extra[tellrawSelectedIndex - 1].with = null;
					}
				}
				else
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].translate = "";
					tellrawTextTag.extra[tellrawSelectedIndex - 1].with = null;
				}

				#endregion translate

				#region scoreboard

				if (TellrawScoreboardGroup.IsEnabled)
				{
					TextRunTag.TextScoreboard score = new TextRunTag.TextScoreboard();

					score.name = TellrawScoreboardPlayerBox.Text;
					score.objective = TellrawScoreboardObjBox.Text;

					tellrawTextTag.extra[tellrawSelectedIndex - 1].score = score;
				}
				else
				{
					tellrawTextTag.extra[tellrawSelectedIndex - 1].score = null;
				}

				#endregion scoreboard
			}

			TellrawPreviewScroll.Content = TextPreviewer.IncludeExtras(
				tellrawTextTag, TellrawTextItem_OnSelected);

			TellrawTree.Items.Clear();
			TellrawTree.Items.Add(tellrawTextTag.ToTreeView());

			TellrawRefreshCmd();
		}

		private void TellrawRefreshCmd()
		{
			string command = "/tellraw " +
				TellrawPlayerBox.Text;

			string nbt = tellrawTextTag.ToJSON();

			TellrawCommandOutputBox.Text = command + " " + nbt;
		}

		// Load from NBT
		private void TellrawLoadFromNBT()
		{
			tellrawIsLoading = true;

			TextRunTag tag;
			if (tellrawSelectedIndex == 0)
			{
				tag = tellrawTextTag;
			}
			else
			{
				tag = tellrawTextTag.extra[tellrawSelectedIndex - 1];
			}

			TellrawTextBox.Text = tag.text;
			TellrawTextColorCombo.SelectedIndex = (int)tag.color;

			TellrawBoldToggle.IsChecked = tag.bold;
			TellrawItalicToggle.IsChecked = tag.italic;
			TellrawUnderlineToggle.IsChecked = tag.italic;
			TellrawStrikethroughToggle.IsChecked = tag.strikethrough;
			TellrawObfuscatedToggle.IsChecked = tag.strikethrough;

			#region clickEvent

			if (tag.clickEvent != null)
			{
				TellrawClickEventEnableCheck.IsChecked = true;
				TellrawClickEventEnableCheck_Click(null, null);

				TellrawClickActionCombo.SelectedIndex = (int)tag.clickEvent.action;
				TellrawClickActionBox.Text = tag.clickEvent.value;
			}
			else
			{
				TellrawClickEventEnableCheck.IsChecked = false;
				TellrawClickEventEnableCheck_Click(null, null);

				TellrawClickActionCombo.SelectedIndex = 0;
				TellrawClickActionBox.Text = "";
			}

			#endregion clickEvent

			#region hoverEvent

			if (tag.hoverEvent != null)
			{
				TellrawHoverEventEnableCheck.IsChecked = true;
				TellrawHoverEventEnableCheck_Click(null, null);

				TellrawHoverActionCombo.SelectedIndex = (int)tag.hoverEvent.action;
				TellrawHoverActionTextBox.Text = tag.hoverEvent.value;
			}
			else
			{
				TellrawHoverEventEnableCheck.IsChecked = false;
				TellrawHoverEventEnableCheck_Click(null, null);

				TellrawHoverActionCombo.SelectedIndex = 0;
				TellrawHoverActionTextBox.Text = "";
			}

			#endregion hoverEvent

			#region translate

			if (tag.translate != "")
			{
				TellrawTranslateEnableCheck.IsChecked = true;
				TellrawTranslateEnableCheck_Click("hello", null);

				TellrawTranslateIdBox.Text = tag.translate;

				TellrawTranslateArgListBox.Items.Clear();
				foreach (string s in tag.with)
				{
					TellrawTranslateArgListBox.Items.Add(s);
				}
			}

			#endregion translate

			#region scoreboard

			if (tag.score != null)
			{
				TellrawScoreboardEnableCheck.IsChecked = true;
				TellrawScoreboardEnableCheck_Click("hello", null);

				TellrawScoreboardObjBox.Text = tag.score.objective;
				TellrawScoreboardPlayerBox.Text = tag.score.name;
			}

			#endregion scoreboard

			tellrawIsLoading = false;
		}

		private void tellrawSelector_onClosed(object sender, EventArgs e)
		{
			if (tellrawSelector.Result != "")
			{
				TellrawPlayerBox.Text = tellrawSelector.Result;
			}

			tellrawSelector = Extras.GenerateSelector(tellrawSelector_onClosed);
		}

		private void TellrawSelectorBtn_Click(object sender, RoutedEventArgs e)
		{
			tellrawSelector.Show();
		}

		private void TellrawViewerBtn_Text_Click(object sender, RoutedEventArgs e)
		{
			TellrawInsertViewer(TellrawTextBox);
		}

		private void tellrawHoverItemCreator_onClosed(object sender, EventArgs e)
		{
			if (!tellrawHoverItemCreator.isCancel)
			{
				tellrawSelectedHoverItem = tellrawHoverItemCreator.SelectedItemStack;

				TellrawHoverActionTextBox.Text = tellrawHoverItemCreator.SelectedItemString;
			}

			tellrawHoverItemCreator = Extras.GenerateItemCreator(true, tellrawHoverItemCreator_onClosed);
		}

		private void TellrawInsertViewer(TextBox textBox)
		{
			if (textBox.IsFocused)
			{
				if (textBox.SelectedText != "")
				{
					textBox.SelectedText = "*";
				}
				else
				{
					textBox.Text.Insert(textBox.SelectionStart, "*");
				}
			}
			else
			{
				textBox.Text += "*";
				textBox.Focus();
			}
		}

		private void TellrawBoldToggle_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.bold = TellrawBoldToggle.IsChecked ?? false;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].bold =
					TellrawBoldToggle.IsChecked ?? false;
			}

			TellrawRefreshNBT();
		}

		private void TellrawItalicToggle_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.italic = TellrawItalicToggle.IsChecked ?? false;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].italic =
					TellrawItalicToggle.IsChecked ?? false;
			}

			TellrawRefreshNBT();
		}

		private void TellrawUnderlineToggle_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.underline = TellrawUnderlineToggle.IsChecked ?? false;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].underline =
					TellrawUnderlineToggle.IsChecked ?? false;
			}

			TellrawRefreshNBT();
		}

		private void TellrawStrikethroughToggle_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.strikethrough = TellrawStrikethroughToggle.IsChecked ?? false;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].strikethrough =
					TellrawStrikethroughToggle.IsChecked ?? false;
			}

			TellrawRefreshNBT();
		}

		private void TellrawObfuscatedToggle_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.obfuscated = TellrawObfuscatedToggle.IsChecked ?? false;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].obfuscated =
					TellrawObfuscatedToggle.IsChecked ?? false;
			}

			TellrawRefreshNBT();
		}

		private void TellrawTextColorCombo_SelectionChanged(
			object sender, SelectionChangedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.color = (TextColor)TellrawTextColorCombo.SelectedIndex;

				if (tellrawTextTag.color == TextColor.reset)
				{
					TellrawBoldToggle.IsEnabled = false;
					TellrawItalicToggle.IsEnabled = false;
					TellrawUnderlineToggle.IsEnabled = false;
					TellrawStrikethroughToggle.IsEnabled = false;
					TellrawObfuscatedToggle.IsEnabled = false;
				}
				else
				{
					TellrawBoldToggle.IsEnabled = true;
					TellrawItalicToggle.IsEnabled = true;
					TellrawUnderlineToggle.IsEnabled = true;
					TellrawStrikethroughToggle.IsEnabled = true;
					TellrawObfuscatedToggle.IsEnabled = true;
				}
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].color =
					(TextColor)TellrawTextColorCombo.SelectedIndex;

				if (tellrawTextTag.extra[tellrawSelectedIndex - 1].color == TextColor.reset)
				{
					TellrawBoldToggle.IsEnabled = false;
					TellrawItalicToggle.IsEnabled = false;
					TellrawUnderlineToggle.IsEnabled = false;
					TellrawStrikethroughToggle.IsEnabled = false;
					TellrawObfuscatedToggle.IsEnabled = false;
				}
				else
				{
					TellrawBoldToggle.IsEnabled = true;
					TellrawItalicToggle.IsEnabled = true;
					TellrawUnderlineToggle.IsEnabled = true;
					TellrawStrikethroughToggle.IsEnabled = true;
					TellrawObfuscatedToggle.IsEnabled = true;
				}
			}

			TellrawRefreshNBT();
		}

		private void TellrawClickEventEnableCheck_Click(object sender, RoutedEventArgs e)
		{
			TellrawClickEventGroup.IsEnabled =
				TellrawClickEventEnableCheck.IsChecked ?? false;

			TellrawClickEventGroup.Visibility =
				Extras.BoolToVisibility(TellrawClickEventEnableCheck.IsChecked);

			TellrawRefreshNBT();
		}

		private void TellrawHoverEventEnableCheck_Click(object sender, RoutedEventArgs e)
		{
			TellrawHoverEventGroup.IsEnabled =
				TellrawHoverEventEnableCheck.IsChecked ?? false;

			TellrawHoverEventGroup.Visibility =
				Extras.BoolToVisibility(TellrawHoverEventEnableCheck.IsChecked);

			TellrawRefreshNBT();
		}

		private void TellrawTranslateEnableCheck_Click(object sender, RoutedEventArgs e)
		{
			TellrawTranslateGroup.IsEnabled =
				TellrawTranslateEnableCheck.IsChecked ?? false;

			TellrawTranslateGroup.Visibility =
				Extras.BoolToVisibility(TellrawTranslateEnableCheck.IsChecked);

			if (sender != null) // To prevent recursion -> StackOverflowException
			{
				TellrawScoreboardEnableCheck_Click(null, null);

				TellrawTextBox.IsEnabled = !(TellrawTranslateEnableCheck.IsChecked ?? false);
				TellrawViewerBtn_Text.IsEnabled = !(TellrawTranslateEnableCheck.IsChecked ?? false);

				TellrawScoreboardEnableCheck.IsEnabled = !(TellrawTranslateEnableCheck.IsChecked ?? false);
			}

			TellrawRefreshNBT();
		}

		private void TellrawScoreboardEnableCheck_Click(object sender, RoutedEventArgs e)
		{
			TellrawScoreboardGroup.IsEnabled =
				TellrawScoreboardEnableCheck.IsChecked ?? false;

			TellrawScoreboardGroup.Visibility =
				Extras.BoolToVisibility(TellrawScoreboardEnableCheck.IsChecked);

			if (sender != null) // To prevent recursion, and thus a StackOverflowException
			{
				TellrawTranslateEnableCheck_Click(null, null);

				TellrawTextBox.IsEnabled = !(TellrawScoreboardEnableCheck.IsChecked ?? false);
				TellrawViewerBtn_Text.IsEnabled = !(TellrawScoreboardEnableCheck.IsChecked ?? false);

				TellrawTranslateEnableCheck.IsEnabled = !(TellrawScoreboardEnableCheck.IsChecked ?? false);
			}

			TellrawRefreshNBT();
		}

		private void TellrawClickActionCombo_SelectionChanged(
			object sender, SelectionChangedEventArgs e)
		{
			if (isLoading)
			{
				return;
			}

			switch (TellrawClickActionCombo.SelectedIndex)
			{
			case 0:
				TellrawClickActionValueHeader.Content = "Command / Chat:";
				break;

			case 1:
				TellrawClickActionValueHeader.Content = "Command / Chat:";
				break;

			case 2:
				TellrawClickActionValueHeader.Content = "URL:";
				break;

			default:
				TellrawClickActionValueHeader.Content = "Something...";
				break;
			}

			TellrawRefreshNBT();
		}

		private void TellrawHoverActionCombo_SelectionChanged(
			object sender, SelectionChangedEventArgs e)
		{
			if (isLoading)
			{
				return;
			}

			switch ((HoverCommand)TellrawHoverActionCombo.SelectedIndex)
			{
			case HoverCommand.show_text:
				TellrawHoverActionValueHeader.Visibility =
					System.Windows.Visibility.Visible;
				TellrawHoverActionValueHeader.Content = "Text:";

				TellrawHoverActionItemMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionEntityMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionTextBox.IsReadOnly = false;
				TellrawHoverActionTextBox.Text = "Hello!";
				break;

			case HoverCommand.show_item:
				TellrawHoverActionValueHeader.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionItemMakerBtn.Visibility =
					System.Windows.Visibility.Visible;

				TellrawHoverActionEntityMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionTextBox.IsReadOnly = true;
				TellrawHoverActionTextBox.Text = "{id:\"diamond_block\"}";
				break;

			case HoverCommand.show_achievement:
				TellrawHoverActionValueHeader.Visibility =
					System.Windows.Visibility.Visible;
				TellrawHoverActionValueHeader.Content = "Achievement:";

				TellrawHoverActionItemMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionEntityMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionTextBox.IsReadOnly = false;
				TellrawHoverActionTextBox.Text = "achievement.openInventory";
				break;

			case HoverCommand.show_entity:
				TellrawHoverActionValueHeader.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionItemMakerBtn.Visibility =
					System.Windows.Visibility.Collapsed;

				TellrawHoverActionEntityMakerBtn.Visibility =
					System.Windows.Visibility.Visible;

				TellrawHoverActionTextBox.IsReadOnly = true;
				TellrawHoverActionTextBox.Text = "{id:50}";
				break;

			default:
				System.Diagnostics.Debugger.Log(1, "",
					"Invalid HoverCommand Selection\n");

				throw new ArgumentOutOfRangeException();
			}

			TellrawRefreshNBT();
		}

		private void TellrawTextAddBtn_Click(object sender, RoutedEventArgs e)
		{
			if (tellrawTextTag.extra == null)
			{
				tellrawTextTag.extra = new List<TextRunTag>();
			}

			tellrawTextTag.extra.Add(tellrawTextTag.DuplicateInherited());
			tellrawTextTag.extra.Last().text = "new";
			tellrawSelectedIndex = tellrawTextTag.extra.Count;

			TellrawRefreshNBT();
		}

		private void TellrawTextRemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			// if (TellrawPreviewPanel.Children.Count != 0) {
			// TellrawPreviewPanel.Children.RemoveAt(tellrawSelectedIndex);
			// tellrawSelectedIndex = Math.Max(TellrawPreviewPanel.Children.Count - 1, 0);
			// } else { tellrawSelectedIndex = 0; }

			if (tellrawSelectedIndex == 0)
			{
				TextRunTag buffer = tellrawTextTag.extra[0];
				List<TextRunTag> extras = tellrawTextTag.extra;
				extras.RemoveAt(0);

				tellrawTextTag = buffer;
				tellrawTextTag.extra = extras;
			}
			else
			{
				tellrawTextTag.extra.RemoveAt(tellrawSelectedIndex - 1);
			}

			if (tellrawTextTag.extra.Count == 0)
			{
				TellrawTextRemoveBtn.IsEnabled = false;
			}
			else
			{
				TellrawTextRemoveBtn.IsEnabled = true;
			}

			TellrawLoadFromNBT();
			TellrawRefreshNBT();
		}

		/// <summary>
		/// Selection event that loads text run data from clicked TextBlock
		/// </summary>
		/// <param name="sender">Sending TextBlock</param>
		/// <param name="e">Junk</param>
		private void TellrawTextItem_OnSelected(object sender, EventArgs e)
		{
			TextBlock block = sender as TextBlock;

			if (block == null)
			{
				System.Diagnostics.Debugger.Log(0, "ERROR",
					"Selected object could not be cast as TextBlock.\n");
				return;
			}

			int spot = (int)block.Tag;
			tellrawSelectedIndex = Math.Max(spot, 0);

			// foreach (TextBlock t in TellrawPreviewPanel.Children) { t.Background = new
			// SolidColorBrush(Colors.Transparent); } block.Background = new SolidColorBrush(Colors.DarkBlue);

			if (tellrawSelectedIndex == 0)
			{
				TellrawClickEventEnableCheck.ToolTip = tellrawEventTooltipWarning;
				TellrawHoverEventEnableCheck.ToolTip = tellrawEventTooltipWarning;
			}
			else
			{
				TellrawClickEventEnableCheck.ToolTip = null;
				TellrawHoverEventEnableCheck.ToolTip = null;
			}

			TellrawLoadFromNBT();
		}

		private void TellrawTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (tellrawSelectedIndex == 0)
			{
				tellrawTextTag.text = TellrawTextBox.Text;
			}
			else
			{
				tellrawTextTag.extra[tellrawSelectedIndex - 1].text = TellrawTextBox.Text;
			}

			TellrawRefreshNBT();
		}

		private void TellrawClickActionBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private void TellrawHoverActionItemMakerBtn_Click(object sender, RoutedEventArgs e)
		{
			tellrawHoverItemCreator.Show();
		}

		private void TellrawHoverActionTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private bool isLoadingTellrawTranslateArg = false;

		private void TellrawTranslateIdBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private void TellrawTranslateArgBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TellrawTranslateArgListBox.SelectedIndex == -1 ||
				isLoadingTellrawTranslateArg)
			{
				return;
			}

			int index = TellrawTranslateArgListBox.SelectedIndex;
			isLoadingTellrawTranslateArg = true;
			TellrawTranslateArgListBox.Items.RemoveAt(index);
			TellrawTranslateArgListBox.Items.Insert(index, TellrawTranslateArgBox.Text);
			TellrawTranslateArgListBox.SelectedIndex = index;
			isLoadingTellrawTranslateArg = false;

			TellrawRefreshNBT();
		}

		private void TellrawTranslateArgAddBox_Click(object sender, RoutedEventArgs e)
		{
			isLoadingTellrawTranslateArg = true;
			TellrawTranslateArgListBox.Items.Add("arg");
			TellrawTranslateArgListBox.SelectedIndex =
				TellrawTranslateArgListBox.Items.Count - 1;
			isLoadingTellrawTranslateArg = false;

			TellrawTranslateArgListBox_SelectionChanged(null, null);

			TellrawRefreshNBT();
		}

		private void TellrawTranslateArgRemoveBox_Click(object sender, RoutedEventArgs e)
		{
			if (TellrawTranslateArgListBox.SelectedIndex == -1)
			{
				return;
			}

			TellrawTranslateArgListBox.Items.RemoveAt(
				TellrawTranslateArgListBox.SelectedIndex);

			TellrawTranslateArgListBox.SelectedIndex =
				TellrawTranslateArgListBox.Items.Count - 1;

			TellrawRefreshNBT();
		}

		private void TellrawTranslateArgListBox_SelectionChanged(
			object sender, SelectionChangedEventArgs e)
		{
			if (isLoading || isLoadingTellrawTranslateArg)
			{
				return;
			}

			if (TellrawTranslateArgListBox.SelectedIndex == -1)
			{
				TellrawTranslateArgBox.Text = "";
				TellrawTranslateArgRemoveBox.IsEnabled = false;
				TellrawTranslateArgBox.IsEnabled = false;
				return;
			}

			isLoadingTellrawTranslateArg = true;
			TellrawTranslateArgBox.Text = TellrawTranslateArgListBox.SelectedItem as string;
			TellrawTranslateArgBox.IsEnabled = true;
			isLoadingTellrawTranslateArg = false;
			TellrawTranslateArgRemoveBox.IsEnabled = true;
		}

		private void TellrawScoreboardPlayerBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private void TellrawScoreboardObjBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private void TellrawScoreboardViewerBtn_Click(object sender, RoutedEventArgs e)
		{
			TellrawScoreboardPlayerBox.Text = "*";
		}

		private void TellrawCommandOutputBox_MouseEnter(object sender, MouseEventArgs e)
		{
			if (this.IsActive)
			{
				TellrawCommandOutputBox.Focus();
				TellrawCommandOutputBox.SelectAll();
			}
		}

		private void TellrawCommandOutputBox_MouseLeave(object sender, MouseEventArgs e)
		{
			if (this.IsActive)
			{
				TellrawCommandOutputBox.Select(0, 0);
			}
		}

		private void TellrawPlayerBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TellrawRefreshNBT();
		}

		private void TellrawCommandOutputBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Clipboard.SetData(DataFormats.Text, TellrawCommandOutputBox.Text);
		}

		#endregion /tellraw
	}

	internal class FireworksStarDisplayTag
	{
		public bool Flicker;
		public bool Trail;
		public byte Shape;
		public List<Color> Main;
		public List<Color> Fade;

		public FireworksStarDisplayTag()
		{
			Flicker = false;
			Trail = false;
			Shape = 0;
			Main = new List<Color>();
			Fade = new List<Color>();
		}

		public override string ToString()
		{
			return ToTag().ToString();
		}

		public ItemTagTag.FireworksStarTag ToTag()
		{
			ItemTagTag.FireworksStarTag tag = new ItemTagTag.FireworksStarTag();

			tag.Flicker = Flicker.ToByte();
			tag.Trail = Trail.ToByte();
			tag.Type = Shape;

			if (Main.Count != 0)
			{
				tag.Colors = new List<int>();
				foreach (Color c in Main)
				{
					tag.Colors.Add(Extras.rgbToInt(c.R, c.G, c.B));
				}
			}
			else
			{
				tag.Colors = null;
			}

			if (Fade.Count != 0)
			{
				tag.FadeColors = new List<int>();
				foreach (Color c in Fade)
				{
					tag.FadeColors.Add(Extras.rgbToInt(c.R, c.G, c.B));
				}
			}
			else
			{
				tag.FadeColors = null;
			}

			return tag;
		}
	}
}