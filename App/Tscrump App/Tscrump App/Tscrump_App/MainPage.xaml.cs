using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tscrump_App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			var x = DatabaseManager.Instance.ExecuteReader($"Select * from dummysensorvalues");

			var Values = DatabaseManager.Instance.ExecuteReader($"Select Date,Temperature,Pressure from dummysensorvalues");

			for (int i = 0; i < x.Count; i++)
			{
				for (int j = 0; j < x[i].Length; j++)
				{
					TestLabel.Text += x[i][j].ToString() + "    ";
				}
				TestLabel.Text += "\n";
			}

			Model[] Models = new Model[Values.Count];
			for (int i = 0; i < Values.Count; i++)
			{
				Models[i] = new Model((DateTime)Values[i][0], (float)Values[i][1], (float)Values[i][2]);
			}

			this.BindingContext = new ViewModel(Models);

			DateTimeAxis primaryAxis = new DateTimeAxis() { Minimum = Models.Select((m) => m.Time).Min(), Maximum = Models.Select((m) => m.Time).Max() };
			ChartView.PrimaryAxis = primaryAxis;

			//NumericalAxis secondaryAxis = new NumericalAxis() {  };
			//ChartView.SecondaryAxis = secondaryAxis;

			NumericalAxis LineAxis1 = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Temperature (C)", TextColor = Color.Blue } };
			NumericalAxis LineAxis2 = new NumericalAxis() { Title = new ChartAxisTitle() { Text = "Pressure (bar)", TextColor = Color.Red } };

			LineSeries Line = new LineSeries() { XAxis = primaryAxis, YAxis = LineAxis1, Color = Color.Blue };
			Line.SetBinding(ChartSeries.ItemsSourceProperty, "Data");
			Line.XBindingPath = "Time";
			Line.YBindingPath = "Temperature";
			ChartView.Series.Add(Line);

			LineSeries Line2 = new LineSeries() { XAxis = primaryAxis, YAxis = LineAxis2, Color = Color.Red };
			Line2.SetBinding(ChartSeries.ItemsSourceProperty, "Data");
			Line2.XBindingPath = "Time";
			Line2.YBindingPath = "Pressure";
			ChartView.Series.Add(Line2);
		}

		class Model
		{
			public DateTime Time { get; set; }
			public float Temperature { get; set; }
			public float Pressure { get; set; }

			public Model(DateTime Time, float Temperature, float Pressure)
			{
				this.Time = Time;
				this.Temperature = Temperature;
				this.Pressure = Pressure;
			}
		}

		class ViewModel
		{
			public List<Model> Data { get; set; }

			public ViewModel(Model[] Data)
			{
				this.Data = Data.ToList();
			}
		}
	}
}
