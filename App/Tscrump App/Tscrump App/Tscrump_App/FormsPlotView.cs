using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace Tscrump_App
{
	public class FormsPlotView : View
	{
		public static readonly BindableProperty ModelProperty = BindableProperty.Create(
			propertyName: "Model",
			returnType: typeof(PlotModel),
			declaringType: typeof(FormsPlotView),
			defaultValue: null);

		public static readonly BindableProperty UpdateProperty = BindableProperty.Create(
			propertyName: "Update",
			returnType: typeof(long),
			declaringType: typeof(FormsPlotView),
			defaultValue: 0L);

		public PlotModel Model
		{
			get
			{
				return (PlotModel)GetValue(ModelProperty);
			}
			set
			{
				SetValue(ModelProperty, value);
			}
		}

		public static readonly BindableProperty ControllerProperty = BindableProperty.Create(
			propertyName: "Controller",
			returnType: typeof(IPlotController),
			declaringType: typeof(FormsPlotView),
			defaultValue: null);

		public IPlotController Controller
		{
			get
			{
				return (IPlotController)GetValue(ControllerProperty);
			}
			set
			{
				SetValue(ModelProperty, value);
			}
		}

		public void Update()
		{
			SetValue(UpdateProperty, (long)GetValue(UpdateProperty) + 1);
		}
	}
}