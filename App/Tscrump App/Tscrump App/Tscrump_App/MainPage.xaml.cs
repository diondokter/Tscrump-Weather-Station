using Syncfusion.SfChart.XForms;
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
		}

		private async void ViewChartButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ChartPage());
		}
	}
}
