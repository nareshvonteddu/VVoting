using System;
using Xamarin.Forms;

namespace VVoting
{
	public class ImageCircle : Image
	{
		public static readonly BindableProperty BorderColorProperty = 
			BindableProperty.Create("BorderColor", typeof(Color),typeof(ImageCircle),Color.Red,BindingMode.TwoWay,null,
			BorderColorChanged);
			

		public Color BorderColor
		{
			get{ return (Color)base.GetValue (BorderColorProperty);}
			set{ base.SetValue (BorderColorProperty, value);}
		}

		protected static void BorderColorChanged(BindableObject obj, object oldValue, object newValue)
		{
			//((ImageCircle)obj).BorderColor = newValue;
		}
	}
}

