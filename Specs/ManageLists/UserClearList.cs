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
	[Trait("User clear list", "")]
	public class UserClearList
	{
		[Fact(DisplayName = "All groceries is removed from list")]
		public void ListIsEmptied()
		{
			var list = new ShoppingList("MyTestList");
			var groceryItem1 = new GroceryItem("MyTestItem_1");
			var groceryItem2 = new GroceryItem("MyTestItem_2");
			var groceryItem3 = new GroceryItem("MyTestItem_3");
			list.GroceryItems.Add(groceryItem1);
			list.GroceryItems.Add(groceryItem2);
			list.GroceryItems.Add(groceryItem3);
			var vm = new ShoppingListViewModel(list);
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
