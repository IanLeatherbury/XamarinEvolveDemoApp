using System;
using System.ComponentModel;
using System.Drawing;

using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using TalkSample;
using TalkSample.iOS;
using System.Threading.Tasks;

using MonoTouch.CoreAnimation;
using System.Collections.Generic;
using MonoTouch.Foundation;

// Uncomment to export custom renderers
//[assembly: ExportRenderer (typeof(CircleImage), typeof(CircleImageRenderer))]
//[assembly: ExportRenderer (typeof(SessionCell), typeof(SessionCellRenderer))]
//[assembly: ExportRenderer (typeof(BlurredImage), typeof(BlurredImageRenderer))]
//[assembly: ExportRenderer (typeof(DropShadowButton), typeof(DropShadowButtonRenderer))]
//[assembly: ExportRenderer (typeof(ViewCell), typeof(HeaderCellRenderer))]
//[assembly: ExportRenderer (typeof(LoadingView), typeof(LoadingViewRenderer))]

namespace TalkSample.iOS
{
	public class CircleImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);



			if (Control == null || e.OldElement != null || Element == null)
				return;

			double min = Math.Min (Element.Width, Element.Height);
			Control.Layer.CornerRadius = (float)(min / 2.0);
			Control.Layer.MasksToBounds = false;
			Control.Layer.BorderColor = new CGColor (1, 1, 1);
			Control.Layer.BorderWidth = 3;
			Control.ClipsToBounds = true;

		}


		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (Control == null)
				return;

			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
			    e.PropertyName == VisualElement.WidthProperty.PropertyName) {
				double min = Math.Min (Element.Width, Element.Height);
				Control.Layer.CornerRadius = (float)(min / 2.0);
				Control.Layer.MasksToBounds = false;
				Control.ClipsToBounds = true;
			}
		}
	}

	public class SessionCellRenderer : ViewCellRenderer
	{
		public override MonoTouch.UIKit.UITableViewCell GetCell (Cell item, MonoTouch.UIKit.UITableView tv)
		{
			var cell = base.GetCell (item, tv);
			cell.Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}
	}

	public class BlurredImageRenderer : ImageRenderer
	{
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			if (Control == null)
				return;

			var blur = UIBlurEffect.FromStyle (UIBlurEffectStyle.Light);
			var blurView = new UIVisualEffectView (blur) {
				Frame = new RectangleF (0, 0, rect.Width, rect.Height)
			};
			Control.Add (blurView);
		}
	
	}

	public class DropShadowButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			Control.Layer.MasksToBounds = false;
			Control.ClipsToBounds = false;
			Control.Layer.ShadowColor = App.XamDarkBlue.ToCGColor ();
			Control.Layer.ShadowRadius = 7;
			Control.Layer.ShadowOffset = new SizeF (10, 10);
			Control.Layer.ShadowOpacity = 0.8f;

		}
	}

	public class HeaderCellRenderer : ViewCellRenderer
	{

		public override UITableViewCell GetCell (Cell item, UITableView tv)
		{
			var cell = base.GetCell (item, tv);
			cell.Accessory = MonoTouch.UIKit.UITableViewCellAccessory.None;
			return cell;
		}
	}


	public class LoadingViewRenderer : ViewRenderer<LoadingView,CustomLoadingView>
	{
		protected override async void OnElementChanged (ElementChangedEventArgs<LoadingView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				SetNativeControl (new CustomLoadingView ());

		}
	}

	public class CustomLoadingView : UIView
	{
		private class Circle
		{
			private readonly float minWidth;
			private readonly float maxWidth;
			private readonly float interval;

			private CAShapeLayer circle;
			private float width;
			private bool increasing;

			public Circle (CAShapeLayer circle, float width, float minWidth, float maxWidth, float interval, bool increasing)
			{
				this.circle = circle;
				this.circle.ContentsGravity = CALayer.GravityResizeAspectFill;
				this.minWidth = minWidth;
				this.maxWidth = maxWidth;
				this.interval = interval;
				this.increasing = increasing;
				this.circle.LineWidth = width;
			}

			public void Update ()
			{
				if (increasing) {
					if (width > maxWidth)
						increasing = false;
				} else {
					if (width < minWidth)
						increasing = true;
				}

				if (increasing) {
					width += interval;
				} else if (!increasing) {
					width -= interval;
				}

				circle.LineWidth = width;
			}
		}

		List<Circle> circles;
		bool wasInit = false;
		private Random rotationSpeed;

		public CustomLoadingView () : base (new RectangleF (0, 0, 1, 1))
		{
			Frame = UIScreen.MainScreen.Bounds;
			BackgroundColor = UIColor.White;
			rotationSpeed = new Random (12);
		}

		public override SizeF SizeThatFits (SizeF size)
		{
			return UIScreen.MainScreen.Bounds.Size;
		}

		public override void LayoutSubviews ()
		{
			if (!wasInit)
				circles = BuildCircles (50);
			AnimateCircles ();
		}

		List<Circle> BuildCircles (int seed)
		{
			wasInit = true;

			var listColors = new List<Color> {
				App.XamBlue,
				App.XamDarkBlue,
				App.XamGray,
				App.XamGreen,
				App.XamLightGray,
				App.XamPurple
			};
			var result = new List<Circle> ();

			float radius = 40;
			const float arcStart = 0;
			const float arcEnd = (float)(2 * Math.PI);

			bool clockwise = false;
			var random = new Random (seed);

			for (int i = 0; i < listColors.Count; i++) {

				var path = UIBezierPath.FromArc (Center, radius, 0f, 0.75f, false);
				var shape = new CAShapeLayer {
					Position = Center,
					Bounds = new RectangleF ((Frame.Width / 2) - 50f, (Frame.Height / 2) - 50f, 100f, 100f),
					Path = path.CGPath,
					StrokeColor = new CGColor ((float)listColors [i].R, (float)listColors [i].G, (float)listColors [i].B, (float)((i % 2 == 0) ? 0.5 : 0.8)),
					FillColor = UIColor.Clear.CGColor,
					LineWidth = 5,
					StrokeEnd = 0.9f
				};

				Layer.AddSublayer (shape);

				Rotate (shape, clockwise);

				var width = 20 + (random.NextDouble () * seed);
				var minWidth = 1 + (random.NextDouble ());
				var maxWidth = 1 + (random.NextDouble () * 35);

				var circle = new Circle (shape, (float)width, (float)minWidth, (float)maxWidth, (i % 2 == 0) ? 0.2f : 1.0f, false);
				result.Add (circle);

				radius += 10;
				clockwise = !clockwise;
			}

			return result;
		}



		void Rotate (CAShapeLayer caShapeLayer, bool clockwise)
		{
			var animRotate = CAKeyFrameAnimation.GetFromKeyPath ("transform");       

			if (clockwise) {
				animRotate.Values = new NSObject[] {
					NSNumber.FromFloat (0f),
					NSNumber.FromFloat ((float)Math.PI * 2)
				};
			} else {
				animRotate.Values = new NSObject[] {
					NSNumber.FromFloat (0f),
					NSNumber.FromFloat (-(float)Math.PI * 2)
				};
			}

			animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateZ);         
			animRotate.RepeatCount = int.MaxValue;
			animRotate.Duration = 2 + rotationSpeed.NextDouble ();

			caShapeLayer.AddAnimation (animRotate, "transform");
		}

		void AnimateCircles ()
		{
			var displayLink = CADisplayLink.Create (UpdateCircles);
			displayLink.AddToRunLoop (NSRunLoop.Main, NSRunLoop.NSDefaultRunLoopMode);
		}

		void UpdateCircles ()
		{
			foreach (var circle in circles)
				circle.Update ();
		}
	}
}