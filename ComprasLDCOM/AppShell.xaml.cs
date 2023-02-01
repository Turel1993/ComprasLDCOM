using ComprasLDCOM.Datos.Configuraciones.BaseDeDatos;
using ComprasLDCOM.Datos.Inicio.Response;
using ComprasLDCOM.API;
using ComprasLDCOM.Modelos.Cuenta;

namespace ComprasLDCOM;

public partial class AppShell : Shell
{
    public CuentaPageViewModel cuentaPageView;
    public AppShell()
	{
        cuentaPageView = new();
        InitializeComponent();
        ObtenerLogo(true).GetAwaiter();
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        var rutaPrevia = args?.Previous?.Location?.OriginalString;
        var actualRuta = args?.Current?.Location?.OriginalString;

        if (rutaPrevia != null && rutaPrevia.Contains("CuentaPage") && actualRuta.Contains("CuentaPage"))
        {
            cuentaPageView.CargarDatosBD();
        }
        base.OnNavigated(args);
    }

    public async Task ObtenerLogo(bool Nuevo)
    {
        try
        {
            string logofinal = "";
            ApiDataStore Api = new();

            string logo = await Api.ObtenerLogo();
            ResAppLogo Reslogo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResAppLogo>(logo);
            String[] DetalleLogo = Reslogo.Data.Split(",");
            for (int i = 0; i <= DetalleLogo.Length - 1; i++)
            {
                if (!(DetalleLogo[i].IndexOf("Tipo_Imagen") >= 0))
                {
                    if (DetalleLogo[i].IndexOf("Imagen") >= 0)
                    {
                        DetalleLogo[i] = DetalleLogo[i].Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                        DetalleLogo[i] = DetalleLogo[i].Replace("\"", "");
                        logofinal = DetalleLogo[i].Replace("Imagen:", "");
                        break;
                    }
                }
            }

            if (logofinal != "")
            {
                List<TblConfiguracion> ConfigDB = App.ServiciosBD.ObtenerListaEntidadLocal("TblConfiguracion").OfType<TblConfiguracion>().ToList();
                if (ConfigDB.Where(x => x.Nombre == "LOGO_APP").ToList().Count <= 0)
                    App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 6, Nombre = "LOGO_APP", Descripcion = "Logo de la APP del cliente", Valor = logofinal });
                else
                    App.ServiciosBD.ActualizarRegistroEntidadLocal(new TblConfiguracion { Id = 6, Nombre = "LOGO_APP", Descripcion = "Logo de la APP del cliente", Valor = logofinal });
            }
            else
                App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 6, Nombre = "LOGO_APP", Descripcion = "Logo de la APP del cliente", Valor = App.Funciones.LogoApp });
        }
        catch (Exception)
        {
            await App.Current.MainPage.DisplayAlert("Aviso", "ERROR al obtener logo.", "Aceptar");
        }
    }
}
