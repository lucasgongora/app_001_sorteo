using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace app_001
{

    public class Grupo
    {
        public string NombreGrupo { get; set; }
        public List<string> Miembros { get; set; }
    }

    public class MatrizGrupos : MonoBehaviour
    {
        public List<Grupo> grupos = new List<Grupo>
        {
                new Grupo { NombreGrupo = "FAMILIA" },
                new Grupo { NombreGrupo = "AMIGOS" },
                new Grupo { NombreGrupo = "Grupo 3" },
                new Grupo { NombreGrupo = "Grupo 4" },
                new Grupo { NombreGrupo = "Grupo 5" },
                new Grupo { NombreGrupo = "Grupo 6" },
                new Grupo { NombreGrupo = "Grupo 7" },
                new Grupo { NombreGrupo = "Grupo 8" },
                new Grupo { NombreGrupo = "Grupo 9" },
                new Grupo { NombreGrupo = "Grupo 10" },
         };
     

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GuardarDatos(Grupo nombre)
        {

        }

        public void SolicitarDatos()
        {

        }

        public void ModificarMatriz(Grupo nombre)
        {

        }
        

    }
}
