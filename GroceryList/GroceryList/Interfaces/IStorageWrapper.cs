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

  }
}
