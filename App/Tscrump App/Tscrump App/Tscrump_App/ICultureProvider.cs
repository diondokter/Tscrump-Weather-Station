using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public interface ICultureProvider
	{
		CultureInfo GetCulture();
	}
}
