using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages;
using Ellevo.mobile.app.paginas.novas;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class ListaInstrucoes : ContentPage
    {
        Filtro filtro;
        int numInstrucoes;
        public ListaInstrucoes(int numInstrucoes)
        {
            this.numInstrucoes = numInstrucoes;
            filtro = new app.Filtro
            {
                OrdenarPor = "OrdenarPor=DataCadastro",
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

            if (numInstrucoes < Sessao.ItensPorPagina)
            {
                btnBack.IsVisible = false;
                btnNext.IsVisible = false;
                pgGo.IsVisible = false;
            }

            this.ToolbarItems.Add(new ToolbarItem("Adicionar", "adicionar.png", async () => { await Navigation.PushAsync(new NovaInstrucao2()); ; }));

            Instrucao inst = new Instrucao();
            listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                inst = (Instrucao)listView.SelectedItem;
                InstrucaoDetalhe pageInstDet = new pages.InstrucaoDetalhe(inst.InstrucaoId.ToString());
                if (!this.Navigation.NavigationStack.Contains(pageInstDet))
                    await Navigation.PushAsync(pageInstDet);
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
            var instrucoes = await ApiReader.GetDataFromApi<IEnumerable<Instrucao>>("/api/v1/mob/instrucao/naolidas" + "?" + 
                filtro.OrdenarPor + "&OrdemTipo=" + filtro.OrdemTipo + "&Pagina=" + filtro.Pagina + "&PaginaInicio=" + 
                filtro.PaginaInicio + "&PaginaTamanho=" + filtro.PaginaTamanho + "&Pesquisa=" + filtro.Pesquisa);

            if (instrucoes.Any())
            {
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
