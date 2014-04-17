using CommandGeneratorWPF.NBT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using CommandGeneratorWPF;

namespace CommandGeneratorWPF
{
	/// <summary>
	/// Interaction logic for ItemSelectWindow.xaml
	/// </summary>
	public partial class ItemSelectWindow : Window
	{
		public InventoryItem CurrentSelection
		{ get; set; }

		public ObservableCollection<InventoryItem> Everything
		{ get; private set; }
		public ObservableCollection<InventoryItem> EverythingNoTech
		{ get; private set; }

		public ObservableCollection<InventoryItem> BlocksAll
		{ get; private set; }
		public ObservableCollection<InventoryItem> BlocksNoTech
		{ get; private set; }
		public ObservableCollection<InventoryItem> BlocksNoTechID
		{ get; private set; }
		public ObservableCollection<InventoryItem> BlocksAllID
		{ get; private set; }

		public ObservableCollection<InventoryItem> ItemsAll
		{ get; private set; }

		public ItemSelectWindow()
		{
			InitializeFull(InventoryItem.BlockStone());
		}
		public ItemSelectWindow(bool blockTargeting)
		{
			InitializeFull(InventoryItem.BlockStone());
			if (blockTargeting)
			{
				ExclusionCombo.SelectedIndex = 6;
				ExclusionCombo.IsEnabled = false;
			}
		}
		public ItemSelectWindow(InventoryItem start)
		{
			InitializeFull(start);
		}

		private void InitializeFull(InventoryItem item)
		{
			CurrentSelection = item;

			Everything = InventoryItem.ListALLTheThings();
			EverythingNoTech = InventoryItem.ListAllNonTech();
			BlocksAll = InventoryItem.ListBlocksAll();
			BlocksNoTech = InventoryItem.ListBlocksNonTechnical_AllVariants();
			BlocksNoTechID = InventoryItem.ListBlocksNonTechnical_IdOnly();
			BlocksAllID = InventoryItem.ListBlocksAllID();
			ItemsAll = InventoryItem.ListItemsAll();

			InitializeComponent();

			foreach (InventoryItem i in EverythingNoTech)
			{
				Stuff.Items.Add(i);
			}

			RenderOptions.SetBitmapScalingMode(Stuff,
				BitmapScalingMode.NearestNeighbor);
		}

		private void SwitchDataSources(ObservableCollection<InventoryItem> list)
		{
			Stuff.Items.Clear();
			foreach (InventoryItem item in list)
			{
				Stuff.Items.Add(item);
			}
		}

		private void Stuff_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Stuff.SelectedIndex != -1)
			{
				InventoryItem sel = Stuff.SelectedItem as InventoryItem;
				if (sel != null)
				{
					CurrentSelection = sel;
				}
			}
		}

		private void ExclusionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Stuff == null)
			{
				return;
			}

			string tag = (ExclusionCombo.SelectedItem as ComboBoxItem).Tag.ToString();

			switch (tag)
			{
			case "everything":
				SwitchDataSources(Everything);
				break;
			case "everythingNoTech":
				SwitchDataSources(EverythingNoTech);
				break;
			case "itemsAll":
				SwitchDataSources(ItemsAll);
				break;
			case "blocksAll":
				SwitchDataSources(BlocksAll);
				break;
			case "blocksNoTech":
				SwitchDataSources(BlocksNoTech);
				break;
			case "blocksID":
				SwitchDataSources(BlocksNoTechID);
				break;
			case "blocksAllID":
				SwitchDataSources(BlocksAllID);
				break;
			default:
				SwitchDataSources(Everything);
				break;
			}
		}

		private void ChooseBtn_Click(object sender, RoutedEventArgs e)
		{
			Stuff_SelectionChanged(sender, null);

			this.Close();
		}
	}
}
