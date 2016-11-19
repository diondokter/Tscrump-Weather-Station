using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Tscrump_App.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseManager))]
namespace Tscrump_App.UWP
{
	public class DatabaseManager : IDatabaseManager, IDisposable
	{
		private bool Disposed = false;
		private MySqlConnection Connection;

		public DatabaseManager()
		{
			//Set our encoding to something that mysql can understand
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			string ConnectionString = $"Server={Tscrump_App.DatabaseManager.Server}; Port={Tscrump_App.DatabaseManager.Port}; Database={Tscrump_App.DatabaseManager.Database}; Uid={Tscrump_App.DatabaseManager.UID}; Pwd={Tscrump_App.DatabaseManager.Password}; SslMode=None;";

			Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
		}

		public void ExecuteNonQuery(string Query)
		{
			MySqlCommand Command = Connection.CreateCommand();
			Command.CommandText = Query;
			Command.ExecuteNonQuery();
		}

		public List<object[]> ExecuteReader(string Query)
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

		public object ExecuteScaler(string Query)
		{
			MySqlCommand Command = Connection.CreateCommand();
			Command.CommandText = Query;
			object Data = Command.ExecuteScalar();

			return Data;
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
	}
}
