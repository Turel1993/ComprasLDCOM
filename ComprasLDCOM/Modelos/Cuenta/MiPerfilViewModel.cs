using ComprasLDCOM.Datos.Cuenta.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Modelos.Cuenta
{
    public class MiPerfilViewModel : BindableObject, INotifyPropertyChanged
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Fecha { get; set; }

        public MiPerfilViewModel()
        {
            Fecha = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "Hola" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].FechaNacimiento;
            Telefono = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "Hola" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].Telefono;
            Correo = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "Hola" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].Email;
            Nombre = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "Hola" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].Nombre;
        }
    }
}
