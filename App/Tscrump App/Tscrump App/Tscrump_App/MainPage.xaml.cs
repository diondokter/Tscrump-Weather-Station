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

			//long Count = (long)DatabaseManager.Instance.ExecuteScaler("Select count(*) from city");

			var Pressure = DatabaseManager.Instance.ExecuteReader($"Select Pressure from dummysensorvalues");
			var Temperature = DatabaseManager.Instance.ExecuteReader($"Select Temperature from dummysensorvalues");
			var Dates = DatabaseManager.Instance.ExecuteReader($"Select Date from dummysensorvalues");

			//for (int i = 0; i < x.Count; i++)
			//{
			//	for (int j = 0; j < x[i].Length; j++)
			//	{
			//		TestLabel.Text += x[i][j].ToString() + "    ";
			//	}
			//	TestLabel.Text += "\n";
			//}


			ChartWebView.Source = new HtmlWebViewSource() { Html = Html };


			Task.Factory.StartNew(async () =>
			{
				await Task.Delay(500); // Wait for the webview to be loaded
				ChartWebView.Eval($"construct({Pressure.ToJSArray()},{Temperature.ToJSArray()},{Dates.ToJSArray()})");
			});
		}

		public string Html
		{
			get
			{
				var assembly = typeof(MainPage).GetTypeInfo().Assembly;
				Stream stream = assembly.GetManifestResourceStream("Tscrump_App.Chart.html");
				string text = "";
				using (StreamReader reader = new StreamReader(stream))
				{
					text = reader.ReadToEnd();
				}
				return text;
			}
		}
	}
}
