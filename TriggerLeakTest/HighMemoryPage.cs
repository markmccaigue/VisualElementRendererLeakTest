using Microsoft.Maui.Controls.Compatibility;
using ViewRenderer = Microsoft.Maui.Controls.Handlers.Compatibility.ViewRenderer;

#if ANDROID
using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

#if IOS
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;
#endif

namespace TriggerLeakTest
{
    public class HighMemoryPage : ContentPage
    {
        public HighMemoryPage()
        {
            var view = new CustomView();
            Content = view;
            
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, EventArgs e)
        {
            // The view is leaked on iOS without calling
            // DisconnectHandler explicitly
            // Content.Handler.DisconnectHandler();
        }
    }

    public class CustomView : View
    {
        private byte[] _bytes = new byte[1024 * 1024 * 10];
    }

#if IOS
    
    public class CustomViewRenderer : VisualElementRenderer<View>
    {
    }

#endif
    
#if ANDROID

    public class CustomViewRenderer : VisualElementRenderer<View>
    {
        public CustomViewRenderer(Context context) : base(context)
        {
        }
    }
    
#endif
    
}