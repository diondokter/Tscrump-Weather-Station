using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class ChartPage : ContentPage
	{
		public XyDataSeries TemperatureSeries
		{
			get
			{
				return ChartView.Series[0] as XyDataSeries;
			}
		}
		public XyDataSeries PressureSeries
		{
			get
			{
				return ChartView.Series[1] as XyDataSeries;
			}
		}
		public XyDataSeries HumiditySeries
		{
			get
			{
				return ChartView.Series[2] as XyDataSeries;
			}
		}
		public XyDataSeries BrightnessSeries
		{
			get
			{
				return ChartView.Series[3] as XyDataSeries;
			}
		}
		public XyDataSeries PrecipitationSeries
		{
			get
			{
				return ChartView.Series[4] as XyDataSeries;
			}
		}

		public bool IsTemperatureVisible
		{
			get
			{
				return TemperatureSwitch.IsToggled;
			}
			set
			{
				TemperatureSwitch.IsToggled = value;
			}
		}
		public bool IsPressureVisible
		{
			get
			{
				return PressureSwitch.IsToggled;
			}
			set
			{
				PressureSwitch.IsToggled = value;
			}
		}
		public bool IsHumidityVisible
		{
			get
			{
				return HumiditySwitch.IsToggled;
			}
			set
			{
				HumiditySwitch.IsToggled = value;
			}
		}
		public bool IsBrightnessVisible
		{
			get
			{
				return BrightnessSwitch.IsToggled;
			}
			set
			{
				BrightnessSwitch.IsToggled = value;
			}
		}
		public bool IsPrecipitationVisible
		{
			get
			{
				return PrecipitationSwitch.IsToggled;
			}
			set
			{
				PrecipitationSwitch.IsToggled = value;
			}
		}

		public ChartSeriesCollection Series
		{
			get
			{
				return ChartView.Series;
			}
		}

		public readonly Dictionary<Type, Func<RangeAxisBase, string, string, string, XyDataSeries>> SeriesCreation = new Dictionary<Type, Func<RangeAxisBase, string, string, string, XyDataSeries>>()
		{
			[typeof(LineSeries)] = (YAxis, XBindingPath, YBindingPath, DataPath) =>
			{
				LineSeries x = new LineSeries() { YAxis = YAxis, Color = YAxis.Title.TextColor, XBindingPath = XBindingPath, YBindingPath = YBindingPath, StrokeWidth = 4 };
				x.SetBinding(ChartSeries.ItemsSourceProperty, DataPath);
				return x;
			},
			[typeof(ColumnSeries)] = (YAxis, XBindingPath, YBindingPath, DataPath) =>
			{
				ColumnSeries x = new ColumnSeries() { YAxis = YAxis, Color = YAxis.Title.TextColor, XBindingPath = XBindingPath, YBindingPath = YBindingPath, StrokeWidth = 4 };
				x.SetBinding(ChartSeries.ItemsSourceProperty, DataPath);
				return x;
			},
			[typeof(StackingAreaSeries)] = (YAxis, XBindingPath, YBindingPath, DataPath) =>
			{
				StackingAreaSeries x = new StackingAreaSeries() { YAxis = YAxis, Color = YAxis.Title.TextColor, XBindingPath = XBindingPath, YBindingPath = YBindingPath };
				x.SetBinding(ChartSeries.ItemsSourceProperty, DataPath);
				return x;
			},
			[typeof(StepAreaSeries)] = (YAxis, XBindingPath, YBindingPath, DataPath) =>
			{
				StepAreaSeries x = new StepAreaSeries() { YAxis = YAxis, Color = YAxis.Title.TextColor, XBindingPath = XBindingPath, YBindingPath = YBindingPath };
				x.SetBinding(ChartSeries.ItemsSourceProperty, DataPath);
				return x;
			},
			[typeof(StepLineSeries)] = (YAxis, XBindingPath, YBindingPath, DataPath) =>
			{
				StepLineSeries x = new StepLineSeries() { YAxis = YAxis, Color = YAxis.Title.TextColor, XBindingPath = XBindingPath, YBindingPath = YBindingPath, StrokeWidth = 4 };
				x.SetBinding(ChartSeries.ItemsSourceProperty, DataPath);
				return x;
			}
		};

		private readonly Type DefaultSeries = typeof(LineSeries);

		public ChartPage()
		{
			InitializeComponent();

			StartDatePicker.Date = DateTime.Today.AddDays(-7);
			EndDatePicker.Date = DateTime.Today;

			StartDatePicker.DateSelected += Update;
			EndDatePicker.DateSelected += Update;

			ChartView.PrimaryAxis = new DateTimeAxis() { LabelsIntersectAction = AxisLabelsIntersectAction.Hide, EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Hide, LabelStyle = new ChartAxisLabelStyle() { LabelFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " - " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern } };

			// Temperature -------------------------------------------------------
			NumericalAxis TemperatureAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Temperature (C)", TextColor = Color.Blue }, RangePadding = NumericalPadding.Additional };
			ChartView.Series.Add(SeriesCreation[DefaultSeries](TemperatureAxis, nameof(DataPoint.Date), nameof(DataPoint.Temperature), nameof(DataPointCollection.Data)));

			// Pressure -------------------------------------------------------
			NumericalAxis PressureAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Pressure (bar)", TextColor = Color.Red }, RangePadding = NumericalPadding.Additional };
			ChartView.Series.Add(SeriesCreation[DefaultSeries](PressureAxis, nameof(DataPoint.Date), nameof(DataPoint.Pressure), nameof(DataPointCollection.Data)));

			// Humidity -------------------------------------------------------
			NumericalAxis HumidityAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Humidity (%)", TextColor = Color.Pink }, RangePadding = NumericalPadding.Additional };
			ChartView.Series.Add(SeriesCreation[DefaultSeries](HumidityAxis, nameof(DataPoint.Date), nameof(DataPoint.Humidity), nameof(DataPointCollection.Data)));

			// Brightness -------------------------------------------------------
			NumericalAxis BrightnessAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Brightness (%)", TextColor = Color.Lime }, RangePadding = NumericalPadding.Additional };
			ChartView.Series.Add(SeriesCreation[DefaultSeries](BrightnessAxis, nameof(DataPoint.Date), nameof(DataPoint.Brightness), nameof(DataPointCollection.Data)));

			// Precipitation -------------------------------------------------------
			NumericalAxis PrecipitationAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Precipitation (mm)", TextColor = Color.Orange }, RangePadding = NumericalPadding.Additional };
			ChartView.Series.Add(SeriesCreation[DefaultSeries](PrecipitationAxis, nameof(DataPoint.Date), nameof(DataPoint.Precipitation), nameof(DataPointCollection.Data)));

			SizeChanged += ChartViewSizer;
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			if (ChartView.HeightRequest == -1) // Only update the first time when the request has not yet been set
			{
				Update();
			}

			base.OnSizeAllocated(width, height);
			ChartViewSizer(null, null);
		}

		private void ChartViewSizer(object s, EventArgs e)
		{
			ChartView.HeightRequest = this.Height - ChartView.Margin.VerticalThickness;
		}

		private void Update(object s, EventArgs e)
		{
			Update(!(s is Switch));
		}

		public async void Update(bool DoNewQuery = true)
		{
			if (DoNewQuery)
			{
				if (await DatabaseManager.GetInstance() == null)
				{
					return;
				}

				var Values = (await DatabaseManager.GetInstance()).ExecuteReader($"Select {nameof(DataPoint.Date)},{nameof(DataPoint.Temperature)},{nameof(DataPoint.Pressure)},{nameof(DataPoint.Humidity)},{nameof(DataPoint.Brightness)},{nameof(DataPoint.Precipitation)} from sensor where Date >= {StartDatePicker.Date.AddDays(-1).ToSQLString()} and Date <= {EndDatePicker.Date.AddDays(1).AddHours(11.99999).ToSQLString()}");

				DataPoint[] Models = new DataPoint[Values?.Count ?? 0];
				for (int i = 0; i < Values.Count; i++)
				{
					Models[i] = new DataPoint((DateTime)Values[i][0], (float)Values[i][1], (float)Values[i][2], (float)Values[i][3], (float)Values[i][4], (float)Values[i][5]);
				}

				this.BindingContext = new DataPointCollection(Models);

				((DateTimeAxis)ChartView.PrimaryAxis).Minimum = StartDatePicker.Date;
				((DateTimeAxis)ChartView.PrimaryAxis).Maximum = EndDatePicker.Date.AddHours(11.999999);

				if (Models.Length > 0)
				{
					// Keep temperature from <min - >max
					float MinimumTemperature = Models.Min((x) => x.Temperature);
					float MaximumTemperature = Models.Max((x) => x.Temperature);
					float DeltaTemperature = MaximumTemperature - MinimumTemperature;
					((NumericalAxis)TemperatureSeries.YAxis).Minimum = MinimumTemperature - DeltaTemperature / 20;
					((NumericalAxis)TemperatureSeries.YAxis).Maximum = MaximumTemperature + DeltaTemperature / 20;
					((NumericalAxis)TemperatureSeries.YAxis).Interval = (DeltaTemperature + DeltaTemperature / 10) / 10;

					// Keep pressure from <min - >max
					float MinimumPressure = Models.Min((x) => x.Pressure);
					float MaximumPressure = Models.Max((x) => x.Pressure);
					float DeltaPressure = MaximumPressure - MinimumPressure;
					((NumericalAxis)PressureSeries.YAxis).Minimum = MinimumPressure - DeltaPressure / 20;
					((NumericalAxis)PressureSeries.YAxis).Maximum = MaximumPressure + DeltaPressure / 20;
					((NumericalAxis)PressureSeries.YAxis).Interval = (DeltaPressure + DeltaPressure / 10) / 10;

					// Always keep humidity from 0 - 100%
					float DeltaHumidity = 100;
					((NumericalAxis)HumiditySeries.YAxis).Minimum = 0;
					((NumericalAxis)HumiditySeries.YAxis).Maximum = 100;
					((NumericalAxis)HumiditySeries.YAxis).Interval = DeltaHumidity / 10;

					// Always keep brightness from 0 - 100%
					float DeltaBrightness = 100;
					((NumericalAxis)BrightnessSeries.YAxis).Minimum = 0;
					((NumericalAxis)BrightnessSeries.YAxis).Maximum = 100;
					((NumericalAxis)BrightnessSeries.YAxis).Interval = DeltaBrightness / 10;

					// Always keep precipitation from 0 - >max
					float MaximumPrecipitation = Models.Max((x) => x.Precipitation);
					float DeltaPrecipitation = MaximumPrecipitation;
					((NumericalAxis)PrecipitationSeries.YAxis).Minimum = 0;
					((NumericalAxis)PrecipitationSeries.YAxis).Maximum = MaximumPrecipitation + DeltaPrecipitation / 20;
					((NumericalAxis)PrecipitationSeries.YAxis).Interval = (DeltaPrecipitation + DeltaPrecipitation / 20) / 10;
				}
			}

			TemperatureSeries.IsVisible = TemperatureSwitch.IsToggled;
			TemperatureSeries.YAxis.IsVisible = TemperatureSwitch.IsToggled;

			PressureSeries.IsVisible = PressureSwitch.IsToggled;
			PressureSeries.YAxis.IsVisible = PressureSwitch.IsToggled;

			HumiditySeries.IsVisible = HumiditySwitch.IsToggled;
			HumiditySeries.YAxis.IsVisible = HumiditySwitch.IsToggled;

			BrightnessSeries.IsVisible = BrightnessSwitch.IsToggled;
			BrightnessSeries.YAxis.IsVisible = BrightnessSwitch.IsToggled;

			PrecipitationSeries.IsVisible = PrecipitationSwitch.IsToggled;
			PrecipitationSeries.YAxis.IsVisible = PrecipitationSwitch.IsToggled;
		}

		private void AdvancedOptionsButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChartOptionsPage(this));
		}

		public class DataPoint
		{
			public DateTime Date { get; set; }
			public float Temperature { get; set; }
			public float Pressure { get; set; }
			public float Humidity { get; set; }
			public float Brightness { get; set; }
			public float Precipitation { get; set; }

			public DataPoint(DateTime Date, float Temperature, float Pressure, float Humidity, float Brightness, float Precipitation)
			{
				this.Date = Date;
				this.Temperature = Temperature;
				this.Pressure = Pressure;
				this.Humidity = Humidity;
				this.Brightness = Brightness;
				this.Precipitation = Precipitation;
			}
		}

		public class DataPointCollection
		{
			public List<DataPoint> Data { get; set; }

			public DataPointCollection(DataPoint[] Data)
			{
				this.Data = Data.ToList();
			}
		}
	}
}
