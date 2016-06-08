using System;

namespace VVoting
{
	public class VoteCount
	{
		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }

		[Microsoft.WindowsAzure.MobileServices.Version]
		public string AzureVersion { get; set; }

		public long DemTotal { get; set; }

		public long RepTotal { get; set; }

		public long DemMale { get; set; }

		public long RepMale { get; set; }

		public long DemFemale { get; set; }

		public long RepFemale { get; set; }

		public long DemOther { get; set; }

		public long RepOther { get; set; }

		public long DemWhite { get; set; }

		public long RepWhite { get; set; }

		public long DemAfricanAmerican { get; set; }

		public long RepAfricanAmerican { get; set; }

		public long DemAsianAmerican { get; set; }

		public long RepAsianAmerican { get; set; }

		public long DemHispanic { get; set; }

		public long RepHispanic { get; set; }
	}
}

