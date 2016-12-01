using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

using Tscrump_App;

[assembly: Dependency(typeof(Tscrump_App.Droid.CultureProvider))]

namespace Tscrump_App.Droid
{
	public class CultureProvider : ICultureProvider
	{
		public CultureInfo GetCulture()
		{
			var androidLocale = Java.Util.Locale.Default;
			var netLanguage = androidLocale.ToString().Replace("_", "-");
			return new CultureInfo(netLanguage.ToLower());
		}
	}
}