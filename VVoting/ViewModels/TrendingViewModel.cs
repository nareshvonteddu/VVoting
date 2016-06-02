using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using Xamarin.Forms;

namespace VVoting
{
	public class TrendingViewModel : ViewModelBase
	{
		private string  _clickCount = "";
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


		public TrendingViewModel ()
		{
		}
		//int i = 0;
		private RelayCommand _tapDemocratic; 

		public RelayCommand TapDemocratic { 
			get { 
				return _tapDemocratic
					?? (_tapDemocratic = new RelayCommand (
						() => 
						{ 
							DemocraticBorderColor = Color.Lime; //String.Format("Clicked {0} times", i++);
							RepublicBorderColor = Color.Gray;
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
							RepublicBorderColor = Color.Lime;
						})); 
			} 
		}
	}
}

