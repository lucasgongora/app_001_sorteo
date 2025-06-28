using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using app_001;

namespace app_001
{
    public class PantallaConfiguracion : MonoBehaviour
    {


        /**************** Variables PERSISTENTES *****************************************/
        [Header("************* DATOS PERSISTENTES ************************************************************************")]
        public bool premium = false;
        //   public bool speedFast = false;
        //   public bool sound = true;
        public int sorteoNumerosCant;
        public int podioNumerosCant;
        public int podioGruposCant;
        public string sorteoSeleccionado;

        

        /**************** Variables de USO LOCAL *****************************************/
        [Header("************* DATOS LOCALES ************************************************************************")]
        [SerializeField] private GameObject imagenTapaNumero;
        [SerializeField] private GameObject imagenTapaGrupo;
        
        [SerializeField] private string sorteoTipo;
        [SerializeField] private int cantidadParticipantes;

        [SerializeField] private GameObject fondoDisufo;
        [SerializeField] private GameObject fondoDisufoSubVentana;
        [SerializeField] private GameObject ventanaConfigNumero;
        [SerializeField] private GameObject ventanaConfigGrupos;
        [SerializeField] private GameObject ventanaConfigPodios;
        [SerializeField] private GameObject subVentanaGrupoNuevo;
        [SerializeField] private GameObject subVentanaIntegranteNuevo;
        [SerializeField] private GameObject subVentanaEditarIntegrante;
        [SerializeField] private GameObject mensajeFaltaDeCaracteres;

        [SerializeField] private string auxiliarDigitosParticipantes = "";
        [SerializeField] private int auxiliarCantidadGanadores = 1;
        [SerializeField] private TextMeshProUGUI digitosParticipantesText;
        [SerializeField] private TextMeshProUGUI cantidadParticipantesText;
        [SerializeField] private TextMeshProUGUI cantidadGanadoresNumeroText;
        [SerializeField] private TextMeshProUGUI cantidadGanadoresGruposText;
        [SerializeField] private TextMeshProUGUI ventanitaGanadoresPodioText;
        [SerializeField] private TMP_InputField subVentanitaIntegranteNuevoText;
        
        /****************************************************************************************** */

        private void Awake()
        {
                
        }
        // Start is called before the first frame update
        void Start()
        {
            sorteoTipo = "numero";
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /*********** PANTALLA PRINCIPAL ******************************************************************************* */

        public void BotonVolverPantalla()
        {
            SceneManager.LoadScene(0);
        }

        public void BotonesEleccionSorteo(int num)
        {
            switch (num)
            {
                case 0:
                    if (imagenTapaNumero != null) imagenTapaNumero.SetActive(false);
                    if (imagenTapaGrupo != null) imagenTapaGrupo.SetActive(true);
                    sorteoTipo = "numero";
                    break;
                case 1:
                    if (imagenTapaGrupo != null) imagenTapaGrupo.SetActive(false);
                    if (imagenTapaNumero != null) imagenTapaNumero.SetActive(true);
                    sorteoTipo = "grupo";
                    break;
            }
            sorteoSeleccionado = sorteoTipo;
        }

        public void BotonConfiguracionSorteos()
        {
            fondoDisufo.SetActive(true);
            
            if (sorteoTipo == "numero")
            { 
                ventanaConfigNumero.SetActive(true);
                //ventanaNumerosConfig = true;
                //ventanaGruposConfig = false;
                //digitosParticipantes = "";
                //digitosParticipantesText.text = cantidadParticipantes.ToString();
            }
            if (sorteoTipo == "grupo")
            {
                ventanaConfigGrupos.SetActive(true);
                //ventanaGruposConfig = true;
                //ventanaNumerosConfig = false;
            }
        }
        public void BotonConfiguracionPodio()
        {
            if (sorteoTipo == "numero")
            {
                ventanitaGanadoresPodioText.text = cantidadGanadoresNumeroText.text;
                auxiliarCantidadGanadores = Convert.ToInt32(cantidadGanadoresNumeroText.text);
            }
            if (sorteoTipo == "grupo")
            {
                ventanitaGanadoresPodioText.text = cantidadGanadoresGruposText.text;
                auxiliarCantidadGanadores = Convert.ToInt32(cantidadGanadoresGruposText.text);
            }
            ventanaConfigPodios.SetActive(true);
            fondoDisufo.SetActive(true);
        }
        public void BotonGo()
        {
            SceneManager.LoadScene(2);
        }

        public void BotonCerrarApp()
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                             Application.Quit();
            #endif
        }


        /************ VENTANA CONFIGURACION PODIO DE GANADORES ****************************************************************************** */
        public void BotonIncrementaGanadores()
        {
            
            if (auxiliarCantidadGanadores < 6)
            {
                auxiliarCantidadGanadores++;
                if (ventanitaGanadoresPodioText != null)
                {
                    ventanitaGanadoresPodioText.text = auxiliarCantidadGanadores.ToString();
                }
                else
                {
                    Debug.LogError("¡Error! El componente TextMeshProUGUI no está asignado en el Inspector.");
                }
            } 
        }
        public void BotonReiniciaPodios()
        {
            auxiliarCantidadGanadores = 1;
            ventanitaGanadoresPodioText.text = auxiliarCantidadGanadores.ToString();
        }
        public void BotonOKPodiosSeteados()
        {
            if (sorteoTipo == "numero")
            {
                auxiliarCantidadGanadores = Convert.ToInt32(ventanitaGanadoresPodioText.text);
                cantidadGanadoresNumeroText.text = ventanitaGanadoresPodioText.text;
            }
            if (sorteoTipo == "grupo")
            {
                auxiliarCantidadGanadores = Convert.ToInt32(ventanitaGanadoresPodioText.text);
                cantidadGanadoresGruposText.text = ventanitaGanadoresPodioText.text;
            }
            ventanaConfigPodios.SetActive(false);
            fondoDisufo.SetActive(false);
        }

        /************ VENTANA TECLADO NUMERICO PARA SETEAR PARTICIPANTES SORTEO NUMERICO ****************************************************************************** */
        public void BotonesTeclado(string digitos)
        {
            if (auxiliarDigitosParticipantes.Length < 9)
            {
                auxiliarDigitosParticipantes += digitos;
                digitosParticipantesText.text = auxiliarDigitosParticipantes;
            }
        }
        public void BotonCancelCerrarVentana()
        {
            ventanaConfigNumero.SetActive(false);
            ventanaConfigGrupos.SetActive(false);
            fondoDisufo.SetActive(false);
        }
        public void BotonOKNumeros()
        {
            // Verificar que se haya ingresado un número válido
            if (string.IsNullOrEmpty(auxiliarDigitosParticipantes))
            {
                Debug.LogWarning("No se ha ingresado ningún número");
                return;
            }
            else
            {
                cantidadParticipantesText.text = auxiliarDigitosParticipantes;
                cantidadParticipantes = Convert.ToInt32(auxiliarDigitosParticipantes);
                ventanaConfigNumero.SetActive(false);
                fondoDisufo.SetActive(false);
            }
        }

        /************ VENTANA CONFIGURACION Y GESTION DE GRUPOS E INTEGRANTES ****************************************************************************** */
        public void BotonOKGrupos()
        {
                ventanaConfigGrupos.SetActive(false);
                fondoDisufo.SetActive(false);
        }
        public void BotonAgregarIntegrante()
        {
            fondoDisufoSubVentana.SetActive(true);
            subVentanaIntegranteNuevo.SetActive(true);
        }

        /************ SUB-VENTANA INGRESO NOMBRE DE INTEGRANTE NUEVO ****************************************************************************** */
        public void BotonCancelarSubVentanaIntegrante()
        {
            fondoDisufoSubVentana.SetActive(false);
            subVentanaIntegranteNuevo.SetActive(false);
        }
        public void BotonOKSubVentanaIntegrante(string nombreIntegrante)
        {
           /* nombreIntegrante = subVentanitaIntegranteNuevoText.text.Trim(); // Obtener el texto del campo de entrada y eliminar espacios en blanco
            if (nombreIntegrante.Length > 0)
            {
                // Aquí se debería agregar el integrante al grupo seleccionado
                // Por ejemplo, si se está trabajando con un array de strings:
                // grupo_01.Add(nombreIntegrante);
                Debug.Log("Integrante agregado: " + nombreIntegrante);
            }
            else
            {
                mensajeFaltaDeCaracteres.SetActive(true);
            } */
            fondoDisufoSubVentana.SetActive(false);
            subVentanaIntegranteNuevo.SetActive(false);
        } 
       

    }
}
