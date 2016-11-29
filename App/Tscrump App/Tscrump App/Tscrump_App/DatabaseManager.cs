using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace Tscrump_App
{
	public static class DatabaseManager
	{
		public const string Server = "127.0.0.1";//"192.168.105.184";
		public const string Port = "3306";
		public const string Database = "dummyweatherstation";
		public const string UID = "App";
		public const string Password = "AppPassword";

		private static IDatabaseManager _Instance;

		public static async Task<IDatabaseManager> GetInstance()
		{
			if (_Instance == null)
			{
				bool ServerReachable = await CrossConnectivity.Current.IsRemoteReachable(Server, int.Parse(Port), 1000); // Doesn't always work :(

				if (ServerReachable)
				{
					_Instance = DependencyService.Get<IDatabaseManager>();
				}
				else
				{
					bool DisplayAnswer = await Application.Current.MainPage.DisplayAlert("Connection error.", "Could not reach the server...", "Retry", "Ok");
					if (DisplayAnswer)
					{
						_Instance = await GetInstance();
					}
					else
					{
						_Instance = null;
					}
				}
			}

			return _Instance;
		}
	}
}
