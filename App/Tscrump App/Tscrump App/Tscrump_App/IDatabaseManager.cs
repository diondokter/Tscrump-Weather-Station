using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public interface IDatabaseManager
	{
		bool IsConnected { get; }

		void ExecuteNonQuery(string Query);
		List<object[]> ExecuteReader(string Query);
		List<object> ExecuteReader(string Query, int Column);
		object ExecuteScaler(string Query);
	}
}
