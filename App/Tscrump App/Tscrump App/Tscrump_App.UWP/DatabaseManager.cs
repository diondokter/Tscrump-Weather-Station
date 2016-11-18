using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Tscrump_App.UWP
{
	public class DatabaseManager : IDatabaseManager, IDisposable
	{
		private bool Disposed = false;
		private MySqlConnection Connection;

		public DatabaseManager()
		{
			string ConnectionString = $"SERVER={Tscrump_App.DatabaseManager.Server};DATABASE={Tscrump_App.DatabaseManager.Database};UID={Tscrump_App.DatabaseManager.UID};PASSWORD={Tscrump_App.DatabaseManager.Password};";

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
