using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app_001
{
    public class PantallaSorteo : MonoBehaviour
    {
        public bool speedFast = false;
        public bool sound = true;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
       

        public void BotonGo()
        {
            
        }
        public void BotonVolverPantalla()
        {
            SceneManager.LoadScene(1);
        }
        public void BotonCerrarApp()
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                             Application.Quit();
            #endif
        }

        /************ SERVICIO GUARDADO DE DATOS PERSISTENTES  ****************************************************************************** */
        public void GuardaDatosPersistentesOpciones()
        {

        }
    }
}
