using System;
namespace VVoting
{
	public class TabOpenedEvent
	{
		public string TabName { get; set; }
		public TabOpenedEvent(string tabName)
		{
			TabName = tabName;
		}
	}
}

