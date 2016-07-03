using GroceryList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Interfaces;

namespace GroceryList.ViewModel
{
	public class GroceryItemsListViewModel : ViewModelBase
	{
		public static async Task<GroceryItemsListViewModel> CreateViewModelAsync(IStorageWrapper storageImplementation)
		{
			var vm = new GroceryItemsListViewModel(storageImplementation);
			vm.m_groceryItems = await vm.PullChangesFromStorage();
      vm.PropertyChanged += delegate { vm.PushChangesToStorage(vm.m_groceryItems); };
      return vm;
		}

		private GroceryItemsListViewModel(IStorageWrapper storage)
			: base(storage)
		{	}

		public List<GroceryItem> GroceryItems
		{
			get { return m_groceryItems; }
		}

		public GroceryItem CreateNewGroceryItem()
		{
			var groceryItem = new GroceryItem("Ny vara", Guid.NewGuid().ToString());
			m_groceryItems.Add(groceryItem);
			NotifyChanged("GroceryItems");
			return groceryItem;
		}

		public async void SetGroceryItem(GroceryItem item)
		{
			var existing = GroceryItems.FirstOrDefault(i => i.Id == item.Id);
			existing = item;
			await m_storageWrapper.WriteGroceryItem(item);
			NotifyChanged("GroceryItems");
		}

		public async Task<GroceryItem> GetGroceryItem(string itemId)
		{
			return await m_storageWrapper.ReadGroceryItem(itemId);
		}

		private async void PushChangesToStorage(List<GroceryItem> list)
		{
			var response = await m_storageWrapper.WriteGroceryList(list);
		}

		private async Task<List<GroceryItem>> PullChangesFromStorage()
		{
			return await m_storageWrapper.ReadGroceryList();
		}

		private List<GroceryItem> m_groceryItems;
	}
}
