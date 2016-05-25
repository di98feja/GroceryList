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
  [Trait("Pull an existing item using a valid key", "")]
  public class PullValidGroceryItem
  {
    [Fact(DisplayName = "The specified item is returned")]
    public async void PullItemFromStorage()
    {
      var storage = new FirebaseStorageService(string.Format("{0}/TEST/", FirebaseStorageService.FIREBASE_URL));
      GroceryItem item = await storage.ReadGroceryItem("MyTestItemId");
      Assert.NotNull(item);
      Assert.Equal("MyItemName", item.Name);
    }
  }
}
