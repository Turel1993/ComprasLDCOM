using ComprasLDCOM.Datos.Carrito.BaseDeDatos;
using ComprasLDCOM.Datos.Configuraciones.BaseDeDatos;
using ComprasLDCOM.Datos.Cuenta.BaseDeDatos;
using ComprasLDCOM.Datos.Inicio.BaseDeDatos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.BaseDeDatos
{
    public class ServicioBD : IServicioBD
    {
        private readonly SQLiteConnection DbConnectionLocal;

        public ServicioBD (string RutaBD)
        {
            DbConnectionLocal = new SQLiteConnection(RutaBD);
            DbConnectionLocal.CreateTable<TblCarrito>();
            DbConnectionLocal.CreateTable<TblConfiguracion>();
            DbConnectionLocal.CreateTable<TblImagenes>();
            DbConnectionLocal.CreateTable<TblPerfil>();
            DbConnectionLocal.CreateTable<TblSesion>();
            DbConnectionLocal.CreateTable<TblTerminos>();
            DbConnectionLocal.CreateTable<TblGeneralesVenta>();
            DbConnectionLocal.CreateTable<TblPayment>();
            DbConnectionLocal.CreateTable<TblFavoritos>();
            DbConnectionLocal.CreateTable<TblArticulosCupones>();
        }

        /// <summary>
        /// ActualizarRegistroEntidadLocal - Para actualizar un registro en una tabla de la base de datos
        /// </summary>
        /// <param name="Entidad">object - el objeto que se actualizara</param>
        public int ActualizarRegistroEntidadLocal(object Entidad)
        {
            return DbConnectionLocal.Update(Entidad);
        }

        /// <summary>
        /// AgregarRegistroEntidadLocal - Para agregar un registro nuevo en una tabla de la base de datos
        /// </summary>
        /// <param name="Entidad">object - el objeto que se agregara</param>
        public int AgregarRegistroEntidadLocal(object Entidad)
        {
            return DbConnectionLocal.Insert(Entidad);
        }

        /// <summary>
        /// EliminarRegistroEntidadLocal - Para eliminar un registro de una tabla de la base de datos
        /// </summary>
        /// <param name="Entidad">object - el objeto que se eliminara</param>
        public int EliminarRegistroEntidadLocal(object Entidad)
        {
            return DbConnectionLocal.Delete(Entidad);
        }

        /// <summary>
        /// EliminarTodaEntidadLocal - Para eliminar todo el contenido de la tabla de la base de datos
        /// </summary>
        /// <param name="Entidad">object - la tabla a limpiar</param>
        public int LimpiarEntidadLocal(object Entidad)
        {
            switch (Entidad.GetType().Name)
            {
                case "TblGeneralesVenta":
                    return DbConnectionLocal.DeleteAll<TblGeneralesVenta>();
                case "TblCarrito":
                    return DbConnectionLocal.DeleteAll<TblCarrito>();
                case "TblPayment":
                    return DbConnectionLocal.DeleteAll<TblPayment>();
                case "TblArticulosCupones":
                    return DbConnectionLocal.DeleteAll<TblArticulosCupones>();
                default:
                    return 0;
            }
        }

        /// <summary>
        /// ObtenerDatoRegistroEntidadLocal - Para obtener el dato de un campo de un registro
        /// </summary>
        /// <param name="Entidad">obj - el objeto con los campos donde se buscara</param>
        /// <param name="ValorPK">string - El dato del campo a buscar</param>
        /// <param name="CampoValor">string - El nombre del campo donde se obtendra el dato</param>
        public string ObtenerDatoRegistroEntidadLocal(string Entidad, string ValorPK, string CampoValor)
        {
            string valor = "";

            if (Entidad.Equals("TblCarrito"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblCarrito>().ToList().Where(x => x.Nombre == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblCarrito>().ToList().Where(x => x.Nombre == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblConfiguracion"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblConfiguracion>().ToList().Where(x => x.Nombre == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblConfiguracion>().ToList().Where(x => x.Nombre == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblImagenes"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblImagenes>().ToList().Where(x => x.Nombre == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblImagenes>().ToList().Where(x => x.Nombre == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblPerfil"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblPerfil>().ToList().Where(x => x.IdSocio == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblPerfil>().ToList().Where(x => x.IdSocio == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblSesion"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblSesion>().ToList().Where(x => x.IdSocio == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblSesion>().ToList().Where(x => x.IdSocio == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblTerminos"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblTerminos>().ToList().Where(x => x.IdTerminos == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblTerminos>().ToList().Where(x => x.IdTerminos == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblGeneralesVenta"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblGeneralesVenta>().ToList().Where(x => x.IdUsuario.ToString() == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblGeneralesVenta>().ToList().Where(x => x.IdUsuario.ToString() == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblPayment"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblPayment>().ToList().Where(x => x.TipoPago == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblPayment>().ToList().Where(x => x.TipoPago == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblFavoritos"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblFavoritos>().ToList().Where(x => x.SkuArticulo == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblFavoritos>().ToList().Where(x => x.SkuArticulo == ValorPK).ToList()[0], CampoValor);
            else if (Entidad.Equals("TblArticulosCupones"))
                valor = App.Funciones.ObtieneValorPropiedad(DbConnectionLocal.Table<TblArticulosCupones>().ToList().Where(x => x.Articulo == ValorPK).ToList().Count <= 0 ? null : DbConnectionLocal.Table<TblArticulosCupones>().ToList().Where(x => x.Articulo == ValorPK).ToList()[0], CampoValor);

            return valor;
        }

        /// <summary>
        /// ObtenerListaEntidadLocal - Para obtener todos los registros de una tabla en forma de lista
        /// </summary>
        /// <param name="Entidad">string - el nombre de la tabla que se va buscar</param>
        public List<object> ObtenerListaEntidadLocal(string Entidad)
        {
            List<object> Entity = new();

            if (Entidad.Equals("TblCarrito"))
                Entity = DbConnectionLocal.Table<TblCarrito>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblConfiguracion"))
                Entity = DbConnectionLocal.Table<TblConfiguracion>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblImagenes"))
                Entity = DbConnectionLocal.Table<TblImagenes>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblPerfil"))
                Entity = DbConnectionLocal.Table<TblPerfil>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblSesion"))
                Entity = DbConnectionLocal.Table<TblSesion>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblTerminos"))
                Entity = DbConnectionLocal.Table<TblTerminos>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblGeneralesVenta"))
                Entity = DbConnectionLocal.Table<TblGeneralesVenta>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblPayment"))
                Entity = DbConnectionLocal.Table<TblPayment>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblFavoritos"))
                Entity = DbConnectionLocal.Table<TblFavoritos>().ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblArticulosCupones"))
                Entity = DbConnectionLocal.Table<TblArticulosCupones>().ToList().Cast<object>().ToList();

            return Entity;
        }

        /// <summary>
        /// ObtenerRegistroEntidadLocal - Para obtener un registro de una tabla de la base de datos
        /// </summary>
        /// <param name="Entidad">obj - el nombre de la tabla donde se buscara</param>
        /// <param name="CampoPK">string - El nombre del campo a buscar</param>
        public List<object> ObtenerRegistroEntidadLocal(string Entidad, string CampoPK)
        {
            List<object> Entity = new();

            if (Entidad.Equals("TblCarrito"))
                Entity = DbConnectionLocal.Table<TblCarrito>().ToList().Where(x => x.SkuArticulo == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblConfiguracion"))
                Entity = DbConnectionLocal.Table<TblConfiguracion>().ToList().Where(x => x.Nombre == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblImagenes"))
                Entity = DbConnectionLocal.Table<TblImagenes>().ToList().Where(x => x.Nombre == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblPerfil"))
                Entity = DbConnectionLocal.Table<TblPerfil>().ToList().Where(x => x.IdSocio == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblSesion"))
                Entity = DbConnectionLocal.Table<TblSesion>().ToList().Where(x => x.IdSocio == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblTerminos"))
                Entity = DbConnectionLocal.Table<TblTerminos>().ToList().Where(x => x.IdTerminos == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblGeneralesVenta"))
                Entity = DbConnectionLocal.Table<TblGeneralesVenta>().ToList().Where(x => x.IdTipoDocumento == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblPayment"))
                Entity = DbConnectionLocal.Table<TblPayment>().ToList().Where(x => x.TipoPago == CampoPK).ToList().Cast<object>().ToList();
            else if (Entidad.Equals("TblFavoritos"))
                Entity = DbConnectionLocal.Table<TblFavoritos>().ToList().Where(x => x.SkuArticulo == CampoPK).ToList().Cast<object>().ToList();
             else if (Entidad.Equals("TblArticulosCupones"))
                Entity = DbConnectionLocal.Table<TblArticulosCupones>().ToList().Where(x => x.Articulo == CampoPK).ToList().Cast<object>().ToList();

            return Entity;
        }
    }
}
