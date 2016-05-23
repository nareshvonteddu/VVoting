using System;
using System.Collections.Generic;
using Xamarin.Forms;
using VVoting.ViewModels;

namespace VVoting.Views
{
	public partial class MainPageView : ContentPage
	{
		public MainPageView (MainPageViewModel viewModel)
		{
			InitializeComponent ();
			BindingContext = viewModel;
			//ClickMeButton.Clicked +=  butttonClicked;
		}

	}
}

