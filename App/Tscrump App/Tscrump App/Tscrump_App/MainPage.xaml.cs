using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

			var x = DatabaseManager.Instance.ExecuteReader($"Select * from dummysensorvalues");

			var Pressure = DatabaseManager.Instance.ExecuteReader($"Select Pressure from dummysensorvalues");
			var Temperature = DatabaseManager.Instance.ExecuteReader($"Select Temperature from dummysensorvalues");
			var Dates = DatabaseManager.Instance.ExecuteReader($"Select Date from dummysensorvalues");

			for (int i = 0; i < x.Count; i++)
			{
				for (int j = 0; j < x[i].Length; j++)
				{
					TestLabel.Text += x[i][j].ToString() + "    ";
				}
				TestLabel.Text += "\n";
			}

			Task.Factory.StartNew(async () =>
			{
				while (Plot.Model == null)
				{
					await Task.Delay(10);
				}

				Plot.Model.Axes.Add(new LinearAxis());
				Plot.Model.Axes.Add(new LinearAxis() { Title = "Test" });

				LineSeries a = new LineSeries();
				a.Points.Add(new DataPoint(2, 2));
				a.Points.Add(new DataPoint(3, 3));
				a.Points.Add(new DataPoint(4, 4));
				a.Points.Add(new DataPoint(5, 5));

				Plot.Model.Series.Add(a);
				Plot.Update();
			});
		}
	}
}
