using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComprasLDCOM.Popups.General;
using ComprasLDCOM.API;
using ComprasLDCOM.Datos.Inicio.Request;
using System.Collections.ObjectModel;

namespace ComprasLDCOM.Modelos.Carrito
{
    /// <summary>
    /// Datos que se pasan entre páginas con los valores del artículo
    /// </summary>
    [QueryProperty(nameof(Sku), "sku")]
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Precio), "precio")]

    internal class CaracteristicasArtViewModel : BindableObject, INotifyPropertyChanged
    {
        ApiDataStore DStore = new();

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #region Variables_Propiedades
        /// <summary>
        /// Variable para el PopUp de Espera
        /// </summary>
        PopupPageEspera popEspera = null;       

        /// <summary>
        /// Propiedad para cambiar el color del Tab Detalle
        /// </summary>
        private Color color_TabDetalle { get; set; }

        /// <summary>
        /// Propiedad para cambiar el color del Tab Informacion
        /// </summary>
        private Color color_TabInformacion { get; set; }
        
        /// <summary>
        /// Propiedad para cambiar el color del Tab Detalle
        /// </summary>
        public Color BGColor_TabDetalle
        {
            set
            {
                if (color_TabDetalle != value)
                {
                    color_TabDetalle = value;
                    OnPropertyChanged("BGColor_TabDetalle");
                }
            }

            get
            {
                return color_TabDetalle;
            }
        }
        
        /// <summary>
        /// Propiedad para cambiar el color del Tab Informacion
        /// </summary>
        public Color BGColor_TabInformacion
        {
            set
            {
                if (color_TabInformacion != value)
                {
                    color_TabInformacion = value;
                    OnPropertyChanged("BGColor_TabInformacion");
                }
            }

            get
            {
                return color_TabInformacion;
            }
        }

        /// <summary>
        /// Propiedad para saber que Tab esta activo
        /// </summary>
        public ICommand TabActivo { get; }

        /// <summary>
        /// Variable para el dato del SKU del artículo a mostrar
        /// </summary>
        private string _sku = "";
        
        /// <summary>
        /// Propiedad para el dato del SKU del artículo a mostrar
        /// </summary>
        public string Sku
        {
            get => _sku;
            set
            {
                if (_sku != value)
                {
                    _sku = value;
                    OnPropertyChanged();
                    getCaracteristica();
                }
            }
        }

        /// <summary>
        /// Variable para el dato del Nombre del artículo a mostrar
        /// </summary>
        private string _nombre = "";
        /// <summary>
        /// Propiedad para el dato del Nombre del artículo a mostrar
        /// </summary>
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Variable para el dato del Precio del artículo a mostrar
        /// </summary>
        private string _precio = "";

        /// <summary>
        /// Propiedad para el dato del Precio del artículo a mostrar
        /// </summary>
        public string Precio
        {
            get => _precio;
            set
            {
                if (_precio != value)
                {
                    _precio = float.Parse(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Variable para el dato de la Imagen del artículo a mostrar
        /// </summary>
        private string _imagen = "";
        
        /// <summary>
        /// Propiedad para el dato de la Imagen del artículo a mostrar
        /// </summary>
        public string Imagen
        {
            get => _imagen;
            set
            {
                if (_imagen != value)
                {
                    _imagen = value;                    
                    ImageSource img = App.Funciones.Base64aImagen(value);
                    ArtImagen64 = img;

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propiedad para el Texto que se muestra en el Overlay de Espera
        /// </summary>
        public string TextoOverlay
        {
            get => (string)GetValue(TextoOverlayProperty);
            set
            {
                SetValue(TextoOverlayProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoOverlay)));
            }
        }

        /// <summary>
        /// Propiedad para mostrar la Imagen en la vista
        /// </summary>
        public ImageSource ArtImagen64
        {
            get => (ImageSource)GetValue(ArtImagen64Property);
            set
            {
                SetValue(ArtImagen64Property, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtImagen64)));
            }
        }

        /// <summary>
        /// Propiedad para mostrar o no el TabDetalle
        /// </summary>
        public bool TabDetalleVisible
        {
            set
            {
                SetValue(TabDetalleProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabDetalleVisible)));
            }

            get => (bool)GetValue(TabDetalleProperty);
        }

        /// <summary>
        /// Propiedad para mostrar o no el TabInformacion
        /// </summary>
        public bool TabInformacionVisible
        {
            set
            {
                SetValue(TabInformacionProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabInformacionVisible)));
            }

            get => (bool)GetValue(TabInformacionProperty);
        }

        #endregion


        //Se debe poner ObservableCollection, para que me permita conectar a la vista y se observe en la pantalla
        #region ObservableCollection
        /// <summary>
        /// Propiedad para actualizar la vista con la Informacion del artículo
        /// </summary>
        public ObservableCollection<ReqInfoArt> infoList { get; set; } = new();


        #endregion


        #region BindableProperty
        /// <summary>
        /// Propiedad para el Texto del PopUp Espera
        /// </summary>
        public BindableProperty TextoOverlayProperty = BindableProperty.Create(nameof(TextoOverlay), typeof(string), typeof(CaracteristicasArtViewModel));
        
        /// <summary>
        /// Propiedad para la Imagen del Artículo en base64
        /// </summary>
        public BindableProperty ArtImagen64Property = BindableProperty.Create(nameof(ArtImagen64), typeof(ImageSource), typeof(CaracteristicasArtViewModel));
        
        /// <summary>
        /// Propiedad para la Imagen del Artículo
        /// </summary>
        public BindableProperty strImagenProperty = BindableProperty.Create(nameof(Imagen), typeof(string), typeof(CaracteristicasArtViewModel));

        /// <summary>
        /// Propiedad para el Detalle del Artículo
        /// </summary>
        public BindableProperty TabDetalleProperty = BindableProperty.Create(nameof(TabDetalleVisible), typeof(bool), typeof(CaracteristicasArtViewModel));

        /// <summary>
        /// Propiedad para la Información del Artículo
        /// </summary>
        public BindableProperty TabInformacionProperty = BindableProperty.Create(nameof(TabInformacionVisible), typeof(bool), typeof(CaracteristicasArtViewModel));

        #endregion

       
         
        public CaracteristicasArtViewModel()
        {
            try
            {
                TextoOverlay = "Cargando...";
                App.Funciones.PopupEspera();
                TabActivo = new Command(Tab_Activo);
                BGColor_TabInformacion = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaInactiva");
                TabInformacionVisible = true;
                BGColor_TabDetalle = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaActiva");
                TabDetalleVisible = true;
                popEspera = null;
                App.Funciones.PopupCerrar();
                App.Current.MainPage.DisplayAlert("A0", "A0-", "Aceptar");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("A1", "A1-" + ex.Message, "Aceptar");
                System.Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// Función para habilitar el Tab seleccionado
        /// </summary>
        /// <param name="valor"></param>
        private void Tab_Activo(object valor)
        {
            switch (valor)
            {
                case "Detalle":
                    BGColor_TabDetalle = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaActiva"); 
                    TabDetalleVisible = true;
                    BGColor_TabInformacion = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaInactiva"); 
                    TabInformacionVisible = false;
                    break;
                case "Informacion":
                    BGColor_TabDetalle = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaInactiva"); 
                    TabDetalleVisible = false;
                    BGColor_TabInformacion = App.Funciones.GetColorResourceValue("ColorCaractArtPestanaActiva"); 
                    TabInformacionVisible = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Función para obtener las Características del Artículo
        /// </summary>
        private async void getCaracteristica()
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("A3", "A3-", "Aceptar");
                TextoOverlay = "Cargando...";                
                var ResBusqueda = await DStore.ObtenListaBusqueda(Sku);
                if (ResBusqueda.Count() > 0)
                {
                    var ResCaractLinea = ResBusqueda[0].Caracteristicas;
                    foreach (var cArt in ResCaractLinea)
                    {
                        ReqInfoArt caract = new ReqInfoArt(
                            cArt.Nombre,
                            cArt.Descripcion,
                            cArt.Orden);

                        infoList.Add(caract);
                    }
                    Imagen = ResBusqueda[0].Imagen64;
                    App.Funciones.PopupCerrar();
                }
                else
                {
                    App.Funciones.PopupCerrar();
                    var toast = Toast.Make("No se encontró el Artículo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }
            }
            catch (Exception)
            {
                App.Funciones.PopupCerrar();
                var toast = Toast.Make("Error al obtener las Características del Artículo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }        

    }
}
