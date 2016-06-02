using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VVoting
{
	public partial class TrendingPageView : ContentPage
	{
		public TrendingPageView (TrendingViewModel viewModel)
		{
			InitializeComponent ();
			BindingContext = viewModel;
		}
	}
}

