using GroceryList.Interfaces;
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
	[Trait("Push complete list of Grocery types","")]
	public class PushGroceryTypesList
	{
		[Fact(DisplayName = "All grocery type items is stored")]
		public async void TypeListIsStored()
		{
			List<GroceryItem> list = new List<GroceryItem>();
			list.Add(new GroceryItem("Item1", "Item1Id"));
			list.Add(new GroceryItem("Item2", "Item2Id"));
			list.Add(new GroceryItem("Item3", "Item3Id"));
			list.Add(new GroceryItem("Item4", "Item4Id"));
			var storage = new FirebaseStorageService(string.Format("{0}/TEST", FirebaseStorageService.FIREBASE_URL));
			var response = await storage.WriteGroceryList(list);
			Assert.Equal(StorageResponse.Success, response);
		}
	}
}
