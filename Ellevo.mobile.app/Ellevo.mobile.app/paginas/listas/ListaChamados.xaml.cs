using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages.itens;
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
            {
                listView.ItemsSource = chamados.OrderByDescending(x => x.ChamadoId);
                Chamado chamado = new app.Chamado();
                listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
                {
                    chamado = (Chamado)listView.SelectedItem;
                    await Navigation.PushAsync(new ChamadoDetalhe(chamado.ChamadoId.ToString()));
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
