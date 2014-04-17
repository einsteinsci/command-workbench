using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public enum RockType : byte
	{
		Stone = 0,
		Granite,
		GranitePolished,
		Diorite,
		DioritePolished,
		Andesite,
		AndesitePolished
	}

	public partial class Block
	{
		public int ID
		{ get; set; }

		public string ShortName
		{ get; set; }

		public string OfficialName
		{
			get
			{
				return "minecraft:" + ShortName;
			}
		}

		public byte MetaData
		{ get; set; }

		public string ImageSource
		{ get; set; }

		public bool Technical
		{ get; private set; }

		public string FriendlyName
		{ get; private set; }

		#region dependent properties

		#endregion

		public Block(int id, string shortName, string friendlyName, string imgSource)
		{
			ID = id;
			ShortName = shortName;
			MetaData = 0;

			Technical = false;
			ImageSource = "assets/blocks/Grid_" + imgSource + ".png";
			FriendlyName = friendlyName;
		}
		public Block(int id, string shortName, string friendlyName, byte meta, string imgSource)
		{
			ID = id;
			ShortName = shortName;
			MetaData = meta;

			Technical = false;
			ImageSource = "assets/blocks/Grid_" + imgSource + ".png";
			FriendlyName = friendlyName;
		}
		public Block(int id, string shortName, string friendlyName, byte meta, bool tech, string imgSource)
		{
			ID = id;
			ShortName = shortName;
			MetaData = meta;

			Technical = tech;

			if (Technical)
			{
				ImageSource = "assets/blocks/technical/" + imgSource + ".png";
			}
			else
			{
				ImageSource = "assets/blocks/Grid_" + imgSource + ".png";
			}
			FriendlyName = friendlyName;
		}
	}
}
