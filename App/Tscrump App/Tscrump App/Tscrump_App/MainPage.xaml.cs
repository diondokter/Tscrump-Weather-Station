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

			// Set the image source for the logo
			LogoImage.Source = ImageSource.FromResource("Tscrump_App.TscrumpLogo.png");
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			// Hide the navbar, because Android doesn't realize an empty one is quite irritating
			NavigationPage.SetHasNavigationBar(this, false);
		}

		private async void ViewChartButtonClicked(object sender, EventArgs e)
		{
			// We want to see the chart, so let's push a new chartpage on the navigation stack
			await Navigation.PushAsync(new ChartPage());
		}

		private void ViewWebsiteButtonClicked(object sender, EventArgs e)
		{
			// Visit the website
			Device.OpenUri(new Uri("http://tscrump.tk"));
		}

		private void ViewSourceButtonClicked(object sender, EventArgs e)
		{
			// Visit the project page
			Device.OpenUri(new Uri("https://github.com/diondokter/Tscrump-Weather-Station"));
		}
	}
}
