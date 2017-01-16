using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class App : Application
	{
		public static CultureInfo DeviceCulture;

		public App()
		{
			DeviceCulture = DependencyService.Get<ICultureProvider>().GetCulture();

			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
			
		}

		protected override void OnSleep()
		{
			
		}

		protected override void OnResume()
		{
			
		}
	}
}
