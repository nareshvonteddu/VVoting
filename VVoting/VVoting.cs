using System;
using Autofac;
using Xamarin.Forms;
using VVoting.ViewModels;
using VVoting.Views;

namespace VVoting
{
	public class App : Application
	{
		public App ()
		{

			//MainPage = new VVoting.Views.MainPageView ();

			ContainerBuilder builder = new ContainerBuilder ();
			builder.RegisterType<MainPageViewModel>();
			builder.RegisterType<MainPageView> ().SingleInstance ();

			IContainer c = builder.Build ();



			var page = c.Resolve<MainPageView> ();
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

