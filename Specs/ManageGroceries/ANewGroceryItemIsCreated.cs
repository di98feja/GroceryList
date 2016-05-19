using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GroceryList.ViewModel;
using GroceryList.Model;
using System.ComponentModel;

namespace Specs.ManageGroceries
{
	[Trait("New grocery is created", "")]
	public class ANewGroceryItemIsCreated
	{
		[Fact(DisplayName = "An empty item is added to grocery items collection")]
		public void AnEmptyItemIsAddedToGroceryItemsCollection()
		{
			var vm = new GroceryItemsListViewModel();
			var item = vm.CreateNewGroceryItem();
			Assert.Contains(item, vm.GroceryItems);
		}

		[Fact(DisplayName = "New item is automatically opened for edit")]
		public void AppGoToEditItemScreen()
		{
			throw new NotImplementedException();
		}

	}
}
