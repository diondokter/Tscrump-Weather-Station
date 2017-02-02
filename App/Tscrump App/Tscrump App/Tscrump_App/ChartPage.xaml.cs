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

		// Dictionary to create chartseries based only on the type. Every type corresponds to a func that returns the new series
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

		public readonly Func<string, Color, NumericalAxis> YAxisCreation = (Text, TextColor) => 
			{
				return new NumericalAxis() { Title = new ChartAxisTitle() { Text = Text, TextColor = TextColor }, RangePadding = NumericalPadding.Additional };
			};

		// We want to spawn the line as default
		private readonly Type DefaultSeries = typeof(LineSeries);

		public ChartPage()
		{
			InitializeComponent();


			// On android the chart crashes if there's no data to bind. So let's give it some data in the form of an empty array.
			this.BindingContext = new DataPointCollection(new DataPoint[0]);

			// Initialize the dates from on week ago to now
			StartDatePicker.Date = DateTime.Today.AddDays(-7);
			EndDatePicker.Date = DateTime.Today;

			// Whenever a date is selected, we want to update the chart
			StartDatePicker.DateSelected += Update;
			EndDatePicker.DateSelected += Update;

			// Create and add the primary axis
			if (Device.OS == TargetPlatform.Windows)
			{
				ChartView.PrimaryAxis = new DateTimeAxis() { LabelsIntersectAction = AxisLabelsIntersectAction.Hide, EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Hide, LabelStyle = new ChartAxisLabelStyle() { LabelFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " - " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern } };
			}
			else // Android doesn't like the short format, so let's just not do it
			{
				ChartView.PrimaryAxis = new DateTimeAxis() { LabelsIntersectAction = AxisLabelsIntersectAction.Hide, EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Hide };
			}

			// Temperature -------------------------------------------------------
			NumericalAxis TemperatureAxis = YAxisCreation("Temperature (C)", Color.Blue);
			ChartView.Series.Add(SeriesCreation[DefaultSeries](TemperatureAxis, nameof(DataPoint.Date), nameof(DataPoint.Temperature), nameof(DataPointCollection.Data)));

			// Pressure -------------------------------------------------------
			NumericalAxis PressureAxis = YAxisCreation("Pressure (hPa)", Color.Red);
			ChartView.Series.Add(SeriesCreation[DefaultSeries](PressureAxis, nameof(DataPoint.Date), nameof(DataPoint.Pressure), nameof(DataPointCollection.Data)));

			// Humidity -------------------------------------------------------
			NumericalAxis HumidityAxis = YAxisCreation("Humidity (%)", Color.Pink);
			ChartView.Series.Add(SeriesCreation[DefaultSeries](HumidityAxis, nameof(DataPoint.Date), nameof(DataPoint.Humidity), nameof(DataPointCollection.Data)));

			// Brightness -------------------------------------------------------
			NumericalAxis BrightnessAxis = YAxisCreation("Brightness (%)", Color.Lime);
			ChartView.Series.Add(SeriesCreation[DefaultSeries](BrightnessAxis, nameof(DataPoint.Date), nameof(DataPoint.Brightness), nameof(DataPointCollection.Data)));

			// Precipitation -------------------------------------------------------
			NumericalAxis PrecipitationAxis = YAxisCreation("Precipitation (mm)", Color.Orange);
			ChartView.Series.Add(SeriesCreation[DefaultSeries](PrecipitationAxis, nameof(DataPoint.Date), nameof(DataPoint.Precipitation), nameof(DataPointCollection.Data)));

			// Whenever the window size has changed, call the appropriate method
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
			// Set the height of the chart to be the height of the window, minus the margin
			ChartView.HeightRequest = this.Height - ChartView.Margin.VerticalThickness;
		}

		private void Update(object s, EventArgs e)
		{
			// We update with a new query, unless the caller is a switch. Then we don't need to query again.
			Update(!(s is Switch));
		}

		public void Update(bool DoNewQuery = true)
		{
			// Make sure the date pickers can't be reversed in date
			if (EndDatePicker.Date < StartDatePicker.Date)
			{
				EndDatePicker.Date = StartDatePicker.Date;
			}
			if (StartDatePicker.Date > EndDatePicker.Date)
			{
				StartDatePicker.Date = EndDatePicker.Date;
			}

			if (DoNewQuery)
			{
				if (DatabaseManager.GetInstance() == null)
				{
					return;
				}

				// Get all the data from the database
				var Values = DatabaseManager.GetInstance().ExecuteReader($"Select {nameof(DataPoint.Date)},{nameof(DataPoint.Temperature)},{nameof(DataPoint.Pressure)},{nameof(DataPoint.Humidity)},{nameof(DataPoint.Brightness)},{nameof(DataPoint.Precipitation)} from sensor where Date >= {StartDatePicker.Date.AddDays(-1).ToSQLString()} and Date <= {EndDatePicker.Date.AddDays(1).AddHours(11.99999).ToSQLString()}");

				// Fill the datapoint array
				DataPoint[] Models = new DataPoint[Values?.Count ?? 0];
				for (int i = 0; i < Values.Count; i++)
				{
					Models[i] = new DataPoint((DateTime)Values[i][0], (float)Values[i][1], (float)Values[i][2], (float)Values[i][3], (float)Values[i][4], (float)Values[i][5]);
				}

				// Set the binding context so the chart knows about the data
				this.BindingContext = new DataPointCollection(Models);

				// Set the x axis min and max
				((DateTimeAxis)ChartView.PrimaryAxis).Minimum = StartDatePicker.Date;
				((DateTimeAxis)ChartView.PrimaryAxis).Maximum = EndDatePicker.Date.AddHours(11.999999);

				if (Models.Length > 0)
				{
					// Keep temperature from <min - >max
					double MinimumTemperature = Math.Floor(Models.Min((x) => x.Temperature)) - 1;
					double MaximumTemperature = Math.Ceiling(Models.Max((x) => x.Temperature)) + 1;
					double DeltaTemperature = MaximumTemperature - MinimumTemperature;
					((NumericalAxis)TemperatureSeries.YAxis).Minimum = MinimumTemperature;
					((NumericalAxis)TemperatureSeries.YAxis).Maximum = MaximumTemperature;
					((NumericalAxis)TemperatureSeries.YAxis).Interval = DeltaTemperature / 10;

					// Keep pressure from <min - >max
					double MinimumPressure = 900;
					double MaximumPressure = 1100;
					double DeltaPressure = MaximumPressure - MinimumPressure;
					((NumericalAxis)PressureSeries.YAxis).Minimum = MinimumPressure;
					((NumericalAxis)PressureSeries.YAxis).Maximum = MaximumPressure;
					((NumericalAxis)PressureSeries.YAxis).Interval = DeltaPressure / 10;

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
					double MaximumPrecipitation = Math.Ceiling(Models.Max((x) => x.Precipitation));
					double DeltaPrecipitation = MaximumPrecipitation;
					((NumericalAxis)PrecipitationSeries.YAxis).Minimum = 0;
					((NumericalAxis)PrecipitationSeries.YAxis).Maximum = MaximumPrecipitation;
					((NumericalAxis)PrecipitationSeries.YAxis).Interval = DeltaPrecipitation / 10;
				}
			}

			// Set the visibilities
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
			// Push a new options page on the navigation stack
			Navigation.PushAsync(new ChartOptionsPage(this));
		}

		/// <summary>
		/// Class used for storing the query'd rows
		/// </summary>
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

		/// <summary>
		/// Class for binding the data to the chart
		/// </summary>
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
