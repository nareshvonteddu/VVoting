using System;
using System.Collections.Generic;
using Xamarin.Forms;
using VVoting.ViewModels;
using Autofac;

namespace VVoting.Views
{
	
	public partial class MainPageView : TabbedPage
	{
		IEventInterface iev;
		public MainPageView (MainPageViewModel viewModel, IEventInterface _iev)
		{
			InitializeComponent ();
			BindingContext = viewModel;

			iev = _iev;

			using (var scope = App.container.BeginLifetimeScope())
			{
				this.Children.Add(App.container.Resolve<TrendingPageView>());
				this.Children.Add(App.container.Resolve<StatsPageView>());
			}

			this.CurrentPageChanged += (object sender, EventArgs e) => 
			{
				if (((MainPageView)sender).CurrentPage.Title == "Trends")
				{
					iev.OnTrendsTabOpened();
				}
				if (((MainPageView)sender).CurrentPage.Title == "Your Vote")
				{
					iev.OnYourVoteTapOpened();
				}
			};
		}



	}
}

