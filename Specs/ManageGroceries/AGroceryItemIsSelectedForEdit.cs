using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GroceryList.Model;
using GroceryList.ViewModel;
using Moq;
using GroceryList.Interfaces;

namespace Specs.ManageGroceries
{
	[Trait("A grocery item is selected for edit","")]
	public class AGroceryItemIsSelectedForEdit
	{
		public GroceryItem Item { get; set; }
		public GroceryItemsListViewModel ViewModel { get; set; }
		public bool ItemWasReadFromStorage { get; set; }
		public bool ItemWasWrittenToStorage { get; set; }

		private async void Init()
		{
			var item = new GroceryItem("MyTestItem");
			var list = new List<GroceryItem>();
			var storageMock = new Mock<IStorageWrapper>();
			ItemWasReadFromStorage = false;
			ItemWasWrittenToStorage = false;
			storageMock.Setup(storage => storage.ReadGroceryList()).ReturnsAsync(list);
			storageMock.Setup(storage => storage.ReadGroceryItem("MyTestItemKey")).ReturnsAsync(item).Callback(delegate { ItemWasReadFromStorage = true; });
			storageMock.Setup(storage => storage.WriteGroceryItem(item)).ReturnsAsync("MyTestItemKey").Callback(delegate { ItemWasWrittenToStorage = true; });

			ViewModel = await GroceryItemsListViewModel.CreateViewModelAsync(storageMock.Object);
		}

		[Fact(DisplayName = "Item is pulled from storage")]
		public async void ItemIsPulledFromStorage()
		{
			Init();
			Item = await ViewModel.GetGroceryItem("MyTestItemKey");
			Assert.Equal("MyTestItem", Item.Name);
			Assert.True(ItemWasReadFromStorage);
		}

		[Fact(DisplayName = "Item is updated")]
		public async void ItemIsUpdated()
		{
			Init();
			Item = await ViewModel.GetGroceryItem("MyTestItemKey");
			Item.Name = "Apa!";
			var wasUpdated = false;
			ViewModel.PropertyChanged += delegate { wasUpdated = true; };
			ViewModel.SetGroceryItem(Item);
			Assert.True(wasUpdated);
		}

		[Fact(DisplayName = "Item is pushed to storage")]
		public async void ItemIsPushedToStorage()
		{
			Init();
			Item = await ViewModel.GetGroceryItem("MyTestItemKey");
			Item.Name = "Apa!";
			ViewModel.SetGroceryItem(Item);
			Assert.True(ItemWasWrittenToStorage);
		}


	}
}
