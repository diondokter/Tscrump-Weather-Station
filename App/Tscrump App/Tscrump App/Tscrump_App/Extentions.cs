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
		/// Removes any character that is not a number
		/// </summary>
		public static string ToNumberString(this string value)
		{
			string RealNewText = "";

			for (int i = 0; i < value.Length; i++)
			{
				if (char.IsDigit(value, i) || value[i] == App.DeviceCulture.NumberFormat.NumberDecimalSeparator[0])
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
			return double.Parse(value).ToString(App.DeviceCulture);
		}
	}
}
