using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Inicio;

public partial class PopupPageCodigoPostal : Popup
{
	public PopupPageCodigoPostal()
	{
		InitializeComponent();
	}

    public void CerrarPopup()
    {
        Close();
    }
}