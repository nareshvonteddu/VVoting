using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using Autofac;
using System.Reactive.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace VVoting
{
	public class TrendingViewModel : ViewModelBase
	{
		private string  _clickCount = "";
		CacheObject cacheObjectOld;
		CacheObject cacheObjectNew = new CacheObject ();
		Cache cache;
		AzureDataService dataService;
		VoteCount voteCount = new VoteCount();

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

			cacheObjectOld = new CacheObject();
			using (var scope = App.container.BeginLifetimeScope ()) {  
				cache = App.container.Resolve<Cache> ();
				dataService = App.container.Resolve<AzureDataService> ();
			}

			//LoadVoteCountAsync ();
			LoadCacheDataAsync ();

		}

		private async Task<VoteCount> LoadVoteCountAsync()
		{
			await dataService.Initialize();

			var voteCounts = await dataService.GetVoteCount ();
			if(voteCounts != null && voteCounts.Count > 0)
			{
				voteCount = voteCounts [0];
			}
			return voteCount;
		}

		private async void LoadCacheDataAsync()
		{
			try
			{
				cacheObjectOld = await cache.GetObject<CacheObject>("first");
			}
			catch(Exception) 
			{
				cacheObjectOld = new CacheObject ();
			}

			if (cacheObjectOld != null) 
			{
				if (cacheObjectOld.VoteForIndex == 1) {
					DemocraticBorderColor = Color.Green;
				} else {
					RepublicBorderColor = Color.Green;
				}

				SelectedRace = cacheObjectOld.RaceIndex;
				SelectedGender = cacheObjectOld.GenderIndex;
			}
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

							cacheObjectNew.VoteForIndex = 1;

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

							cacheObjectNew.VoteForIndex = 2;
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
							cacheObjectNew.GenderIndex = SelectedGender;
							cacheObjectNew.RaceIndex = SelectedRace;
							cache.InsertObject<CacheObject>("first",cacheObjectNew);

							UpdateToCloudAndCache();

						})); 
			} 
		}

		private async Task UpdateToCloudAndCache()
		{
			VoteCount vc = await LoadVoteCountAsync ();
			if (vc != null) {
				if (cacheObjectOld != null && cacheObjectOld.VoteForIndex != 0) {
					if (cacheObjectOld.VoteForIndex == 1) {
						if (vc.DemTotal != 0)
							vc.DemTotal--;
						if (cacheObjectOld.GenderIndex == 1 && vc.DemMale != 0)
							vc.DemMale--;
						if (cacheObjectOld.GenderIndex == 2 && vc.DemFemale != 0)
							vc.DemFemale--;
						if (cacheObjectOld.RaceIndex == 1 && vc.DemOther != 0)
							vc.DemOther--;
						if (cacheObjectOld.RaceIndex == 2 && vc.DemWhite != 0)
							vc.DemWhite--;
						if (cacheObjectOld.RaceIndex == 3 && vc.DemAfricanAmerican != 0)
							vc.DemAfricanAmerican--;
						if (cacheObjectOld.RaceIndex == 4 && vc.DemAsianAmerican != 0)
							vc.DemAsianAmerican--;
						if (cacheObjectOld.RaceIndex == 5 && vc.DemHispanic != 0)
							vc.DemHispanic--;
					}
					if (cacheObjectOld.VoteForIndex == 2) {
						if (vc.RepTotal != 0)
							vc.RepTotal--;
						if (cacheObjectOld.GenderIndex == 1 && vc.RepMale != 0)
							vc.RepMale--;
						if (cacheObjectOld.GenderIndex == 2 && vc.RepFemale != 0)
							vc.RepFemale--;
						if (cacheObjectOld.RaceIndex == 1 && vc.RepOther != 0)
							vc.RepOther--;
						if (cacheObjectOld.RaceIndex == 2 && vc.RepWhite != 0)
							vc.RepWhite--;
						if (cacheObjectOld.RaceIndex == 3 && vc.RepAfricanAmerican != 0)
							vc.RepAfricanAmerican--;
						if (cacheObjectOld.RaceIndex == 4 && vc.RepAsianAmerican != 0)
							vc.RepAsianAmerican--;
						if (cacheObjectOld.RaceIndex == 5 && vc.RepHispanic != 0)
							vc.RepHispanic--;
					}
				}
				if (cacheObjectNew != null && cacheObjectNew.VoteForIndex != 0) {
					if (cacheObjectNew.VoteForIndex == 1) {
						vc.DemTotal++;
						if (cacheObjectNew.GenderIndex == 1)
							vc.DemMale++;
						if (cacheObjectNew.GenderIndex == 2)
							vc.DemFemale++;
						if (cacheObjectNew.RaceIndex == 1)
							vc.DemOther++;
						if (cacheObjectNew.RaceIndex == 2)
							vc.DemWhite++;
						if (cacheObjectNew.RaceIndex == 3)
							vc.DemAfricanAmerican++;
						if (cacheObjectNew.RaceIndex == 4)
							vc.DemAsianAmerican++;
						if (cacheObjectNew.RaceIndex == 5)
							vc.DemHispanic++;
					}
					if (cacheObjectNew.VoteForIndex == 2) {
						vc.RepTotal++;
						if (cacheObjectNew.GenderIndex == 1)
							vc.RepMale++;
						if (cacheObjectNew.GenderIndex == 2)
							vc.RepFemale++;
						if (cacheObjectNew.RaceIndex == 1)
							vc.RepOther++;
						if (cacheObjectNew.RaceIndex == 2)
							vc.RepWhite++;
						if (cacheObjectNew.RaceIndex == 3)
							vc.RepAfricanAmerican++;
						if (cacheObjectNew.RaceIndex == 4)
							vc.RepAsianAmerican++;
						if (cacheObjectNew.RaceIndex == 5)
							vc.RepHispanic++;
					}
				}

				dataService.UpdateVoteCount (voteCount);
				LoadCacheDataAsync ();
			}
		}
	}
}

