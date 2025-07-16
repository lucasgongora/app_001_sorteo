using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using GoogleMobileAds.Api;

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

        public int[] ganadoresNoRepetirNum = new int[6];
        private string[] ganadoresNoRepetirGrup = new string[6];
        private int contPodios;
        private int contPodiosGrupo;
        private bool repetido;
        private bool sorteando;
        private int contadorReseteosHechos;

        public void Awake()
        {
            RecuperacionDatosPersistentes();
            
        }
        // Start is called before the first frame update
        void Start()
        {
            contPodios = 0;
            contador = 0;
            contadorReseteosHechos = 0;
            sorteando = false;

            // Mostrar banner de AdMob al entrar a la pantalla
            if (GestorPublicidad.instance != null)
            {
                GestorPublicidad.instance.MostrarBanner();
            }

            // Verificar que gestorDeGrupos no sea null antes de usarlo
            if (gestorDeGrupos != null)
            {
                gestorDeGrupos.RecuperacionDatosPersistentesGrupos();
            }
            if(sorteoSeleccionado == "numero")
            {
                indicadorSorteo.text = numerosParaSorteo.ToString() + " PARTICIPANTS";
            }
            else
            {
                indicadorSorteo.text = "GROUP " + grupoSeleccionado.ToUpper();
            }
            grupoParaSorteo = gestorDeGrupos.GrupoParaCargarBotones(indexDropdown);
            arrayRuleta = LimpiaGrupo(grupoParaSorteo);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /************ PANTALLA PRINCIPAL - BOTON ACCION Y NAVEGACION  ****************************************************************************** */

        public void SumarRandom(int num)
        {
            ganadoresNoRepetirNum[contPodios] = num;
            contPodios++;
        }
        public void BotonGo()
        {
            
            if (speedFast == false)
            {
                tiempoSorteando = 6f;
            }
            else
            {
                tiempoSorteando = 1.5f;
            }

            if (sorteoSeleccionado == "numero" && numerosParaSorteo > 0 && contador < cantPodioNum)
            {
                sorteando = true; // Indica que se est� sorteando
                int intentos = 0;
                const int maxIntentos = 100; // Evita bucles infinitos
                int nuevoNumero;

                do
                {
                    nuevoNumero = Random.Range(1, numerosParaSorteo + 1);
                    intentos++;
                    if (intentos > maxIntentos)
                    {
                        Debug.LogWarning("No se pudo encontrar un n�mero no repetido.");
                        return;
                    }  
                } while (ComprobarRepetidos(nuevoNumero));
                Debug.LogWarning("cantidad de contador: " + contador);
                numeroRandom = nuevoNumero;
                SumarRandom(numeroRandom);

                ruleta.SetActive(true);
                botonGo.SetActive(false);
                ganador.SetActive(false);
                ganadorText.text = "GANADOR: " + numeroRandom.ToString();
                Invoke("ContinuaSorteo", tiempoSorteando);
            }
            else if (sorteoSeleccionado == "grupo" && grupoParaSorteo != null && grupoParaSorteo.Length > 0 && arrayRuleta != null && arrayRuleta.Length > 0 && contador != cantPodioGrupo)
            {
                sorteando = true; // Indica que se est� sorteando
                int intentos = 0;
                const int maxIntentos = 100;
                string nuevoGanador;

                do
                {
                    // Verificar que arrayRuleta tenga elementos antes de acceder
                    if (arrayRuleta.Length == 0)
                    {
                        Debug.LogError("arrayRuleta est� vac�o. No se puede realizar el sorteo.");
                        return;
                    }

                    int index = Random.Range(0, arrayRuleta.Length);
                    indexRandom = index; // Guardar el �ndice seleccionado
                    nuevoGanador = arrayRuleta[index];
                    intentos++;
                    if (intentos > maxIntentos)
                    {
                        Debug.LogWarning("No se pudo encontrar un ganador de grupo no repetido.");
                        return;
                    }
                } while (ComprobarRepetidosGrupo(nuevoGanador));
                
                // Verificar que contPodiosGrupo no exceda el tama�o del array
                if (contPodiosGrupo < ganadoresNoRepetirGrup.Length)
                {
                    // Guardar el ganador en el array de ganadores de grupo
                    ganadoresNoRepetirGrup[contPodiosGrupo] = nuevoGanador;
                    contPodiosGrupo++;
                }
                else
                {
                    Debug.LogWarning("Se ha alcanzado el l�mite m�ximo de ganadores de grupo.");
                    return;
                }

                ruleta.SetActive(true);
                botonGo.SetActive(false);
                ganador.SetActive(false);
                ganadorText.text = "WINNER: " + nuevoGanador;
                Invoke("ContinuaSorteo", tiempoSorteando);
            }
            else
            {
                Debug.LogWarning("No se puede realizar el sorteo. Verificar configuraci�n.");
            }
        }

        public bool ComprobarRepetidos(int num)
        {
            for (int i = 0; i < contPodios; i++)
            {
                if (ganadoresNoRepetirNum[i] == num)
                {
                    return true; // El n�mero ya fue sorteado antes
                }
            }
            return false; // El n�mero no est� repetido
        }

        public bool ComprobarRepetidosGrupo(string nombre)
        {
            for (int i = 0; i < contPodiosGrupo; i++)
            {
                if (ganadoresNoRepetirGrup[i] == nombre)
                {
                    return true; // Ya fue ganador
                }
            }
            return false; // No fue ganador
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
            if (sorteoSeleccionado == "numero")
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
            }
            if (sorteoSeleccionado == "grupo")
            {
                // Verificar que indexRandom sea v�lido y arrayRuleta tenga elementos
                if (arrayRuleta != null && indexRandom >= 0 && indexRandom < arrayRuleta.Length)
                {
                    switch (contador)
                    {
                        case 0:
                            ganadorPrimero.text = arrayRuleta[indexRandom].ToString();
                            break;
                        case 1:
                            ganadorSegundo.text = arrayRuleta[indexRandom].ToString();
                            break;
                        case 2:
                            ganadorTercero.text = arrayRuleta[indexRandom].ToString();
                            break;
                        case 3:
                            ganadorCuarto.text = arrayRuleta[indexRandom].ToString();
                            break;
                        case 4:
                            ganadorQuinto.text = arrayRuleta[indexRandom].ToString();
                            break;
                        case 5:
                            ganadorSexto.text = arrayRuleta[indexRandom].ToString();
                            break;
                    }
                }
                else
                {
                    Debug.LogError("Error: indexRandom inv�lido o arrayRuleta vac�o");
                }
            }
            contador++;
            sorteando = false;
        }
        public void BotonVolverPantalla()
        {

            SceneManager.LoadScene(1);
        }
        public void BotonCerrarApp()
        {
            PlayerPrefs.Save(); // Asegurarse de guardar los cambios antes de cerrar la aplicaci�n
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
            if (!sorteando)
            {
                if (contadorReseteosHechos == 3)
                {
                    contadorReseteosHechos = 0;
                    ShowInterstitialAd();

                }

                contadorReseteosHechos++;
                botonGo.SetActive(true);
                ruleta.SetActive(false);
                ganador.SetActive(false);
                contador = 0;
                contPodios = 0;
                contPodiosGrupo = 0;
                ganadorPrimero.text = "WINNER";
                ganadorSegundo.text = "2°";
                ganadorTercero.text = "3°";
                ganadorCuarto.text = "4°";
                ganadorQuinto.text = "5°";
                ganadorSexto.text = "6°";

                ganadoresNoRepetirNum[0] = 0;
                ganadoresNoRepetirNum[1] = 0;
                ganadoresNoRepetirNum[2] = 0;
                ganadoresNoRepetirNum[3] = 0;
                ganadoresNoRepetirNum[4] = 0;
                ganadoresNoRepetirNum[5] = 0;
            }
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
                tiempoSorteando = 1.5f;
            }
            else
            {
                speedFast = false;
                tortuga.SetActive(true);
                liebre.SetActive(false);
                tiempoSorteando = 6f;
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


        /************ SERVICIO MUESTRA PUBLICIDAD INTERSTITIAL  ****************************************************************************** */

        public void ShowInterstitialAd()
        {
            if (GestorPublicidad.instance != null)
            {
                GestorPublicidad.instance.MostrarInterstitial();
            }
            else
            {
                Debug.LogError("GestorPublicidad instance is null.");
            }
        }
    }
}
