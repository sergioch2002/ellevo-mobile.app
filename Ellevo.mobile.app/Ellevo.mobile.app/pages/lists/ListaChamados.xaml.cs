﻿using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaChamados : ContentPage
    {
        public ListaChamados()
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
            var chamados = await ApiReader.GetDataFromApi<IEnumerable<Chamado>>("/api/v1/mob/chamado");
            if(chamados != null)
                listView.ItemsSource = chamados.OrderByDescending(x => x.ChamadoId);
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
