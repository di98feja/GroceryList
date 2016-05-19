namespace GroceryList.Model
{
	public class GroceryItem
	{
		public GroceryItem(string name)
		{
			Name = name;
		}

		public string Name			{ get; set; }
		public bool		InBasket	{ get; set; }
		public double Amount		{ get; set; }
	}
}
