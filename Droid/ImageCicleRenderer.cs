using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using VVoting;
using Xamarin.Forms;
using VVoting.Droid;
using System.Diagnostics;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCicleRenderer))]
namespace VVoting.Droid
{
	public class ImageCicleRenderer : ImageRenderer
	{
		Canvas _canvas;
		global::Android.Views.View _child;
		long _drawingTime;

		public ImageCicleRenderer ()
		{
		}



		protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
		{
			try
			{
//				_canvas = canvas;
//				_child = child;
//				_drawingTime = drawingTime;

				var radius = Math.Min(Width, Height) / 2;
				var strokeWidth = 20;
				radius -= strokeWidth / 2;

				//Create path to clip
				var path = new Path();
				path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
				canvas.Save();
				canvas.ClipPath(path);

				var result = base.DrawChild(canvas, child, drawingTime);

				canvas.Restore();

				// Create path for circle border
				path = new Path();
				path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

				var paint = new Paint();
				paint.AntiAlias = true;
				paint.StrokeWidth = 20;
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color =  ((ImageCircle)Element).BorderColor.ToAndroid(); //global::Android.Graphics.Color.White;

				canvas.DrawPath(path, paint);

				//Properly dispose
				paint.Dispose();
				path.Dispose();



				return result;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
			}

			return base.DrawChild(canvas, child, drawingTime);
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == "BorderColor") 
			{
				this.Invalidate ();
			}
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{

				if ((int)Android.OS.Build.VERSION.SdkInt < 18)
					SetLayerType(LayerType.Software, null);
			}
		}

	}
}

