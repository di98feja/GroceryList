using System;
using System.Collections.Generic;

namespace GroceryList.Model
{
	public class ShoppingList
	{
		public ShoppingList(string name, string id)
		{
      Id = id;
			Name = name;
			GroceryItems = new List<GroceryItem>();
		}

    public string Id { get; set; }

		public string Name { get; set; }

		public List<GroceryItem> GroceryItems { get; }
	}
}
