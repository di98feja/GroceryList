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
	[Trait("User reset list", "")]
	public class UserResetList
	{
		[Fact(DisplayName = "All groceries is marked as not picked")]
		public async void AllGroceriesIsUnpicked()
		{
			var list = new ShoppingList("MyTestList", "MyTestListKey");
			var groceryItem1 = new GroceryItem("MyTestItem_1", "ItemId1") { InBasket = true };
			var groceryItem2 = new GroceryItem("MyTestItem_2", "ItemId2") { InBasket = true };
			var groceryItem3 = new GroceryItem("MyTestItem_3", "ItemId3") { InBasket = true };
			list.Add(groceryItem1);
			list.Add(groceryItem2);
			list.Add(groceryItem3);
			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list);

			var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Equal(0, list.Where(i => i.InBasket).ToList().Count);
				wasCalled = true;
			};
			vm.ClearList();
			Assert.True(wasCalled);
		}
	}
}
