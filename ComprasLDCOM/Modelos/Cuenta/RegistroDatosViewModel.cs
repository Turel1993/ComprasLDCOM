
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;
using ComprasLDCOM.Datos.Cuenta.BaseDeDatos;
using ComprasLDCOM.Datos.Cuenta.Response;
using ComprasLDCOM.Paginas.Cuenta;
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
    internal class RegistroDatosViewModel : BindableObject, INotifyPropertyChanged
    {
        ApiDataStore Store = new();
        IniciaSesion IniciaSesion = new IniciaSesion();

        public new event PropertyChangedEventHandler PropertyChanged;

        public ICommand RegistrarDatos { set; get; }
        public ICommand IniciarSesion { set; get; }
        public ICommand EntraLogin { set; get; }
        public ICommand Terminos { set; get; }
        public ICommand Aviso { set; get; }

        # region variables y propiedades
        public string _identificador;
        public string _nombre;
        public string _apellidoPaterno;
        public string _apellidoMaterno;
        public string _telefono;
        public string _correo;
        public string _codigoPostal;
        public string _claveSocio;
        private bool _habilitarBtnRegistro = false;
        private DateTime _fechaNacimiento;
        private string _sexo;
        public string _usuario;
        public string _password;
        public Ladas _ladaCode;
        public List<Ladas> _ladasList;
        public DateTime FechaNacimiento
        {
            get => _fechaNacimiento;
            set
            {
                if (_fechaNacimiento == value)
                    return;
                _fechaNacimiento = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FechaNacimiento)));
            }
        }
        public string Identificador
        {
            get => _identificador;
            set
            {
                if (_identificador == value)
                    return;
                _identificador = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Identificador)));
            }
        }
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre == value)
                    return;
                _nombre = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Nombre)));
            }
        }
        public string ApellidoPaterno
        {
            get => _apellidoPaterno;
            set
            {
                if (_apellidoPaterno == value)
                    return;
                _apellidoPaterno = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ApellidoPaterno)));
            }
        }
        public string ApellidoMaterno
        {
            get => _apellidoMaterno;
            set
            {
                if (_apellidoMaterno == value)
                    return;
                _apellidoMaterno = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ApellidoMaterno)));
            }
        }
        public string Telefono
        {
            get => _telefono;
            set
            {
                if (_telefono == value)
                    return;
                _telefono = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Telefono)));
            }
        }
        public string Correo
        {
            get => _correo;
            set
            {
                if (_correo == value)
                    return;
                _correo = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Correo)));
            }
        }
        public string CodigoPostal
        {
            get => _codigoPostal;
            set
            {
                if (_codigoPostal == value)
                    return;
                _codigoPostal = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CodigoPostal)));
            }
        }
        public string ClaveSocio
        {
            get => _claveSocio;
            set
            {
                if (_claveSocio == value)
                    return;
                _claveSocio = value;
                btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_claveSocio)));
            }
        }
        public string ClaveSocioConfirma
        {
            get => _claveSocio;
            set
            {
                if (_claveSocio == value)
                    return;
                _claveSocio = value;
                //btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_claveSocio)));
                
            }
        }

        public string Sexo
        {
            get => _sexo;
            set
            {
                if (_sexo == value)
                    return;
                _sexo = value;
                //btnProp();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_sexo)));

            }
        }
        public bool HabilitarBtnRegistro
        {
            get => _habilitarBtnRegistro;
            set
            {
                if (_habilitarBtnRegistro != value)
                {
                    _habilitarBtnRegistro = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(HabilitarBtnRegistro)));

                }
            }
        }
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

        public Ladas LadaCode
        {
            get { return _ladaCode; }
            set
            {
                if (_ladaCode != value)
                {

                    _ladaCode = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LadaCode)));
                }
            }
        }

        public List<Ladas> LadasList
        {
            get { return _ladasList; }
            set
            {
                _ladasList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LadasList)));
            }

        }

 #endregion

        public RegistroDatosViewModel()
        {
            RegistrarDatos = new Command(btn_RegistrarDatos);
            IniciarSesion = new Command(btn_IniciarSesion);
            EntraLogin = new Command(btn_Login);
            Aviso = new Command(lbl_Aviso);
            Terminos = new Command(lbl_Terminos);
            getLada().GetAwaiter();
        }



        /// <summary>
        /// Actualiza el estado de las propiedas al ser llenadas en su totalidad
        /// </summary>
        private void btnProp() 
        {
            if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(ApellidoPaterno) && !string.IsNullOrEmpty(ApellidoMaterno)
                && !string.IsNullOrEmpty(Telefono) && !string.IsNullOrEmpty(Correo)
                && !string.IsNullOrEmpty(CodigoPostal) && !string.IsNullOrEmpty(ClaveSocio))
            {
                HabilitarBtnRegistro = true;
            }
        }


        /// <summary>
        /// metodo que ejecuta commando al tocar boton registrardatos
        /// </summary>
        public async void btn_RegistrarDatos()
        {
            string fecha = FechaNacimiento.ToString("yyyy-MM-dd");
            Telefono = LadaCode.dial_code + Telefono;
            Identificador = Correo;
            await Store.RegistrarCliente(Identificador, Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Correo, fecha, CodigoPostal, ClaveSocio, Sexo);
        }

        /// <summary>
        /// metodo que ejecuta commando al tocar boton iniciarsesion
        /// </summary>
        public void btn_IniciarSesion() 
        {
            IniciaSesion = new IniciaSesion();
            Application.Current.MainPage.ShowPopup(IniciaSesion);        
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

        /// <summary>
        /// metodo que ejecuta commando al tocar boton EntraLogin
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
                    tbl.IdSocio = JObject.Parse(rq)["Correo"].ToString();
                    tbl.Nombre = JObject.Parse(rq)["Nombre"].ToString();
                    tbl.Email = JObject.Parse(rq)["Correo"].ToString();
                    tbl.Telefono = JObject.Parse(rq)["Telefono"].ToString();
                    tbl.FechaNacimiento = JObject.Parse(rq)["Fecha_Nacimiento"].ToString();
                    App.ServiciosBD.AgregarRegistroEntidadLocal(tbl);
                    IniciaSesion.CerrarPopup();
                    await Shell.Current.GoToAsync($"///{nameof(CuentaPage)}");
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "No se pudo contactar con el servidor", "Aceptar");
                }
            }
        }

        /// <summary>
        /// obtiene las ladas de la api
        /// </summary>
        public async Task getLada() 
        {
            var lds = await Store.LadaApi();
            List<Ladas> ladasOrdenadas = lds.OrderBy(c => c.name).ToList();

            foreach (var lada in ladasOrdenadas) 
            {
                string espacio = "";
                switch (lada.dial_code.Length) 
                {
                    case 2:
                        espacio = "        ";
                        break;
                    case 3:
                        espacio = "      ";
                        break;
                    case 4:
                        espacio = "    ";
                        break;
                    case 5:
                        espacio = "  ";
                        break;
                    default:
                        espacio = "   ";
                        break;
                }

                lada.ladaCompuesta = "(" + lada.dial_code + ") - " + espacio + lada.code;
            }
            LadasList = ladasOrdenadas;
            var ladaDefault = LadasList.First(item => item.name == "Mexico" );
            LadaCode = ladaDefault;
            
        }
    }
}
