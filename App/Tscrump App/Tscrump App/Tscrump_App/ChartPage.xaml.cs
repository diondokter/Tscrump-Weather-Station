using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class ChartPage : ContentPage
	{
		public ChartPage()
		{
			InitializeComponent();

			var Values = DatabaseManager.Instance.ExecuteReader($"Select Date,Temperature,Pressure from dummysensorvalues");

			DataPoint[] Models = new DataPoint[Values.Count];
			for (int i = 0; i < Values.Count; i++)
			{
				Models[i] = new DataPoint((DateTime)Values[i][0], (float)Values[i][1], (float)Values[i][2]);
			}

			this.BindingContext = new DataPointCollection(Models);

			DateTimeAxis primaryAxis = new DateTimeAxis() { Minimum = Models.Select((m) => m.Time).Min(), Maximum = Models.Select((m) => m.Time).Max(), IntervalType = DateTimeIntervalType.Days, Interval = 1, LabelsIntersectAction = AxisLabelsIntersectAction.Hide, EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Hide, MinorTicksPerInterval = 12 };
			ChartView.PrimaryAxis = primaryAxis;

			//NumericalAxis secondaryAxis = new NumericalAxis() {  };
			//ChartView.SecondaryAxis = secondaryAxis;

			NumericalAxis LineAxis1 = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Temperature (C)", TextColor = Color.Blue } };
			NumericalAxis LineAxis2 = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Pressure (bar)", TextColor = Color.Red } };

			StackingAreaSeries Line = new StackingAreaSeries() { XAxis = primaryAxis, YAxis = LineAxis1, Color = Color.Blue, Opacity = 0.5 };
			Line.SetBinding(ChartSeries.ItemsSourceProperty, nameof(DataPointCollection.Data));
			Line.XBindingPath = nameof(DataPoint.Time);
			Line.YBindingPath = nameof(DataPoint.Temperature);
			ChartView.Series.Add(Line);

			LineSeries Line2 = new LineSeries() { XAxis = primaryAxis, YAxis = LineAxis2, Color = Color.Red };
			Line2.SetBinding(ChartSeries.ItemsSourceProperty, nameof(DataPointCollection.Data));
			Line2.XBindingPath = nameof(DataPoint.Time);
			Line2.YBindingPath = nameof(DataPoint.Pressure);

			ChartView.Series.Add(Line2);

			Task.Run(() => ChartViewSizer());
		}

		private void ChartViewSizer()
		{
			while (true)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					ChartView.HeightRequest = ChartView.Width * 7/16;
				});

				Task.Delay(100).Wait();
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
