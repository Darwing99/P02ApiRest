using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Newtonsoft.Json;

namespace P02ApiRest.model
{
    class RestService
    {
        HttpClient cliente;

        public RestService()
        {
            cliente = new HttpClient();

            if (Device.RuntimePlatform == Device.UWP)
            {
                cliente.DefaultRequestHeaders.Add("User-Agent", "");
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            }
        }

        public async Task<List<ModelCountry>> GetRepositoriesAsync(string url)
        {
            List<ModelCountry> lista = null;
            try
            {
                HttpResponseMessage respuesta = await cliente.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    string informacion = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<ModelCountry>>(informacion);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error", ex.Message);
            }

            return lista;
        }
    }
}
