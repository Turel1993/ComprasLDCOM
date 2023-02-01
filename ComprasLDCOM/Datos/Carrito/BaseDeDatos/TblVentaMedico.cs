using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    internal class TblVentaMedico
    {
        /// <summary>
        /// Id del Médico Ej: 42353
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre del Médico Ej: HECTOR JOSE ALVAREZ T.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Nombre del Médico Ej: "HECTOR JOSE ALVAREZ T."
        /// </summary>
        public string Identificacion { get; set; }

        /// <summary>
        /// Telefono del Médico Ej: 23031180
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Direccion del Médico Ej: "AV. NICOLAS COPERNICO NO. 3638 COL. ARBOLEDAS ZAPOPAN, JAL C.P 45070"
        /// </summary>
        public string Direccion { get; set; }

        public TblVentaMedico(string id, string nombre, string identificacion, string telefono, string direccion)
        {
            Id = id;
            Nombre = nombre;
            Identificacion = identificacion;
            Telefono = telefono;
            Direccion = direccion;
        }
    }
}

