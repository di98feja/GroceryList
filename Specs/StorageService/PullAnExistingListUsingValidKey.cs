using GroceryList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Specs.StorageService
{
	[Trait("Pull an existing list using valid key", "")]
	public class PullAnExistingListUsingValidKey
	{
		[Fact(DisplayName = "The specific list is returned")]
		public async void ReadItemFromStorage()
		{
			var storage = new FirebaseStorageService(string.Format("{0}/TEST", FirebaseStorageService.FIREBASE_URL));
			var list = await storage.ReadShoppingList("MyTestListKey");
			Assert.NotNull(list);
			Assert.Equal(3, list.Count);
		}
	}
}
