using GroceryList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.ViewModel
{
	public class GroceryItemsListViewModel
	{
		public GroceryItemsListViewModel()
		{
			m_groceryItems = new List<GroceryItem>();
		}

		public List<GroceryItem> GroceryItems
		{
			get { return m_groceryItems; }
		}

		public GroceryItem CreateNewGroceryItem()
		{
			var groceryItem = new GroceryItem("Ny vara");
			m_groceryItems.Add(groceryItem);
			return groceryItem;
		}

		private List<GroceryItem> m_groceryItems;
	}
}
