using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{
	public class SessionCell : ViewCell
	{
		const int imageSize = 80;

		private CircleImage customSpeakerImage;
		private Label titleLabel;
		private ContentView titleContent;
		private Label whoLabel;
		private Label whenLabel;
		private Grid gridContent;

		public SessionCell ()
		{
			Height = 150;

			customSpeakerImage = new CircleImage {
				WidthRequest = imageSize,
				HeightRequest = imageSize,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Source = "missing.png"
			};
			customSpeakerImage.SetBinding (Image.SourceProperty, "ImagePath");

			titleLabel = new Label ();

			titleContent = new ContentView {
				Content = titleLabel
			};
			titleLabel.SetBinding (Label.TextProperty, "Title");

			whoLabel = new Label ();
			whoLabel.SetBinding (Label.TextProperty, "Who");

			whenLabel = new Label ();
			whenLabel.SetBinding (Label.TextProperty, "When");

			gridContent = new Grid ();

			gridContent.RowDefinitions.Add (new RowDefinition { Height = new GridLength (.55, GridUnitType.Star) });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = new GridLength (.225, GridUnitType.Star) });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = new GridLength (.225, GridUnitType.Star) });
			gridContent.ColumnDefinitions.Add (new ColumnDefinition { Width = GridLength.Auto });
			gridContent.ColumnDefinitions.Add (new ColumnDefinition { Width = 5 });
			gridContent.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

			gridContent.Children.Add (customSpeakerImage);
			gridContent.Children.Add (titleContent);
			gridContent.Children.Add (whoLabel);
			gridContent.Children.Add (whenLabel);

			Grid.SetRowSpan (customSpeakerImage, 3);
			Grid.SetColumn (titleContent, 2);
			Grid.SetColumn (whoLabel, 2);
			Grid.SetRow (whoLabel, 1);
			Grid.SetColumn (whenLabel, 2);
			Grid.SetRow (whenLabel, 2);

			// Uncomment to add some styling to the SessionCell
//			Beautify ();

			View = gridContent;
		}

		private void Beautify ()
		{
			titleLabel.VerticalOptions = LayoutOptions.End;
			titleLabel.Font = Font.OfSize ("HelveticaNeue-Light", 16);
			titleLabel.LineBreakMode = LineBreakMode.WordWrap;


			var font = Device.OnPlatform ("iOS", "Android", "WindowsPhone");

			titleContent.HorizontalOptions = LayoutOptions.FillAndExpand;
			titleContent.VerticalOptions = LayoutOptions.End;

			whoLabel.VerticalOptions = LayoutOptions.End;
			whoLabel.Font = Font.SystemFontOfSize (NamedSize.Micro);
			whoLabel.LineBreakMode = LineBreakMode.TailTruncation;

			whenLabel.VerticalOptions = LayoutOptions.Start;
			whenLabel.Font = Font.OfSize ("HelveticaNeue", NamedSize.Micro);

			gridContent.Padding = new Thickness (5, 10);
			gridContent.VerticalOptions = LayoutOptions.FillAndExpand;
		}
	}
}
