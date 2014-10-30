using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TalkSample
{
	internal class SessionsPage : ContentPage
	{
		private ListView list;
		private bool initialLoad;
		private bool shouldBeautify;

		public SessionsPage ()
		{
			Title = "Sessions";
			initialLoad = true;

			// Set this to true to load native loading animation
			shouldBeautify = false;

			var sessions = App.GetSessions ();

			list = new ListView {
				IsGroupingEnabled = true,
				GroupHeaderTemplate = new DataTemplate (typeof(SessionsHeaderCell)),
				ItemTemplate = new DataTemplate (typeof(SessionCell)),
				ItemsSource = sessions,
				HasUnevenRows = true,
				Opacity = 0
			};

			list.ItemSelected += (sender, e) => {
				if (list.SelectedItem == null)
					return;

				var session = (Session)e.SelectedItem;
				this.Navigation.PushAsync (new SessionPage (session));

				list.SelectedItem = null;
			};

			Content = list;
		}

		protected override async void OnAppearing ()
		{
			var loadingView = new LoadingView {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			var activityIndicatorView = new ContentView {
				Content = new ActivityIndicator {
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center,
					IsRunning = true
				}
			};
					
			if (initialLoad) {

				if (shouldBeautify) {
					Content = loadingView;
					await Task.Delay (3000);
					await loadingView.FadeTo (0, 100);
				} else {
					Content = activityIndicatorView;
					await Task.Delay (3000);
					await loadingView.FadeTo (0, 100);
				}

				list.Opacity = 0;
				Content = list;
				await list.FadeTo (1, 500);
				initialLoad = false;
			}
		}
	}
}
