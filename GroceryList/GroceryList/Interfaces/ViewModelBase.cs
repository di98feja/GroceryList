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
			if (null != PropertyChanged)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected IStorageWrapper m_storageWrapper;
	}
}
