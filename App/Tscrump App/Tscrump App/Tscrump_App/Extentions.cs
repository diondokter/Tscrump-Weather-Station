using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public static class Extentions
	{
		public static string ToSQLString(this DateTime Value)
		{
			return $"\"{Value.ToString("yyyy-MM-dd HH:mm:ss")}\"";
		}
	}
}
