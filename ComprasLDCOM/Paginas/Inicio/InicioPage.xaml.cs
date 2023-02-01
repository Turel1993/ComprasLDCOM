using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Paginas.Inicio;

public partial class InicioPage : ContentPage
{
    private SynchronizationContext _uic;
    
    public InicioPage()
	{
		InitializeComponent();
        _uic = SynchronizationContext.Current ?? throw new Exception("Current synchronization context is null!");
        Task.Run(() => {
            Thread.Sleep(500);
            SetFocus(BarraBusqueda);
        });
    }

    private void SetFocus(SearchBar target_)
    {
        _uic.Post(s => {
            target_.Focus();
        }, null);
    }

    private void BarraBusqueda_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Trim() == "")
            ((SearchBar)sender).SearchCommand?.Execute(null);
    }
}