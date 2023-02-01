using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Carrito;

public partial class PopupPageGeneralesVenta : Popup
{
	public PopupPageGeneralesVenta()
	{
		InitializeComponent();
	}
     
    public void CerrarPopup()
    {
        Close();
    }
}