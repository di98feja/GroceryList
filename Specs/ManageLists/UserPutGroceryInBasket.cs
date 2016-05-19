using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GroceryList.ViewModel;
using GroceryList.Model;
using System.ComponentModel;

namespace Specs.ManageLists
{
	[Trait("User put grocery in basket", "")]
	public class UserPutGroceryInBasket
	{
		[Fact(DisplayName = "Grocery is marked as picked")]
		public void GroceryIsMarkedAsPicked()
		{
			var list = new ShoppingList("MyTestList");
			var groceryItem = new GroceryItem("MyTestItem");
			list.GroceryItems.Add(groceryItem);
			var vm = new ShoppingListViewModel(list);
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
