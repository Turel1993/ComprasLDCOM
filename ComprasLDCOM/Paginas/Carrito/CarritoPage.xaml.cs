using ComprasLDCOM.Datos.Inicio.Request;
using ComprasLDCOM.Modelos.Carrito;

namespace ComprasLDCOM.Paginas.Carrito;

public partial class CarritoPage : ContentPage
{
	public CarritoPage()
	{
		InitializeComponent();
    }
     
    private void MenuButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    protected override void OnAppearing()
    {
        BindingContext = new CarritoViewModel();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void btnCaracteristicasArt_Clicked(object sender, EventArgs e)
    {

        Articulo articulo; 
        if (sender is ImageButton)
            articulo = ((Datos.Inicio.Request.Articulo)((ImageButton)sender).CommandParameter);
        else
            articulo = ((Datos.Inicio.Request.Articulo)((Microsoft.Maui.Controls.Button)sender).CommandParameter);

        await Shell.Current.GoToAsync($"Caracteristicasart?sku={articulo.Sku}&nombre={articulo.Nombre}&precio={articulo.Precio}");
    }


}