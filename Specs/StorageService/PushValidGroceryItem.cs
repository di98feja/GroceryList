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
  [Trait("Push a new item using a valid key", "")]
  public class PushValidGroceryItem
  {
    [Fact(DisplayName = "Item is written to storage")]
    public async void ItemIsWritten()
    {
      var storage = new FirebaseStorageService(string.Format("{0}/TEST", FirebaseStorageService.FIREBASE_URL));
      var groceryItem = new GroceryItem("MyItemName");
      groceryItem.Id = "MyTestItemId";
      await storage.WriteGroceryItem(groceryItem);
    }
  }
}
