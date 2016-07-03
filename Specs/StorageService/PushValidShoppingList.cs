using GroceryList.Interfaces;
using GroceryList.Model;
using GroceryList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Specs.StorageService
{
  [Trait("Push a shopping list using valid key", "")]
  public class PushValidShoppingList
  {
    [Fact(DisplayName = "Shoppinglist is written to storage")]
    public async void PushListToStorage()
    {
      var list = new ShoppingList("MyTestList", "MyTestListKey");
      list.Add(new GroceryItem("Item1", "Item1ID") { Amount = 1, InBasket = false });
      list.Add(new GroceryItem("Item2", "Item2ID") { Amount = 2, InBasket = true });
      list.Add(new GroceryItem("Item3", "Item3ID") { Amount = 3, InBasket = false });

      var storage = new FirebaseStorageService(string.Format("{0}/TEST", FirebaseStorageService.FIREBASE_URL));
      var response = await storage.WriteShoppingList(list);
      Assert.Equal(StorageResponse.Success, response);
    }
  }
}
