using GroceryList.Model;
using GroceryList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Specs.StorageService
{
	[Trait("Pull complete list of grocery types","")]
	public class PullGroceryTypesList
	{
		[Fact(DisplayName = "All items is read from storage")]
		public async void ListIsReadFromStorage()
		{
			var storage = new FirebaseStorageService(string.Format("{0}/TEST", FirebaseStorageService.FIREBASE_URL));
			var list = await storage.ReadGroceryList();
			Assert.NotNull(list);
			Assert.Equal(4, list.Count);
		}
	}
}
