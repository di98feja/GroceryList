using GroceryList.Services;
using GroceryList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryList.View
{
  public partial class MainView : ContentPage
  {
    public MainView()
    {
      //InitializeComponent();
    }

    public async void Init()
    {
      ViewModel = await ShoppingListViewModel.CreateViewModelAsync("DefaultShoppingList", new FirebaseStorageService(FirebaseStorageService.FIREBASE_URL));
      BindingContext = ViewModel;

      ViewModel.BeginBatchUpdate();
      ViewModel.ClearList();
      ViewModel.AddGroceryItem(new Model.GroceryItem("FirstItem", "FirstItemId"));
      ViewModel.AddGroceryItem(new Model.GroceryItem("SecondItem", "SecondItemId"));
      ViewModel.AddGroceryItem(new Model.GroceryItem("ThirdItem", "ThirdItemId"));
      ViewModel.EndBatchUpdate();
    }

    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
      public string Source { get; set; }

      public object ProvideValue(IServiceProvider serviceProvider)
      {
        if (Source == null)
          return null;

        // Do your translation lookup here, using whatever method you require
        var imageSource = ImageSource.FromResource(Source);

        return imageSource;
      }
    }

    private ShoppingListViewModel ViewModel;
  }
}
