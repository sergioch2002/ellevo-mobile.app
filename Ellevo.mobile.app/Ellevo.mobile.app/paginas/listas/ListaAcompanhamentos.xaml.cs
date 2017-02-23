using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages.itens;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaAcompanhamentos : ContentPage
    {
        public ListaAcompanhamentos()
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
            var acompanhamentos = await ApiReader.GetDataFromApi<IEnumerable<Conta>>("/api/v1/mob/conta");
            if (acompanhamentos != null)
            {
                listView.ItemsSource = acompanhamentos.OrderByDescending(x => x.ContaId);
                Conta conta = new Conta();
                
                listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
                {
                    conta = (Conta)listView.SelectedItem;
                    await Navigation.PushAsync(new ContaDetalhe(conta.ContaId.ToString()));
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
