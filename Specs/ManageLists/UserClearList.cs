using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GroceryList.ViewModel;
using GroceryList.Model;
using System.ComponentModel;
using Moq;
using GroceryList.Interfaces;

namespace Specs.ManageLists
{
	[Trait("User clear list", "")]
	public class UserClearList
	{
		[Fact(DisplayName = "All groceries is removed from list")]
		public async void ListIsEmptied()
		{
			var list = new ShoppingList("MyTestList", "MyTestListKey");
			var groceryItem1 = new GroceryItem("MyTestItem_1", "ItemId1");
			var groceryItem2 = new GroceryItem("MyTestItem_2", "ItemId2");
			var groceryItem3 = new GroceryItem("MyTestItem_3", "ItemId3");
			list.GroceryItems.Add(groceryItem1);
			list.GroceryItems.Add(groceryItem2);
			list.GroceryItems.Add(groceryItem3);

			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Equal(0, list.GroceryItems.Count);
				wasCalled = true;
			};
			vm.ClearList();

			Assert.True(wasCalled);
		}
	}
}
