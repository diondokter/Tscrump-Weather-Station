using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tscrump_App;
using Xamarin.Forms;

[assembly: Dependency(typeof(Tscrump_App.iOS.CultureProvider))]
namespace Tscrump_App.iOS
{
	public class CultureProvider : ICultureProvider
	{
		public CultureInfo GetCulture()
		{
			var iOSLocale = NSLocale.CurrentLocale.CountryCode;
			return new CultureInfo(iOSLocale.ToLower());
		}
	}
}
