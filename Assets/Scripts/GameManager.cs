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



public class GameManager : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown desplegable;
    public bool ventanaNumerosConfig = false;
    public bool ventanaGruposConfig = false;
    public bool botonOKtecladoNumerico = false;
    public int cantidadParticipantes = 0;
    public int limitParticipantesXGrupo = 50;

    [SerializeField] private GameObject imagenTapaNumero;
    [SerializeField] private GameObject imagenTapaGrupo;
    [SerializeField] private GameObject panelEmergenteConfiguracion;
    [SerializeField] private GameObject fondoDisufo;
    [SerializeField] private GameObject ventanaConfigPodios;
    [SerializeField] private GameObject ventanaConfigCantParticipantes;
    [SerializeField] private GameObject ventanaConfigGrupos;
    [SerializeField] private GameObject ventanaGruposEIntegrantes;
    [SerializeField] private GameObject ventanaMensajesConfig;
    public TMP_InputField nombreGrupo;
    public string nombreConfirmadoGrupo;
    [SerializeField] private string grupoSeleccionado;
    [SerializeField] private GameObject nombreIntegranteNuevo;
    [SerializeField] private GameObject nombreGrupoNuevo;
    [SerializeField] private GameObject nombreIntegranteModif;
    [Header("Mensajes emergentes")]
    [SerializeField] private GameObject mensajeNumeroGrande;
    [SerializeField] private GameObject mensajeLimiteNombres;
    [SerializeField] private GameObject mensajeRestricLetras;
    [SerializeField] private GameObject mensajeConfirmElimination;

    [Header("Gestión de Participantes")]
    [SerializeField] private GameObject participanteBotonPrefab; // El prefab del botón para un integrante.
    [SerializeField] private Transform contentAreaScrollView; // El objeto "Content" del ScrollView.
    public TMP_InputField ultimoIntegranteIngresado; // Panel para añadir integrante.
    //[SerializeField] private TMP_InputField inputNombreParticipante; // InputField para el nombre del integrante.

    private bool isEditingGroup = false;

    // Estructura de datos principal para almacenar todo.
    private Dictionary<string, List<string>> gruposConParticipantes = new Dictionary<string, List<string>>();


    [SerializeField] private string tipoSorteo;
    [Header("Datos de NUMEROS")]
    [SerializeField] private int participantesSorteo;
    [SerializeField] private int podioSorteoNumerico;
    [Header("Datos de GRUPOS")]
    [SerializeField] private string ultimoGrupo;
    [SerializeField] private int podioSorteoGrupo;
    [Header("Sorteo seleccionado")]
    [SerializeField] public string sorteoTipo;
    [Header("Podio")]
    [SerializeField] public int participantes;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI participantesText;
    [SerializeField] private TextMeshProUGUI digitosParticipantesText;
    [SerializeField] private TextMeshProUGUI cantidadParticipantesText;
    [SerializeField] private TextMeshProUGUI cantidadGanadoresNumerosText;
    [SerializeField] private TextMeshProUGUI cantidadGanadoresGruposText;


    private string digitosParticipantes = "";
    private int digitosConvertidos = 0;

    // Start is called before the first frame update
    void Start()
    {
        sorteoTipo = "numero";
        participantes = 1;
        
        // Conectar el listener para el evento onValueChanged
        desplegable.onValueChanged.AddListener(OnDropdownValueChanged);

        // --- NUEVA INICIALIZACIÓN CON DICCIONARIO ---
        InicializarGrupos();

        // Asegurarse de que el valor inicial se muestre correctamente
        OnDropdownValueChanged(desplegable.value);

        // Establecer el límite de caracteres para el InputField.
        if (nombreGrupo != null)
        {
            nombreGrupo.characterLimit = 13;
        }
    }

    void InicializarGrupos()
    {
        // Limpiar estado anterior para consistencia.
        gruposConParticipantes.Clear();
        desplegable.ClearOptions();

        // Añadir grupos por defecto.
        gruposConParticipantes.Add("AMIGOS", new List<string>());
        gruposConParticipantes.Add("FAMILIA", new List<string>());

        // Poblar el Dropdown desde el diccionario.
        List<string> nombresDeGrupos = new List<string>(gruposConParticipantes.Keys);
        desplegable.AddOptions(nombresDeGrupos);
        
        // Opcional: Cargar la lista del primer grupo.
        MostrarListaGrupos();
    }

    // Update is called once per frame
    void Update()
    {
        cantidadParticipantesText.text = cantidadParticipantes.ToString();
        cantidadGanadoresNumerosText.text = participantes.ToString();
        participantesSorteo = cantidadParticipantes;
        podioSorteoNumerico = participantes;
    }

    // Este método se llamará cada vez que el valor del Dropdown cambie
    void OnDropdownValueChanged(int index)
    {
        // Asegurarse de que el índice es válido antes de intentar acceder a las opciones
        if (index >= 0 && index < desplegable.options.Count)
        {
            grupoSeleccionado = desplegable.options[index].text;
            MostrarListaGrupos(); // ¡Llamada clave para actualizar la lista de participantes!
        }
        else
        {
            // Si no hay opciones o el índice es inválido, limpiar la variable
            grupoSeleccionado = "";
            MostrarListaGrupos(); // Limpiará la vista si no hay nada seleccionado.
        }
    }

    //********** Pantalla 2 - Seleccion de sorteo y configuraciones **********************************************************************

    public void SorteoSelection(int num)
    {
        switch (num){
            case 0:
                imagenTapaNumero.SetActive(false);
                imagenTapaGrupo.SetActive(true);
                sorteoTipo = "numero";
                break;
            case 1:
                imagenTapaGrupo.SetActive(false);
                imagenTapaNumero.SetActive(true);
                sorteoTipo = "grupo";
                break;
        }   
    }

    
   

    public void BotonOKNumeros()
    {
        cantidadParticipantes = Convert.ToInt32(digitosParticipantes);
        ventanaConfigCantParticipantes.SetActive(false);
        ventanaNumerosConfig = false;
        //ventanaGruposConfig = false;
        fondoDisufo.SetActive(false);

    }

    public void BotonNumeroTeclado(string digitos)
    {  
        if(digitosParticipantes.Length < 9) {
            digitosParticipantes += digitos;
            digitosParticipantesText.text = digitosParticipantes.ToString();
        }
    }


    //********** Sub-botones de pantalla 2 - Seleccion de sorteo y configuraciones **********************************************************************
    public void BotonIncrementaGanadores()
    {
        if (participantes < 6)
        {
            participantes++;
            if (participantesText != null)
            {
                participantesText.text = participantes.ToString();
            }
            else
            {
                Debug.LogError("¡Error! El componente TextMeshProUGUI no está asignado en el Inspector.");
            }
        }
    }



    public void BotonReiniciaPodios()
    {
        participantes = 1;
        participantesText.text = participantes.ToString();
    }
    public void BotonCerrarGestorPodios()
    {
        ventanaConfigPodios.SetActive(false);
        fondoDisufo.SetActive(false);
    }
    public void BotonCerrarConfigGrupos()
    {
        ventanaConfigGrupos.SetActive(false);
        fondoDisufo.SetActive(false);
    }
    public void BotonCancelNumeros()
    {
        ventanaConfigCantParticipantes.SetActive(false);
        fondoDisufo.SetActive(false);
    }

    //********** Sub-botones de pantalla 2 - Configuraciones de Grupos **********************************************************************

    public void agregarOpcionDesplegable(string nuevaOpcion)
    {
        // Esta función ahora solo es un ayudante, la lógica principal está en ConfirmarNombreGrupo.
        desplegable.options.Add(new TMP_Dropdown.OptionData(nuevaOpcion));

        int nuevoIndice = desplegable.options.Count - 1;
        desplegable.value = nuevoIndice;

        desplegable.RefreshShownValue();
        OnDropdownValueChanged(desplegable.value);
    }

    public void BotonAgregaGrupo()
    {
        ventanaGruposEIntegrantes.SetActive(true);
        ventanaMensajesConfig.SetActive(true);
        nombreGrupoNuevo.SetActive(true);
        isEditingGroup = false;
        nombreGrupo.text = "";
        
    }
    public void BotonCancelarNombreGrupo()
    {
        ventanaGruposEIntegrantes.SetActive(false);
        nombreGrupoNuevo.SetActive(false);
        //nombreConfirmadoGrupo = "";
    }
    public void ConfirmarNombreGrupo()
    {
        string nuevoNombre = nombreGrupo.text.ToUpper();

        if (string.IsNullOrWhiteSpace(nuevoNombre))
        {
            Debug.LogWarning("El nombre del grupo no puede estar vacío.");
            return; 
        }
        
        if (nuevoNombre.Length > 13)
        {
            nuevoNombre = nuevoNombre.Substring(0, 13);
        }

        if (isEditingGroup)
        {
            // Modo Edición
            int indiceSeleccionado = desplegable.value;
            string nombreAntiguo = desplegable.options[indiceSeleccionado].text;

            // Actualizar el diccionario: Guardar la lista de participantes, eliminar la entrada antigua y crear la nueva.
            List<string> participantes = gruposConParticipantes[nombreAntiguo];
            gruposConParticipantes.Remove(nombreAntiguo);
            gruposConParticipantes.Add(nuevoNombre, participantes);

            // Actualizar el dropdown
            desplegable.options[indiceSeleccionado].text = nuevoNombre;
            desplegable.RefreshShownValue();
            OnDropdownValueChanged(indiceSeleccionado);
        }
        else
        {
            // Modo Agregar
            if (gruposConParticipantes.ContainsKey(nuevoNombre))
            {
                Debug.LogWarning("El grupo '" + nuevoNombre + "' ya existe.");
                return;
            }
            gruposConParticipantes.Add(nuevoNombre, new List<string>());
            agregarOpcionDesplegable(nuevoNombre);
        }

        ventanaGruposEIntegrantes.SetActive(false);
        nombreGrupoNuevo.SetActive(false);
    }
    public void BotonEditarNombreGrupo()
    {
        ventanaGruposEIntegrantes.SetActive(true);
        ventanaMensajesConfig.SetActive(true);
        nombreGrupoNuevo.SetActive(true);
        if (desplegable.options.Count == 0)
        {
            return;
        }

        isEditingGroup = true;
        
        nombreGrupo.text = desplegable.options[desplegable.value].text; 
    }
    public void BotonEliminaGrupo()
    {
        if (desplegable.options.Count == 0)
        {
            return; 
        }

        int indiceSeleccionado = desplegable.value;
        string nombreGrupoAEliminar = desplegable.options[indiceSeleccionado].text;

        // Eliminar del diccionario y del dropdown.
        gruposConParticipantes.Remove(nombreGrupoAEliminar);
        desplegable.options.RemoveAt(indiceSeleccionado);

        desplegable.RefreshShownValue();
        OnDropdownValueChanged(desplegable.value);
    }

    //********** Gestión de Participantes **********************************************************************

    public void MostrarListaGrupos()
    {
        // 1. Limpiar la vista anterior para no duplicar botones.
        foreach (Transform child in contentAreaScrollView)
        {
            Destroy(child.gameObject);
        }

        // Si no hay grupo seleccionado o no existe en el diccionario, no hacer nada más.
        if (string.IsNullOrEmpty(grupoSeleccionado) || !gruposConParticipantes.ContainsKey(grupoSeleccionado))
        {
            return;
        }

        // 2. Poblar la vista con los participantes del grupo seleccionado.
        List<string> participantes = gruposConParticipantes[grupoSeleccionado];
        foreach (string nombreParticipante in participantes)
        {
            GameObject nuevoBoton = Instantiate(participanteBotonPrefab, contentAreaScrollView);
            // Asumiendo que el prefab tiene un componente TextMeshProUGUI en uno de sus hijos.
            nuevoBoton.GetComponentInChildren<TMP_Text>().text = nombreParticipante;
        }
    }

    // --- AÑADE LAS FUNCIONES PARA AGREGAR PARTICIPANTES ---
    public void AbrirPanelAgregarParticipante() 
    { 
        ventanaMensajesConfig.SetActive(true);
        nombreIntegranteNuevo.SetActive(true);
    }
    public void ConfirmarIntegranteNuevo()
    { 
        //... lógica para añadir al diccionario y llamar a MostrarListaGrupos ...
        MostrarListaGrupos();
        ventanaMensajesConfig.SetActive(false);
        nombreIntegranteNuevo.SetActive(false);
    }

    public void CancelarIngresoParticipantes()
    {
        ventanaMensajesConfig.SetActive(false);
        nombreIntegranteNuevo.SetActive(false);
    }

    public void ConfirmarGrupoEditado()
    {
        
    }

    //********** Botones de pantallas principales **********************************************************************
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void ConfigSorteos()
    {
        if (sorteoTipo == "numero")
        {
            ventanaConfigCantParticipantes.SetActive(true);
            ventanaNumerosConfig = true;
            ventanaGruposConfig = false;
            fondoDisufo.SetActive(true);
            digitosParticipantes = "";
            digitosParticipantesText.text = cantidadParticipantes.ToString();
        }
        if (sorteoTipo == "grupo")
        {
            ventanaConfigGrupos.SetActive(true);
            ventanaGruposConfig = true;
            ventanaNumerosConfig = false;
            fondoDisufo.SetActive(true);
        }

    }
    public void ConfiguracionPodio()
    {
        ventanaConfigPodios.SetActive(true);
        fondoDisufo.SetActive(true);
    }


    public void CerrarApp()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }
}

