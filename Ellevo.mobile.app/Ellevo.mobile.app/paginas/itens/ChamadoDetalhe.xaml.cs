using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages.itens
{
    public partial class ChamadoDetalhe : ContentPage
    {
        private string _chamadoId;
        private int _currentTramite;
        private int _numTramites;
        private bool _isHtml;
        public ChamadoDetalhe(string chamadoId)
        {
            this._chamadoId = chamadoId;
            InitializeComponent();
            lblVencimento.IsVisible = false;
            lblVencimentoValor.IsVisible = false;
            lblCliente.IsVisible = false;
            lblClienteValor.IsVisible = false;
            lblProvId.IsVisible = false;
            lblProvData.IsVisible = false;
            textEditor.IsVisible = false;
            btnBack.IsVisible = false;
            pgGo.IsVisible = false;
            btnNext.IsVisible = false;
            lblPages.IsVisible = false;
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(chamadoId))
                GetData();
            this.Title += " " + chamadoId;
            btnNext.Text = "\u003e\u003e";
            btnBack.Text = "\u003c\u003c";
            lblPages.Text = "Page\n10/10";
            this.ToolbarItems.Add(new ToolbarItem("Adicionar", "adicionar.png", async () => { await DisplayAlert("Clicado!", "Adicionar clicado.", "Fechar"); }));
            this.ToolbarItems.Add(new ToolbarItem("Remover", "remover.png", async () => { await DisplayAlert("Clicado!", "Remover clicado.", "Fechar"); }));
            this.ToolbarItems.Add(new ToolbarItem("Lido", "lido.png", async () => { await DisplayAlert("Clicado!", "Lido clicado.", "Fechar"); }));
        }
        private async void OnTramClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Clicado!", "Novo Tramite clicado.", "Fechar");
        }
        private async void OnInstrClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Clicado!", "Nova Instrução clicado.", "Fechar");
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private void OnBackClicked(object sender, EventArgs e)
        {
            if (this._currentTramite > 1 && this._currentTramite <= this._numTramites)
                this._currentTramite--;
            else
                this._currentTramite = this._numTramites;

            LoadTramites(this._currentTramite);
        }
        private void OnNextClicked(object sender, EventArgs e)
        {
            if (this._currentTramite < this._numTramites)
                this._currentTramite++;
            else
                this._currentTramite = 1;

            LoadTramites(this._currentTramite);
        }
        private void OnPageGo(object sender, EventArgs e)
        {
            int pgToGo = 0;
            if (int.TryParse(pgGo.Text, out pgToGo) && (pgToGo >= 1 && pgToGo <= this._numTramites))
                LoadTramites(pgToGo);
            else
                DisplayAlert("Erro", "Digite um valor válido", "Fechar");

        }
        private async void GetData()
        {
            var chamado = await ApiReader.GetDataFromApi<Chamado>("/api/v1/mob/chamado/" + _chamadoId);
            if (chamado != null)
            {
                lblClienteValor.Text = chamado.NomeCliente;
                lblDeValor.Text = chamado.UsuarioResponsavel;
                lblTituloValor.Text = chamado.Titulo;
                lblVencimentoValor.Text = chamado.Vencimento.ToString();
                lblStatusValor.Text = chamado.Status  == 1 ? "Iniciado" : "Não Iniciado";
                lblDescValor.Text = chamado.Descricao;
                
                if (!string.IsNullOrEmpty(lblVencimentoValor.Text))
                {
                    lblVencimento.IsVisible = true;
                    lblVencimentoValor.IsVisible = true;
                }
                if (!string.IsNullOrEmpty(lblClienteValor.Text))
                {
                    lblCliente.IsVisible = true;
                    lblClienteValor.IsVisible = true;
                }

                if (chamado.QuantidadeTramites > 0)
                {
                    this._numTramites = chamado.QuantidadeTramites;
                    LoadTramites(chamado.QuantidadeTramites);
                }
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Erro ao carregar o Chamado",
                    TextColor = Color.Black,
                    FontSize = 20
                };
                this.Content = lbl;
                this.Content.VerticalOptions = LayoutOptions.Center;
                this.Content.HorizontalOptions = LayoutOptions.Center;
            }
        }
        private async void LoadTramites(int qtdTramites)
        {
            this._currentTramite = qtdTramites;

            lblProvId.IsVisible = true;
            lblProvData.IsVisible = true;
            textEditor.IsVisible = true;
            btnBack.IsVisible = true;
            pgGo.IsVisible = true;
            btnNext.IsVisible = true;
            lblPages.IsVisible = true;
            lblProvId.Text = "Trâmite " + qtdTramites.ToString();
            pgGo.Placeholder = this._currentTramite.ToString();
            var tramite = await ApiReader.GetDataFromApi<Tramite>("/api/v1/mob/tramite/chamado/" + _chamadoId + "/tramite/" + this._currentTramite.ToString());
            this._isHtml = tramite.TipoDescricao.ToLower().Contains(".htm") ? true : false;
            lblProvData.Text = tramite.Data.Value.ToString();
            lblPages.Text = this._currentTramite.ToString() + "-" + this._numTramites.ToString();
            if(this._isHtml)
            {
                var browser = new WebView
                {
                    HeightRequest = 180
                };
                var htmlSource = new HtmlWebViewSource
                {
                    Html = tramite.Descricao.Replace(@"\", string.Empty)
            };
                browser.Source = htmlSource;
            }
            textEditor.Text = tramite.Descricao;
        }
    }
}
