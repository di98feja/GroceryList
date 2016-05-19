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
	[Trait("User reset list", "")]
	public class UserResetList
	{
		[Fact(DisplayName = "All groceries is marked as not picked")]
		public void AllGroceriesIsUnpicked()
		{
			var list = new ShoppingList("MyTestList");
			var groceryItem1 = new GroceryItem("MyTestItem_1") { InBasket = true };
			var groceryItem2 = new GroceryItem("MyTestItem_2") { InBasket = true };
			var groceryItem3 = new GroceryItem("MyTestItem_3") { InBasket = true };
			list.GroceryItems.Add(groceryItem1);
			list.GroceryItems.Add(groceryItem2);
			list.GroceryItems.Add(groceryItem3);
			var vm = new ShoppingListViewModel(list);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Equal(0, list.GroceryItems.Where(i => i.InBasket).ToList().Count);
				wasCalled = true;
			};
			vm.ClearList();
			Assert.True(wasCalled);
		}
	}
}
