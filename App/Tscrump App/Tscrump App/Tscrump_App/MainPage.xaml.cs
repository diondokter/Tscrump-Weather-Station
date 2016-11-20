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

			var x = DatabaseManager.Instance.ExecuteReader($"Select * from dummysensorvalues where Date > {new DateTime(2016, 11, 17).ToSQLString()} order by Pressure");

			for (int i = 0; i < x.Count; i++)
			{
				for (int j = 0; j < x[i].Length; j++)
				{
					TestLabel.Text += x[i][j].ToString() + "    ";
				}
				TestLabel.Text += "\n";
			}

			ChartWebView.Source = new HtmlWebViewSource() { Html = Html };
		}

		public string Html
		{
			get
			{
				var assembly = typeof(MainPage).GetTypeInfo().Assembly;
				Stream stream = assembly.GetManifestResourceStream("Tscrump_App.Chart.html");
				string text = "";
				using (var reader = new System.IO.StreamReader(stream))
				{
					text = reader.ReadToEnd();
				}
				return text;
			}
		}
	}
}
