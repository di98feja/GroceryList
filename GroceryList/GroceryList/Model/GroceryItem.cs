using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroceryList.Model
{
	public class GroceryItem : INotifyPropertyChanged
	{
		public GroceryItem(string name, string id)
		{
      m_id = id;
			m_name = name;
		}

    public string Id
    {
      get { return m_id; }

      set
      {
        if (m_id != value)
        {
          m_id = value;
          NotifyChanged();
        }
      }
    }

    public string Name
    {
      get { return m_name; }
      set
      {
        if (m_name != value)
        {
          m_name = value;
          NotifyChanged();
        }
      }
    }

    public bool InBasket
    {
      get { return m_inBasket; }
      set
      {
        if (m_inBasket != value)
        {
          m_inBasket = value;
          NotifyChanged();
        }
      }
    }

    public double Amount
    {
      get { return m_amount; }
      set
      {
        if (m_amount != value)
        {
          m_amount = value;
          NotifyChanged();
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private string m_id;
    private string m_name;
    private bool m_inBasket;
    private double m_amount;

    private void NotifyChanged([CallerMemberName] string propertyName = "")
    {
      if (null == PropertyChanged) return;

      PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
