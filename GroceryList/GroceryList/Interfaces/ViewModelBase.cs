using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroceryList.Interfaces
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected ViewModelBase(IStorageWrapper storage)
		{
			if (null == storage)
				throw new ArgumentNullException("Storage must not be null");

			m_storageWrapper = storage;
		}

		protected void NotifyChanged([CallerMemberName] string propertyName = "")
		{
      if (null == PropertyChanged) return;

      if (!m_propertiesToNotify.Contains(propertyName) && propertyName != "EndBatchUpdate")
        m_propertiesToNotify.Add(propertyName);

      if (m_batchUpdateCount == 0)
			{
        foreach (string property in m_propertiesToNotify)
				  PropertyChanged(this, new PropertyChangedEventArgs(property));
        m_propertiesToNotify.Clear();
			}
		}

    public void BeginBatchUpdate()
    {
      m_batchUpdateCount++;
    }

    public void EndBatchUpdate()
    {
      m_batchUpdateCount -= 1;
      if (m_batchUpdateCount < 0)
        throw new InvalidOperationException("Begin/End batch update not in sync");

      if (m_batchUpdateCount == 0)
      {
        NotifyChanged();
      }
    }

    protected List<string> m_propertiesToNotify = new List<string>();
		protected IStorageWrapper m_storageWrapper;
    protected int m_batchUpdateCount;
	}
}
