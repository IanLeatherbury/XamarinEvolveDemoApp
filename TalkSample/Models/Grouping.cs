using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{

	internal class Grouping<K, T> : ObservableCollection<T>
	{
		public K Key { get; private set; }

		public Grouping (K key, IEnumerable<T> items)
		{
			Key = key;
			foreach (var item in items)
				Items.Add (item);
		}
	}
		
}
