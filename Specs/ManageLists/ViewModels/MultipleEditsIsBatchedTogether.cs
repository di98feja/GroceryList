using GroceryList.Interfaces;
using GroceryList.Model;
using GroceryList.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Specs.ManageLists.ViewModel
{
  [Trait("Multiple edits is handled in one batch", "")]
  public class MultipleEditsIsBatchedTogether
  {
    [Fact(DisplayName = "All edits is stored in one batch")]
    public async void AllEditsIsStoredInOneBatch()
    {
      var list = new ShoppingList("MyTestList", "MyTestListKey");
      var storageMock = new Mock<IStorageWrapper>();
      bool listWasReadFromStorage = false;
      bool listWasWrittenToStorage = false;
      storageMock.Setup(storage => storage.ReadShoppingList("MyTestListKey")).ReturnsAsync(list).Callback(delegate { listWasReadFromStorage = true; });
      storageMock.Setup(storage => storage.WriteShoppingList(list)).ReturnsAsync(StorageResponse.Success).Callback(delegate { listWasWrittenToStorage = true; });

      var vm = await ShoppingListViewModel.CreateViewModelAsync("MyTestListKey", storageMock.Object);
      Assert.True(listWasReadFromStorage);
      int numTimesCalled = 0;
      vm.PropertyChanged += delegate (object caller, PropertyChangedEventArgs args)
      {
        numTimesCalled++;
      };
      vm.BeginBatchUpdate();
      vm.ClearList();
      vm.AddGroceryItem(new GroceryItem("MyTestItem", "ItemId1"));
      vm.AddGroceryItem(new GroceryItem("MyTestItem", "ItemId2"));
      vm.AddGroceryItem(new GroceryItem("MyTestItem", "ItemId3"));
      vm.EndBatchUpdate();
      Assert.Equal(1, numTimesCalled);
      Assert.True(listWasWrittenToStorage);

    }
  }
}
