using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tscrump_App
{
	public static class DatabaseManager
	{
		public const string Server = "192.168.105.184:3306";
		public const string Database = "world";
		public const string UID = "root";
		public const string Password = "Qwerty01";

		public static IDatabaseManager Instance
		{
			get
			{
				return DependencyService.Get<IDatabaseManager>();
			}
		}
	}
}
