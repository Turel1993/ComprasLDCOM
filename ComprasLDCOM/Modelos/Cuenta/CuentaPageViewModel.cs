using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;
using ComprasLDCOM.Datos.Cuenta.BaseDeDatos;
using ComprasLDCOM.Popups.Cuenta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComprasLDCOM.Modelos.Cuenta
{
    public class CuentaPageViewModel : BindableObject, INotifyPropertyChanged
    {
        ApiDataStore Store = new();

        IniciaSesion iniciaSesion = null;

        public new event PropertyChangedEventHandler PropertyChanged;
        public ICommand CrearCuenta { set; get; }
        public ICommand IniciarSesion { set; get; }
        public ICommand EntraLogin { set; get; }
        public ICommand CerrarSesion { set; get; }
        public ICommand MiPerfil { set; get; }
        public ICommand MisDirecciones { set; get; }
        public ICommand Terminos { set; get; }
        public ICommand Aviso { set; get; }

        public string _usuario;
        public string _password;


        /// <summary>
        /// String que muestra label nombre
        /// </summary>
        public string Nombre
        {
            get => (string)GetValue(NombreProperty);
            set
            {
                SetValue(NombreProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));
            }
        }

        /// <summary>
        /// String que muestra label correo
        /// </summary>
        public string Correo
        {
            get => (string)GetValue(CorreoProperty);
            set
            {
                SetValue(CorreoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Correo)));
            }
        }

        /// <summary>
        /// String que muestra label telefono
        /// </summary>
        public string Telefono
        {
            get => (string)GetValue(TelefonoProperty);
            set
            {
                SetValue(TelefonoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Telefono)));
            }
        }

        /// <summary>
        /// Booleano controla que debe ver en la pantalla de cuenta.
        /// </summary>
        public bool SesionIniciada
        {
            get => (bool)GetValue(SesionIniciadaProperty);
            set
            {
                SetValue(SesionIniciadaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SesionIniciada)));
            }
        }

        /// <summary>
        /// String que recibe el entry usuario
        /// </summary>
        public string Usuario
        {
            get => _usuario;
            set
            {
                if (_usuario == value)
                    return;
                _usuario = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Usuario)));
            }
        }

        /// <summary>
        /// String que recibe el entry password
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }



        public BindableProperty NombreProperty = BindableProperty.Create(nameof(Nombre), typeof(string), typeof(CuentaPageViewModel));
        public BindableProperty TelefonoProperty = BindableProperty.Create(nameof(Telefono), typeof(string), typeof(CuentaPageViewModel));
        public BindableProperty CorreoProperty = BindableProperty.Create(nameof(Correo), typeof(string), typeof(CuentaPageViewModel));
        public BindableProperty SesionIniciadaProperty = BindableProperty.Create(nameof(SesionIniciada), typeof(bool), typeof(CuentaPageViewModel));
        public CuentaPageViewModel()
        {
            CrearCuenta = new Command(btn_CrearCuenta);
            IniciarSesion = new Command(btn_IniciarSesion);
            EntraLogin = new Command(btn_Login);
            MiPerfil = new Command(lbl_MiPerfil);
            MisDirecciones = new Command(lbl_MisDirecciones);
            CerrarSesion = new Command(lb_CerrarSesion);
            Aviso = new Command(lbl_Aviso);
            Terminos = new Command(lbl_Terminos);
            CargarDatosBD();
        }

        /// <summary>
        /// Metodo que actualiza la vista de pagina cuenta
        /// </summary>
        public void CargarDatosBD() 
        {
            string nombre = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].Nombre;
            int posicion = nombre.IndexOf(" ");
            if (posicion == -1)
                posicion = nombre.Length;
            Nombre = "Bienvenido " + nombre.Substring(0, posicion);
            Correo = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? "" : App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList()[0].Email;
            SesionIniciada = App.ServiciosBD.ObtenerListaEntidadLocal("TblPerfil").OfType<TblPerfil>().ToList().Count <= 0 ? true : false;

        }

        /// <summary>
        /// muestra popup de inicio de sesion
        /// </summary>
        public void btn_IniciarSesion()
        {
            iniciaSesion = new IniciaSesion();
            Application.Current.MainPage.ShowPopup(iniciaSesion);
        }

        /// <summary>
        /// comando que se ejecuta dentro del pop up para logear al usuario
        /// </summary>
        public async void btn_Login()
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Password))
            {
                var toast = Toast.Make("Falló al iniciar sesión. Debes ingresar usuario y contraseña.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                try
                {
                    TblPerfil tbl = new();
                    string rq = await Store.LoginCliente(Usuario, Password);
                    Nombre = JObject.Parse(rq)["Nombre"].ToString();
                    Telefono = JObject.Parse(rq)["Telefono"].ToString();
                    Correo = JObject.Parse(rq)["Correo"].ToString();

                    tbl.IdSocio = Correo;
                    tbl.Nombre = Nombre;
                    tbl.Email = Correo;
                    tbl.Telefono = Telefono;
                    tbl.FechaNacimiento = JObject.Parse(rq)["Fecha_Nacimiento"].ToString();

                    App.ServiciosBD.AgregarRegistroEntidadLocal(tbl);
                    SesionIniciada = false;
                    iniciaSesion.CerrarPopup();
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "No se pudo contactar con el servidor", "Aceptar");
                }
            }
            
        }

        /// <summary>
        /// comando que dirige a la pagina de registrat usuario
        /// </summary>
        public async void btn_CrearCuenta() 
        {
            await Shell.Current.GoToAsync("RegistroDatosPage");
        }

        /// <summary>
        /// comando que cierra la sesion
        /// </summary>
        public void lb_CerrarSesion() 
        {
            
            SesionIniciada = true;
            TblPerfil tbl = new();
            tbl.IdSocio = Correo;
            tbl.Nombre = Nombre;
            tbl.Email = Correo;
            tbl.Telefono = Telefono;
            App.ServiciosBD.EliminarRegistroEntidadLocal(tbl);
            CargarDatosBD();
        }

        /// <summary>
        /// comando que dirige a la pagina de mi perfil
        /// </summary>
        public async void lbl_MiPerfil()
        {
            await Shell.Current.GoToAsync("MiPerfilPage");
        }
        /// <summary>
        /// comando que dirige a la pagina de mis direcciones
        /// </summary>
        public async void lbl_MisDirecciones()
        {
            await Shell.Current.GoToAsync("DireccionesPage");
        }

        /// <summary>
        /// metodo que muestra popup al tocar label aviso
        /// </summary>
        public void lbl_Aviso()
        {
            AvisoPopUp AvisoPop = new AvisoPopUp();
            Application.Current.MainPage.ShowPopup(AvisoPop);
        }

        /// <summary>
        /// metodo que muestra popup al tocar label terminos
        /// </summary>
        public void lbl_Terminos()
        {
            TerminosPopUp TerminosPop = new TerminosPopUp();
            Application.Current.MainPage.ShowPopup(TerminosPop);
        }
    }
}
