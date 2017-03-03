using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.paginas.novas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages.itens
{
    public partial class ContaDetalhe : ContentPage
    {
        private string _contaId;
        private int _currentAcompanhamento;
        private int _numAcompanhamentos;
        public ContaDetalhe(string contaId)
        {
            this._contaId = contaId;
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(contaId))
                GetData();
            this.Title += " " + contaId;
            textEditor.IsVisible = false;
            btnBack.IsVisible = false;
            pgGo.IsVisible = false;
            btnNext.IsVisible = false;
            lblPages.IsVisible = false;
            btnNext.Text = "\u003e\u003e";
            btnBack.Text = "\u003c\u003c";
            lblPages.Text = "Page\n10/10";
            //this.ToolbarItems.Add(new ToolbarItem("Adicionar", "adicionar.png", async () => { await DisplayAlert("Clicado!", "Adicionar clicado.", "Fechar"); }));
            //this.ToolbarItems.Add(new ToolbarItem("Remover", "remover.png", async () => { await DisplayAlert("Clicado!", "Remover clicado.", "Fechar"); }));
            this.ToolbarItems.Add(new ToolbarItem("Lido", "lido.png", async () => { await DisplayAlert("Clicado!", "Lido clicado.", "Fechar"); }));
        }
        private async void OnInstrClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovaInstrucao2());
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private void OnBackClicked(object sender, EventArgs e)
        {
            if (this._currentAcompanhamento > 1 && this._currentAcompanhamento <= this._numAcompanhamentos)
                this._currentAcompanhamento--;
            else
                this._currentAcompanhamento = this._numAcompanhamentos;

            LoadAcompanhamentos(this._currentAcompanhamento);
        }
        private void OnNextClicked(object sender, EventArgs e)
        {
            if (this._currentAcompanhamento < this._numAcompanhamentos)
                this._currentAcompanhamento++;
            else
                this._currentAcompanhamento = 1;

            LoadAcompanhamentos(this._currentAcompanhamento);
        }
        private void OnPageGo(object sender, EventArgs e)
        {
            int pgToGo = 0;
            if (int.TryParse(pgGo.Text, out pgToGo) && (pgToGo >= 1 && pgToGo <= this._numAcompanhamentos))
                LoadAcompanhamentos(pgToGo);
            else
                DisplayAlert("Erro", "Digite um valor válido", "Fechar");

        }
        private async void LoadAcompanhamentos(int qtdAcompanhamentos)
        {
            this._currentAcompanhamento = qtdAcompanhamentos;

            lblProvId.IsVisible = true;
            lblProvData.IsVisible = true;
            textEditor.IsVisible = true;
            btnBack.IsVisible = true;
            pgGo.IsVisible = true;
            btnNext.IsVisible = true;
            lblPages.IsVisible = true;
            lblProvId.Text = "Acompanhamento " + qtdAcompanhamentos.ToString();
            pgGo.Placeholder = this._currentAcompanhamento.ToString();
            var acompanhamento = await ApiReader.GetDataFromApi<Acompanhamento>("/api/v1/mob/conta/" + _contaId + "/acompanhamento/" + this._currentAcompanhamento.ToString());
            //this._isHtml = tramite.TipoDescricao.ToLower().Contains(".htm") ? true : false;
            lblProvData.Text = acompanhamento.Data.ToString();
            lblPages.Text = this._currentAcompanhamento.ToString() + "-" + this._numAcompanhamentos.ToString();
            //if (this._isHtml)
            //{
            //    var browser = new WebView
            //    {
            //        HeightRequest = 180
            //    };
            //    var htmlSource = new HtmlWebViewSource
            //    {
            //        Html = "<html><body>" + acompanhamento.AcompanhamentoDesc.Replace(@"\", string.Empty) + "</body></html>"
            //    };
            //    browser.Source = htmlSource;
            //}
            textEditor.Text = acompanhamento.AcompanhamentoDesc;
        }
        private async void GetData()
        {
            var conta = await ApiReader.GetDataFromApi<Conta>("/api/v1/mob/conta/" + _contaId);
            if (conta != null)
            {
                lblOrigem.Text = "Conta: ";
                lblId.Text = conta.ContaId.ToString();
                lblRazao.Text = "Razão Social: ";
                lblRazaoValor.Text = conta.RazaoSocial;
                lblFantasia.Text = "Nome Fantasia: ";
                lblFantasiaValor.Text = conta.NomeFantasia;
                _numAcompanhamentos = conta.QuantidadeAcompanhamentos;

                
                if (conta.QuantidadeAcompanhamentos > 0)
                {
                    this._numAcompanhamentos = conta.QuantidadeAcompanhamentos;
                    LoadAcompanhamentos(conta.QuantidadeAcompanhamentos);
                }
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Erro ao carregar a Conta",
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
