using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class ChartPage : ContentPage
	{
		StackingAreaSeries TemperatureSeries;
		LineSeries PressureSeries;

		public ChartPage()
		{
			InitializeComponent();

			StartDatePicker.Date = DateTime.Today.AddDays(-7);
			EndDatePicker.Date = DateTime.Today;

			StartDatePicker.DateSelected += Update;
			EndDatePicker.DateSelected += Update;

			ChartView.PrimaryAxis = new DateTimeAxis() { LabelsIntersectAction = AxisLabelsIntersectAction.Hide, EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Hide, LabelStyle = new ChartAxisLabelStyle() { LabelFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " - " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern } };

			NumericalAxis TemperatureAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Temperature (C)", TextColor = Color.Blue }, RangePadding = NumericalPadding.Additional };

			TemperatureSeries = new StackingAreaSeries() { YAxis = TemperatureAxis, Color = Color.Blue, Opacity = 0.5 };
			TemperatureSeries.SetBinding(ChartSeries.ItemsSourceProperty, nameof(DataPointCollection.Data));
			TemperatureSeries.XBindingPath = nameof(DataPoint.Time);
			TemperatureSeries.YBindingPath = nameof(DataPoint.Temperature);

			ChartView.Series.Add(TemperatureSeries);


			NumericalAxis PressureAxis = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Pressure (bar)", TextColor = Color.Red }, RangePadding = NumericalPadding.Additional };

			PressureSeries = new LineSeries() {YAxis = PressureAxis, Color = Color.Red };
			PressureSeries.SetBinding(ChartSeries.ItemsSourceProperty, nameof(DataPointCollection.Data));
			PressureSeries.XBindingPath = nameof(DataPoint.Time);
			PressureSeries.YBindingPath = nameof(DataPoint.Pressure);

			ChartView.Series.Add(PressureSeries);

			SizeChanged += ChartViewSizer;
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
			ChartViewSizer(null, null);
			Update();
		}

		private void ChartViewSizer(object s, EventArgs e)
		{
			ChartView.HeightRequest = this.Height - ChartView.Margin.VerticalThickness;
		}

		private void Update(object s, EventArgs e)
		{
			Update();
		}

		public async void Update()
		{
			if (await DatabaseManager.GetInstance() == null)
			{
				return;
			}

			var Values = (await DatabaseManager.GetInstance()).ExecuteReader($"Select Date,Temperature,Pressure from dummysensorvalues where Date >= {StartDatePicker.Date.ToSQLString()} and Date <= {EndDatePicker.Date.AddHours(11.99999).ToSQLString()}");

			DataPoint[] Models = new DataPoint[Values.Count];
			for (int i = 0; i < Values.Count; i++)
			{
				Models[i] = new DataPoint((DateTime)Values[i][0], (float)Values[i][1], (float)Values[i][2]);
			}

			this.BindingContext = new DataPointCollection(Models);

			((DateTimeAxis)ChartView.PrimaryAxis).Minimum = StartDatePicker.Date;
			((DateTimeAxis)ChartView.PrimaryAxis).Maximum = EndDatePicker.Date.AddHours(11.999999);

			TemperatureSeries.IsVisible = TemperatureSwitch.IsToggled;
			TemperatureSeries.YAxis.IsVisible = TemperatureSwitch.IsToggled;

			PressureSeries.IsVisible = PressureSwitch.IsToggled;
			PressureSeries.YAxis.IsVisible = PressureSwitch.IsToggled;

			if (Models.Length > 0)
			{
				float MinimumTemperature = Models.Min((x) => x.Temperature);
				float MaximumTemperature = Models.Max((x) => x.Temperature);
				float DeltaTemperature = MaximumTemperature - MinimumTemperature;
				((NumericalAxis)TemperatureSeries.YAxis).Minimum = MinimumTemperature - DeltaTemperature / 20;
				((NumericalAxis)TemperatureSeries.YAxis).Maximum = MaximumTemperature + DeltaTemperature / 20;
				((NumericalAxis)TemperatureSeries.YAxis).Interval = DeltaTemperature / ChartView.HeightRequest * 20;

				float MinimumPressure = Models.Min((x) => x.Pressure);
				float MaximumPressure = Models.Max((x) => x.Pressure);
				float DeltaPressure = MaximumPressure - MinimumPressure;
				((NumericalAxis)PressureSeries.YAxis).Minimum = MinimumPressure - DeltaPressure / 20;
				((NumericalAxis)PressureSeries.YAxis).Maximum = MaximumPressure + DeltaPressure / 20;
				((NumericalAxis)PressureSeries.YAxis).Interval = DeltaPressure / ChartView.HeightRequest * 20;
			}
		}

		class DataPoint
		{
			public DateTime Time { get; set; }
			public float Temperature { get; set; }
			public float Pressure { get; set; }

			public DataPoint(DateTime Time, float Temperature, float Pressure)
			{
				this.Time = Time;
				this.Temperature = Temperature;
				this.Pressure = Pressure;
			}
		}

		class DataPointCollection
		{
			public List<DataPoint> Data { get; set; }

			public DataPointCollection(DataPoint[] Data)
			{
				this.Data = Data.ToList();
			}
		}

	}
}
