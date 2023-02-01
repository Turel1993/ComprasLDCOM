using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Carrito
{

	public partial class PopupPageTarjetaBancaria : Popup
	{
		public PopupPageTarjetaBancaria()
		{
			InitializeComponent();
		}

        public void CerrarPopup()
        {
            Close();
        }
    }
}