using System;
namespace VVoting
{
	public interface IEventInterface
	{
		event EventHandler YourVoteTabOpened;
		event EventHandler TrendsTabOpened;

		void OnYourVoteTapOpened();
		void OnTrendsTabOpened();
	}
}

