using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using P02ApiRest.model;

namespace P02ApiRest
{
    public partial class MainPage : ContentPage
    {
        RestService restService;

        public MainPage()
        {
            InitializeComponent();
            restService = new RestService();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            List<ModelCountry> service = await restService.GetRepositoriesAsync(DataConstants.url);
            collectionView.ItemsSource = service;
        }
    }
  
}
