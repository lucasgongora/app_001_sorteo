using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

namespace app_001
{
    public class PantallaInicio : MonoBehaviour
    {
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
        {    /*
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                SolicitarAnuncioBanner();
            });  */
            MobileAds.Initialize(initStatus =>
            {
                SolicitarBannerAdaptativo();
            });
        }


       
        public void SolicitarBannerAdaptativo()
        {
            // Si ya hay un banner, destrúyelo
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            // Calcula el ancho en píxeles del dispositivo
            int anchoPantalla = Screen.width;

            // Obtiene el tamaño adaptativo para el ancho actual y orientación
            AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(anchoPantalla);

            // Puedes elegir la posición (por ejemplo, centrado horizontalmente y y fijo)
            int x = (Screen.width - adaptiveSize.Width) / 2;
            int y = -1060; // O la altura que prefieras

            // Crea el banner adaptativo
            bannerView = new BannerView(adUnitIdBanner, adaptiveSize, x, y);

            // Carga el anuncio
            AdRequest request = new AdRequest();
            bannerView.LoadAd(request);
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
                SolicitarBannerAdaptativo(); // Retry loading the ad
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

        // Update is called once per frame
        void Update()
        {
        
        }

        public void BotonIdiomaEspañol()
        {
            SceneManager.LoadScene(1);
        }
        
        public void BotonAyudaCreditos()
        {
            SceneManager.LoadScene(3);
        }

        public void BotonCerrarApp()
        {
            #if UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
