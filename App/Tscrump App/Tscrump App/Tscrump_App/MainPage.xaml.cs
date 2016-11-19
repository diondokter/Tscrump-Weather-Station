using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			long Count = (long)DatabaseManager.Instance.ExecuteScaler("Select count(*) from city");

			var x = DatabaseManager.Instance.ExecuteReader("Select * from city order by population desc");

			for (int i = 0; i < x.Count; i++)
			{
				for (int j = 0; j < x[i].Length; j++)
				{
					TestLabel.Text += x[i][j].ToString() + "    ";
				}
				TestLabel.Text += "\n";
			}
		}
	}
}
