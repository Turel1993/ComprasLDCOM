using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.General;

public partial class PopupPageEspera : Popup
{
	public PopupPageEspera()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Para cerrar el popup
    /// </summary>
    public void CerrarPopup()
    {
        Close();
    }
}