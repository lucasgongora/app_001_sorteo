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
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
