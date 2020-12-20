using Foundation;
using UIKit;
using WebKit;

namespace IOSApp.Controllers
{
    public class ViewController : UIViewController
    {
        private WKWebView _webView;

        public override void ViewDidLoad()
        {
            _webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
            View.AddSubview(_webView);
            _webView.LoadRequest(new NSUrlRequest(new NSUrl("https://emarketua.azurewebsites.net/")));
            base.ViewDidLoad();
        }
    }
}