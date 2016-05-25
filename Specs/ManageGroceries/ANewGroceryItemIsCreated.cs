using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GroceryList.ViewModel;
using GroceryList.Model;
using System.ComponentModel;
using GroceryList.Interfaces;
using Moq;

namespace Specs.ManageGroceries
{

	[Trait("New grocery is created", "")]
	public class ANewGroceryItemIsCreated
	{
		public void Setup()
		{
			var list = new List<GroceryItem>();
			var storageMock = new Mock<IStorageWrapper>();
			storageMock.Setup(storage => storage.ReadGroceryList()).ReturnsAsync(list);
			m_storageMock = storageMock.Object;
		}

		[Fact(DisplayName = "An empty item is added to grocery items collection")]
		public async void AnEmptyItemIsAddedToGroceryItemsCollection()
		{
			Setup();
			var vm = await GroceryItemsListViewModel.CreateViewModelAsync(m_storageMock);
			var item = vm.CreateNewGroceryItem();
			Assert.Contains(item, vm.GroceryItems);
		}

		[Fact(DisplayName = "New item is automatically opened for edit")]
		public void AppGoToEditItemScreen()
		{
			throw new NotImplementedException();
		}

		IStorageWrapper m_storageMock;
	}
}
