﻿using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages.itens;
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
        public ListaTarefas()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
        private async void GetData()
        {
            var tarefas = await ApiReader.GetDataFromApi<IEnumerable<Tarefa>>("/api/v1/mob/tarefa");
            if(tarefas != null)
            {
                listView.ItemsSource = tarefas.OrderByDescending(x => x.TarefaId);
                Tarefa tarefa = new app.Tarefa();
                listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
                {
                    tarefa = (Tarefa)listView.SelectedItem;
                    await Navigation.PushAsync(new TarefaDetalhe(tarefa.TarefaId.ToString()));
                };
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Não há itens para exibir",
                    TextColor = Color.Black,
                    FontSize = 20
                };
                this.Content = lbl;
                this.Content.VerticalOptions = LayoutOptions.Center;
                this.Content.HorizontalOptions = LayoutOptions.Center;
            }
        }
    }
}
