using System;
using System.Collections.Generic;
using Xamarin.Forms;
using VVoting.ViewModels;
using Autofac;

namespace VVoting.Views
{
	public partial class MainPageView : TabbedPage
	{
		public MainPageView (MainPageViewModel viewModel)
		{
			InitializeComponent ();
			BindingContext = viewModel;

			using (var scope = App.container.BeginLifetimeScope ()) {  
				this.Children.Add(App.container.Resolve<TrendingPageView> ());
				this.Children.Add(App.container.Resolve<HistoryPageView> ());
			}


			//this.Children.Add (new TrendingPageView (){ Title = "Trending" });
			//this.Children.Add (new HistoryPageView (){ Title = "History" });
		}

	}
}

