using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Carrito;

public partial class PopupPageBusquedaMonedero : Popup
{ 
	public PopupPageBusquedaMonedero()
	{
        InitializeComponent();
	}

    public void CerrarPopup()
    {
        Close();
    }
}