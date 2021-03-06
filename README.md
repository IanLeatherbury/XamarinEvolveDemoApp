XamarinEvolveDemoApp
====================

Extending Xamarin.Forms on iOS

This is a demo app showing how to extend Xamarin.Forms. It has four main ideas.

## **1. Do what you can with Xamarin.Forms**
Enable the Beautify() functions in SessionCell.cs and SessionPage.cs to exhaust the possible options with Xamarin.Forms

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
* Be sure to set the shouldBeautify bool to true to see the custom loading view come up.
* 

## **4. The pattern**
This is the example given in LoadingViewRenderer:

```csharp
if (Control == null)
  SetNativeControl (new CustomLoadingView ());
				
if (e.OldElement != null)
{
  // unsubscribe
  // that may mean you’re doing something like…
  e.OldElement.ChildAdded -= OldElementOnChildAdded;
}

Then check that the new element !- null then subscribe.
if( e.NewElement != null) {
  //subscribe
  e.NewElement.ChildAdded += OldElementOChildAdded;
}
```

*Note: I did not write this code. Special thanks to the Xamarin team for putting this together!*
