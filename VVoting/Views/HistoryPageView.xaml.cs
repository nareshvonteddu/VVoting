using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VVoting
{
	public partial class HistoryPageView : ContentPage
	{
		public HistoryPageView (HistoryPageViewModel viewModel)
		{
			InitializeComponent ();
			BindingContext = viewModel;
		}
	}
}

