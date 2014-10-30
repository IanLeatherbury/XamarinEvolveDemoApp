using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{
	public class SessionsHeaderCell : ViewCell
	{
		private readonly Label dateLabel;
		private readonly ContentView cellView;

		public SessionsHeaderCell ()
		{
			Height = 25;
			dateLabel = new Label ();
			dateLabel.SetBinding (Label.TextProperty, "Key");

			cellView = new ContentView {
				Content = dateLabel
			};

			// Uncomment to add some styling to the SessionHeaderCell
//			Beautify ();

			View = cellView;
		}

		private void Beautify ()
		{
			dateLabel.Font = Font.SystemFontOfSize (NamedSize.Small, FontAttributes.Italic);
			dateLabel.TextColor = Color.White;
			dateLabel.VerticalOptions = LayoutOptions.Center;

			cellView.BackgroundColor = App.XamBlue;
			cellView.Padding = 5;
		}
	}
	
}
