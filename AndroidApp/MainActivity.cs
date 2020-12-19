using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            RequestedOrientation = ScreenOrientation.Portrait;

            _webView = FindViewById<WebView>(Resource.Id.webView1);
            _webView.SetWebViewClient(new WebViewClient());

            _webView.Settings.JavaScriptEnabled = true;
            _webView.Settings.UserAgentString = "AndroidTemplate";

            _webView.LoadUrl("https://emarketua.azurewebsites.net/");
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == KeyEvent.KeyCodeFromString("BACK") && _webView.CanGoBack())
            {
                _webView.GoBack();

                return true;
            }

            Finish();

            return base.OnKeyDown(keyCode, e);
        }
    }
}