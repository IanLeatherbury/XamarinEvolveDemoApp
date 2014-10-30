XamarinEvolveDemoApp
====================

Extending Xamarin.Forms on iOS

This is a demo app showing how to extend Xamarin.Forms. It has three main ideas.

## **1. Do what you can with Xamarin.Forms**
Enable the beautify functions in SessionCell.cs and SessionPage.cs to exhaust the possible options with Xamarin.Forms

## **2. Modify existing controls**
Enable the first 5 custom renderers in the PlatformEnhancements.cs page to modify some custom iOS controls. These are:
* [assembly: ExportRenderer (typeof(CircleImage), typeof(CircleImageRenderer))]
* [assembly: ExportRenderer (typeof(SessionCell), typeof(SessionCellRenderer))]
* [assembly: ExportRenderer (typeof(BlurredImage), typeof(BlurredImageRenderer))]
* [assembly: ExportRenderer (typeof(DropShadowButton), typeof(DropShadowButtonRenderer))]
* [assembly: ExportRenderer (typeof(ViewCell), typeof(HeaderCellRenderer))]

## **3. Add an existing Xamarin.iOS view**
Enable the final customer renderer in PlatformEnhancements.cs to add an existing Xamarin.iOS loading view to your app.
* [assembly: ExportRenderer (typeof(LoadingView), typeof(LoadingViewRenderer))]
Be sure to set the shouldBeautify bool to true to see the custom loading view come up.

