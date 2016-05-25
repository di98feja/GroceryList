using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Model;
using System.ComponentModel;
using GroceryList.Interfaces;

namespace GroceryList.ViewModel
{
	public class EditGroceryItemViewModel : ViewModelBase
	{
		public static async Task<EditGroceryItemViewModel> CreateViewModelAsync(string groceryItemId, IStorageWrapper storageImplementation)
		{
			var vm = new EditGroceryItemViewModel(storageImplementation);
			vm.CurrentGroceryItem = await vm.PullChangesFromStorage(groceryItemId);
			return vm;
		}

		private EditGroceryItemViewModel(IStorageWrapper storage) : base (storage) { }

		public GroceryItem CurrentGroceryItem { get; set; }

		private async void PushChangesToStorage(GroceryItem item)
		{
			var response = await m_storageWrapper.WriteGroceryItem(item);
		}

		private async Task<GroceryItem> PullChangesFromStorage(string key)
		{
			return await m_storageWrapper.ReadGroceryItem(key);
		}
	}
}
