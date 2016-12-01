using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class ChartOptionsPage : ContentPage
	{
		public ChartOptionsPage()
		{
			InitializeComponent();
			InitializeElements();

			SizeChanged += (sender, e) => SetElementWidths();
		}

		private void InitializeElements()
		{
			InitializeColorPickers();
		}

		private void InitializeColorPickers()
		{
		}

		private void SetElementWidths()
		{
			TemperatureVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			TemperatureColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			TemperatureYAxisMaxUpDown.WidthRequest = App.Current.MainPage.Width / 2;
			TemperatureYAxisMinUpDown.WidthRequest = App.Current.MainPage.Width / 2;

			PressureVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			PressureColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			PressureYAxisMaxUpDown.WidthRequest = App.Current.MainPage.Width / 2;
			PressureYAxisMinUpDown.WidthRequest = App.Current.MainPage.Width / 2;

			HumidityVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			HumidityColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			HumidityYAxisMaxUpDown.WidthRequest = App.Current.MainPage.Width / 2;
			HumidityYAxisMinUpDown.WidthRequest = App.Current.MainPage.Width / 2;

			BrightnessVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			BrightnessColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			BrightnessYAxisMaxUpDown.WidthRequest = App.Current.MainPage.Width / 2;
			BrightnessYAxisMinUpDown.WidthRequest = App.Current.MainPage.Width / 2;

			PrecipitationVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			PrecipitationColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			PrecipitationYAxisMaxUpDown.WidthRequest = App.Current.MainPage.Width / 2;
			PrecipitationYAxisMinUpDown.WidthRequest = App.Current.MainPage.Width / 2;
		}
	}
}
