using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Inicio;

public partial class PopupPageBienvenida : Popup
{
	public PopupPageBienvenida()
	{
		InitializeComponent();
	}

    public void CerrarPopup()
    {
        Close();
    }
}