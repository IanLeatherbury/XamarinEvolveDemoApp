using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{
		
	internal class Session
	{
		public string ImagePath { get; private set; }
		public string Title { get; private set; }
		public string Who { get; private set; }
		public string When { get; private set; }
		public string Location { get; private set; }

		public Session (string imagePath, string title, string who, string when, string location)
		{
			ImagePath = imagePath;
			Title = title;
			Who = who;
			When = when;
			Location = location;
		}
	}

}
