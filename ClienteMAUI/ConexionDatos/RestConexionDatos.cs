using ClienteMAUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace ClienteMAUI.ConexionDatos
{
    public class RestConexionDatos : IRestConexionDatos
    {
        private readonly HttpClient httpClient;
        private readonly string dominio;
        private readonly string url;
        private readonly JsonSerializerOptions opcionesJson;

        public RestConexionDatos()
        {
            httpClient = new HttpClient();
            dominio = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5253" : "http://localhost:5253";
            url = $"{dominio}/api";
            opcionesJson = new JsonSerializerOptions { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task<List<Plato>> GetPlatosAsync()
        {
            List<Plato> platos = new List<Plato>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
                Debug.WriteLine("[RED] Sin acceso a internet.");
                return platos;
            }
            try { 
                HttpResponseMessage response = await httpClient.GetAsync($"{url}/plato");
                if (response.IsSuccessStatusCode)
                { // [{id= 123},{id=321}]
                    //Deserializar
                    var contenido = await response.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, opcionesJson);
                    Random rand = new Random();
                    platos = platos.OrderBy(_ => rand.Next()).ToList();
                }
                else {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX)");
                }
            } catch (Exception e) {
                Debug.WriteLine($"[ERROR] {e.Message}");
            }

            return platos;
        }
        public async Task AddPlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] Sin acceso a internet.");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer,Encoding.UTF8,"application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{url}/plato", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[RED] Se registró correctamente.");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message}");
            }
        }
        public async Task UpdatePlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] Sin acceso a internet.");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{url}/plato/{plato.Id}", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[RED] Se registró correctamente.");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message}");
            }
        }
        public async Task DeletePlatoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] Sin acceso a internet.");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/plato/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[RED] Se registró correctamente.");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message}");
            }
        }
    }
}
