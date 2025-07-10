using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace app_001
{
    public class PantallaSorteo : MonoBehaviour
    {

        public bool speedFast = false;
        public bool sound = true;
        string grupoSeleccionado;
        public string[] grupoParaSorteo;
        public int numerosParaSorteo;
        public int cantPodioNum;
        public int cantPodioGrupo;
        public string sorteoSeleccionado;
        public int indexDropdown;
        public float tiempoSorteando;

        public string[] arrayRuleta;
        public int contador;

        [SerializeField] TextMeshProUGUI ganadorPrimero;
        [SerializeField] TextMeshProUGUI ganadorSegundo;
        [SerializeField] TextMeshProUGUI ganadorTercero;
        [SerializeField] TextMeshProUGUI ganadorCuarto;
        [SerializeField] TextMeshProUGUI ganadorQuinto;
        [SerializeField] TextMeshProUGUI ganadorSexto;

        [SerializeField] GameObject ruleta;
        [SerializeField] GameObject ganador;
        [SerializeField] GameObject botonGo;

        [SerializeField] GameObject tortuga;
        [SerializeField] GameObject liebre;
        [SerializeField] GameObject soundON;
        [SerializeField] GameObject soundOFF;
        [SerializeField] TextMeshProUGUI indicadorSorteo;
        [SerializeField] TextMeshProUGUI ganadorText;

        public int numeroRandom;
        public int indexRandom;

        public GestorDeGrupos gestorDeGrupos;


        public void Awake()
        {
            RecuperacionDatosPersistentes();
            
        }
        // Start is called before the first frame update
        void Start()
        {
            contador = 0;
            tiempoSorteando = 5f;
            // Verificar que gestorDeGrupos no sea null antes de usarlo
            if (gestorDeGrupos != null)
            {
                gestorDeGrupos.RecuperacionDatosPersistentesGrupos();
            }
            if(sorteoSeleccionado == "numero")
            {
                indicadorSorteo.text = numerosParaSorteo.ToString() + " PARTICIPANTES";
            }
            else
            {
                indicadorSorteo.text = "GRUPO " + grupoSeleccionado.ToUpper();
            }
            grupoParaSorteo = gestorDeGrupos.GrupoParaCargarBotones(indexDropdown);
            arrayRuleta = LimpiaGrupo(grupoParaSorteo);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /************ PANTALLA PRINCIPAL - BOTON ACCION Y NAVEGACION  ****************************************************************************** */


        public void BotonGo()
        {
            if(sorteoSeleccionado == "numero" && numerosParaSorteo > 0 && contador != cantPodioNum)
            {
                numeroRandom = Random.Range(1, numerosParaSorteo);
                ruleta.SetActive(true);
                botonGo.SetActive(false);
                ganador.SetActive(false);
                ganadorText.text = "GANADOR: " + numeroRandom.ToString();
                Invoke("ContinuaSorteo", tiempoSorteando);

            }
            else if(sorteoSeleccionado == "grupo" && grupoParaSorteo.Length > 0)
            {
                indexRandom = Random.Range(0, arrayRuleta.Length);
                ruleta.SetActive(true);
                botonGo.SetActive(false);
                ganador.SetActive(false);
                

            }   
        }


        public void ReiniciarSorteo()
        {
            ganador.SetActive(false);
            ruleta.SetActive(false);
            botonGo.SetActive(true);

            ImprimirTablaNumeros();
        }

        public void ContinuaSorteo()
        {
            ruleta.SetActive(false);
            ganador.SetActive(true);
            Invoke("ReiniciarSorteo", 2f);
        }

        public void ImprimirTablaNumeros()
        {
            switch (contador)
            {
                case 0:
                    ganadorPrimero.text = numeroRandom.ToString();
                    break;
                case 1:
                    ganadorSegundo.text = numeroRandom.ToString();
                    break;
                case 2:
                    ganadorTercero.text = numeroRandom.ToString();
                    break;
                case 3:
                    ganadorCuarto.text = numeroRandom.ToString();
                    break;
                case 4:
                    ganadorQuinto.text = numeroRandom.ToString();
                    break;
                case 5:
                    ganadorSexto.text = numeroRandom.ToString();
                    break;
            }
            contador++;
        }
        public void BotonVolverPantalla()
        {
            SceneManager.LoadScene(1);
        }
        public void BotonCerrarApp()
        {
            PlayerPrefs.Save(); // Asegurarse de guardar los cambios antes de cerrar la aplicación
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                             Application.Quit();
            #endif
        }

        public string[] LimpiaGrupo(string[] grupo)
        {
            string[] array = grupo;
            array = array.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return array;
        }

        /************ BOTONES DE OPCIONES  ****************************************************************************** */
        public void BotonSpeed()
        {
            if (speedFast)
            {
                speedFast = false;
                int _speedFast = 0;
                PlayerPrefs.SetInt("velocidad", _speedFast);
                tortuga.SetActive(true);
                liebre.SetActive(false);
             }
            else
            {
                speedFast = true;
                int _speedFast = 1;
                PlayerPrefs.SetInt("velocidad", _speedFast);
                tortuga.SetActive(false);
                liebre.SetActive(true);
            
            }
            PlayerPrefs.Save();
        }
        public void BotonSound()
        {
            if (sound)
            {
                sound = false;
                int _sound = 0;
                PlayerPrefs.SetInt("sound", _sound);
                soundON.SetActive(false);
                soundOFF.SetActive(true);
            }
            else
            {
                sound = true;
                int _sound = 1;
                PlayerPrefs.SetInt("sound", _sound);
                soundON.SetActive(true);
                soundOFF.SetActive(false);
            }
            PlayerPrefs.Save();
        }
        
        public void BotonReset()
        {
            botonGo.SetActive(true);
            ruleta.SetActive(false);
            ganador.SetActive(false);
            contador = 0;
            ganadorPrimero.text = "GANADOR";
            ganadorSegundo.text = "SEGUNDO";
            ganadorTercero.text = "TERCERO";
            ganadorCuarto.text = "CUARTO";
            ganadorQuinto.text = "QUINTO";
            ganadorSexto.text = "SEXTO";
        }

        /************ SERVICIO RECUPERACION DE DATOS PERSISTENTES  ****************************************************************************** */
        public void RecuperacionDatosPersistentes()
        {
            int _speedFast = PlayerPrefs.GetInt("velocidad");
            if (_speedFast == 1)
            {
                speedFast = true;
                tortuga.SetActive(false);
                liebre.SetActive(true);
            }
            else
            {
                speedFast = false;
                tortuga.SetActive(true);
                liebre.SetActive(false);
            }

            int _sound = PlayerPrefs.GetInt("sound");
            if (_sound == 1)
            {
                sound = true;
                soundON.SetActive(true);
                soundOFF.SetActive(false);
            }
            else
            {
                sound = false;
                soundON.SetActive(false);
                soundOFF.SetActive(true);
            }

            string _grupoSeleccionado = PlayerPrefs.GetString("grupoSeleccionado");
            int _sorteoNumerosCant = PlayerPrefs.GetInt("sorteoNumerosCant");
            int _podioNumerosCant = PlayerPrefs.GetInt("podioNumerosCant", 1);
            int _podioGruposCant = PlayerPrefs.GetInt("podioGruposCant", 1);
            string _sorteoSeleccionado = PlayerPrefs.GetString("sorteoSeleccionado", "numero");
            int _indexDropdown = PlayerPrefs.GetInt("indexDropdown", 0);
            grupoSeleccionado = _grupoSeleccionado;
            numerosParaSorteo = _sorteoNumerosCant;
            cantPodioNum = _podioNumerosCant;
            cantPodioGrupo = _podioGruposCant;
            sorteoSeleccionado = _sorteoSeleccionado;
            indexDropdown = _indexDropdown;


        }
    }
}
