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
	public class StatsPageViewModel : ViewModelBase
	{
		AzureDataService dataService;
		VoteCount voteCount;

		private double _initWidth;
		public double InitWidth 
		{
			get{ return _initWidth; }
			set
			{
				Set (() => InitWidth, ref _initWidth, value);
			}
		}	

		private double _demOverallPercent;
		public double DemOverallPercent 
		{
			get{ return _demOverallPercent; }
			set
			{
				if (Set(() => DemOverallPercent, ref _demOverallPercent, value))
				{
					RaisePropertyChanged(() => DemOverallPercent);
				}
			}
		}	

		private double _demOverallWidth;
		public double DemOverallWidth 
		{
			get{ return _demOverallWidth; }
			set
			{
				Set (() => DemOverallWidth, ref _demOverallWidth, value);
			}
		}	


		private double _repOverallPercent;
		public double RepOverallPercent 
		{
			get{ return _repOverallPercent; }
			set
			{
				Set (() => RepOverallPercent, ref _repOverallPercent, value);
			}
		}	

		private double _repOverallWidth;
		public double RepOverallWidth 
		{
			get{ return _repOverallWidth; }
			set
			{
				Set (() => RepOverallWidth, ref _repOverallWidth, value);
			}
		}	

		private double _demMalePercent;
		public double DemMalePercent 
		{
			get{ return _demMalePercent; }
			set
			{
				Set (() => DemMalePercent, ref _demMalePercent, value);
			}
		}	

		private double _demMaleWidth;
		public double DemMaleWidth 
		{
			get{ return _demMaleWidth; }
			set
			{
				Set (() => DemMaleWidth, ref _demMaleWidth, value);
			}
		}	


		private double _repMalePercent;
		public double RepMalePercent 
		{
			get{ return _repMalePercent; }
			set
			{
				Set (() => RepMalePercent, ref _repMalePercent, value);
			}
		}	

		private double _repMaleWidth;
		public double RepMaleWidth 
		{
			get{ return _repMaleWidth; }
			set
			{
				Set (() => RepMaleWidth, ref _repMaleWidth, value);
			}
		}	

		private double _demFemalePercent;
		public double DemFemalePercent 
		{
			get{ return _demFemalePercent; }
			set
			{
				Set (() => DemFemalePercent, ref _demFemalePercent, value);
			}
		}	

		private double _demFemaleWidth;
		public double DemFemaleWidth 
		{
			get{ return _demFemaleWidth; }
			set
			{
				Set (() => DemFemaleWidth, ref _demFemaleWidth, value);
			}
		}	


		private double _repFemalePercent;
		public double RepFemalePercent 
		{
			get{ return _repFemalePercent; }
			set
			{
				Set (() => RepFemalePercent, ref _repFemalePercent, value);
			}
		}	

		private double _repFemaleWidth;
		public double RepFemaleWidth 
		{
			get{ return _repFemaleWidth; }
			set
			{
				Set (() => RepFemaleWidth, ref _repFemaleWidth, value);
			}
		}	

		private double _demWhitePercent;
		public double DemWhitePercent 
		{
			get{ return _demWhitePercent; }
			set
			{
				Set (() => DemWhitePercent, ref _demWhitePercent, value);
			}
		}	

		private double _demWhiteWidth;
		public double DemWhiteWidth 
		{
			get{ return _demWhiteWidth; }
			set
			{
				Set (() => DemWhiteWidth, ref _demWhiteWidth, value);
			}
		}	


		private double _repWhitePercent;
		public double RepWhitePercent 
		{
			get{ return _repWhitePercent; }
			set
			{
				Set (() => RepWhitePercent, ref _repWhitePercent, value);
			}
		}	

		private double _repWhiteWidth;
		public double RepWhiteWidth 
		{
			get{ return _repWhiteWidth; }
			set
			{
				Set (() => RepWhiteWidth, ref _repWhiteWidth, value);
			}
		}	

		private double _demAfricanAmericanPercent;
		public double DemAfricanAmericanPercent 
		{
			get{ return _demAfricanAmericanPercent; }
			set
			{
				Set (() => DemAfricanAmericanPercent, ref _demAfricanAmericanPercent, value);
			}
		}	

		private double _demAfricanAmericanWidth;
		public double DemAfricanAmericanWidth 
		{
			get{ return _demAfricanAmericanWidth; }
			set
			{
				Set (() => DemAfricanAmericanWidth, ref _demAfricanAmericanWidth, value);
			}
		}	


		private double _repAfricanAmericanPercent;
		public double RepAfricanAmericanPercent 
		{
			get{ return _repAfricanAmericanPercent; }
			set
			{
				Set (() => RepAfricanAmericanPercent, ref _repAfricanAmericanPercent, value);
			}
		}	

		private double _repAfricanAmericanWidth;
		public double RepAfricanAmericanWidth 
		{
			get{ return _repAfricanAmericanWidth; }
			set
			{
				Set (() => RepAfricanAmericanWidth, ref _repAfricanAmericanWidth, value);
			}
		}	

		private double _demAsianAmericanPercent;
		public double DemAsianAmericanPercent 
		{
			get{ return _demAsianAmericanPercent; }
			set
			{
				Set (() => DemAsianAmericanPercent, ref _demAsianAmericanPercent, value);
			}
		}	

		private double _demAsianAmericanWidth;
		public double DemAsianAmericanWidth 
		{
			get{ return _demAsianAmericanWidth; }
			set
			{
				Set (() => DemAsianAmericanWidth, ref _demAsianAmericanWidth, value);
			}
		}	


		private double _repAsianAmericanPercent;
		public double RepAsianAmericanPercent 
		{
			get{ return _repAsianAmericanPercent; }
			set
			{
				Set (() => RepAsianAmericanPercent, ref _repAsianAmericanPercent, value);
			}
		}	

		private double _repAsianAmericanWidth;
		public double RepAsianAmericanWidth 
		{
			get{ return _repAsianAmericanWidth; }
			set
			{
				Set (() => RepAsianAmericanWidth, ref _repAsianAmericanWidth, value);
			}
		}	

		private double _demHispanicPercent;
		public double DemHispanicPercent 
		{
			get{ return _demHispanicPercent; }
			set
			{
				Set (() => DemHispanicPercent, ref _demHispanicPercent, value);
			}
		}	

		private double _demHispanicWidth;
		public double DemHispanicWidth 
		{
			get{ return _demHispanicWidth; }
			set
			{
				Set (() => DemHispanicWidth, ref _demHispanicWidth, value);
			}
		}	


		private double _repHispanicPercent;
		public double RepHispanicPercent 
		{
			get{ return _repHispanicPercent; }
			set
			{
				Set (() => RepHispanicPercent, ref _repHispanicPercent, value);
			}
		}	

		private double _repHispanicWidth;
		public double RepHispanicWidth 
		{
			get{ return _repHispanicWidth; }
			set
			{
				Set (() => RepHispanicWidth, ref _repHispanicWidth, value);
			}
		}

		private double _demOtherPercent;
		public double DemOtherPercent 
		{
			get{ return _demOtherPercent; }
			set
			{
				Set (() => DemOtherPercent, ref _demOtherPercent, value);
			}
		}	

		private double _demOtherWidth;
		public double DemOtherWidth 
		{
			get{ return _demOtherWidth; }
			set
			{
				Set (() => DemOtherWidth, ref _demOtherWidth, value);
			}
		}	


		private double _repOtherPercent;
		public double RepOtherPercent 
		{
			get{ return _repOtherPercent; }
			set
			{
				Set (() => RepOtherPercent, ref _repOtherPercent, value);
			}
		}	

		private double _repOtherWidth;
		public double RepOtherWidth 
		{
			get{ return _repOtherWidth; }
			set
			{
				Set (() => RepOtherWidth, ref _repOtherWidth, value);
			}
		}

		public StatsPageViewModel ()
		{
			using (var scope = App.container.BeginLifetimeScope ()) { 
				dataService = App.container.Resolve<AzureDataService> ();
			}
			LoadVoteCountToCharts ();
		}

		public async Task LoadVoteCountToCharts()
		{
			await LoadVoteCountAsync ();
			if (voteCount != null) 
			{
				long totalCount = voteCount.DemTotal + voteCount.RepTotal;
				DemOverallPercent = GetPercentage (voteCount.DemTotal, totalCount);
				DemOverallWidth = GetWidth(DemOverallPercent);
				RepOverallPercent = GetPercentage (voteCount.RepTotal, totalCount);
				RepOverallWidth = GetWidth(RepOverallPercent);

				long maleTotalCount = voteCount.DemMale + voteCount.RepMale;
				DemMalePercent = GetPercentage (voteCount.DemMale, maleTotalCount);
				DemMaleWidth = GetWidth (DemMalePercent);
				RepMalePercent = GetPercentage (voteCount.RepMale, maleTotalCount);
				RepMaleWidth = GetWidth (RepMalePercent);

				long femaleTotalCount = voteCount.DemFemale + voteCount.RepFemale;
				DemFemalePercent = GetPercentage (voteCount.DemFemale, femaleTotalCount);
				DemFemaleWidth = GetWidth (DemFemalePercent);
				RepFemalePercent = GetPercentage (voteCount.RepFemale, femaleTotalCount);
				RepFemaleWidth = GetWidth (RepFemalePercent);

				long whiteTotalCount = voteCount.DemWhite + voteCount.RepWhite;
				DemWhitePercent = GetPercentage (voteCount.DemWhite, whiteTotalCount);
				DemWhiteWidth = GetWidth (DemWhitePercent);
				RepWhitePercent = GetPercentage (voteCount.RepWhite, whiteTotalCount);
				RepWhiteWidth = GetWidth (RepWhitePercent);

				long africanAmericanTotalCount = voteCount.DemAfricanAmerican + voteCount.RepAfricanAmerican;
				DemAfricanAmericanPercent = GetPercentage (voteCount.DemAfricanAmerican, africanAmericanTotalCount);
				DemAfricanAmericanWidth = GetWidth (DemAfricanAmericanPercent);
				RepAfricanAmericanPercent = GetPercentage (voteCount.RepAfricanAmerican, africanAmericanTotalCount);
				RepAfricanAmericanWidth = GetWidth (RepAfricanAmericanPercent);

				long asianAmericanTotalCount = voteCount.DemAsianAmerican + voteCount.RepAsianAmerican;
				DemAsianAmericanPercent = GetPercentage (voteCount.DemAsianAmerican, asianAmericanTotalCount);
				DemAsianAmericanWidth = GetWidth (DemAsianAmericanPercent);
				RepAsianAmericanPercent = GetPercentage (voteCount.RepAsianAmerican, asianAmericanTotalCount);
				RepAsianAmericanWidth = GetWidth (RepAsianAmericanPercent);

				long hispanicTotalCount = voteCount.DemHispanic + voteCount.RepHispanic;
				DemHispanicPercent = GetPercentage (voteCount.DemHispanic, hispanicTotalCount);
				DemHispanicWidth = GetWidth (DemHispanicPercent);
				RepHispanicPercent = GetPercentage (voteCount.RepHispanic, hispanicTotalCount);
				RepHispanicWidth = GetWidth (RepHispanicPercent);

				long otherTotalCount = voteCount.DemOther + voteCount.RepOther;
				DemOtherPercent = GetPercentage (voteCount.DemOther, otherTotalCount);
				DemOtherWidth = GetWidth (DemOtherPercent);
				RepOtherPercent = GetPercentage (voteCount.RepOther, otherTotalCount);
				RepOtherWidth = GetWidth (RepOtherPercent);

			}
		}

		public double GetPercentage(long value, long total)
		{
			if (value <= 0 || total <= 0)
				return 0;
			return Convert.ToDouble((double)(value * 100) / (double)total);
		}

		public double GetWidth(double value)
		{
			if (value <= 0)
				return 0;
			return Convert.ToDouble(((double) (275 * value)) / 100.0);
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
	}
}

