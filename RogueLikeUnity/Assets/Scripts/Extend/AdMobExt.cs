using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.Extend
{
    public class AdMobExt : MonoBehaviour
    {
        public string Android_Banner;
        public string Android_Interstitial;
        public string ios_Banner;
        public string ios_Interstitial;

        public static BannerView bannerView;
        public static InterstitialAd interstitial;
        private AdRequest request;

        bool is_close_interstitial = false;

        // Use this for initialization
        void Awake()
        {
            RequestInterstitial();
            RequestBanner();
        }


        //
        public void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = Android_Banner;
#elif UNITY_IPHONE
		string adUnitId = ios_Banner;
#else
		string adUnitId = "unexpected_platform";
#endif

            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            // Create an empty ad request.
            request = new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
                .AddTestDevice("xxxxxxxxxxxxxx")  // My test Device.
                .Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);
        }

        public void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = Android_Interstitial;
#elif UNITY_IPHONE
		string adUnitId = ios_Interstitial;
#else
		string adUnitId = "unexpected_platform";
#endif

            if (is_close_interstitial == true)
            {
            }

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            request = new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
                    .AddTestDevice("xxxxxxxxxxxxxx")  // My test Device.
                    .Build();

            // Load the interstitial with the request.
            interstitial.LoadAd(request);
            //interstitial.AdClosed += HandleAdClosed;

        }

        //Interstitialの破棄と再読み込み
        void HandleAdClosed(object sender, System.EventArgs e)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }
    }
}