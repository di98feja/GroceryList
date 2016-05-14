using System;
using Xunit;
using GroceryList.Model;
using GroceryList.ViewModel;
using System.ComponentModel;

namespace Specs.ManageLists
{

	[Trait("User pick a grocery to add", "")]
	public class UserPickGroceryToAdd
	{
		[Fact(DisplayName = "Grocery is added to list")]
		public void GroceryIsAddedToList()
		{
			var list = new ShoppingList("MyTestList");
			var groceryItem = new GroceryItem("MyTestItem");
			var vm = new ShoppingListViewModel(list);
			bool wasCalled = false;
			vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
			{
				Assert.Contains(groceryItem, list.GroceryItems);
				wasCalled = true;
			};
			vm.AddGroceryItem(groceryItem);
			Assert.True(wasCalled);
		}

		[Fact(DisplayName = "List is persisted")]
		public void ListIsPersisted()
		{
			throw new NotImplementedException();
		}
	}
}
