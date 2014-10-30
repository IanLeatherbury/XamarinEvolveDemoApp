using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TalkSample
{
	public static class App
	{
		public static readonly Color XamGreen = Color.FromHex ("77D065");
		public static readonly Color XamPurple = Color.FromHex ("B455B6");
		public static readonly Color XamBlue = Color.FromHex ("3498DB");
		public static readonly Color XamDarkBlue = Color.FromHex ("2C3E50");
		public static readonly Color XamGray = Color.FromHex ("738182");
		public static readonly Color XamLightGray = Color.FromHex ("B4BCBC");

		public static Page GetMainPage ()
		{	
			var navigationPage = new NavigationPage (new SessionsPage ());

			navigationPage.BarTextColor = XamBlue;

			return navigationPage;
		}

		internal static ObservableCollection<Grouping<string, Session>> GetSessions ()
		{

			var sessions = new ObservableCollection<Grouping<string, Session>> {
				new Grouping<string, Session> (
					"Thursday October 9",
					new [] {
						new Session ("xamarin-icon.png", "Breakfast", "By Xamarin Evolve", "7:30 AM - 8:30 AM", "Meal & Sponsor Hall"),
						new Session ("craig-icon.jpg", "Your First Xamarin.Forms App", "By Craig Dunn", "11:00 AM - 12:15 PM", "Linnaeus Salon"),
						new Session ("jason-icon.jpg", "Extending Xamarin.Forms with your own controls and layouts", "By Jason Smith", "3:15 PM - 4:00 PM", "Crick Salon"),
					}
				),
				new Grouping<string, Session> (
					"Friday October 10",
					new [] {
						new Session ("xamarin-icon.png", "Breakfast", "By Xamarin Evolve", "7:30 AM - 8:30 AM", "Meal & Sponsor Hall"),
						new Session ("craig-icon.jpg", "Your First Xamarin.Forms App", "By Craig Dunn", "11:00 AM - 12:15 PM", "Linnaeus Salon"),
						new Session ("jason-icon.jpg", "Extending Xamarin.Forms with your own controls and layouts", "By Jason Smith", "3:15 PM - 4:00 PM", "Crick Salon"),
					}
				)
			};

			return sessions;
		}


	}
}

