using Syncfusion.SfChart.XForms;
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
		private ChartPage ChartSource;

		private Dictionary<ColorValue, Color> ColorTable = new Dictionary<ColorValue, Color>()
		{
			[ColorValue.Accent] = Color.Accent,
			[ColorValue.Aqua] = Color.Aqua,
			[ColorValue.Black] = Color.Black,
			[ColorValue.Blue] = Color.Blue,
			[ColorValue.Fuchsia] = Color.Fuchsia,
			[ColorValue.Gray] = Color.Gray,
			[ColorValue.Green] = Color.Green,
			[ColorValue.Lime] = Color.Lime,
			[ColorValue.Maroon] = Color.Maroon,
			[ColorValue.Navy] = Color.Navy,
			[ColorValue.Olive] = Color.Olive,
			[ColorValue.Orange] = Color.Orange,
			[ColorValue.Pink] = Color.Pink,
			[ColorValue.Purple] = Color.Purple,
			[ColorValue.Red] = Color.Red,
			[ColorValue.Silver] = Color.Silver,
			[ColorValue.Teal] = Color.Teal,
			[ColorValue.White] = Color.White,
			[ColorValue.Yellow] = Color.Yellow,
		};

		private Picker[] ColorPickers;
		private Picker[] LineTypePickers;

		public ChartOptionsPage(ChartPage ChartSource)
		{
			this.ChartSource = ChartSource;
			InitializeComponent();

			ColorPickers = new Picker[] { TemperatureColorPicker, PressureColorPicker, HumidityColorPicker, BrightnessColorPicker, PrecipitationColorPicker };
			LineTypePickers = new Picker[] { TemperatureLineTypePicker, PressureLineTypePicker, HumidityLineTypePicker, BrightnessLineTypePicker, PrecipitationLineTypePicker };

			InitializeElements();

			SizeChanged += (sender, e) => SetElementWidths();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			for (int i = 0; i < ChartSource.Series.Count; i++)
			{
				ChartSource.Series[i].Color = ColorPickers[i].TextColor;
				((XyDataSeries)ChartSource.Series[i]).YAxis.Title.TextColor = ColorPickers[i].TextColor;

				ChartSource.Series[i] = ChartSource.SeriesCreation[ChartSource.SeriesCreation.Keys.ElementAt(LineTypePickers[i].SelectedIndex)](((XyDataSeries)ChartSource.Series[i]).YAxis, nameof(ChartPage.DataPoint.Date), ((XyDataSeries)ChartSource.Series[i]).YBindingPath, nameof(ChartPage.DataPointCollection.Data));
			}

			ChartSource.IsTemperatureVisible = TemperatureVisibleSwitch.IsToggled;
			ChartSource.IsPressureVisible = PressureVisibleSwitch.IsToggled;
			ChartSource.IsHumidityVisible = HumidityVisibleSwitch.IsToggled;
			ChartSource.IsBrightnessVisible = BrightnessVisibleSwitch.IsToggled;
			ChartSource.IsPrecipitationVisible = PrecipitationVisibleSwitch.IsToggled;

			ChartSource.Update();
		}

		private void InitializeElements()
		{
			InitializeSwitches();
			InitializeColorPickers();
			InitializeLineTypePickers();
		}

		private void InitializeColorPickers()
		{
			for (int i = 0; i < ColorPickers.Length; i++)
			{
				ColorPickers[i].SelectedIndexChanged += (sender, e) => ((Picker)sender).TextColor = ColorTable[ColorTable.Keys.ElementAt(((Picker)sender).SelectedIndex)];

				for (int j = 0; j < ColorTable.Count; j++)
				{
					ColorPickers[i].Items.Add(ColorTable.Keys.ElementAt(j).ToString());

					if (ChartSource.Series[i].Color == ColorTable[ColorTable.Keys.ElementAt(j)])
					{
						ColorPickers[i].SelectedIndex = j;
					}
				}
			}
		}

		private void InitializeLineTypePickers()
		{
			for (int i = 0; i < LineTypePickers.Length; i++)
			{
				for (int j = 0; j < ChartSource.SeriesCreation.Count; j++)
				{
					LineTypePickers[i].Items.Add(ChartSource.SeriesCreation.Keys.ElementAt(j).Name);

					if (ChartSource.Series[i].GetType() == ChartSource.SeriesCreation.Keys.ElementAt(j))
					{
						LineTypePickers[i].SelectedIndex = j;
					}
				}
			}
		}

		private void InitializeSwitches()
		{
			TemperatureVisibleSwitch.IsToggled = ChartSource.TemperatureSeries.IsVisible;
			PressureVisibleSwitch.IsToggled = ChartSource.PressureSeries.IsVisible;
			HumidityVisibleSwitch.IsToggled = ChartSource.HumiditySeries.IsVisible;
			BrightnessVisibleSwitch.IsToggled = ChartSource.BrightnessSeries.IsVisible;
			PrecipitationVisibleSwitch.IsToggled = ChartSource.PrecipitationSeries.IsVisible;
		}

		private void SetElementWidths()
		{
			TemperatureVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			TemperatureColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			TemperatureLineTypePicker.WidthRequest = App.Current.MainPage.Width / 2;

			PressureVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			PressureColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			PressureLineTypePicker.WidthRequest = App.Current.MainPage.Width / 2;

			HumidityVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			HumidityColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			HumidityLineTypePicker.WidthRequest = App.Current.MainPage.Width / 2;

			BrightnessVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			BrightnessColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			BrightnessLineTypePicker.WidthRequest = App.Current.MainPage.Width / 2;

			PrecipitationVisibleSwitch.WidthRequest = App.Current.MainPage.Width / 2;
			PrecipitationColorPicker.WidthRequest = App.Current.MainPage.Width / 2;
			PrecipitationLineTypePicker.WidthRequest = App.Current.MainPage.Width / 2;
		}

		private enum ColorValue
		{
			Accent, Aqua, Black, Blue, Fuchsia, Gray, Green, Lime, Maroon, Navy, Olive, Orange, Pink, Purple, Red, Silver, Teal, White, Yellow
		}
	}
}
