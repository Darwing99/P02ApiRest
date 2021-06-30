﻿using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace P02ApiRest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa()
        {
            InitializeComponent();
            BindingContext =new Lista();
            locationGPS();
        }
        public async void locationGPS()
        {

            var location = CrossGeolocator.Current;

            if (!location.IsGeolocationEnabled || !location.IsGeolocationAvailable)
            {

                await DisplayAlert("Warning", " GPS no esta activo", "ok");
                return;
            }
            if (!location.IsListening)
            {
                await location.StartListeningAsync(TimeSpan.FromSeconds(10), 1);

                return;
            }




        }
        protected  override void OnAppearing()
        {
            base.OnAppearing();


            Pin pin = new Pin
            {
                Label = nombre.Text,
                Address = descripcion.Text,
                Type = PinType.Generic,
                Position = new Position(Convert.ToDouble(latitud.Text), Convert.ToDouble(longitud.Text))
            };
            m.Pins.Add(pin);
            m.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitud.Text), Convert.ToDouble(longitud.Text)), Distance.FromKilometers(1)));




        }
    }
}