using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using UnityEngine;

namespace app_001
{
    public class GestorPublicidad : MonoBehaviour
    {
        // Singleton pattern
        public static GestorPublicidad instance;

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

        void Awake()
        {
            // Singleton pattern - solo una instancia
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Hace que persista entre escenas
            }
            else
            {
                Destroy(gameObject); // Destruye duplicados
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                CreateBannerView();
                LoadInterstitialAd();
            });
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            // Create a 320x50 banner at bottom of the screen
            bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.Bottom);

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            bannerView.LoadAd(adRequest);

            // Configurar eventos del banner
            ListenToAdEvents();
        }

        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + bannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
                CreateBannerView();
            };
            // Raised when the ad is estimated to have earned money.
            bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
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

        public void DestroyAd()
        {
            if (bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                bannerView.Destroy();
                bannerView = null;
            }
        }

        // Métodos públicos para usar desde otras escenas
        public void MostrarBanner()
        {
            CreateBannerView();
        }

        public void OcultarBanner()
        {
            DestroyAd();
        }

        public void MostrarInterstitial()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        // Método para limpiar al destruir
        void OnDestroy()
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
            }
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }
        }
    }
}