using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TalkSample
{

	public class SessionPage : ContentPage
	{
		private Label titleLabel;
		private Label whoLabel;
		private Button favoriteButton;
		private Label whenWhereLabel;
		private Label timeLabel;
		private Label locationLabel;
		private Label detailsLabel;
		private Label descriptionLabel;
		private Button feedbackButton;

		private Grid gridContent;

		internal SessionPage (Session session)
		{
			Title = "Session";

			const int bannerImageHeight = 135;
			string bannerImagePath = session.ImagePath.Replace ("icon", "large");
			var bannerImage = new BlurredImage {
				HeightRequest = bannerImageHeight,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFill,
				Source = bannerImagePath
			};

			titleLabel = new Label { 
				Text = session.Title,
				TextColor = Color.White
			};
					
			whoLabel = new Label { 
				Text = session.Who,
				TextColor = Color.White
			};
					
			const int speakerPhotoSize = 80;
			var speakerPhoto = new CircleImage {
				WidthRequest = speakerPhotoSize,
				HeightRequest = speakerPhotoSize,
				Aspect = Aspect.AspectFill,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Source = session.ImagePath,
			};

			favoriteButton = new Button {
				Text = " ADDED TO FAVORITES ",
				HeightRequest = 25,
				WidthRequest = 150,
			};

			const double relativeHeight = bannerImageHeight + (speakerPhotoSize * .75);
			var relativeLayout = new RelativeLayout {
				HeightRequest = relativeHeight,
				VerticalOptions = LayoutOptions.Start
			};

			relativeLayout.Children.Add (bannerImage, null, null, Constraint.RelativeToParent (v => v.Width));
			relativeLayout.Children.Add (
				titleLabel,
				Constraint.Constant (10),
				Constraint.RelativeToParent (parent => 5), 
				Constraint.RelativeToParent (parent => (parent.Width - 20)), 
				Constraint.Constant (bannerImageHeight * .6)
			);
				
			relativeLayout.Children.Add (
				whoLabel,
				Constraint.Constant (10),
				Constraint.Constant (bannerImageHeight * .68)
			);

			relativeLayout.Children.Add (
				speakerPhoto,
				Constraint.Constant (speakerPhotoSize / 4),
				Constraint.Constant (bannerImageHeight - speakerPhotoSize / 4)
			);

			double addHeightForFravorite = favoriteButton.HeightRequest / 2;
			relativeLayout.Children.Add (
				favoriteButton, 
				Constraint.RelativeToParent (v => {
					var width = 150;
					return v.Width - 35 - width;
				}), 
				Constraint.Constant (bannerImageHeight + addHeightForFravorite)
			);

			whenWhereLabel = new Label { Text = "WHEN AND WHERE", };

			timeLabel = new Label { Text = session.When, XAlign = TextAlignment.End };
			timeLabel.SetBinding (Label.TextProperty, "Time");

			locationLabel = new Label { Text = session.Location, XAlign = TextAlignment.End };

			detailsLabel = new Label { Text = "DETAILS" };

			descriptionLabel = new Label { Text = "Details are not available at this time.", XAlign = TextAlignment.End };
					
			feedbackButton = new DropShadowButton {
				Text = "Provide Feedback",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 35,
			};

			gridContent = new Grid ();

			gridContent.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			gridContent.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			gridContent.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			gridContent.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

			gridContent.Children.Add (whenWhereLabel);
			gridContent.Children.Add (timeLabel);
			gridContent.Children.Add (locationLabel);
			gridContent.Children.Add (detailsLabel);
			gridContent.Children.Add (descriptionLabel);
			gridContent.Children.Add (feedbackButton);

			Grid.SetRow (whenWhereLabel, 0);
			Grid.SetColumnSpan (whenWhereLabel, 2);

			Grid.SetRow (timeLabel, 1);
			Grid.SetColumnSpan (timeLabel, 2);

			Grid.SetRow (locationLabel, 2);
			Grid.SetColumnSpan (locationLabel, 2);

			Grid.SetRow (detailsLabel, 3);
			Grid.SetColumnSpan (detailsLabel, 2);

			Grid.SetRow (descriptionLabel, 4);
			Grid.SetColumnSpan (descriptionLabel, 2);

			Grid.SetRow (feedbackButton, 5);
			Grid.SetColumnSpan (feedbackButton, 2);

			var gridMain = new Grid ();

			gridMain.RowDefinitions.Add (new RowDefinition { Height = new GridLength (relativeHeight) });
			gridMain.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			gridMain.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

			gridMain.Children.Add (relativeLayout);
			gridMain.Children.Add (gridContent);
			Grid.SetRow (relativeLayout, 0);
			Grid.SetRow (gridContent, 1);

			// Uncomment to style the SessionPage
//			Beautify ();

			Content = new ScrollView { Content = gridMain };
		}

		private void Beautify ()
		{
			titleLabel.Font = Font.OfSize ("HelveticaNeue-Light", 22);
			titleLabel.LineBreakMode = LineBreakMode.WordWrap;

			whoLabel.Font = Font.SystemFontOfSize (NamedSize.Small);
			whoLabel.LineBreakMode = LineBreakMode.TailTruncation;

			favoriteButton.BackgroundColor = Color.Transparent;
			favoriteButton.TextColor = App.XamDarkBlue;
			favoriteButton.Font = Font.SystemFontOfSize (NamedSize.Micro);
			favoriteButton.BorderWidth = 1;
			favoriteButton.BorderRadius = 5;
			favoriteButton.BorderColor = App.XamDarkBlue;

			whenWhereLabel.TextColor = App.XamLightGray;
			whenWhereLabel.Font = Font.SystemFontOfSize (NamedSize.Small, FontAttributes.Bold);

			timeLabel.Font = Font.SystemFontOfSize (NamedSize.Small);
			timeLabel.TextColor = App.XamGray;
			timeLabel.XAlign = TextAlignment.Start;

			locationLabel.Font = Font.SystemFontOfSize (NamedSize.Small);
			locationLabel.TextColor = App.XamGray;
			locationLabel.XAlign = TextAlignment.Start;

			detailsLabel.TextColor = App.XamLightGray;
			detailsLabel.Font = Font.SystemFontOfSize (NamedSize.Small, FontAttributes.Bold);

			descriptionLabel.Font = Font.SystemFontOfSize (NamedSize.Small);
			descriptionLabel.TextColor = App.XamGray;
			descriptionLabel.XAlign = TextAlignment.Start;

			feedbackButton.TextColor = Color.White;
			feedbackButton.BackgroundColor = App.XamBlue;
			feedbackButton.BorderRadius = 5;
			feedbackButton.BorderColor = App.XamBlue;
			feedbackButton.Font = Font.SystemFontOfSize (NamedSize.Small);	

			gridContent.Padding = 20;
		}
	}
}
