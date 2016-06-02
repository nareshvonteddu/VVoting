using System;
using Autofac;
using Xamarin.Forms;
using VVoting.ViewModels;
using VVoting.Views;

namespace VVoting
{
	public class App : Application
	{
		public static IContainer container { get; set; }
		public App ()
		{

			//MainPage = new VVoting.Views.MainPageView ();

			ContainerBuilder builder = new ContainerBuilder ();
			builder.RegisterType<MainPageViewModel>();
			builder.RegisterType<MainPageView> ().SingleInstance ();
			builder.RegisterType<TrendingViewModel> ();
			builder.RegisterType<TrendingPageView> ().SingleInstance ();
			builder.RegisterType<HistoryPageViewModel> ();
			builder.RegisterType<HistoryPageView> ().SingleInstance ();


			container = builder.Build ();



			var page = container.Resolve<MainPageView> ();
			MainPage = page;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

