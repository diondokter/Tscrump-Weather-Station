using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public interface IDatabaseManager
	{
		/// <summary>
		/// Returns whether or not the connection has been established.
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Exectutes a query that does not return anything.
		/// </summary>
		/// <param name="Query">The SQL query to execute</param>
		void ExecuteNonQuery(string Query);
		/// <summary>
		/// Executes a query that returns rows of columns
		/// </summary>
		/// <param name="Query">The SQL query to execute</param>
		/// <returns>A list of object arrays where the list represents the rows and where the arrays represent the columns.</returns>
		List<object[]> ExecuteReader(string Query);
		/// <summary>
		/// Executes a query that returns rows of columns
		/// </summary>
		/// <param name="Query">The SQL query to execute</param>
		/// <param name="Column">Selects the column to return</param>
		/// <returns>The selected column data</returns>
		List<object> ExecuteReader(string Query, int Column);
		/// <summary>
		/// Executes a query that returns one value
		/// </summary>
		/// <param name="Query">The SQL query to execute</param>
		/// <returns>The returned value</returns>
		object ExecuteScaler(string Query);
	}
}
