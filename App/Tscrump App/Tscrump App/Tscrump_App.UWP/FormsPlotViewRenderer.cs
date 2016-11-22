using OxyPlot.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tscrump_App;
using Tscrump_App.UWP;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using Windows.UI.Xaml.Media;
using Windows.UI;

[assembly : ExportRenderer(typeof(FormsPlotView), typeof(FormsPlotViewRenderer))]
namespace Tscrump_App.UWP
{
	public class FormsPlotViewRenderer : ViewRenderer<FormsPlotView, PlotView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<FormsPlotView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new PlotView() { Height = 200, Width = 200 });

				Control.Model = new OxyPlot.PlotModel();

				Element.Model = Control.ActualModel;
				Element.Controller = Control.ActualController;
			}
		}

		protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
			{
				Control.UpdateLayout();
			});
		}
	}
}
