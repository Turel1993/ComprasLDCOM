using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.BaseDeDatos
{
    internal interface IServicioBD
    {
        public int AgregarRegistroEntidadLocal(object Entidad);
        public int EliminarRegistroEntidadLocal(object Entidad);
        public int ActualizarRegistroEntidadLocal(object Entidad);
        public List<object> ObtenerListaEntidadLocal(string Entidad);
        public List<object> ObtenerRegistroEntidadLocal(string Entidad, string CampoPK);
        public string ObtenerDatoRegistroEntidadLocal(string Entidad, string ValorPK, string CampoValor);
    }
}
