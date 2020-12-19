using Foundation;
using UIKit;

namespace IOSApp.Resources
{
    public class ViewController : UIViewController
    {
        private UIWebView _webView;

        public override void ViewDidLoad()
        {
            _webView = new UIWebView(View.Bounds);
            _webView.LoadRequest(new NSUrlRequest(new NSUrl("https://emarketua.azurewebsites.net/")));
            base.ViewDidLoad();
        }
    }
}