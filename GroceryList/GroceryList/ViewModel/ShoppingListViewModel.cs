using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroceryList.ViewModel
{
	public class ShoppingListViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ShoppingListViewModel(ShoppingList defaultShoppingList)
		{
			if (null == defaultShoppingList)
				defaultShoppingList = new ShoppingList("DefaultShoppingList");
			m_shoppingList = defaultShoppingList;
		}

		public void ClearList()
		{
			DefaultShoppingList.GroceryItems.Clear();
			NotifyChanged("DefaultShoppingList");
		}

		public void AddGroceryItem(GroceryItem item)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");
			m_shoppingList.GroceryItems.Add(item);
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

		private void NotifyChanged([CallerMemberName] string propertyName = "")
		{
			if (null != PropertyChanged)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private ShoppingList m_shoppingList;

	}
}
