using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Tscrump_App.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseManager))]
namespace Tscrump_App.Droid
{
	public class DatabaseManager : IDatabaseManager, IDisposable
	{
		private bool Disposed = false;
		private MySqlConnection Connection;

		public bool IsConnected
		{
			get;
		} = true;

		public DatabaseManager()
		{
			try
			{
				new I18N.West.CP1250();
				//Set our encoding to something that mysql can understand
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

				string ConnectionString = $"Server={Tscrump_App.DatabaseManager.Server}; Port={Tscrump_App.DatabaseManager.Port}; Database={Tscrump_App.DatabaseManager.Database}; Uid={Tscrump_App.DatabaseManager.UID}; Pwd={Tscrump_App.DatabaseManager.Password};";

				Connection = new MySqlConnection(ConnectionString);
				Connection.Open();
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Something went wrong...", "Couldn't connect to the database.\n" + e.Message, "Ok");
				IsConnected = false;
			}
		}

		public void ExecuteNonQuery(string Query)
		{
			try
			{
				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = Query;
				Command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Something went wrong...", e.Message, "Ok");
			}
		}

		public List<object[]> ExecuteReader(string Query)
		{
			try
			{
				MySqlCommand Command = Connection.CreateCommand();
				Command.Prepare();
				Command.CommandText = Query;
				MySqlDataReader Reader = Command.ExecuteReader();

				List<object[]> Data = new List<object[]>();

				while (Reader.Read())
				{
					object[] Row = new object[Reader.FieldCount];

					for (int i = 0; i < Row.Length; i++)
					{
						Row[i] = Reader.GetValue(i);
					}

					Data.Add(Row);
				}

				Reader.Close();
				Reader.Dispose();

				return Data;
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Something went wrong...", e.Message, "Ok");
				return null;
			}
		}

		public object ExecuteScaler(string Query)
		{
			try
			{
				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = Query;
				object Data = Command.ExecuteScalar();

				return Data;
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Something went wrong...", e.Message, "Ok");
				return null;
			}
		}

		public void Dispose()
		{
			if (!Disposed)
			{
				Close();
				Disposed = true;
			}
		}

		private void Close()
		{
			Connection.Close();
			Connection.Dispose();
			Connection = null;
		}

		public List<object> ExecuteReader(string Query, int Column)
		{
			try
			{
				MySqlCommand Command = Connection.CreateCommand();
				Command.Prepare();
				Command.CommandText = Query;
				MySqlDataReader Reader = Command.ExecuteReader();

				List<object> Data = new List<object>();

				while (Reader.Read())
				{
					object Row = new object[Reader.FieldCount];
					Row = Reader.GetValue(Column);
					Data.Add(Row);
				}

				Reader.Close();
				Reader.Dispose();

				return Data;
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Something went wrong...", e.Message, "Ok");
				return null;
			}
		}
	}
}
