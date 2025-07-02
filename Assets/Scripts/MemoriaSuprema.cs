using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app_001
{
    public class MemoriaSuprema : MonoBehaviour
    {
        public static MemoriaSuprema instance;

        public bool premium = false;
        public bool speedFast = false;
        public bool sound = true;
        public int sorteoNumerosCant;
        public int podioNumerosCant;
        public int podioGruposCant;

        public string[] grupos = new string[10];
        public string[] grupo_01;
        public string[] grupo_02;
        public string[] grupo_03;
        public string[] grupo_04;
        public string[] grupo_05;
        public string[] grupo_06;
        public string[] grupo_07;
        public string[] grupo_08;
        public string[] grupo_09;
        public string[] grupo_10;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GuardarDatosPersistentes()
        {

        }
        public void RecuperarDatosPersistentes()
        {
            
        }
    }
}
