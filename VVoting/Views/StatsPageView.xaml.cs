using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Autofac;

namespace VVoting
{
	public partial class StatsPageView : ContentPage
	{
		public StatsPageView (StatsPageViewModel statsPageViewModel)
		{
			InitializeComponent ();
			BindingContext = statsPageViewModel;
		}
	}
}

