using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using Autofac;
using System.Threading.Tasks;
using VVoting.Views;
using System.Threading;

namespace VVoting
{
	public class TrendingViewModel : ViewModelBase
	{
		CacheObject cacheObjectOld;
		CacheObject cacheObjectNew = new CacheObject ();
		Cache cache;
		AzureDataService dataService;
		VoteCount voteCount = new VoteCount();

		//private string _status = "";
		//public string Status
		//{
		//	get { return _status; }
		//	set
		//	{
		//		if (Set(() => Status, ref _status, value))
		//		{
		//			RaisePropertyChanged(() => Status);
		//		}
		//	}
		//}

		private bool _isBusy = false;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (Set(() => IsBusy, ref _isBusy, value))
				{
					RaisePropertyChanged(() => IsBusy);
				}
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

		public TrendingViewModel (IEventInterface iev)
		{

			cacheObjectOld = new CacheObject();
			using (var scope = App.container.BeginLifetimeScope ()) {  
				cache = App.container.Resolve<Cache> ();
				dataService = App.container.Resolve<AzureDataService> ();
			}
			//iev.YourVoteTabOpened += TabOpenedMethod;
			TabOpenedMethod();
		}

		private async Task TabOpenedMethod()//(object s, EventArgs e)
		{
			await LoadCacheDataAsync();
			await LoadVoteCountAsync();
		}


		private async Task LoadVoteCountAsync()
		{
			try
			{
				dataService.Initialize();
				var voteCounts = await dataService.GetVoteCount();
				if (voteCounts != null && voteCounts.Count > 0)
				{
					voteCount = voteCounts[0];
				}
				await cache.InsertObject("second", voteCount);
			}
			catch(Exception)
			{
				//Status += ex.Message;
			}
		}

		private async Task LoadCacheDataAsync()
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
				if (cacheObjectOld.VoteForIndex == 1) 
				{
					DemocraticBorderColor = Color.Green;
					RepublicBorderColor = Color.Gray;
				} 
				else if (cacheObjectOld.VoteForIndex == 2)
				{
					RepublicBorderColor = Color.Green;
					DemocraticBorderColor = Color.Gray;
				}

				SelectedRace = cacheObjectOld.RaceIndex;
				SelectedGender = cacheObjectOld.GenderIndex;
			}
		}

		private RelayCommand _tapDemocratic; 

		public RelayCommand TapDemocratic { 
			get { 
				return _tapDemocratic
					?? (_tapDemocratic = new RelayCommand (
						() => 
						{ 
							DemocraticBorderColor = Color.Green; 
							RepublicBorderColor = Color.Gray;

							cacheObjectNew.VoteForIndex = 1;
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
							DemocraticBorderColor = Color.Gray; 
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

							 UpdateToCloudAndCache();
							
						})); 
			} 
		}



		private async Task UpdateToCloudAndCache()
		{
			IsBusy = true;
			try
			{
				await cache.InsertObject<CacheObject>("first", cacheObjectNew);

				if (voteCount != null && !string.IsNullOrEmpty(voteCount.Id))
				{
					updateVoteCountValues();
					await dataService.UpdateVoteCount(voteCount);
					await cache.InsertObject<VoteCount>("second", voteCount);

				}
				await LoadCacheDataAsync();
				IsBusy = false;
			}
			catch (Exception)
			{
				//Status = e.Message;
			}
			finally
			{
				IsBusy = false;
			}

		}

		void updateVoteCountValues()
		{
			if (cacheObjectOld != null && cacheObjectOld.VoteForIndex != 0)
			{
				if (cacheObjectOld.VoteForIndex == 1)
				{
					if (voteCount.DemTotal != 0)
						voteCount.DemTotal--;
					if (cacheObjectOld.GenderIndex == 1 && voteCount.DemMale != 0)
						voteCount.DemMale--;
					if (cacheObjectOld.GenderIndex == 2 && voteCount.DemFemale != 0)
						voteCount.DemFemale--;
					if (cacheObjectOld.RaceIndex == 1 && voteCount.DemOther != 0)
						voteCount.DemOther--;
					if (cacheObjectOld.RaceIndex == 2 && voteCount.DemWhite != 0)
						voteCount.DemWhite--;
					if (cacheObjectOld.RaceIndex == 3 && voteCount.DemAfricanAmerican != 0)
						voteCount.DemAfricanAmerican--;
					if (cacheObjectOld.RaceIndex == 4 && voteCount.DemAsianAmerican != 0)
						voteCount.DemAsianAmerican--;
					if (cacheObjectOld.RaceIndex == 5 && voteCount.DemHispanic != 0)
						voteCount.DemHispanic--;
				}
				if (cacheObjectOld.VoteForIndex == 2)
				{
					if (voteCount.RepTotal != 0)
						voteCount.RepTotal--;
					if (cacheObjectOld.GenderIndex == 1 && voteCount.RepMale != 0)
						voteCount.RepMale--;
					if (cacheObjectOld.GenderIndex == 2 && voteCount.RepFemale != 0)
						voteCount.RepFemale--;
					if (cacheObjectOld.RaceIndex == 1 && voteCount.RepOther != 0)
						voteCount.RepOther--;
					if (cacheObjectOld.RaceIndex == 2 && voteCount.RepWhite != 0)
						voteCount.RepWhite--;
					if (cacheObjectOld.RaceIndex == 3 && voteCount.RepAfricanAmerican != 0)
						voteCount.RepAfricanAmerican--;
					if (cacheObjectOld.RaceIndex == 4 && voteCount.RepAsianAmerican != 0)
						voteCount.RepAsianAmerican--;
					if (cacheObjectOld.RaceIndex == 5 && voteCount.RepHispanic != 0)
						voteCount.RepHispanic--;
				}
			}
			if (cacheObjectNew != null && cacheObjectNew.VoteForIndex != 0)
			{
				if (cacheObjectNew.VoteForIndex == 1)
				{
					voteCount.DemTotal++;
					if (cacheObjectNew.GenderIndex == 1)
						voteCount.DemMale++;
					if (cacheObjectNew.GenderIndex == 2)
						voteCount.DemFemale++;
					if (cacheObjectNew.RaceIndex == 1)
						voteCount.DemOther++;
					if (cacheObjectNew.RaceIndex == 2)
						voteCount.DemWhite++;
					if (cacheObjectNew.RaceIndex == 3)
						voteCount.DemAfricanAmerican++;
					if (cacheObjectNew.RaceIndex == 4)
						voteCount.DemAsianAmerican++;
					if (cacheObjectNew.RaceIndex == 5)
						voteCount.DemHispanic++;
				}
				if (cacheObjectNew.VoteForIndex == 2)
				{
					voteCount.RepTotal++;
					if (cacheObjectNew.GenderIndex == 1)
						voteCount.RepMale++;
					if (cacheObjectNew.GenderIndex == 2)
						voteCount.RepFemale++;
					if (cacheObjectNew.RaceIndex == 1)
						voteCount.RepOther++;
					if (cacheObjectNew.RaceIndex == 2)
						voteCount.RepWhite++;
					if (cacheObjectNew.RaceIndex == 3)
						voteCount.RepAfricanAmerican++;
					if (cacheObjectNew.RaceIndex == 4)
						voteCount.RepAsianAmerican++;
					if (cacheObjectNew.RaceIndex == 5)
						voteCount.RepHispanic++;
				}
			}
		}
}
}

