using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace VVoting.ViewModels
{
	public class MainPageViewModel : ViewModelBase, IMainPageViewModel
	{
		private string _clickCount;
		public string ClickCount 
		{
			get{ return _clickCount; } 
			set
			{
				Set (() => ClickCount, ref _clickCount, value);
			}
		}


		public MainPageViewModel ()
		{
			
		}

		public void ClickFirstButton()
		{
			
		}

		private RelayCommand _clickMeButton; 
		private int i;

		public RelayCommand ClickMeButton { 
			get { 
				return _clickMeButton
				?? (_clickMeButton = new RelayCommand (
					 () => 
					{ 
							ClickCount = String.Format("Clicked {0} times", i++);

					})); 
			} 
		}
	}

	public interface IMainPageViewModel
	{
		
	}
}

