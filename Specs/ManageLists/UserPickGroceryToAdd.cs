using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Specs.ManageLists
{
	public class GroceryItem
	{
		public GroceryItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}

	public class GroceryList
	{
		public GroceryList(string name)
		{
			Name = name;
			GroceryItems = new List<GroceryItem>();
		}

		public string Name { get; set; }

		public void AddGroceryItem(GroceryItem groceryItem)
		{
			if (null == groceryItem)
				throw new ArgumentNullException("groceryItem must not be null");

			GroceryItems.Add(groceryItem);
		}

		public List<GroceryItem> GroceryItems { get; }
	}

	[Trait("User pick a grocery to add", "")]
	public class UserPickGroceryToAdd
	{
		public void AddGroceryItemToList(GroceryList list, GroceryItem groceryItem)
		{
			list.AddGroceryItem(groceryItem);

		}

		[Fact(DisplayName = "Grocery is added to list")]
		public void GroceryIsAddedToList()
		{
			var list = new GroceryList("MyTestList");
			var groceryItem = new GroceryItem("MyTestItem");
			AddGroceryItemToList(list, groceryItem);
			Assert.Contains<GroceryItem>(groceryItem, list.GroceryItems);
		}

		[Fact(DisplayName = "List is persisted")]
		public void ListIsPersisted()
		{
			throw new NotImplementedException();
		}
	}
}
