using GroceryList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.Interfaces
{
  public interface IStorageWrapper
  {
    Task<string> WriteShoppingList(ShoppingList shoppingList);

    Task<ShoppingList> ReadShoppingList(string key);

		Task<GroceryItem> ReadGroceryItem(string key);

		Task<string> WriteGroceryItem(GroceryItem item);

		Task<List<GroceryItem>> ReadGroceryList();

		Task<string> WriteGroceryList(List<GroceryItem> list);
  }
}
