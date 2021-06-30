using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using P02ApiRest.model;
using FFImageLoading;
namespace P02ApiRest
{
    public partial class MainPage : ContentPage
    {
        List<Regiones> region;
        List<ModelCountry> service;
        RestService restService;

        public MainPage()
        {
            InitializeComponent();
            restService = new RestService();
            refrescar.IsRefreshing = true;
            Region();

        }



        void Region()

        {
            region = new List<Regiones>();
            region.Add(new Regiones { Name = "Africa" });
            region.Add(new Regiones { Name = "Americas" });
            region.Add(new Regiones { Name = "Asia" });
            region.Add(new Regiones { Name = "Europe" });
            region.Add(new Regiones { Name = "Oceania" });
            foreach (var regiones in region)
            {
                pickerRegion.Items.Add(regiones.Name);
            }
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            service = await restService.GetRepositoriesAsync(DataConstants.urlall);
            collectionView.ItemsSource = service;
            refrescar.IsRefreshing = false;
        }


        private async void pickerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int position = pickerRegion.SelectedIndex;
            if (position > -1)
            {
                var name = region[position].Name;
                service = await restService.GetRepositoriesAsync(DataConstants.url + name);
                collectionView.ItemsSource = service;

            }

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            refrescar.IsRefreshing = true;
            var buscar = service.Where(c => c.name.ToLower().Contains(search.Text.ToLower()));
            collectionView.ItemsSource = buscar;
            refrescar.IsRefreshing = false;
        }
        private async void selectedItem(object sender, SelectedItemChangedEventArgs e)
        {

            var country = new ModelCountry();
            var objeto = (ModelCountry)e.SelectedItem;
            if (!string.IsNullOrEmpty(objeto.name.ToString()))
            {
                var listaSeleccionada = service.Where(c => c.name.Contains(objeto.name.ToString()));
                if (listaSeleccionada != null)
                {
                    var getLista = new Lista
                    {
                        nombre = listaSeleccionada.FirstOrDefault().name,
                        capital = listaSeleccionada.FirstOrDefault().capital,
                        latitude = listaSeleccionada.FirstOrDefault().latlng[0],
                        longitude = listaSeleccionada.FirstOrDefault().latlng[1],
                        poblacion = listaSeleccionada.FirstOrDefault().population

                    };
                    var mapa = new Mapa();
                    mapa.BindingContext = getLista;
                    await Navigation.PushAsync(mapa);


                }

            }
        }
    }
        
}
