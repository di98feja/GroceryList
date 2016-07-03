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

namespace Specs.ManageLists.ViewModel
{
	[Trait("User put grocery in basket", "")]
	public class UserPutGroceryInBasket
	{
		[Fact(DisplayName = "Grocery is marked as picked - ViewModel")]
		public async void GroceryIsMarkedAsPicked()
		{
			var list = new ShoppingList("MyTestList", "MyTestListKey");
			var groceryItem = new GroceryItem("MyTestItem", "ItemId1");
			list.Add(groceryItem);
			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				var item = vm.DefaultShoppingList[0];
				Assert.True(item.InBasket);
				wasCalled = true;
			};
			vm.SetItemInBasketState(groceryItem, true);
			Assert.True(wasCalled);
		}

    [Fact(DisplayName="Item sorting is applied to the list")]
    public async void GroceryListIsReSorted()
    {
      var list = new ShoppingList("MyTestList", "MyTestListKey");
      var groceryItem1 = new GroceryItem("MyTestItem1", "ItemId1");
      var groceryItem2 = new GroceryItem("MyTestItem2", "ItemId2");
      var groceryItem3 = new GroceryItem("MyTestItem3", "ItemId3");
      var groceryItem4 = new GroceryItem("MyTestItem4", "ItemId4");
      list.Add(groceryItem1);
      list.Add(groceryItem2);
      list.Add(groceryItem3);
      list.Add(groceryItem4);
      var storageMock = new Mock<IStorageWrapper>();
      storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

      var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
      Assert.Equal("ItemId1", list.First().Id);
      vm.SetItemInBasketState(groceryItem1, true);
      Assert.Equal(3, vm.GroceriesGrouped[0].Count);
      Assert.Equal("ItemId1", vm.GroceriesGrouped[1][0].Id);
    }
  }
}
