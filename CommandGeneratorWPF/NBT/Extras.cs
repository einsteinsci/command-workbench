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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CommandGeneratorWPF.NBT
{
	public static class Extras
	{
		public static Dictionary<TKey, TValue> DictionaryFromArrays
			<TKey, TValue>(TKey[] keys, TValue[] values)
		{
			int count = Math.Min(keys.GetLength(0), values.GetLength(0));
			Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

			for (int i = 0; i < count; i++)
			{
				result.Add(keys[i], values[i]);
			}

			return result;
		}

		public static int rgbToInt(int r, int g, int b)
		{
			return (r << 16) + (g << 8) + b;
		}

		public static int potionData(ushort effectID, byte II, 
			byte extended, byte splash)
		{
			int props = 0;

			if (splash != 0)
			{
				props += 16384 + 8192;
			}
			if (extended != 0)
			{
				props += 64;
			}

			return props + effectID;
		}

		public static StackPanel TextImg(string header, 
			string imageSource)
		{
			StackPanel stack = new StackPanel();
			stack.Orientation = Orientation.Horizontal;
			stack.Margin = new Thickness(2.0);

			TextBlock text = new TextBlock();
			text.Text = header;
			text.Margin = new Thickness(5, 0, 5, 0);

			bool imageOK = true;
			Image image = new Image();
			try
			{
				image.Source = new BitmapImage(new Uri(
					"pack://application:,,/assets/ui/" + imageSource));
			}
			catch
			{
				imageOK = false;
			}
			
			if (imageOK)
			{
				stack.Children.Add(image);
			}

			stack.Children.Add(text);

			return stack;
		}
		public static StackPanel TextColor(string text, Color color)
		{
			StackPanel stack = new StackPanel();
			stack.Orientation = Orientation.Horizontal;
			stack.Margin = new Thickness(0);

			TextBlock t = new TextBlock();
			t.Text = text;
			t.Margin = new Thickness(5, 0, 5, 0);
			t.VerticalAlignment = VerticalAlignment.Center;

			Rectangle rect = new Rectangle();
			rect.Fill = new SolidColorBrush(color);
			rect.Width = 12;
			rect.Height = 12;
			rect.VerticalAlignment = VerticalAlignment.Center;

			stack.Children.Add(rect);
			stack.Children.Add(t);

			return stack;
		}

		public static void AddRange<T>(this ObservableCollection<T> o, 
			IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				o.Add(item);
			}
		}

		public static void Sort<T>(this ObservableCollection<T> o,
			Comparison<T> comparer)
		{
			List<T> list = o.ToList<T>();
			list.Sort(comparer);

			o.Clear();
			o.AddRange(list);
		}

		public static ComboBoxItem CbItemFromEnchTag(
			ItemTagTag.EnchantmentTag enchTag)
		{
			ComboBoxItem cbItem = new ComboBoxItem();

			cbItem.Content = ItemTagTag.EnchantmentFriendlyNames()[enchTag.id];
			cbItem.Tag = enchTag.id;

			return cbItem;
		}

		public static string Serialize(object tag)
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
			serializer.NullValueHandling = NullValueHandling.Ignore;

			StringWriter stringWriter = new StringWriter();
			JsonTextWriter writer = new JsonTextWriter(stringWriter);
			writer.Formatting = Formatting.None;
			writer.QuoteName = false;

			serializer.Serialize(writer, tag);
			writer.Close();
			string json = stringWriter.ToString();

			if (json == "{}")
			{
				json = "";
			}

			return json;
		}

		public static string IntToRoman(int number)
		{
			string output = "";

			if (number > 1000 || number <= 0)
			{
				return number.ToString();
			}
			else if (number == 1000)
			{
				return "M";
			}

			int hundreds = (int)((float)number / 100.0f);
			int tens = (int)((float)number / 10.0f);
			tens = tens - 10 * hundreds;
			int ones = number - ((100 * hundreds) + (10 * tens));

			// DEBUG
			//output = number.ToString() + " --> 100s: " + hundreds.ToString()
			//	+ " | 10s: " + tens.ToString() + " | 1s: " + ones.ToString();
			string rHundreds = "";
			for (int i = 0; i < hundreds; i++)
			{
				rHundreds += "C";
			}
			rHundreds = rHundreds.Replace("CCCCC", "D");
			rHundreds = rHundreds.Replace("DCCCC", "CM");
			rHundreds = rHundreds.Replace("CCCC", "CD");

			string rTens = "";
			for (int i = 0; i < tens; i++)
			{
				rTens += "X";
			}
			rTens = rTens.Replace("XXXXX", "L");
			rTens = rTens.Replace("LXXXX", "XC");
			rTens = rTens.Replace("XXXX", "XL");

			string rOnes = "";
			for (int i = 0; i < ones; i++)
			{
				rOnes += "I";
			}
			rOnes = rOnes.Replace("IIIII", "V");
			rOnes = rOnes.Replace("VIIII", "IX");
			rOnes = rOnes.Replace("IIII", "IV");

			output = rHundreds + rTens + rOnes;

			//A note about multi-nines:
			//They are not combined. ie,
			//99 != IC. 99 == XCIX.

			return output;
		}

		public static Image GenerateSkullImage(string username, Image img)
		{
			Image i = img;

			string skinUrl = "http://s3.amazonaws.com/MinecraftSkins/" + 
				username + ".png";

			BitmapImage bmp = new BitmapImage(new Uri(skinUrl, UriKind.Absolute));

			bmp.SourceRect = new Int32Rect(8, 8, 8, 8);

			i.Source = bmp;
			return i;
		}

		public static byte ToByte(this bool b)
		{
			if (b)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		public static SelectorWindow GenerateSelector(EventHandler onClosed)
		{
			SelectorWindow selector = new SelectorWindow();
			selector.Closed += onClosed;

			return selector;
		}
		public static ItemCreationWindow GenerateItemCreator(bool countless, EventHandler onClosed)
		{
			ItemCreationWindow creator = new ItemCreationWindow(countless);
			creator.Closed += onClosed;

			return creator;
		}

		public static InventoryItem FindByShortName(string shortName)
		{
			foreach (InventoryItem i in InventoryItem.ListALLTheThings())
			{
				if (i.ShortName == shortName)
				{
					return i;
				}
			}

			return null;
		}

		public static Visibility BoolToVisibility(bool b)
		{
			if (b)
			{
				return Visibility.Visible;
			}
			else
			{
				return Visibility.Collapsed;
			}
		}
		public static Visibility BoolToVisibility(bool? bq)
		{
			return BoolToVisibility(bq ?? false);
		}

		/// <summary>
		/// Determines if the IEnumerable contains the contents of the specified
		/// IEnumerable.
		/// </summary>
		/// <param name="enumerable">Container to be checked</param>
		/// <param name="collection">Items checked</param>
		/// <returns>True if the IEnumerable contains the contents, false otherwise.</returns>
		public static bool Contains<T>(this IEnumerable<T> enumerable, IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!enumerable.Contains(item))
				{
					return false;
				}
			}

			return true;
		}

		public static int IndexOf<T>(this IEnumerable<T> owner, T item, Comparison<T> compareWith)
		{
			for (int i = 0; i < owner.Count(); i++)
			{
				if (compareWith(owner.ElementAt(i), item) == 0)
				{
					return i;
				}
			}

			return -1;
		}
		public static int IndexOf(this UIElementCollection owner, UIElement item, 
			Comparison<UIElement> compareWith)
		{
			List<UIElement> stuff = new List<UIElement>();

			foreach (UIElement i in owner)
			{
				stuff.Add(i);
			}

			return stuff.IndexOf<UIElement>(item, compareWith);
		}
	}
}
