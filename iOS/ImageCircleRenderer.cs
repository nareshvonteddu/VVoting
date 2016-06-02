using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using VVoting;
using Xamarin.Forms;
using VVoting.iOS;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]
namespace VVoting.iOS
{
	public class ImageCircleRenderer : ImageRenderer
	{
		public ImageCircleRenderer ()
		{
		}

		private void CreateCircle()
		{
			try
			{
				double min = Math.Min(Element.Width, Element.Height);
				Control.Layer.CornerRadius = (float)(min / 2.0) ;
				Control.Layer.MasksToBounds = false;
				Control.Layer.BorderColor = ((ImageCircle)Element).BorderColor.ToCGColor(); //Color.Red.ToCGColor();
				Control.Layer.BorderWidth = 10;
				Control.ClipsToBounds = true;
			
			}
			catch(Exception ex)
			{
				Debug.WriteLine ("Unable to create circle image: " + ex);
			}
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Image> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null || Element == null)
				return;

			CreateCircle();
		}



		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.WidthProperty.PropertyName ||
				e.PropertyName == "BorderColor")
			{
				CreateCircle();
			}
		}
	}
}

