using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GroceryList.Interfaces;

namespace GroceryList.ViewModel
{
	public class ShoppingListViewModel : ViewModelBase
	{
		public async static Task<ShoppingListViewModel> CreateViewModelAsync(string shoppingListID, IStorageWrapper storageImplementation)
		{
			var vm = new ShoppingListViewModel(storageImplementation);
			vm.m_shoppingList = await vm.PullChangesFromStorage(shoppingListID);
			return vm;
		}

		private ShoppingListViewModel(IStorageWrapper storage) : base(storage) { }

		public void ClearList()
		{
			DefaultShoppingList.GroceryItems.Clear();
			PushChangesToStorage(DefaultShoppingList);
			NotifyChanged("DefaultShoppingList");
		}

		public void AddGroceryItem(GroceryItem item)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");
			m_shoppingList.GroceryItems.Add(item);
			PushChangesToStorage(DefaultShoppingList);
			NotifyChanged("DefaultShoppingList");
		}

		public void SetItemInBasketState(GroceryItem item, bool state)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");

			var itemInList = DefaultShoppingList.GroceryItems.FirstOrDefault(i => i.Name == item.Name);
			if (null == itemInList)
				throw new ArgumentException("item was not found in current list");

			if (itemInList.InBasket == state)
				return;

			itemInList.InBasket = state;
			PushChangesToStorage(DefaultShoppingList);
			NotifyChanged("DefaultShoppingList");
		}

		public void SetItemAmount(GroceryItem item, double amount)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");

			var itemInList = DefaultShoppingList.GroceryItems.FirstOrDefault(i => i.Name == item.Name);
			if (null == itemInList)
				throw new ArgumentException("item was not found in current list");

			if (itemInList.Amount.CompareTo(amount) == 0)
				return;

			itemInList.Amount = amount;
			PushChangesToStorage(DefaultShoppingList);
			NotifyChanged("DefaultShoppingList");
		}

		public ShoppingList DefaultShoppingList
		{
			get { return m_shoppingList; }
			set
			{
				if (null == value)
					throw new ArgumentNullException("DefaultShoppingList must not be null");

				m_shoppingList = value;
				NotifyChanged();
			}
		}

		private async void PushChangesToStorage(ShoppingList list)
		{
			string key = await m_storageWrapper.WriteShoppingList(list);
		}

		private async Task<ShoppingList> PullChangesFromStorage(string key)
		{
			return await m_storageWrapper.ReadShoppingList(key);
		}

		private ShoppingList m_shoppingList;
	}
}
