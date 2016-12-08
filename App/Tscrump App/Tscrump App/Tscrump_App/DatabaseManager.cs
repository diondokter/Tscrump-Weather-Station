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
		//public const string Server = "127.0.0.1";//"192.168.105.184";
		//public const string Port = "3306";
		//public const string Database = "dummyweatherstation";
		//public const string UID = "App";
		//public const string Password = "AppPassword";

		public const string Server = "86.85.185.207";//"192.168.105.184";
		public const string Port = "3306";
		public const string Database = "weatherr";
		public const string UID = "app";
		public const string Password = "weerstation";

		private static IDatabaseManager _Instance;

		public static IDatabaseManager GetInstance()
		{
			if (_Instance == null)
			{
				_Instance = DependencyService.Get<IDatabaseManager>(DependencyFetchTarget.NewInstance);
				if (!_Instance.IsConnected)
				{
					_Instance = null;
				}
			}

			return _Instance;
		}
	}
}
