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

using CommandGeneratorWPF;

namespace CommandGeneratorWPF
{
	/// <summary>
	/// Interaction logic for SelectorWindow.xaml
	/// </summary>
	public partial class SelectorWindow : Window
	{
		public string Result
		{ get; private set; }

		public SelectorWindow()
		{
			InitializeComponent();
			Result = "";
		}

		public string ScoreboardArgToText()
		{
			string min = "";
			if ((scoreMaxMinBtn.Content as string) == "min")
			{
				min = "_min";
			}
			string objective = scoreNameBox.Text.Trim();

			int val = 0;
			if (!int.TryParse(scoreValueBox.Text, out val))
			{
				return null;
			}

			string result = "score_" + objective + min + "=" + val.ToString();
			if (ScoresList.Items.Contains(result))
			{
				return null;
			}

			return result;
		}

		public string GenerateString()
		{
			#region target
			string target = "@p";
			if (atAll.IsChecked ?? false)
			{
				target = "@a";
			}
			if (atClose.IsChecked ?? false)
			{
				target = "@p";
			}
			if (atRandom.IsChecked ?? false)
			{
				target = "@r";
			}
			if (atEntity.IsChecked ?? false)
			{
				target = "@e";
			}
			#endregion

			// used with linked arguments like x/y/z.
			bool working = true;

			#region pos
			int x, y, z;
			string pos = "";
			working &= int.TryParse(xPosBox.Text, out x);
			working &= int.TryParse(yPosBox.Text, out y);
			working &= int.TryParse(zPosBox.Text, out z);

			if (working)
			{
				pos = "x=" + x.ToString() + "," +
					"y=" + y.ToString() + "," +
					"z=" + z.ToString() + ",";
			}
			#endregion

			#region searchArea
			string searchArea = "";
			if (SearchAreaTabs.SelectedIndex == 0)
			{
				int dx, dy, dz;
				working = true;
				working &= int.TryParse(xDeltaBox.Text, out dx);
				working &= int.TryParse(yDeltaBox.Text, out dy);
				working &= int.TryParse(zDeltaBox.Text, out dz);

				if (working)
				{
					searchArea = "dx=" + dx.ToString() + "," +
						"dy=" + dy.ToString() + "," +
						"dz=" + dz.ToString() + ",";
				}
			}
			else if (SearchAreaTabs.SelectedIndex == 1)
			{
				int r, rm;
				if (int.TryParse(rMaxBox.Text, out r))
				{
					searchArea = "r=" + r.ToString() + ",";
				}
				if (int.TryParse(rMinBox.Text, out rm))
				{
					searchArea += "rm=" + rm.ToString() + ",";
				}
			}
			#endregion

			#region rot
			string rot = "";
			int rx, rxm, ry, rym;

			if (int.TryParse(xRotMaxBox.Text, out rx))
			{
				rot += "rx=" + rx.ToString() + ",";
			}
			if (int.TryParse(xRotMinBox.Text, out rxm))
			{
				rot += "rxm=" + rxm.ToString() + ",";
			}
			if (int.TryParse(yRotMaxBox.Text, out ry))
			{
				rot += "ry=" + ry.ToString() + ",";
			}
			if (int.TryParse(yRotMinBox.Text, out rym))
			{
				rot += "rym=" + ry.ToString() + ",";
			}
			#endregion

			#region gamemode
			// 0 --> -1 (all), 1 --> 0 (survival), etc. Max to eliminate -2.
			int m = Math.Max(GameModeCombo.SelectedIndex - 1, -1);
			string gamemode = "";
			if (m != -1)
			{
				gamemode = m.ToString() + ",";
			}
			#endregion

			#region count
			string count = "";
			if (MaxTargetBox.IsEnabled)
			{
				int c = 0;
				if (int.TryParse(MaxTargetBox.Text, out c))
				{
					count = "c=" + c.ToString() + ",";
				}
			}
			#endregion

			#region level
			string level = "";
			int l, lm;
			if (int.TryParse(levelMaxBox.Text, out l))
			{
				level = "l=" + l.ToString() + ",";
			}
			if (int.TryParse(levelMinBox.Text, out lm))
			{
				level += "lm=" + lm.ToString() + ",";
			}
			#endregion

			#region scoreboard
			string scoreboard = "";
			foreach (string s in ScoresList.Items)
			{
				scoreboard += s + ",";
			}

			//fix spaces
			scoreboard = scoreboard.Replace(" ", "_");
			#endregion

			#region team
			string team = TeamBox.Text + ",";
			if ((TeamNot.IsChecked ?? false) &&
				TeamNot.IsEnabled)
			{
				team = "!" + team;
			}
			team = "team=" + team;
			if (team.Trim() == "!" || team == "team=,")
			{
				team = "";
			}

			//fix spaces
			team = team.Replace(" ", "_");
			#endregion

			#region playername
			string name = PlayerBox.Text + ",";
			if ((PlayerNot.IsChecked ?? false) &&
				PlayerNot.IsEnabled)
			{
				name = "!" + name;
			}
			name = "name=" + name;
			if (name.Trim() == "!" || name == "name=,")
			{
				name = "";
			}

			//fix spaces
			name = name.Replace(" ", "_");
			#endregion

			#region type
			string type = EntityBox.Text;
			if ((EntityNot.IsChecked ?? false) &&
				EntityNot.IsEnabled)
			{
				type = "!" + type;
			}
			type = "type=" + type;
			if (type.Trim() == "!" || type == "type=" ||
				EntityBox.Visibility == Visibility.Collapsed)
			{
				type = "";
			}
			#endregion

			string total = pos + searchArea + rot + gamemode +
				count + level + scoreboard + team + name + type;

			// fix comma glitches
			total = total.Replace(",,", ",");
			total = total.TrimEnd(',');

			total = target + "[" + total + "]";

			// if no parameters for the selector
			if (total.EndsWith("[]"))
			{
				return "";
			}
			else
			{
				return total;
			}
		}

		private void atAll_Click(object sender, RoutedEventArgs e)
		{
			atAll.IsChecked = true;
			atClose.IsChecked = false;
			atRandom.IsChecked = false;
			atEntity.IsChecked = false;
			EntityGrid.Visibility = System.Windows.Visibility.Collapsed;
		}
		private void atClose_Click(object sender, RoutedEventArgs e)
		{
			atAll.IsChecked = false;
			atClose.IsChecked = true;
			atRandom.IsChecked = false;
			atEntity.IsChecked = false;
			EntityGrid.Visibility = System.Windows.Visibility.Collapsed;
		}
		private void atRandom_Click(object sender, RoutedEventArgs e)
		{
			atAll.IsChecked = false;
			atClose.IsChecked = false;
			atRandom.IsChecked = true;
			atEntity.IsChecked = false;
			EntityGrid.Visibility = System.Windows.Visibility.Collapsed;
		}
		private void atEntity_Click(object sender, RoutedEventArgs e)
		{
			atAll.IsChecked = false;
			atClose.IsChecked = false;
			atRandom.IsChecked = false;
			atEntity.IsChecked = true;
			EntityGrid.Visibility = System.Windows.Visibility.Visible;
		}

		private void AddScoreBtn_Click(object sender, RoutedEventArgs e)
		{
			string added = ScoreboardArgToText();
			if (added == null)
			{
				System.Diagnostics.Debugger.Log(0, "Selector",
					"ScoreboardArgToText returned null");
				return;
			}

			ScoresList.Items.Add(added);
		}
		private void RemoveScoreBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ScoresList.SelectedIndex == -1)
			{
				return;
			}

			ScoresList.Items.RemoveAt(ScoresList.SelectedIndex);
		}

		private void scoreMaxMinBtn_Click(object sender, RoutedEventArgs e)
		{
			if ((scoreMaxMinBtn.Content as string) == "min")
			{
				scoreMaxMinBtn.Content = "max";
			}
			else
			{
				scoreMaxMinBtn.Content = "min";
			}
		}

		private void AcceptBtn_Click(object sender, RoutedEventArgs e)
		{
			Result = GenerateString();
			this.Close();
		}

		private void TeamBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TeamBox.Text.StartsWith("!"))
			{
				TeamNot.IsEnabled = false;
			}
			else
			{
				TeamNot.IsEnabled = true;
			}
		}
		private void PlayerBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (PlayerBox.Text.StartsWith("!"))
			{
				PlayerNot.IsEnabled = false;
			}
			else
			{
				PlayerNot.IsEnabled = true;
			}
		}
		private void EntityBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (EntityBox.Text.StartsWith("!"))
			{
				EntityNot.IsEnabled = false;
			}
			else
			{
				EntityNot.IsEnabled = true;
			}
		}
	}
}
