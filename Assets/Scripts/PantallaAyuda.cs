using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app_001
{
    public class PantallaAyuda : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Mostrar banner de AdMob al entrar a la pantalla
            if (GestorPublicidad.instance != null)
            {
                GestorPublicidad.instance.MostrarBanner();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void AbrirLinkedIn()
        {
            Application.OpenURL("https://www.linkedin.com/in/lucas-góngora-developer-games-unity-a714256b");
        }

        public void AbrirKlanstart()
        {
            Application.OpenURL("https://www.klanstart.com/");
        }
        public void BotonVolver()
        {
            SceneManager.LoadScene(0);
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
