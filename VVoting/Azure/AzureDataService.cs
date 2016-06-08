using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Linq;
using System.Collections.Generic;


namespace VVoting
{
	public class AzureDataService
	{
		public MobileServiceClient MobileService { get; set; }
		//IMobileServiceSyncTable<VoteCount> voteCountTable;
		IMobileServiceTable<VoteCount> voteCountTable;

		public async Task Initialize()
		{
			//Create our client
			MobileService = new MobileServiceClient("https://vvoteamerica.azurewebsites.net");

//			const string path = "syncstore.db";
//			//setup our local sqlite store and intialize our table
//			var store = new MobileServiceSQLiteStore(path);
//			store.DefineTable<VoteCount> ();
//			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

			//Get our sync table that will call out to azure
			voteCountTable = MobileService.GetTable<VoteCount>();
		}

		public async Task<List<VoteCount>> GetVoteCount()
		{
			//await SyncVoteCount ();
			return await voteCountTable.ToListAsync();
		}


		public async Task UpdateVoteCount(VoteCount voteCount)
		{
			await voteCountTable.UpdateAsync (voteCount);
		}


//		public async Task AddVoteCount(VoteCount voteCount)
//		{
//			await voteCountTable.InsertAsync (voteCount);
//			//await SyncVoteCount ();
//		}

//		public async Task SyncVoteCount()
//		{
//			try
//			{
//				if (voteCountTable != null) 
//				{
//					await voteCountTable.PullAsync ("allVoteCounts", voteCountTable.CreateQuery ());
//					await MobileService.SyncContext.PushAsync ();
//				}
//			}
//			catch(Exception ex) 
//			{
//				
//			}
//		}
	}
}

