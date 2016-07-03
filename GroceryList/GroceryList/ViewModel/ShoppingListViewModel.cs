using System;
using System.Linq;
using System.Threading.Tasks;
using GroceryList.Model;
using GroceryList.Interfaces;
using System.Collections.ObjectModel;

namespace GroceryList.ViewModel
{
  public class ShoppingListViewModel : ViewModelBase
	{
		public async static Task<ShoppingListViewModel> CreateViewModelAsync(string shoppingListID, IStorageWrapper storageImplementation)
		{
			var vm = new ShoppingListViewModel(storageImplementation);
			vm.m_shoppingList = await vm.PullChangesFromStorage(shoppingListID);
      if (null == vm.m_shoppingList)
        vm.m_shoppingList = new ShoppingList("Inköpslista", shoppingListID);

      vm.m_shoppingList.CollectionChanged += (src, e) =>
      {
        vm.NotifyChanged("GroceryItems");
      };
      
      vm.PropertyChanged += delegate { vm.PushChangesToStorage(vm.m_shoppingList); };
      vm.SortShoppingList(vm.DefaultShoppingList);
			return vm;
		}

		private ShoppingListViewModel(IStorageWrapper storage) : base(storage) { }

		public void ClearList()
		{
			DefaultShoppingList.Clear();
      SortShoppingList(DefaultShoppingList);
			NotifyChanged("GroceriesGrouped");
		}

		public void AddGroceryItem(GroceryItem item)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");
			m_shoppingList.Add(item);
      SortShoppingList(DefaultShoppingList);
			NotifyChanged("GroceriesGrouped");
		}

		public void SetItemInBasketState(GroceryItem item, bool state)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");

			var itemInList = DefaultShoppingList.FirstOrDefault(i => i.Name == item.Name);
			if (null == itemInList)
				throw new ArgumentException("item was not found in current list");

			itemInList.InBasket = state;
      SortShoppingList(DefaultShoppingList);
      NotifyChanged("GroceriesGrouped");
		}

    public void SetItemAmount(GroceryItem item, double amount)
		{
			if (null == item)
				throw new ArgumentNullException("item must not be null");

			var itemInList = DefaultShoppingList.FirstOrDefault(i => i.Name == item.Name);
			if (null == itemInList)
				throw new ArgumentException("item was not found in current list");

			if (itemInList.Amount.CompareTo(amount) == 0)
				return;

			itemInList.Amount = amount;
			NotifyChanged("GroceriesGrouped");
		}

    public void SortShoppingList(ShoppingList shoppingList)
    {
      var sorted = from grocery in shoppingList
                   orderby grocery.Name
                   group grocery by grocery.InBasket into groceryGroup
                   orderby groceryGroup.Key
                   select new Grouping<bool, GroceryItem>(groceryGroup.Key, groceryGroup);

      //create a new collection of groups
      GroceriesGrouped = new ObservableCollection<Grouping<bool, GroceryItem>>();
      foreach (var group in sorted)
        GroceriesGrouped.Add(group);
    }

		public ShoppingList DefaultShoppingList
		{
			get { return m_shoppingList; }
			set
			{
				if (null == value)
					throw new ArgumentNullException("DefaultShoppingList must not be null");

				m_shoppingList = value;
        SortShoppingList(DefaultShoppingList);
				NotifyChanged();
			}
		}

    public ObservableCollection<Grouping<bool, GroceryItem>> GroceriesGrouped { get; set; }

    private async void PushChangesToStorage(ShoppingList list)
		{
			var response = await m_storageWrapper.WriteShoppingList(list);
		}

		private async Task<ShoppingList> PullChangesFromStorage(string key)
		{
			return await m_storageWrapper.ReadShoppingList(key);
		}

		private ShoppingList m_shoppingList;
	}
}
