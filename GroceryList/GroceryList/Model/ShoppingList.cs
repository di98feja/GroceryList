using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GroceryList.Model
{
  public class ShoppingList : ObservableCollection<GroceryItem>
  {
    public ShoppingList()
      : this(string.Empty, string.Empty)
    { }

    public ShoppingList(string name, string id)
    {
      Id = id;
      Name = name;
    }

    public string Id { get; set; }

    public string Name { get; set; }
  }
}
