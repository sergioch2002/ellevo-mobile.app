using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Ellevo.mobile.app
{
    public partial class ListaInstrucoes : ContentPage
    {
        IEnumerable<Instrucao> instrucoes;
        public ListaInstrucoes()
        {
            InitializeComponent();

            GetDataFromApi();
        }

        
        private async void GetDataFromApi()
        {
            string endpoint = Sessao.UrlBase;
            endpoint += "/api/v1/mob/instrucao/naolidas";

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
                    instrucoes = JsonConvert.DeserializeObject<IEnumerable<Instrucao>>(result);

                    foreach (var item in instrucoes)
                    {
                        switch (item.OrigemId)
                        {
                            case 1://Avulsa
                                item.Origem = "Avulsa";
                                break;
                            case 2://Chamado
                                item.Origem = "Chamado";
                                break;
                            case 3://Tarefa
                                item.Origem = "Tarefa";
                                break;
                            case 4://CRM-Conta
                                item.Origem = "CRM-Conta";
                                break;
                            case 5://CRM-Campanha
                                item.Origem = "CRM-Campanha";
                                break;
                            case 6://Prospect
                                item.Origem = "Prospect";
                                break;
                            default:
                                break;
                        } 
                    }

                    listView.ItemsSource = instrucoes;
                }
                else
                {
                    instrucoes = null;
                }
            }
        }
    }
}
