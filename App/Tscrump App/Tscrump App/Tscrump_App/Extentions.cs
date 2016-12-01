using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tscrump_App
{
	public static class Extentions
	{
		public static string ToSQLString(this DateTime Value)
		{
			return $"\"{Value.ToString("yyyy-MM-dd HH:mm:ss")}\"";
		}

		/// <summary>
		/// Gives a nicely formatted string representing the TimeSpan
		/// </summary>
		/// <param name="value">The target TimeSpan</param>
		/// <returns>A string in this format: ##:##:##</returns>
		public static string ToNiceString(this TimeSpan value)
		{
			return ((int)Math.Floor(value.TotalHours)).ToString("00") + ":" + value.Minutes.ToString("00") + ":" + value.Seconds.ToString("00");
		}

		/// <summary>
		/// Removes any character that is not a number
		/// </summary>
		public static string ToNumberString(this string value)
		{
			string RealNewText = "";

			for (int i = 0; i < value.Length; i++)
			{
				if (char.IsDigit(value, i))
				{
					RealNewText += value[i];
				}
			}

			return RealNewText;
		}

		/// <summary>
		/// Gives a 'NumberString' proper punctuation.
		/// </summary>
		public static string ToFormattedNumberString(this string value)
		{
			return long.Parse(value).ToString("N0", App.DeviceCulture);
		}
	}
}
