namespace GroceryList.Model
{
	public class GroceryItem
	{
		public GroceryItem(string name)
		{
			Name = name;
		}

		public string Id				{ get; set; }
		public string Name			{ get; set; }
		public bool		InBasket	{ get; set; }
		public double Amount		{ get; set; }
	}
}
