using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app_001
{


    public class GestorPublicidad : MonoBehaviour
    {
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string adUnitIdInterstitial = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
  private string adUnitIdInterstitial = "ca-app-pub-3940256099942544/4411468910";
#else
  private string adUnitIdInterstitial = "unused";
#endif

        public InterstitialAd interstitialAd;

        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
              private string adUnitIdBanner = "ca-app-pub-3940256099942544/2934735716";
#else
              private string adUnitIdBanner = "unused";
#endif

        BannerView bannerView;


        // Start is called before the first frame update
        void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                SolicitarAnuncioBanner();
                LoadInterstitialAd();
            });
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SolicitarAnuncioBanner()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            // Create a 320x50 banner at top of the screen
            int x = 70; // Position at the left of the screen
            int y = 600; // Position at the top of the screen
            bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, x, y);

            var adRequest = new AdRequest();

            bannerView.LoadAd(adRequest);

        }
        public void LoadInterstitialAd()
        {
            // Limpia el anuncio anterior si existe
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }

            Debug.Log("Loading the interstitial ad.");

            // Crea la solicitud de anuncio
            var adRequest = new AdRequest();

            // Carga el interstitial
            InterstitialAd.Load(adUnitIdInterstitial, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // Si hay error, la carga falló
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Interstitial ad failed to load an ad with error: " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response: " + ad.GetResponseInfo());

                    interstitialAd = ad;

                    // Suscribirse al evento de cierre para recargar el interstitial automáticamente
                    interstitialAd.OnAdFullScreenContentClosed += () =>
                    {
                        Debug.Log("Interstitial cerrado, recargando...");
                        LoadInterstitialAd();
                    };
                });
        }
    }
}
