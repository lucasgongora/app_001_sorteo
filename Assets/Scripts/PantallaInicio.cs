using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app_001
{
    public class PantallaInicio : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
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
