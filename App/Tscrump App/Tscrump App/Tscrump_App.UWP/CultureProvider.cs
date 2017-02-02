using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tscrump_App;
using Windows.Globalization.DateTimeFormatting;
using Xamarin.Forms;

[assembly: Dependency(typeof(Tscrump_App.UWP.CultureProvider))]
namespace Tscrump_App.UWP
{
	public class CultureProvider : ICultureProvider
	{
		public CultureInfo GetCulture()
		{
			string LocaleName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;
			return new CultureInfo(LocaleName);
		}
	}
}
