using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages.itens
{
    public partial class TarefaDetalhe : ContentPage
    {
        private string _tarefaId;
        private int _currentProv;
        private int _numProvidencias;
        private bool _isHtml;
        public TarefaDetalhe(string tarefaId)
        {
            this._tarefaId = tarefaId;
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
            if (!string.IsNullOrEmpty(tarefaId))
                GetData();
            this.Title += " " + tarefaId;
            btnNext.Text = "\u003e\u003e";
            btnBack.Text = "\u003c\u003c";
            lblPages.Text = "Page\n10/10";
            this.ToolbarItems.Add(new ToolbarItem("Adicionar", "adicionar.png", async () => { await DisplayAlert("Clicado!", "Adicionar clicado.", "Fechar"); }));
            this.ToolbarItems.Add(new ToolbarItem("Remover", "remover.png", async () => { await DisplayAlert("Clicado!", "Remover clicado.", "Fechar"); }));
            this.ToolbarItems.Add(new ToolbarItem("Lido", "lido.png", async () => { await DisplayAlert("Clicado!", "Lido clicado.", "Fechar"); }));
        }
        private async void OnProvClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Clicado!", "Nova Providência clicado.", "Fechar");
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
            if (this._currentProv > 1 && this._currentProv <= this._numProvidencias)
                this._currentProv--;
            else
                this._currentProv = this._numProvidencias;

            LoadTramites(this._currentProv);
        }
        private void OnNextClicked(object sender, EventArgs e)
        {
            if (this._currentProv < this._numProvidencias)
                this._currentProv++;
            else
                this._currentProv = 1;

            LoadTramites(this._currentProv);
        }
        private void OnPageGo(object sender, EventArgs e)
        {
            int pgToGo = 0;
            if (int.TryParse(pgGo.Text, out pgToGo) && (pgToGo >= 1 && pgToGo <= this._numProvidencias))
                LoadTramites(pgToGo);
            else
                DisplayAlert("Erro", "Digite um valor válido", "Fechar");

        }
        private async void GetData()
        {
            var tarefa = await ApiReader.GetDataFromApi<Tarefa>("/api/v1/mob/tarefa/" + _tarefaId);
            
            if (tarefa != null)
            {
                lblClienteValor.Text = tarefa.NomeCliente;
                lblDeValor.Text = tarefa.UsuarioResponsavel;
                lblTituloValor.Text = tarefa.Titulo;
                lblVencimentoValor.Text = tarefa.Vencimento.ToString();
                lblStatusValor.Text = tarefa.Status == 1 ? "Iniciado" : "Não Iniciado";
                lblDescValor.Text = tarefa.Descricao;
                

                if (!string.IsNullOrEmpty(lblVencimentoValor.Text))
                {
                    lblVencimento.IsVisible = true;
                    lblVencimentoValor.IsVisible = true;
                }
                if(!string.IsNullOrEmpty(lblClienteValor.Text))
                {
                    lblCliente.IsVisible = true;
                    lblClienteValor.IsVisible = true;
                }

                if (tarefa.QuantidadeProvidencias > 0)
                {
                    this._numProvidencias = tarefa.QuantidadeProvidencias;
                    LoadTramites(tarefa.QuantidadeProvidencias);
                }
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Erro ao carregar a Tarefa",
                    TextColor = Color.Black,
                    FontSize = 20
                };
                this.Content = lbl;
                this.Content.VerticalOptions = LayoutOptions.Center;
                this.Content.HorizontalOptions = LayoutOptions.Center;
            }
        }
        private async void LoadTramites(int qtdProvidencias)
        {
            this._currentProv = qtdProvidencias;

            lblProvId.IsVisible = true;
            lblProvData.IsVisible = true;
            textEditor.IsVisible = true;
            btnBack.IsVisible = true;
            pgGo.IsVisible = true;
            btnNext.IsVisible = true;
            lblPages.IsVisible = true;
            lblProvId.Text = "Providencia " + qtdProvidencias.ToString();
            pgGo.Placeholder = this._currentProv.ToString();
            var providencia = await ApiReader.GetDataFromApi<IEnumerable<Providencia>>("/api/v1/mob/providencia/tarefa/" + _tarefaId + "/providencia/" + this._currentProv.ToString());
            this._isHtml = providencia.FirstOrDefault().TipoDescricao.ToLower().Contains(".htm") ? true : false;
            lblProvData.Text = providencia.FirstOrDefault().Data.Value.ToString();
            lblPages.Text = this._currentProv.ToString() + "-" + this._numProvidencias.ToString();
            if (this._isHtml)
            {
                var browser = new WebView
                {
                    HeightRequest = 180
                };
                var htmlSource = new HtmlWebViewSource
                {
                    Html = providencia.FirstOrDefault().Descricao.Replace(@"\", string.Empty)
                };
                browser.Source = htmlSource;
            }
            textEditor.Text = providencia.FirstOrDefault().Descricao;
        }
    }
}
