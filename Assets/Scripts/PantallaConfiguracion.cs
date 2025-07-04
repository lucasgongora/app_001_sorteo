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
using static UnityEditor.Progress;

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
        [SerializeField] private string grupoActualmenteSeleccionado;

        [SerializeField] private GameObject fondoDisufo;
        [SerializeField] private GameObject fondoDisufoSubVentana;
        [SerializeField] private GameObject ventanaConfigNumero;
        [SerializeField] private GameObject ventanaConfigGrupos;
        [SerializeField] private GameObject ventanaConfigPodios;
        [SerializeField] private GameObject subVentanaGrupoNuevo;
        [SerializeField] private GameObject subVentanaIntegranteNuevo;
        [SerializeField] private GameObject subVentanaEditarIntegrante;
        [SerializeField] private GameObject mensajeFaltaDeCaracteres;
        [SerializeField] private GameObject mensajeNoMasGrupos;
        [SerializeField] private GameObject mensajeConfirmaEliminacion;
        [SerializeField] private GameObject mensajeNoEliminarUltimoGrupo;

        [SerializeField] private string auxiliarDigitosParticipantes = "";
        [SerializeField] private int auxiliarCantidadGanadores = 1;
        [SerializeField] private TextMeshProUGUI digitosParticipantesText;
        [SerializeField] private TextMeshProUGUI cantidadParticipantesText;
        [SerializeField] private TextMeshProUGUI cantidadGanadoresNumeroText;
        [SerializeField] private TextMeshProUGUI cantidadGanadoresGruposText;
        [SerializeField] private TextMeshProUGUI nombreGrupoSeleccionadoText;
        [SerializeField] private TextMeshProUGUI ventanitaGanadoresPodioText;
        [SerializeField] private TMP_InputField subVentanitaIntegranteNuevoText;
        [SerializeField] private TMP_InputField subVentanitaGrupoNuevoText;
        [SerializeField] private TMP_Dropdown desplegableGrupos;

        public GestorDeGrupos gestorDeGrupos;
        public string auxiliarGrupoNuevoIngresado;

        public int indexDropdown;
        public string valueDropdown;
        public bool edicionGrupo;
        public bool configGrupo;
        public string[] auxiliarGrupo = new string[10]; // Array para almacenar los grupos

        public GameObject botonBT;
        public Transform contenidoScrollView;

        private string[] auxiliarIntegrantes;
        private string auxiliarGrupoEnUso;
        /****************************************************************************************** */

        private void Awake()
        {
            if (VerificacionDatosVacios())
            {
                CargaGruposPorDefecto();
            }
            else
            {
                gestorDeGrupos.RecuperacionDatosPersistentesGrupos();
            }
            //sorteoTipo = "grupo";
            RecuperacionDatosPersistentesGenerales();
            CargaDatosGenerales(); 

        }
        // Start is called before the first frame update
        void Start()
        {
            configGrupo = false;    
        }

        // Update is called once per frame
        void Update()
        {
            if (configGrupo)
            {
                grupoActualmenteSeleccionado = desplegableGrupos.options[desplegableGrupos.value].text;
                gestorDeGrupos.grupoSeleccionado = grupoActualmenteSeleccionado;
                indexDropdown = desplegableGrupos.value;
                gestorDeGrupos.indiceDropDownGrupoSeleccionado = indexDropdown;
            }
            
        }
        public bool VerificacionDatosVacios()
        {
            bool datosVacios = true;
            string _grupos = PlayerPrefs.GetString("grupos");
            string[] __grupos = _grupos.Split(',');

            Debug.Log("Verificando datos vacíos: " + _grupos);
            if (__grupos[0] == "" || __grupos == null)
            {
                datosVacios = true;
            }
            else
            {
                datosVacios = false;
            }
            return datosVacios;
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
            configGrupo = true;
            if (sorteoTipo == "numero")
            { 
                ventanaConfigNumero.SetActive(true);
                digitosParticipantesText.text = cantidadParticipantes.ToString();
                auxiliarDigitosParticipantes = "";
                //ventanaNumerosConfig = true;
                //ventanaGruposConfig = false;
                //digitosParticipantes = "";
                //digitosParticipantesText.text = cantidadParticipantes.ToString();
            }
            if (sorteoTipo == "grupo")
            {
                ventanaConfigGrupos.SetActive(true);
                
                CargaTodosLosGruposAlDropdown();
                BuscaGrupoEnUso();
                //ventanaGruposConfig = true;
                //ventanaNumerosConfig = false;
            }
        }
        public void BuscaGrupoEnUso()
        {
            for (int i = 0; i < desplegableGrupos.options.Count; i++)
            {
                if (desplegableGrupos.options[i].text == auxiliarGrupoEnUso)
                {
                    desplegableGrupos.value = i;
                    desplegableGrupos.RefreshShownValue();
                }
            }
        }
        public void BotonConfiguracionPodio()
        {
            if (sorteoTipo == "numero")
            {
                ventanitaGanadoresPodioText.text = cantidadGanadoresNumeroText.text;
                auxiliarCantidadGanadores = Convert.ToInt32(cantidadGanadoresNumeroText.text);
                podioNumerosCant = auxiliarCantidadGanadores;
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
            GuardaDatosPersistentesConfig();
            gestorDeGrupos.GuardarDatosPersistentes();
            PlayerPrefs.Save();
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
                podioNumerosCant = auxiliarCantidadGanadores;
            }
            if (sorteoTipo == "grupo")
            {
                auxiliarCantidadGanadores = Convert.ToInt32(ventanitaGanadoresPodioText.text);
                cantidadGanadoresGruposText.text = ventanitaGanadoresPodioText.text;
                podioGruposCant = auxiliarCantidadGanadores;
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

        //temporalmente en desuso. 
        //esta funsion se ampliará o modificará en otra posible version para que al presionar el boton de
        //cancelar se reviertan los cambios efectuados en los grupos y no se guarden en el playerprefs.
        public void BotonCancelCerrarVentana()         {
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
                sorteoNumerosCant = cantidadParticipantes;
                ventanaConfigNumero.SetActive(false);
                fondoDisufo.SetActive(false);
            }
        }

        /************ VENTANA CONFIGURACION Y GESTION DE GRUPOS E INTEGRANTES ****************************************************************************** */
        public void BotonOKGrupos()
        {
            grupoActualmenteSeleccionado = desplegableGrupos.options[desplegableGrupos.value].text;
            nombreGrupoSeleccionadoText.text = grupoActualmenteSeleccionado;        
            gestorDeGrupos.GuardarDatosPersistentes();
            auxiliarGrupoEnUso = grupoActualmenteSeleccionado;
            gestorDeGrupos.grupoSeleccionado = grupoActualmenteSeleccionado;
            GuardaDatosPersistentesConfig();
            PlayerPrefs.Save();
            
            ventanaConfigGrupos.SetActive(false);
            fondoDisufo.SetActive(false);
            configGrupo = false;
        }
        public void BotonAgregarIntegrante()
        {
            subVentanitaIntegranteNuevoText.text = "";
            fondoDisufoSubVentana.SetActive(true);
            subVentanaIntegranteNuevo.SetActive(true);
        }
        public void BotonIntegranteSeleccinoado()
        {
            subVentanaEditarIntegrante.SetActive(true);
            fondoDisufoSubVentana.SetActive(true);
        }
        public void BotonAgregarGrupo()
        {
            bool puedeAgregarGrupo = gestorDeGrupos.ReportarCantidadGrupos();
            if (puedeAgregarGrupo)
            {
                fondoDisufoSubVentana.SetActive(true);
                subVentanaGrupoNuevo.SetActive(true);
            }
            else
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeNoMasGrupos.SetActive(true);
            }

        }
        public void BotonEditarGrupo()
        {
            edicionGrupo = true;
            subVentanitaGrupoNuevoText.text = desplegableGrupos.options[desplegableGrupos.value].text;
            fondoDisufoSubVentana.SetActive(true);
            subVentanaGrupoNuevo.SetActive(true);
        }
        public void EliminaIntegrante(/*MasterServerEvent msEvent*/)
        {

            fondoDisufoSubVentana.SetActive(false);
            subVentanaEditarIntegrante.SetActive(false);
        }
        public void EditaIntegrante()
        {

            fondoDisufoSubVentana.SetActive(false);
            subVentanaEditarIntegrante.SetActive(false);
        }

        /*********** SUB-VENTANA INGRESO NOMBRE DE GRUPO NUEVO ****************************************************************************** */
        public void BotonOkGrupoNuevo()
        {   
            if(edicionGrupo == false)
            {
                auxiliarGrupoNuevoIngresado = subVentanitaGrupoNuevoText.text.Trim().ToUpper();
                CargaGrupoAlDropdown(auxiliarGrupoNuevoIngresado);// Obtener el texto del campo de entrada y eliminar espacios en blanco
            }
            else
            {
                int index = desplegableGrupos.value;
                string grupoSeleccionado = desplegableGrupos.options[desplegableGrupos.value].text;
                auxiliarGrupoNuevoIngresado = subVentanitaGrupoNuevoText.text.Trim().ToUpper();
                gestorDeGrupos.EditarNombreDeGrupo(grupoSeleccionado, auxiliarGrupoNuevoIngresado);
                desplegableGrupos.options[index].text = auxiliarGrupoNuevoIngresado;
                edicionGrupo = false;
            }
            
            fondoDisufoSubVentana.SetActive(false);
            subVentanaGrupoNuevo.SetActive(false);
            desplegableGrupos.RefreshShownValue();
        }
        public void BotonCancelarIngresoGrupoNuevo()
        {
            edicionGrupo = false;
            fondoDisufoSubVentana.SetActive(false);
            subVentanaGrupoNuevo.SetActive(false);
        }
        public void BotonBorrarGrupoSeleccionado()
        {
            if (desplegableGrupos.options.Count < 2)
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeNoEliminarUltimoGrupo.SetActive(true);
                return;
            }
            else
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeConfirmaEliminacion.SetActive(true);
                if (desplegableGrupos.value >= 1)
                {
                    subVentanitaGrupoNuevoText.text = desplegableGrupos.options[desplegableGrupos.value - 1].text;

                }
                else
                {
                    subVentanitaGrupoNuevoText.text = desplegableGrupos.options[desplegableGrupos.value + 1].text;
                }
            }
        }
        public void BotonCerrarVentanNoEliminarUltimoGrupo()
        {
            fondoDisufoSubVentana.SetActive(false);
            mensajeNoEliminarUltimoGrupo.SetActive(false);
        }
        public void ConfirmaEliminacion()
        {
            
            string grupoSeleccionado = desplegableGrupos.options[desplegableGrupos.value].text;
            gestorDeGrupos.BorrarGrupo(grupoSeleccionado);

            //aqui se debería eliminar el grupo del dropdown
            int index = desplegableGrupos.value;

            if(desplegableGrupos.options.Count -1 == index)
            {
                desplegableGrupos.options.RemoveAt(index);
                desplegableGrupos.value = index - 1; // Cambia al grupo anterior
            }
            else
            {
                desplegableGrupos.options.RemoveAt(index);
            }
            desplegableGrupos.RefreshShownValue();
            fondoDisufoSubVentana.SetActive(false);
            mensajeConfirmaEliminacion.SetActive(false);

        }
        public void CancelaEliminacion()
        {
            fondoDisufoSubVentana.SetActive(false);
            mensajeConfirmaEliminacion.SetActive(false);
        }
        public void BotonCerrarMensajeNoMasGrupo()
        {
            mensajeNoMasGrupos.SetActive(false);
            fondoDisufoSubVentana.SetActive(false);
        }

        /************ SUB-VENTANA INGRESO NOMBRE DE INTEGRANTE NUEVO ****************************************************************************** */
        public void BotonCancelarSubVentanaIntegrante()
        {
            fondoDisufoSubVentana.SetActive(false);
            subVentanaIntegranteNuevo.SetActive(false);
        }
        public void BotonOKSubVentanaIntegrante()
        {
            string nombreIngresado = subVentanitaIntegranteNuevoText.text.Trim().ToUpper();
            if(nombreIngresado != "")
            {
                GameObject nuevoIntegrante = Instantiate(botonBT, contenidoScrollView);
                TextMeshProUGUI textoBoton = nuevoIntegrante.GetComponentInChildren<TextMeshProUGUI>();
                if (textoBoton != null)
                {
                    textoBoton.text = nombreIngresado;
                }

                Button botonComponente = nuevoIntegrante.GetComponent<Button>();
                if (botonComponente != null)
                {
                    botonComponente.onClick.AddListener(BotonIntegranteSeleccinoado);
                }

                gestorDeGrupos.GestorDeIntegrantes(indexDropdown, grupoActualmenteSeleccionado, nombreIngresado);
                fondoDisufoSubVentana.SetActive(false);
                subVentanaIntegranteNuevo.SetActive(false);
            }
            
        }
        /************ SERVICIOS DE CARGA DE INTEGRANTES POR GRUPO ****************************************************************************** */

        public void PasaIntegrantePorGrupoCorrespondiente()
        {

        }


        /************ SERVICIOS DE CARGA DE DATOS EN PANTALLA ****************************************************************************** */
        public void CargaGrupoAlDropdown(string grupoNuevo)
        {
            desplegableGrupos.options.Add(new TMP_Dropdown.OptionData(grupoNuevo));
            desplegableGrupos.RefreshShownValue();
            gestorDeGrupos.AgregarGrupo(grupoNuevo);
        }
        public void CargaTodosLosGruposAlDropdown()
        {
            string grupos_auxiliar = gestorDeGrupos.PasaDatosPersistentesGrupos(10);
            string[] array_grupos_auxiliar = grupos_auxiliar.Split(',');

            // Limpia todas las opciones actuales
            desplegableGrupos.ClearOptions();

            // Crea una lista de opciones nuevas
            List<TMP_Dropdown.OptionData> opciones = new List<TMP_Dropdown.OptionData>();
            foreach (string grupo in array_grupos_auxiliar)
            {
                if (!string.IsNullOrEmpty(grupo))
                    opciones.Add(new TMP_Dropdown.OptionData(grupo));
            }

            // Asigna las nuevas opciones al dropdown
            desplegableGrupos.AddOptions(opciones);
            desplegableGrupos.RefreshShownValue();

        }
        public void CargaDatosGenerales()
        {
            cantidadParticipantesText.text = sorteoNumerosCant.ToString();
            cantidadGanadoresNumeroText.text = podioNumerosCant.ToString();
            cantidadGanadoresGruposText.text = podioGruposCant.ToString();
            if (auxiliarGrupoEnUso == null || auxiliarGrupoEnUso == "")
            {
                nombreGrupoSeleccionadoText.text = desplegableGrupos.options[0].text;
            }
            else
            {
                nombreGrupoSeleccionadoText.text = auxiliarGrupoEnUso;
            }
            
            if (sorteoSeleccionado == "grupo")
            {
                sorteoTipo = "grupo";
                imagenTapaNumero.SetActive(true);
                imagenTapaGrupo.SetActive(false);
            }
            else 
            {
                sorteoTipo = "numero";
                imagenTapaNumero.SetActive(false);
                imagenTapaGrupo.SetActive(true);
            }
        }

        /************ SERVICIO GUARDADO DE DATOS PERSISTENTES  ****************************************************************************** */
        public void GuardaDatosPersistentesConfig()
        {
            int _sorteoNumerosCant = sorteoNumerosCant;
            int _podioNumerosCant = podioNumerosCant;
            int _podioGruposCant = podioGruposCant;
            string _sorteoSeleccionado = sorteoSeleccionado;
            string _auxiliarGrupoEnUso = auxiliarGrupoEnUso;

            PlayerPrefs.SetInt("sorteoNumerosCant", _sorteoNumerosCant);
            PlayerPrefs.SetInt("podioNumerosCant", _podioNumerosCant);
            PlayerPrefs.SetInt("podioGruposCant", _podioGruposCant);
            PlayerPrefs.SetString("sorteoSeleccionado", _sorteoSeleccionado);
            PlayerPrefs.SetString("grupoEnUso", _auxiliarGrupoEnUso);

            PlayerPrefs.Save();
        }
        /************ SERVICIO RECUPERACION DE DATOS PERSISTENTES  ****************************************************************************** */
        public void RecuperacionDatosPersistentesGruposThis()
        {
            string _grupos = PlayerPrefs.GetString("grupos");
            if (!string.IsNullOrEmpty(_grupos))
            {
                string[] temp = _grupos.Split(',');
                for (int i = 0; i < temp.Length && i < auxiliarGrupo.Length; i++)
                {
                    auxiliarGrupo[i] = temp[i];
                }
            }
        }
        public void RecuperacionDatosPersistentesGenerales()
        {
            int _sorteoNumerosCant = PlayerPrefs.GetInt("sorteoNumerosCant", 3);
            int _podioNumerosCant = PlayerPrefs.GetInt("podioNumerosCant", 1);
            int _podioGruposCant = PlayerPrefs.GetInt("podioGruposCant", 1);
            string _sorteoSeleccionado = PlayerPrefs.GetString("sorteoSeleccionado", "numero");
            string _auxiliarGrupoEnUso = PlayerPrefs.GetString("grupoEnUso", "");

            sorteoNumerosCant = _sorteoNumerosCant;
            podioNumerosCant = _podioNumerosCant;
            podioGruposCant = _podioGruposCant;
            sorteoSeleccionado = _sorteoSeleccionado;
            auxiliarGrupoEnUso = _auxiliarGrupoEnUso;
        }

        /************ CARGA GRUPOS POR DEFECTO PARA USUARIO POR PRIMERA VEZ  ****************************************************************************** */
        public void CargaGruposPorDefecto()
        {
         
                string[] elements = new string[] {"FAMILIA","AMIGOS","TRABAJO"};
                int item = 0;
                while (item < 3)
                {
                    string grupoPorDefecto = elements[item];
                    CargaGrupoAlDropdown(grupoPorDefecto);
                    //gestorDeGrupos.AgregarGrupo(grupoPorDefecto);
                    item++;
                }
            auxiliarGrupoEnUso = "FAMILIA";


        }
    }
}
