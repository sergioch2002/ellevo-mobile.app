using Ellevo.mobile.app.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaProvidencias : ContentPage
    {
        IEnumerable<Providencia> providencias;
        public ListaProvidencias()
        {
            InitializeComponent();

            GetDataFromApi();
        }
        private async void GetDataFromApi()
        {
            string endpoint = Sessao.UrlBase;
            endpoint += "/api/v1/mob/providencia";

            HttpClientHandler handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Sessao.Token.Token_type, Sessao.Token.Access_token);

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    providencias = JsonConvert.DeserializeObject<IEnumerable<Providencia>>(result);

                    listView.ItemsSource = providencias.OrderByDescending(x => x.ProvidenciaId);
                }
                else
                {
                    providencias = null;
                }
            }
        }
    }
}
