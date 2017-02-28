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
        Filtro filtro;
        int numChamados;
        public ListaChamados(int numChamados)
        {
            this.numChamados = numChamados;
            filtro = new app.Filtro
            {
                OrdenarPor = "OrdenarPor=ChamadoId",
                OrdemTipo = EPaginacaoOrdem.Desc,
                Pagina = 0,
                PaginaInicio = 0,
                PaginaTamanho = Sessao.ItensPorPagina,
                Pesquisa = ""
            };

            InitializeComponent();
            SizeChanged += OnSizeChanged;
            GetData();
            btnNext.Text = "\u003e\u003e";
            btnBack.Text = "\u003c\u003c";

            if (numChamados < Sessao.ItensPorPagina)
            {
                btnBack.IsVisible = false;
                btnNext.IsVisible = false;
                pgGo.IsVisible = false;
            }

            Chamado chamado = new app.Chamado();
            listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                chamado = (Chamado)listView.SelectedItem;
                await Navigation.PushAsync(new ChamadoDetalhe(chamado.ChamadoId.ToString()));
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
        private void OnBackClicked(object sender, EventArgs e)
        {
            if (filtro.Pagina == 0)
                filtro.Pagina = 1;

            filtro.Pagina--;
            GetData();
        }
        private void OnNextClicked(object sender, EventArgs e)
        {
            filtro.Pagina++;
            GetData();
        }
        private void OnPageGo(object sender, EventArgs e)
        {
            int pgToGo = 0;
            if (int.TryParse(pgGo.Text, out pgToGo))
            {
                filtro.Pagina = pgToGo;
                GetData();
            }
            else
                DisplayAlert("Erro", "Digite um valor válido", "Fechar");

        }
        private async void GetData()
        {
            var chamados = await ApiReader.GetDataFromApi<IEnumerable<Chamado>>("/api/v1/mob/chamado" + "?" +
                filtro.OrdenarPor + "&OrdemTipo=" + filtro.OrdemTipo + "&Pagina=" + filtro.Pagina + "&PaginaInicio=" +
                filtro.PaginaInicio + "&PaginaTamanho=" + filtro.PaginaTamanho + "&Pesquisa=" + filtro.Pesquisa);

            if (chamados.Any())
            {
                listView.ItemsSource = chamados.OrderByDescending(x => x.ChamadoId);
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
