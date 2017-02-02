using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tscrump_App
{
	public class NumericEntry : Entry
	{
		public NumericEntry()
		{
			base.TextChanged += NumericEntryTextChanged;
		}

		private void NumericEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			char LastChar = e.NewTextValue?.ToCharArray().LastOrDefault() ?? char.MinValue;
			if (!char.IsNumber(LastChar) && LastChar != char.MinValue && LastChar != '+' && LastChar != '-' && LastChar != App.DeviceCulture.NumberFormat.NumberGroupSeparator[0] && LastChar != App.DeviceCulture.NumberFormat.NumberDecimalSeparator[0])
			{
				Text = e.OldTextValue ?? "";
			}
			else
			{
				string DesiredText = e.NewTextValue.Replace(App.DeviceCulture.NumberFormat.NumberGroupSeparator, App.DeviceCulture.NumberFormat.NumberDecimalSeparator);

				if (DesiredText.Contains("-"))
				{
					DesiredText = "-" + DesiredText.Replace("-", "");
				}

				if (DesiredText.Contains("+"))
				{
					DesiredText = DesiredText.Replace("-", "").Replace("+", "");
				}

				while (DesiredText.ToCharArray().Count((x) => x.ToString() == App.DeviceCulture.NumberFormat.NumberDecimalSeparator) > 1)
				{
					DesiredText = DesiredText.Remove(DesiredText.IndexOf(App.DeviceCulture.NumberFormat.NumberDecimalSeparator), 1);
				}

				if (DesiredText != e.NewTextValue)
				{
					Text = DesiredText;
				}
			}
		}
	}
}
