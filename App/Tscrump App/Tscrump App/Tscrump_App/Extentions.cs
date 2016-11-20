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

		public static string ToJSArray(this IEnumerable Value)
		{
			return $"[{ToSubJSArray(Value)}]";
		}

		private static string ToSubJSArray(IEnumerable Value)
		{
			StringBuilder Builder = new StringBuilder();

			foreach (object Child in Value)
			{
				if (Child is IEnumerable)
				{
					Builder.Append(ToSubJSArray((IEnumerable)Child));
				}
				else if (Child is string || Child is DateTime)
				{
					Builder.Append('"');
					Builder.Append(Child);
					Builder.Append('"');
					Builder.Append(',');
				}
				else
				{
					Builder.Append(Child);
					Builder.Append(',');
				}
			}

			return Builder.ToString();
		}
	}
}
