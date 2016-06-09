using System;
using System.Collections.Generic;
using Autofac;

using Xamarin.Forms;

namespace VVoting
{
	public partial class TrendingPageView : ContentPage
	{
		public TrendingPageView (TrendingViewModel viewModel)
		{
			InitializeComponent ();
			BindingContext = viewModel;

			DoneButton.Clicked += (object sender, EventArgs e) => 
			{
				App.container.Resolve<StatsPageViewModel>().LoadVoteCountToCharts();
				App.container.Resolve<Views.MainPageView>().CurrentPage = App.container.Resolve<Views.MainPageView>().Children[1];
			};
		}
	}
}

