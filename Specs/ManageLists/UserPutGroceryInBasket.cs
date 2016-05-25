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
	[Trait("User put grocery in basket", "")]
	public class UserPutGroceryInBasket
	{
		[Fact(DisplayName = "Grocery is marked as picked")]
		public async void GroceryIsMarkedAsPicked()
		{
			var list = new ShoppingList("MyTestList", "MyTestListKey");
			var groceryItem = new GroceryItem("MyTestItem", "ItemId1");
			list.GroceryItems.Add(groceryItem);
			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				var item = vm.DefaultShoppingList.GroceryItems[0];
				Assert.True(item.InBasket);
				wasCalled = true;
			};
			vm.SetItemInBasketState(groceryItem, true);
			Assert.True(wasCalled);
		}


	}
}
