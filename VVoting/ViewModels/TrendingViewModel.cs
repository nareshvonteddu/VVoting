using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using Autofac;

namespace VVoting
{
	public class TrendingViewModel : ViewModelBase
	{
		private string  _clickCount = "";
		CacheObject cacheObject;
		Cache cache;

		public string ClickCount 
		{
			get{ return _clickCount; } 
			set
			{
				Set (() => ClickCount, ref _clickCount, value);
					
			}
		}


		private Color _democraticBorderColor = Color.Gray;
		public Color DemocraticBorderColor 
		{
			get{ return _democraticBorderColor; } 
			set
			{
				if (Set (() => DemocraticBorderColor, ref _democraticBorderColor, value)) 
				{
					RaisePropertyChanged (() => DemocraticBorderColor);
				}
			}
		}

		private Color _republicBorderColor = Color.Gray;
		public Color RepublicBorderColor 
		{
			get{ return _republicBorderColor; } 
			set
			{
				Set (() => RepublicBorderColor, ref _republicBorderColor, value);
			}
		}

//
//		private List<string> _genderOptions = new List<string>();
//		public List<string> GenderOptions 
//		{
//			get{ return _genderOptions; }
//			set
//			{
//				Set (() => GenderOptions, ref _genderOptions, value);
//			}
//		}

		private int _selectedGender;
		public int SelectedGender 
		{
			get{ return _selectedGender; }
			set
			{
				Set (() => SelectedGender, ref _selectedGender, value);
			}
		}

		private int _selectedRace;
		public int SelectedRace 
		{
			get{ return _selectedRace; }
			set
			{
				Set (() => SelectedRace, ref _selectedRace, value);
			}
		}

		public TrendingViewModel ()
		{
//			GenderOptions.Add ("Male");
//			GenderOptions.Add ("Female");

			cacheObject = new CacheObject();
			using (var scope = App.container.BeginLifetimeScope ()) {  
				cache = App.container.Resolve<Cache> ();
			}

			LoadCacheDataAsync ();

		}

		private async void LoadCacheDataAsync()
		{
			try
			{
				cacheObject = await cache.GetObject<CacheObject>("first");
			}
			catch(Exception) 
			{
				cacheObject = new CacheObject ();
			}

			if (cacheObject.VoteForIndex == 0) {
				DemocraticBorderColor = Color.Green;
			} else {
				RepublicBorderColor = Color.Green;
			}

			SelectedRace = cacheObject.RaceIndex;
			SelectedGender = cacheObject.GenderIndex;
		}


		//int i = 0;
		private RelayCommand _tapDemocratic; 

		public RelayCommand TapDemocratic { 
			get { 
				return _tapDemocratic
					?? (_tapDemocratic = new RelayCommand (
						() => 
						{ 
							DemocraticBorderColor = Color.Green; //String.Format("Clicked {0} times", i++);
							RepublicBorderColor = Color.Gray;

							cacheObject.VoteForIndex = 0;

//							ClickCount = String.Format("you have clicke it {0} times", i);
//							i++;
						})); 
			} 
		}

		private RelayCommand _tapRepublic; 

		public RelayCommand TapRepublic { 
			get { 
				return _tapRepublic
					?? (_tapRepublic = new RelayCommand (
						() => 
						{ 
							DemocraticBorderColor = Color.Gray; //String.Format("Clicked {0} times", i++);
							RepublicBorderColor = Color.Green;

							cacheObject.VoteForIndex = 1;
						})); 
			} 
		}

		private RelayCommand _doneTaped;
		public RelayCommand DoneTaped { 
			get { 
				return _doneTaped
					?? (_doneTaped = new RelayCommand (
						() => 
						{ 
							cacheObject.GenderIndex = SelectedGender;
							cacheObject.RaceIndex = SelectedRace;
							cache.InsertObject<CacheObject>("first",cacheObject);

						})); 
			} 
		}
	}
}

