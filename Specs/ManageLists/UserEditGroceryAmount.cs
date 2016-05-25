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
	[Trait("User edit grocery amount", "")]
	public class UserEditGroceryAmount
	{
		[Fact(DisplayName = "Grocery amount is updated")]
		public async void GroceryAmountIsUpdated()
		{
			var list = new ShoppingList("MyTestList", "MyTestListKey");
			var groceryItem1 = new GroceryItem("MyTestItem_1", "ItemId1");
			list.GroceryItems.Add(groceryItem1);

			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Equal(12.34, vm.DefaultShoppingList.GroceryItems[0].Amount);
				wasCalled = true;
			};
			vm.SetItemAmount(groceryItem1, 12.34);
			Assert.True(wasCalled);
		}
	}
}
