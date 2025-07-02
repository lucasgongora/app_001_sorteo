using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace app_001
{
    public class GestorDeGrupos : MonoBehaviour
    {

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

        private void Awake()
        {
            
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void DireccionadorGrupo(string grupo)
        {
            
        }

        public void GestorDeIntegrantes(string grupo, string[] integrantes)
        {

        }

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
        }
    }
}
