using System;
using Xunit;
using GroceryList.Model;
using GroceryList.ViewModel;
using System.ComponentModel;
using Moq;
using GroceryList.Interfaces;

namespace Specs.ManageLists
{

	[Trait("User pick a grocery to add", "")]
	public class UserPickGroceryToAdd
	{

		[Fact(DisplayName = "Grocery is added to list")]
		public async void GroceryIsAddedToList()
		{
			var list = new ShoppingList("MyTestList");
			var groceryItem = new GroceryItem("MyTestItem");
			var storageMock = new Mock<IStorageWrapper>();
			bool listWasReadFromStorage = false;
			bool listWasWrittenToStorage = false;
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list).Callback(delegate { listWasReadFromStorage = true; });
			storageMock.Setup(storage => storage.WriteShoppingList(list)).ReturnsAsync("MyTestListKey").Callback(delegate { listWasWrittenToStorage = true; });

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			Assert.True(listWasReadFromStorage);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Contains(groceryItem, list.GroceryItems);
				wasCalled = true;
			};
			vm.AddGroceryItem(groceryItem);
			Assert.True(wasCalled);
			Assert.True(listWasWrittenToStorage);
		}
	}
}
