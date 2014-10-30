using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{

	internal class SessionsPageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Grouping<string, Session>> SessionInfoSessionGrouping { get; private set; }

		public SessionsPageViewModel (ObservableCollection<Grouping<string, Session>> sessionInfoSessionGrouping)
		{
			SessionInfoSessionGrouping = sessionInfoSessionGrouping;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged ([CallerMemberName] string property = null) {
			var handler = PropertyChanged;

			if (handler != null)
				handler (this, new PropertyChangedEventArgs (property));
		}
	}
	
}
