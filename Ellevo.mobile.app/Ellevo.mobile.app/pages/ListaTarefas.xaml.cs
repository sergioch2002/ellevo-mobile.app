﻿using Ellevo.mobile.app.objects;
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
    public partial class ListaTarefas : ContentPage
    {
        IEnumerable<Tarefa> tarefas;
        public ListaTarefas()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            GetDataFromApi();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
        private async void GetDataFromApi()
        {
            string endpoint = Sessao.UrlBase;
            endpoint += "/api/v1/mob/tarefa";

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
                    tarefas = JsonConvert.DeserializeObject<IEnumerable<Tarefa>>(result);

                    listView.ItemsSource = tarefas.OrderByDescending(x => x.TarefaId);
                }
                else
                {
                    tarefas = null;
                }
            }
        }
    }
}
