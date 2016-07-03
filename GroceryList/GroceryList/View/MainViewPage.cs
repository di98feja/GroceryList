using GroceryList.Model;
using GroceryList.Services;
using GroceryList.ViewModel;
using System;

using Xamarin.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;
using GroceryList.Interfaces;

namespace GroceryList.View
{
  public class MainViewPage : ContentPage
  {
    public MainViewPage()
    {
    }

    public async void Init(IStorageWrapper storageWrapper)
    {
      ViewModel = await ShoppingListViewModel.CreateViewModelAsync("DefaultShoppingList", storageWrapper);
      BindingContext = ViewModel;

      //ViewModel.BeginBatchUpdate();
      //ViewModel.ClearList();
      //ViewModel.AddGroceryItem(new Model.GroceryItem("FirstItem", "FirstItemId"));
      //ViewModel.AddGroceryItem(new Model.GroceryItem("SecondItem", "SecondItemId"));
      //ViewModel.AddGroceryItem(new Model.GroceryItem("ThirdItem", "ThirdItemId") { InBasket = true });
      //ViewModel.AddGroceryItem(new Model.GroceryItem("FourthItem", "FourthItemId"));
      //ViewModel.EndBatchUpdate();

      var shoppingListView = new ListView();
      shoppingListView.SetBinding(ListView.ItemsSourceProperty, "GroceriesGrouped", BindingMode.TwoWay);
      shoppingListView.ItemTemplate = new DataTemplate(typeof(ShoppingListItemCell));
      shoppingListView.ItemTapped += (src, args) =>
      {
        var clickedItem = args.Item as GroceryItem;
        ViewModel.SetItemInBasketState(clickedItem, !clickedItem.InBasket);
      };
      shoppingListView.IsGroupingEnabled = true;
      shoppingListView.GroupDisplayBinding = new Binding("Key");
      if (Device.OS != TargetPlatform.WinPhone)
      {
        shoppingListView.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));
      }
      shoppingListView.HasUnevenRows = true; //you might want to enable if using custom cells.

      Content = new StackLayout
      {
        BackgroundColor = Color.White,
        
        Children = {
          new Label { Text = ViewModel.DefaultShoppingList.Name, TextColor = Color.Black },
          shoppingListView
        },

        VerticalOptions = LayoutOptions.CenterAndExpand,
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        Padding = 10,
        Spacing = 5
      };
    }

    private ShoppingListViewModel ViewModel;
  }

  public class ShoppingListItemCell : ViewCell
  {
    public ShoppingListItemCell()
    {
      var image = new Image
      {
        HorizontalOptions = LayoutOptions.StartAndExpand,
        HeightRequest = Device.OnPlatform(48, 48, 72),
      };
      image.SetBinding(Image.SourceProperty, new Binding("InBasket", BindingMode.OneWay, new InBasketImagePropertyValueConverter()));

      var outerLayout = new StackLayout()
      {
        Padding = 1,
        Spacing = 1,
        Orientation = StackOrientation.Vertical
      };

      var layout = new StackLayout()
      {
        Padding = 1,
        Spacing = 1,
        Orientation = StackOrientation.Horizontal
      };
      layout.SetBinding(StackLayout.BackgroundColorProperty, new Binding("InBasket", BindingMode.OneWay, new InBasketColorPropertyValueConverter()));
      outerLayout.Children.Add(layout);

      var nameLabel = new Label()
      {
        TextColor = Color.Black,
        HorizontalOptions = LayoutOptions.FillAndExpand,
        FontSize = 14
      };
      nameLabel.SetBinding(Label.TextProperty, "Name");

      var amountLabel = new Label()
      {
        TextColor = Color.Black,
        FontSize = 14,
        HorizontalOptions = LayoutOptions.FillAndExpand,
        HorizontalTextAlignment = TextAlignment.End
      };
      amountLabel.SetBinding(Label.TextProperty, "Amount");

      layout.Children.Add(image);
      layout.Children.Add(nameLabel);
      layout.Children.Add(amountLabel);

      View = outerLayout;
    }
    public bool InBasket {
      get { return (bool)GetValue(InBasketProperty); }
      set { SetValue(InBasketProperty, value); }
    }

    public static readonly BindableProperty InBasketProperty = 
      BindableProperty.Create("InBasket", typeof(bool), typeof(ShoppingListItemCell), false, BindingMode.TwoWay);
  }

  public class InBasketImagePropertyValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool inBasket = (bool)value;
      return inBasket ? ImageSource.FromResource("GroceryList.Images.shopping-cart-out.png") :
                        ImageSource.FromResource("GroceryList.Images.shopping-cart-in.png");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class InBasketColorPropertyValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool inBasket = (bool)value;
      return inBasket ? Color.FromRgba(0, 100, 0, 50) : Color.FromRgba(0, 0, 100, 50);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }


  public class InBasketStringPropertyValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool inBasket = (bool)value;
      return inBasket ? "I korgen" : "Kvar att plocka";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class HeaderCell : ViewCell
  {
    public HeaderCell()
    {
      this.Height = 25;
      var title = new Label
      {
        FontAttributes = FontAttributes.Bold,
        FontFamily = "Arial",
        TextColor = Color.White,
        VerticalOptions = LayoutOptions.Center
      };

      title.SetBinding(Label.TextProperty, new Binding("Key", BindingMode.OneWay, new InBasketStringPropertyValueConverter()));

      View = new StackLayout
      {
        HorizontalOptions = LayoutOptions.FillAndExpand,
        HeightRequest = 25,
        BackgroundColor = Color.FromRgb(52, 152, 218),
        Padding = 5,
        Orientation = StackOrientation.Horizontal,
        Children = { title }
      };
    }
  }
}
