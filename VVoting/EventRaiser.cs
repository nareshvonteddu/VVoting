using System;
namespace VVoting
{
	public class EventRaiser : IEventInterface
	{
		public EventRaiser()
		{
		}

		public event EventHandler TrendsTabOpened;
		public event EventHandler YourVoteTabOpened;



		public void OnTrendsTabOpened()
		{
			if (TrendsTabOpened != null) TrendsTabOpened(this, new EventArgs());
		}

		public void OnYourVoteTapOpened()
		{
			if (YourVoteTabOpened != null) YourVoteTabOpened(this, new EventArgs());
		}
	}
}

