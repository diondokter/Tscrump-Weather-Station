using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public interface IDatabaseManager
	{
		void ExecuteNonQuery(string Query);
		List<object[]> ExecuteReader(string Query);
		object ExecuteScaler(string Query);
	}
}
