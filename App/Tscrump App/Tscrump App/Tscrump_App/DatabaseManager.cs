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
		public const string Server = "127.0.0.1";//"192.168.105.184";
		public const string Port = "3306";
		public const string Database = "dummyweatherstation";
		public const string UID = "App";
		public const string Password = "AppPassword";

		public static IDatabaseManager Instance
		{
			get
			{
				return DependencyService.Get<IDatabaseManager>();
			}
		}
	}
}
