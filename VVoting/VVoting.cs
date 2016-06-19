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
			//Akavache.BlobCache.ApplicationName = "vvoting";
			//MainPage = new VVoting.Views.MainPageView ();

			ContainerBuilder builder = new ContainerBuilder ();

			builder.RegisterType<EventRaiser>().As<IEventInterface>().SingleInstance();
			builder.RegisterType<MainPageViewModel>();
			builder.RegisterType<MainPageView> ();
			builder.RegisterType<TrendingViewModel> ().SingleInstance();
			builder.RegisterType<TrendingPageView> ().SingleInstance ();
			builder.RegisterType<StatsPageViewModel> ().SingleInstance();
			builder.RegisterType<StatsPageView> ().SingleInstance ();
			builder.RegisterType<Cache> ().SingleInstance ();
			builder.RegisterType<AzureDataService> ().SingleInstance ();

			container = builder.Build ();


			using (var scope = container.BeginLifetimeScope())
			{
				var page = container.Resolve<MainPageView>();
				MainPage = page;
			}
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

