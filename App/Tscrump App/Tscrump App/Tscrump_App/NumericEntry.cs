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
			base.Keyboard = Keyboard.Numeric;
		}

		private void NumericEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			// We don't want any of the 'filthy' letters in our text, don't we?
			// Let's make it a nicely formatted number
			if (e.NewTextValue != null && !string.IsNullOrWhiteSpace(e.NewTextValue))
			{
				Entry TargetEntry = (Entry)sender;

				string RealNewText = e.NewTextValue.ToNumberString();

				if (RealNewText.Length == 0)
				{
					TargetEntry.Text = "";
				}
				else if (RealNewText.Length < 19)
				{
					TargetEntry.Text = RealNewText.ToFormattedNumberString();
				}
				else
				{
					TargetEntry.Text = e.OldTextValue;
				}
			}

		}
	}
}
