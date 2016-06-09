using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VVoting
{
	public partial class StatsPageView : ContentPage
	{
		public StatsPageView (StatsPageViewModel statsPageViewModel)
		{
			InitializeComponent ();
			BindingContext = statsPageViewModel;
			//SizeRequest sz = BVDEM.Measure (0,0,MeasureFlags.None);
			//BVDEM
		}


	}
}

