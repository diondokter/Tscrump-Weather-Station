using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new Tscrump_App.MainPage();
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
