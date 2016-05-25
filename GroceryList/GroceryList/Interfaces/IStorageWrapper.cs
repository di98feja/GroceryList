using GroceryList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.Interfaces
{
  public enum StorageResponse { Success, Failure };

  public interface IStorageWrapper
  {
    Task<StorageResponse> WriteShoppingList(ShoppingList shoppingList);
    Task<StorageResponse> WriteGroceryItem(GroceryItem item);
    Task<StorageResponse> WriteGroceryList(List<GroceryItem> list);

    Task<ShoppingList> ReadShoppingList(string key);
		Task<GroceryItem> ReadGroceryItem(string key);
		Task<List<GroceryItem>> ReadGroceryList();

  }
}
