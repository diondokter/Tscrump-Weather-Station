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

			//long Count = (long)DatabaseManager.Instance.ExecuteScaler("Select count(*) from city");

			var x = DatabaseManager.Instance.ExecuteReader($"Select * from dummysensorvalues where Date > {new DateTime(2016, 11, 17).ToSQLString()} order by Pressure");

			for (int i = 0; i < x.Count; i++)
			{
				for (int j = 0; j < x[i].Length; j++)
				{
					TestLabel.Text += x[i][j].ToString() + "    ";
				}
				TestLabel.Text += "\n";
			}

			//TestChart.Series.Add(new ChartView.Serie() { TargetColor = Color.Blue, Values = new List<Point>() { new Point(0, 0.1), new Point(0.1, 0.2), new Point(0.2, 0.25), new Point(0.3, 0.2), new Point(0.4, 0.5), new Point(0.5, 0.4), new Point(0.6, 0.8) } });
			//TestChart.Series.Add(new ChartView.Serie() { TargetColor = Color.Red, Values = new List<Point>() { new Point(0, 0.9), new Point(0.1, 0.8), new Point(0.2, 0.6), new Point(0.3, 0.7), new Point(0.4, 0.5), new Point(0.5, 0.2), new Point(0.6, 0.3) } });

		}
	}
}
