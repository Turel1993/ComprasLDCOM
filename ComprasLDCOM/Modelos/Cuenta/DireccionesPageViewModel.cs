using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;
using ComprasLDCOM.Datos.Cuenta.Request;
using ComprasLDCOM.Datos.Cuenta.Response;
using ComprasLDCOM.Popups.Cuenta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComprasLDCOM.Modelos.Cuenta
{
    public class DireccionesPageViewModel : BindableObject
    {

        ApiDataStore Store = new();
        public ICommand AgregarDomicilio { get; set; }

        public List<Estado> _edos;
        public List<Estado> EstadosList
        {
            get { return _edos; }
            set
            {
                _edos = value;
                OnPropertyChanged();
            }
        }

        private Estado estado_Id { get; set; }
        public Estado Estado_IdSelected
        {
            get { return estado_Id; }
            set
            {
                if (Estado_IdSelected != value)
                {
                    estado_Id = value;
                    getMunicipios(Estado_IdSelected.Nivel1_id).GetAwaiter();
                    OnPropertyChanged();
                }
            }
        }

        public List<Municipio> _municipios;
        public List<Municipio> MunicipiosList
        {
            get { return _municipios; }
            set
            {
                _municipios = value;
                OnPropertyChanged();
            }
        }

        private Municipio municipio_Id { get; set; }
        public Municipio Municipio_IdSelected
        {
            get { return municipio_Id; }
            set
            {
                if (Municipio_IdSelected != value)
                {
                    municipio_Id = value;
                    getColonias(Estado_IdSelected.Nivel1_id, Municipio_IdSelected.Nivel2_id).GetAwaiter();
                    OnPropertyChanged();
                }
            }
        }

        public List<Colonia> _colonias;
        public List<Colonia> ColoniasList
        {
            get { return _colonias; }
            set
            {
                _colonias = value;
                OnPropertyChanged();
            }
        }

        private Colonia colonia_Id { get; set; }
        public Colonia Colonia_IdSelected
        {
            get { return colonia_Id; }
            set
            {
                if (Colonia_IdSelected != value)
                {
                    colonia_Id = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<Direccion> Direcciones { get; set; }

        public DireccionesPageViewModel()
        {
            AgregarDomicilio = new Command(btn_AgregarDomicilio);
            Direcciones = new ObservableCollection<Direccion>
            {
                new Direccion {Id=1,Alias="Casa centro", Calle="Betancourt 61", Ciudad = "Xalapa"},
                new Direccion {Id=1,Alias="Oficina", Calle="Insurgentes", Ciudad = "CDMX"},
                new Direccion {Id=1,Alias="Tienda", Calle="Av Orizaba", Ciudad = "Orizaba"},

            };
            getEstados().GetAwaiter();
        }
        public void btn_AgregarDomicilio() 
        {
            DomiciliosPopUp domiciliosPop = new();
            Application.Current.MainPage.ShowPopup(domiciliosPop);
        }
        public async Task getEstados() 
        {
            EstadosList = await Store.ObtenerEstadosApi();
        }
        public async Task getMunicipios(int zona1)
        {
            MunicipiosList = await Store.ObtenerMunicipiosApi(zona1);
        }
        public async Task getColonias(int zona1, int zona2)
        {
            ColoniasList = await Store.ObtenerColoniasApi(zona1,zona2);
        }
    }
}
