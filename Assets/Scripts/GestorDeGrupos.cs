using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace app_001
{
    public class GestorDeGrupos : MonoBehaviour
    {
        public int cantIntegrantesPorGrupo = 15;
        public string[] grupos = new string[10];
        public string[] grupo_00;
        public string[] grupo_01;
        public string[] grupo_02;
        public string[] grupo_03;
        public string[] grupo_04;
        public string[] grupo_05;
        public string[] grupo_06;
        public string[] grupo_07;
        public string[] grupo_08;
        public string[] grupo_09;
        public string grupoSeleccionado;
        public int indiceDropDownGrupoSeleccionado = 0;
        public int indiceIntegranteGrupo;
        private void Awake()
        {
            // Inicializar todos los arrays con el tamaño deseado
            grupo_00 = new string[cantIntegrantesPorGrupo];
            grupo_01 = new string[cantIntegrantesPorGrupo];
            grupo_02 = new string[cantIntegrantesPorGrupo];
            grupo_03 = new string[cantIntegrantesPorGrupo];
            grupo_04 = new string[cantIntegrantesPorGrupo];
            grupo_05 = new string[cantIntegrantesPorGrupo];
            grupo_06 = new string[cantIntegrantesPorGrupo];
            grupo_07 = new string[cantIntegrantesPorGrupo];
            grupo_08 = new string[cantIntegrantesPorGrupo];
            grupo_09 = new string[cantIntegrantesPorGrupo];
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string[] GrupoParaCargarBotones(int grupo)
        {
            string[] array = new string[cantIntegrantesPorGrupo];
            switch (grupo)
            {
                case 0:
                    array = grupo_00;
                    break;
                case 1:
                    array = grupo_01;
                    break;
                case 2:
                    array = grupo_02;
                    break;
                case 3:
                    array = grupo_03;
                    break;
                case 4:
                    array = grupo_04;
                    break;
                case 5:
                    array = grupo_05;
                    break;
                case 6:
                    array = grupo_06;
                    break;
                case 7:
                    array = grupo_07;
                    break;
                case 8:
                    array = grupo_08;
                    break;
                case 9:
                    array = grupo_09;
                    break;
            }
            return array;
        }

        public string[] ObtenerGrupoPorNombre(string nombreGrupo)
        {
            for (int i = 0; i < grupos.Length; i++)
            {
                if (grupos[i] == nombreGrupo)
                {
                    switch (i)
                    {
                        case 0: return grupo_00;
                        case 1: return grupo_01;
                        case 2: return grupo_02;
                        case 3: return grupo_03;
                        case 4: return grupo_04;
                        case 5: return grupo_05;
                        case 6: return grupo_06;
                        case 7: return grupo_07;
                        case 8: return grupo_08;
                        case 9: return grupo_09;
                    }
                }
            }
            return null; // Si no se encuentra el grupo
        }
        public void GestorDeIntegrantes(int index, string grupo, string integrante)
        {
            // Verificar que el grupo existe en la posición indicada
            if (grupos[index] == grupo)
            {
                switch (index)
                {
                    case 0:
                        AgregarIntegranteAGrupo(ref grupo_00, integrante);
                        break;
                    case 1:
                        AgregarIntegranteAGrupo(ref grupo_01, integrante);
                        break;
                    case 2:
                        AgregarIntegranteAGrupo(ref grupo_02, integrante);
                        break;
                    case 3:
                        AgregarIntegranteAGrupo(ref grupo_03, integrante);
                        break;
                    case 4:
                        AgregarIntegranteAGrupo(ref grupo_04, integrante);
                        break;
                    case 5:
                        AgregarIntegranteAGrupo(ref grupo_05, integrante);
                        break;
                    case 6:
                        AgregarIntegranteAGrupo(ref grupo_06, integrante);
                        break;
                    case 7:
                        AgregarIntegranteAGrupo(ref grupo_07, integrante);
                        break;
                    case 8:
                        AgregarIntegranteAGrupo(ref grupo_08, integrante);
                        break;
                    case 9:
                        AgregarIntegranteAGrupo(ref grupo_09, integrante);
                        break;
                }
            }
        }

        // Nueva función auxiliar que maneja la lógica de agregar integrantes
        private void AgregarIntegranteAGrupo(ref string[] grupoArray, string integrante)
        {
            // Si el array es null o vacío, crear uno con 1 elemento
            if (grupoArray == null || grupoArray.Length == 0)
            {
                grupoArray = new string[1];
                grupoArray[0] = integrante;
                return;
            }

            // Buscar la primera posición vacía
            int posicionVacia = BuscarPrimeraPosicionVacia(grupoArray);

            // Si encontró una posición vacía, usar esa
            if (posicionVacia < grupoArray.Length)
            {
                grupoArray[posicionVacia] = integrante;
            }
            else
            {
                // Si no hay posiciones vacías, redimensionar y agregar al final
                Array.Resize(ref grupoArray, grupoArray.Length + 1);
                grupoArray[grupoArray.Length - 1] = integrante;
            }
        }

        // Función mejorada para detectar posiciones vacías
        private int BuscarPrimeraPosicionVacia(string[] grupoArray)
        {
            for (int i = 0; i < grupoArray.Length; i++)
            {
                // Verificar si la posición está vacía o es null
                if (string.IsNullOrEmpty(grupoArray[i]))
                {
                    return i; // Retorna la primera posición vacía encontrada
                }
            }

            // Si no encontró posiciones vacías, retorna el tamaño del array
            return grupoArray.Length;
        }
        public void ReagrupacionIntegrantes(int index)
        {
            switch (index)
            {
                case 0:
                    grupo_00 = new string[cantIntegrantesPorGrupo];
                    break;
                case 1:
                    grupo_01 = new string[cantIntegrantesPorGrupo];
                    break;
                case 2:
                    grupo_02 = new string[cantIntegrantesPorGrupo];
                    break;
                case 3:
                    grupo_03 = new string[cantIntegrantesPorGrupo];
                    break;
                case 4:
                    grupo_04 = new string[cantIntegrantesPorGrupo];
                    break;
                case 5:
                    grupo_05 = new string[cantIntegrantesPorGrupo];
                    break;
                case 6:
                    grupo_06 = new string[cantIntegrantesPorGrupo];
                    break;
                case 7:
                    grupo_07 = new string[cantIntegrantesPorGrupo];
                    break;
                case 8:
                    grupo_08 = new string[cantIntegrantesPorGrupo];
                    break;
                case 9:
                    grupo_09 = new string[cantIntegrantesPorGrupo];
                    break;
            }
            for(int i = index; i < grupos.Length; i++)
            {
                IntercambiarIntegrantes(i);
            }
        }
        public void IntercambiarIntegrantes(int index)
        {
            switch (index)
            {
                case 0:
                    if (grupo_01 != null && grupo_00 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_01.Length, grupo_00.Length);
                        Array.Copy(grupo_01, grupo_00, tamanoACopiar);
                    }
                    break;
                case 1:
                    if (grupo_02 != null && grupo_01 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_02.Length, grupo_01.Length);
                        Array.Copy(grupo_02, grupo_01, tamanoACopiar);
                    }
                    break;
                case 2:
                    if (grupo_03 != null && grupo_02 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_03.Length, grupo_02.Length);
                        Array.Copy(grupo_03, grupo_02, tamanoACopiar);
                    }
                    break;
                case 3:
                    if (grupo_04 != null && grupo_03 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_04.Length, grupo_03.Length);
                        Array.Copy(grupo_04, grupo_03, tamanoACopiar);
                    }
                    break;
                case 4:
                    if (grupo_05 != null && grupo_04 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_05.Length, grupo_04.Length);
                        Array.Copy(grupo_05, grupo_04, tamanoACopiar);
                    }
                    break;
                case 5:
                    if (grupo_06 != null && grupo_05 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_06.Length, grupo_05.Length);
                        Array.Copy(grupo_06, grupo_05, tamanoACopiar);
                    }
                    break;
                case 6:
                    if (grupo_07 != null && grupo_06 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_07.Length, grupo_06.Length);
                        Array.Copy(grupo_07, grupo_06, tamanoACopiar);
                    }
                    break;
                case 7:
                    if (grupo_08 != null && grupo_07 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_08.Length, grupo_07.Length);
                        Array.Copy(grupo_08, grupo_07, tamanoACopiar);
                    }
                    break;
                case 8:
                    if (grupo_09 != null && grupo_08 != null)
                    {
                        int tamanoACopiar = Math.Min(grupo_09.Length, grupo_08.Length);
                        Array.Copy(grupo_09, grupo_08, tamanoACopiar);
                    }
                    break;
                case 9:
                    grupo_09 = new string[cantIntegrantesPorGrupo];
                    break;
            }
        }
        

        // Función auxiliar para copiar arrays correctamente
        public void AgregarGrupo(string nombreGrupo)
        {
            for (int i = 0; i < grupos.Length; i++)
            {
                if (string.IsNullOrEmpty(grupos[i]))
                {
                    grupos[i] = nombreGrupo;
                    return; // Salir después de agregar
                }
            }
        }

        public bool ReportarCantidadGrupos()
        {
            int gruposOcupados = 0;

            for (int i = 0; i < grupos.Length; i++)
            {
                if (!string.IsNullOrEmpty(grupos[i]))
                {
                    gruposOcupados++;
                }
            }

            return gruposOcupados < 10; // true si hay menos de 10 grupos ocupados
        }

        public void BorrarGrupo(string grupo)
        {
            //aqui va la logica para eliminar el grupo pasado por parametro del array "grupos" y en caso de que no sea el ultimo elemento del array realizaz el corrimiento de todos los elementos que le seguian, para que no quede un item vacio dentro del array sin que sea el ultimo.
            for (int i = 0; i < grupos.Length; i++)
            {
                if (grupos[i] == grupo)
                {
                    // Desplazar los elementos hacia la izquierda
                    for (int j = i; j < grupos.Length - 1; j++)
                    {
                        grupos[j] = grupos[j + 1];
                    }
                    // Limpiar el último elemento
                    grupos[grupos.Length - 1] = null;
                    return; // Salir después de borrar
                }
            }


        }
        public void EditarNombreDeGrupo(string grupoActual, string nuevoNombreGrupo)
        {
            for (int i = 0; i < grupos.Length; i++)
            {
                if (grupos[i] == grupoActual)
                {
                    grupos[i] = nuevoNombreGrupo;
                    return; // Salir después de editar
                }
            }
        }


        /************ SERVICIOS PASA DATOS AL DROPDOWN ****************************************************************************** */
        public string PasaDatosPersistentesGrupos(int ID)
        {
            switch (ID)
            {
                case 0:
                    string __grupo_00 = string.Join(",", grupo_00);
                    return __grupo_00;
                case 1:
                    string __grupo_01 = string.Join(",", grupo_01);
                    return __grupo_01;
                case 2:
                    string __grupo_02 = string.Join(",", grupo_02);
                    return __grupo_02;
                case 3:
                    string __grupo_03 = string.Join(",", grupo_03);
                    return __grupo_03;
                case 4:
                    string __grupo_04 = string.Join(",", grupo_04);
                    return __grupo_04;
                case 5:
                    string __grupo_05 = string.Join(",", grupo_05);
                    return __grupo_05;
                case 6:
                    string __grupo_06 = string.Join(",", grupo_06);
                    return __grupo_06;
                case 7:
                    string __grupo_07 = string.Join(",", grupo_07);
                    return __grupo_07;
                case 8:
                    string __grupo_08 = string.Join(",", grupo_08);
                    return __grupo_08;
                case 9:
                    string __grupo_09 = string.Join(",", grupo_09);
                    return __grupo_09;
                case 10:
                    string __grupos = string.Join(",", grupos);
                    return __grupos;
                default:
                    Debug.LogError("ID de grupo no válido: " + ID);
                    return "";
            }

        }



        /************ SERVICIOS DE GUARDADO DE DATOS PERSISTENTES ****************************************************************************** */
        public void GuardarDatosPersistentes()
        {
            string _grupos = string.Join(",", grupos);
            string _grupo_00 = string.Join(",", grupo_00);
            string _grupo_01 = string.Join(",", grupo_01);
            string _grupo_02 = string.Join(",", grupo_02);
            string _grupo_03 = string.Join(",", grupo_03);
            string _grupo_04 = string.Join(",", grupo_04);
            string _grupo_05 = string.Join(",", grupo_05);
            string _grupo_06 = string.Join(",", grupo_06);
            string _grupo_07 = string.Join(",", grupo_07);
            string _grupo_08 = string.Join(",", grupo_08);
            string _grupo_09 = string.Join(",", grupo_09);
            string _grupoSeleccionado = grupoSeleccionado;
            int _indiceDropDownGrupoSeleccionado = this.indiceDropDownGrupoSeleccionado;
            PlayerPrefs.SetString("grupos", _grupos);
            PlayerPrefs.SetString("grupo_00", _grupo_00);
            PlayerPrefs.SetString("grupo_01", _grupo_01);
            PlayerPrefs.SetString("grupo_02", _grupo_02);
            PlayerPrefs.SetString("grupo_03", _grupo_03);
            PlayerPrefs.SetString("grupo_04", _grupo_04);
            PlayerPrefs.SetString("grupo_05", _grupo_05);
            PlayerPrefs.SetString("grupo_06", _grupo_06);
            PlayerPrefs.SetString("grupo_07", _grupo_07);
            PlayerPrefs.SetString("grupo_08", _grupo_08);
            PlayerPrefs.SetString("grupo_09", _grupo_09);
            PlayerPrefs.SetString("grupoSeleccionado", _grupoSeleccionado);
            PlayerPrefs.SetInt("indiceDropDownGrupoSeleccionado", _indiceDropDownGrupoSeleccionado);
            PlayerPrefs.Save();

        }

        /************ SERVICIO RECUPERACION DE DATOS PERSISTENTES  ****************************************************************************** */
        public void RecuperacionDatosPersistentesGrupos()
        {
            string _grupos = PlayerPrefs.GetString("grupos");
            string _grupo_00 = PlayerPrefs.GetString("grupo_00");
            string _grupo_01 = PlayerPrefs.GetString("grupo_01");
            string _grupo_02 = PlayerPrefs.GetString("grupo_02");
            string _grupo_03 = PlayerPrefs.GetString("grupo_03");
            string _grupo_04 = PlayerPrefs.GetString("grupo_04");
            string _grupo_05 = PlayerPrefs.GetString("grupo_05");
            string _grupo_06 = PlayerPrefs.GetString("grupo_06");
            string _grupo_07 = PlayerPrefs.GetString("grupo_07");
            string _grupo_08 = PlayerPrefs.GetString("grupo_08");
            string _grupo_09 = PlayerPrefs.GetString("grupo_09");
            string _grupoSeleccionado = PlayerPrefs.GetString("grupoSeleccionado");
            int _indiceDropDownGrupoSeleccionado = PlayerPrefs.GetInt("indiceDropDownGrupoSeleccionado", 0);
            grupos = _grupos.Split(',');
            grupo_00 = _grupo_00.Split(',');
            grupo_01 = _grupo_01.Split(',');
            grupo_02 = _grupo_02.Split(',');
            grupo_03 = _grupo_03.Split(',');
            grupo_04 = _grupo_04.Split(',');
            grupo_05 = _grupo_05.Split(',');
            grupo_06 = _grupo_06.Split(',');
            grupo_07 = _grupo_07.Split(',');
            grupo_08 = _grupo_08.Split(',');
            grupo_09 = _grupo_09.Split(',');
            grupoSeleccionado = _grupoSeleccionado;
            indiceDropDownGrupoSeleccionado = _indiceDropDownGrupoSeleccionado;
        }

        /************ SERVICIO EDICION Y ELIMINACION DE INTEGRAANTES (hecha por CURSOR)  ****************************************************************************** */

        public void EliminarIntegrante(string grupo, string nombreIntegrante)
        {
            // Paso 1: Encontrar el índice del grupo en el array "grupos"
            int indiceGrupo = -1;
            for (int i = 0; i < grupos.Length; i++)
            {
                if (grupos[i] == grupo)
                {
                    indiceGrupo = i;
                    break;
                }
            }

            // Paso 2: Si no se encontró el grupo, salir
            if (indiceGrupo == -1)
            {
                Debug.LogWarning("No se encontró el grupo: " + grupo);
                return;
            }

            // Paso 3: Obtener el array del grupo correspondiente
            string[] arrayGrupo = null;
            switch (indiceGrupo)
            {
                case 0: arrayGrupo = grupo_00; break;
                case 1: arrayGrupo = grupo_01; break;
                case 2: arrayGrupo = grupo_02; break;
                case 3: arrayGrupo = grupo_03; break;
                case 4: arrayGrupo = grupo_04; break;
                case 5: arrayGrupo = grupo_05; break;
                case 6: arrayGrupo = grupo_06; break;
                case 7: arrayGrupo = grupo_07; break;
                case 8: arrayGrupo = grupo_08; break;
                case 9: arrayGrupo = grupo_09; break;
            }

            // Paso 4: Si el array es null, salir
            if (arrayGrupo == null)
            {
                Debug.LogWarning("El array del grupo está vacío");
                return;
            }

            // Paso 5: Buscar y eliminar el integrante
            for (int i = 0; i < arrayGrupo.Length; i++)
            {
                if (arrayGrupo[i] == nombreIntegrante)
                {
                    // Encontramos el integrante, lo eliminamos poniendo null
                    arrayGrupo[i] = null;

                    // Reorganizar el array para que no queden espacios vacíos en el medio
                    ReorganizarArray(ref arrayGrupo);

                    // Actualizar el array correspondiente
                    ActualizarArrayGrupo(indiceGrupo, arrayGrupo);

                    Debug.Log("Integrante eliminado: " + nombreIntegrante + " del grupo: " + grupo);
                    return;
                }
            }

            // Si llegamos aquí, no se encontró el integrante
            Debug.LogWarning("No se encontró el integrante: " + nombreIntegrante + " en el grupo: " + grupo);
        }

        // Función auxiliar para reorganizar el array (eliminar espacios vacíos)
        private void ReorganizarArray(ref string[] array)
        {
            // Crear una lista temporal para almacenar solo los elementos no vacíos
            List<string> elementosValidos = new List<string>();

            // Recorrer el array y agregar solo los elementos que no sean null o vacíos
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    elementosValidos.Add(array[i]);
                }
            }

            // Crear un nuevo array con el tamaño correcto
            array = new string[cantIntegrantesPorGrupo];

            // Copiar los elementos válidos al nuevo array
            for (int i = 0; i < elementosValidos.Count && i < cantIntegrantesPorGrupo; i++)
            {
                array[i] = elementosValidos[i];
            }
        }

        // Función auxiliar para actualizar el array correspondiente
        private void ActualizarArrayGrupo(int indiceGrupo, string[] nuevoArray)
        {
            switch (indiceGrupo)
            {
                case 0: grupo_00 = nuevoArray; break;
                case 1: grupo_01 = nuevoArray; break;
                case 2: grupo_02 = nuevoArray; break;
                case 3: grupo_03 = nuevoArray; break;
                case 4: grupo_04 = nuevoArray; break;
                case 5: grupo_05 = nuevoArray; break;
                case 6: grupo_06 = nuevoArray; break;
                case 7: grupo_07 = nuevoArray; break;
                case 8: grupo_08 = nuevoArray; break;
                case 9: grupo_09 = nuevoArray; break;
            }
        }

        public void EditarIntegrante(string grupo, string nombreAnterior, string nombreNuevo)
        {
            // Paso 1: Encontrar el índice del grupo en el array "grupos"
            int indiceGrupo = -1;
            for (int i = 0; i < grupos.Length; i++)
            {
                if (grupos[i] == grupo)
                {
                    indiceGrupo = i;
                    break;
                }
            }

            // Paso 2: Si no se encontró el grupo, salir
            if (indiceGrupo == -1)
            {
                Debug.LogWarning("No se encontró el grupo: " + grupo);
                return;
            }

            // Paso 3: Obtener el array del grupo correspondiente
            string[] arrayGrupo = null;
            switch (indiceGrupo)
            {
                case 0: arrayGrupo = grupo_00; break;
                case 1: arrayGrupo = grupo_01; break;
                case 2: arrayGrupo = grupo_02; break;
                case 3: arrayGrupo = grupo_03; break;
                case 4: arrayGrupo = grupo_04; break;
                case 5: arrayGrupo = grupo_05; break;
                case 6: arrayGrupo = grupo_06; break;
                case 7: arrayGrupo = grupo_07; break;
                case 8: arrayGrupo = grupo_08; break;
                case 9: arrayGrupo = grupo_09; break;
            }

            // Paso 4: Si el array es null, salir
            if (arrayGrupo == null)
            {
                Debug.LogWarning("El array del grupo está vacío");
                return;
            }

            // Paso 5: Buscar y editar el integrante
            for (int i = 0; i < arrayGrupo.Length; i++)
            {
                if (arrayGrupo[i] == nombreAnterior)
                {
                    // Encontramos el integrante, lo editamos
                    arrayGrupo[i] = nombreNuevo;

                    // Actualizar el array correspondiente
                    ActualizarArrayGrupo(indiceGrupo, arrayGrupo);

                    Debug.Log("Integrante editado: " + nombreAnterior + " -> " + nombreNuevo + " en el grupo: " + grupo);
                    return;
                }
            }

            // Si llegamos aquí, no se encontró el integrante
            Debug.LogWarning("No se encontró el integrante: " + nombreAnterior + " en el grupo: " + grupo);
        }
    }
}
