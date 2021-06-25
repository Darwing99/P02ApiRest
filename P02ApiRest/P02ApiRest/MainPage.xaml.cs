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

            Region();
        }

       

        void Region()

        {
            region = new List<Regiones>();
            region.Add(new Regiones { Name="Africa" });
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
            
        }


        private async void pickerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int position = pickerRegion.SelectedIndex;
            if (position>-1)
            {
                var name = region[position].Name;
                 service = await restService.GetRepositoriesAsync(DataConstants.url + name);
                collectionView.ItemsSource = service;
            }
           
         

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var buscar= service.Where(c => c.name.ToLower().Contains(search.Text.ToLower()));
            collectionView.ItemsSource = buscar;
        }
    }
  
}
