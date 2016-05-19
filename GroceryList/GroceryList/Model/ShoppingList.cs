using System;
using System.Collections.Generic;

namespace GroceryList.Model
{
	public class ShoppingList
	{
		public ShoppingList(string name)
		{
			Name = name;
			GroceryItems = new List<GroceryItem>();
		}

		public string Name { get; set; }

		public List<GroceryItem> GroceryItems { get; }
	}
}
