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
//using static UnityEditor.Progress;
using System.Linq;

namespace app_001
{
    public class PantallaConfiguracion : MonoBehaviour
    {


        /**************** Variables PERSISTENTES *****************************************/
        [Header("************* DATOS PERSISTENTES ************************************************************************")]
        public bool premium = false;
        
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
        [SerializeField] private GameObject mensajeTamañoNumero;
        [SerializeField] private GameObject mensajeNoMasNombres;
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
        public bool edicionNumeros;
        public bool edicionGrupo;
        public bool configGrupo;
        public string[] auxiliarGrupo = new string[10]; // Array para almacenar los grupos

        public GameObject botonBT;
        public Transform contenidoScrollView;

        // Variables para manejo de integrantes
        private bool edicionIntegrante = false;
        private GameObject botonIntegranteSeleccionado;
        private int indiceIntegranteSeleccionado = -1;
        private string[] auxiliarIntegrantes;
        private string auxiliarGrupoEnUso;
        private int valorAnteriorDropdown = -1; // Variable para guardar el valor anterior
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
            // Mostrar banner de AdMob al entrar a la pantalla
            if (GestorPublicidad.instance != null)
            {
                GestorPublicidad.instance.MostrarBanner();
            }
            configGrupo = false;    
        }

        // Update is called once per frame
        void Update()
        {
            if (configGrupo && desplegableGrupos != null)
            {
                // Verificar que el dropdown tiene opciones
                if (desplegableGrupos.options.Count > 0)
                {
                    grupoActualmenteSeleccionado = desplegableGrupos.options[desplegableGrupos.value].text;
                    gestorDeGrupos.grupoSeleccionado = grupoActualmenteSeleccionado;
                    indexDropdown = desplegableGrupos.value;
                    gestorDeGrupos.indiceDropDownGrupoSeleccionado = indexDropdown;

                    // Solo ejecutar si el valor del dropdown cambió
                    if (indexDropdown != valorAnteriorDropdown)
                    {
                        CargaBotonesIntegrantesGrupoSeleccionado(indexDropdown);
                        valorAnteriorDropdown = indexDropdown;
                    }
                }
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
                
            }
            if (sorteoTipo == "grupo")
            {
                ventanaConfigGrupos.SetActive(true);
                
                CargaTodosLosGruposAlDropdown();
                BuscaGrupoEnUso();
                
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
            if (auxiliarCantidadGanadores < 6 )
            {
                auxiliarCantidadGanadores++;
                if (ventanitaGanadoresPodioText != null)
                {
                    ventanitaGanadoresPodioText.text = auxiliarCantidadGanadores.ToString();
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
            else
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeTamañoNumero.SetActive(true);
            }
        }

        public void BotonCerrarTamañoNumero()
        {
            fondoDisufoSubVentana.SetActive(false);
            mensajeTamañoNumero.SetActive(false);
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

        public string[] LimpiaGrupo(string[] grupo)
        {
            string[] array = grupo;
            array = array.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return array;
        }
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
            auxiliarGrupo = gestorDeGrupos.ObtenerGrupoPorNombre(grupoActualmenteSeleccionado);
            if (LimpiaGrupo(auxiliarGrupo).Length > gestorDeGrupos.cantIntegrantesPorGrupo - 1)
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeNoMasNombres.SetActive(true);
                return;
            }
            subVentanitaIntegranteNuevoText.text = "";
            fondoDisufoSubVentana.SetActive(true);
            subVentanaIntegranteNuevo.SetActive(true);
        }
        public void BotonIntegranteSeleccinoado()
        {
            // Capturar el botón que fue clickeado
            botonIntegranteSeleccionado = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            // Obtener el índice del botón en el ScrollView
            if (botonIntegranteSeleccionado != null)
            {
                indiceIntegranteSeleccionado = botonIntegranteSeleccionado.transform.GetSiblingIndex();
            }

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
        public void EliminaIntegrante()
        {
            if (botonIntegranteSeleccionado != null)
            {
                // Obtener el texto del botón antes de destruirlo
                TextMeshProUGUI textoBoton = botonIntegranteSeleccionado.GetComponentInChildren<TextMeshProUGUI>();
                string nombreIntegrante = textoBoton != null ? textoBoton.text : "";

                // Eliminar del gestor de grupos
                gestorDeGrupos.EliminarIntegrante(grupoActualmenteSeleccionado, nombreIntegrante);

                // Destruir el botón del ScrollView
                Destroy(botonIntegranteSeleccionado);

                // Limpiar referencia
                botonIntegranteSeleccionado = null;
                indiceIntegranteSeleccionado = -1;
            }

            fondoDisufoSubVentana.SetActive(false);
            subVentanaEditarIntegrante.SetActive(false);
        }
        public void EditaIntegrante()
        {
            if (botonIntegranteSeleccionado != null)
            {
                // Obtener el texto actual del botón
                TextMeshProUGUI textoBoton = botonIntegranteSeleccionado.GetComponentInChildren<TextMeshProUGUI>();
                string textoActual = textoBoton != null ? textoBoton.text : "";

                // Poner el texto actual en el campo de entrada
                subVentanitaIntegranteNuevoText.text = textoActual;

                edicionIntegrante = true;
                fondoDisufoSubVentana.SetActive(true);
                subVentanaIntegranteNuevo.SetActive(true);
                fondoDisufoSubVentana.SetActive(false);
                subVentanaEditarIntegrante.SetActive(false);
            }
        }

        /*********** SUB-VENTANA INGRESO NOMBRE DE GRUPO NUEVO ****************************************************************************** */
        public void BotonOkGrupoNuevo()
        {
            // Verificar que el campo de texto no esté vacío
            if (string.IsNullOrEmpty(subVentanitaGrupoNuevoText.text.Trim()))
            {
                fondoDisufoSubVentana.SetActive(true);
                mensajeFaltaDeCaracteres.SetActive(true);
                return;
            }
            if (edicionGrupo == false)
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
            gestorDeGrupos.ReagrupacionIntegrantes(index);
            desplegableGrupos.RefreshShownValue();

            // Eliminar todos los botones del scrollView
            for (int i = contenidoScrollView.childCount - 1; i >= 0; i--)
            {
                Destroy(contenidoScrollView.GetChild(i).gameObject);
                CargaBotonesIntegrantesGrupoSeleccionado(index);
            }

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
        public void BotonCerrarMensajeNoMasNombres()
        {
            fondoDisufoSubVentana.SetActive(false);
            mensajeNoMasNombres.SetActive(false);
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

            if (nombreIngresado != "")
            {
                if (edicionIntegrante && botonIntegranteSeleccionado != null)
                {
                    // Modo edición: actualizar el botón existente
                    TextMeshProUGUI textoBoton = botonIntegranteSeleccionado.GetComponentInChildren<TextMeshProUGUI>();
                    if (textoBoton != null)
                    {
                        // Obtener el texto anterior para actualizar en el gestor
                        string textoAnterior = textoBoton.text;
                        textoBoton.text = nombreIngresado;

                        // Actualizar en el gestor de grupos
                        gestorDeGrupos.EditarIntegrante(grupoActualmenteSeleccionado, textoAnterior, nombreIngresado);
                    }

                    // Limpiar variables de edición
                    edicionIntegrante = false;
                    botonIntegranteSeleccionado = null;
                    indiceIntegranteSeleccionado = -1;
                }
                else
                {
                    // Modo agregar: crear nuevo botón (código existente)
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
                }

                fondoDisufoSubVentana.SetActive(false);
                subVentanaIntegranteNuevo.SetActive(false);
            }

        }
        public void BotonCerrarMensajeLetras()
        {
            fondoDisufoSubVentana.SetActive(false);
            mensajeFaltaDeCaracteres.SetActive(false);
        }

    /************ SERVICIOS DE CARGA DE INTEGRANTES POR GRUPO EN PANTALLA SCROLL-VIEW ****************************************************************************** */
        public void LimpiarScrollView()
        {
            // Eliminar todos los botones del scrollView
            for (int i = contenidoScrollView.childCount - 1; i >= 0; i--)
            {
                Destroy(contenidoScrollView.GetChild(i).gameObject);
            }
        }
        public void CargaBotonesIntegrantesGrupoSeleccionado(int indexDropDown)
        {
            // Primero limpiar el scrollView para no acumular botones
            LimpiarScrollView();

            // Verificar que el dropdown tiene opciones
            if (desplegableGrupos.options.Count == 0)
            {
                Debug.Log("No hay grupos en el dropdown");
                return;
            }

            // Verificar que el índice es válido
            if (indexDropDown < 0 || indexDropDown >= desplegableGrupos.options.Count)
            {
                Debug.Log("Índice de dropdown inválido: " + indexDropDown);
                return;
            }

            // Obtener el grupo seleccionado
            string grupoSeleccionado = desplegableGrupos.options[indexDropDown].text;

            // Encontrar el array de integrantes del grupo
            string[] arrayGrupoAuxiliar = null;

            // Buscar el grupo en el array de grupos y obtener sus integrantes
            for (int i = 0; i < gestorDeGrupos.grupos.Length; i++)
            {
                if (gestorDeGrupos.grupos[i] == grupoSeleccionado)
                {
                    // Obtener el array correcto según el índice
                    switch (i)
                    {
                        case 0: arrayGrupoAuxiliar = gestorDeGrupos.grupo_00; break;
                        case 1: arrayGrupoAuxiliar = gestorDeGrupos.grupo_01; break;
                        case 2: arrayGrupoAuxiliar = gestorDeGrupos.grupo_02; break;
                        case 3: arrayGrupoAuxiliar = gestorDeGrupos.grupo_03; break;
                        case 4: arrayGrupoAuxiliar = gestorDeGrupos.grupo_04; break;
                        case 5: arrayGrupoAuxiliar = gestorDeGrupos.grupo_05; break;
                        case 6: arrayGrupoAuxiliar = gestorDeGrupos.grupo_06; break;
                        case 7: arrayGrupoAuxiliar = gestorDeGrupos.grupo_07; break;
                        case 8: arrayGrupoAuxiliar = gestorDeGrupos.grupo_08; break;
                        case 9: arrayGrupoAuxiliar = gestorDeGrupos.grupo_09; break;
                    }
                    break;
                }
            }

            // Verificar que se encontró el array
            if (arrayGrupoAuxiliar == null)
            {
                Debug.Log("No se encontró el grupo: " + grupoSeleccionado);
                return;
            }

            // Verificar que el array tiene elementos
            if (arrayGrupoAuxiliar.Length == 0)
            {
                Debug.Log("El grupo " + grupoSeleccionado + " no tiene integrantes");
                return;
            }

            // Cargar los integrantes en el scrollView
            for (int i = 0; i < gestorDeGrupos.cantIntegrantesPorGrupo - 1; i++)
            {
                // Verificar que el índice es válido
                if (i < arrayGrupoAuxiliar.Length)
                {
                    if (!string.IsNullOrEmpty(arrayGrupoAuxiliar[i]))
                    {
                        string nombre = arrayGrupoAuxiliar[i];
                        AgregaUnBoton(nombre);
                    }
                    else
                    {
                        // Si encuentra una posición vacía, salir del bucle
                        break;
                    }
                }
                else
                {
                    // Si el índice es mayor que el tamaño del array, salir
                    break;
                }
            }
        }

        public void AgregaUnBoton(string nombre)
        {
            //creo un objeto nuevo para instanciar un boton dentro del scrollView
            GameObject nuevoIntegrante = Instantiate(botonBT, contenidoScrollView);
            // le imprimo el nombre ingresado en el campo de texto del boton
            TextMeshProUGUI textoBoton = nuevoIntegrante.GetComponentInChildren<TextMeshProUGUI>();
            if (textoBoton != null)
            {
                textoBoton.text = nombre;
            }

            // le asigno un listener al boton para que al hacer click se llame a la funcion BotonIntegranteSeleccinoado
            Button botonComponente = nuevoIntegrante.GetComponent<Button>();
            if (botonComponente != null)
            {
                botonComponente.onClick.AddListener(BotonIntegranteSeleccinoado);
            }
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
            int _indexDropdown = indexDropdown;

            PlayerPrefs.SetInt("sorteoNumerosCant", _sorteoNumerosCant);
            PlayerPrefs.SetInt("podioNumerosCant", _podioNumerosCant);
            PlayerPrefs.SetInt("podioGruposCant", _podioGruposCant);
            PlayerPrefs.SetString("sorteoSeleccionado", _sorteoSeleccionado);
            PlayerPrefs.SetString("grupoEnUso", _auxiliarGrupoEnUso);
            PlayerPrefs.SetInt("indexDropdown", _indexDropdown);

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
         
                string[] elements = new string[] {"FAMILY","FRIENDS","SCHOOL"};
                int item = 0;
                while (item < 3)
                {
                    string grupoPorDefecto = elements[item];
                    CargaGrupoAlDropdown(grupoPorDefecto);
                    //gestorDeGrupos.AgregarGrupo(grupoPorDefecto);
                    item++;
                }
            auxiliarGrupoEnUso = "FAMILY";


        }
    }
}
