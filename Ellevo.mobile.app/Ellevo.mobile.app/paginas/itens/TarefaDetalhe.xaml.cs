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
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(tarefaId))
                GetData();
            this.Title += " " + tarefaId;
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
                    lblProvId.IsVisible = true;
                    lblProvData.IsVisible = true;
                    textEditor.IsVisible = true;
                    lblProvId.Text = "Providência " + tarefa.QuantidadeProvidencias.ToString();
                    var providencia = await ApiReader.GetDataFromApi<IEnumerable<Providencia>>("/api/v1/mob/providencia/tarefa/" + _tarefaId + "/providencia/" + (tarefa.QuantidadeProvidencias).ToString());
                    lblProvData.Text = providencia.FirstOrDefault().Data.Value.ToString();
                    textEditor.Text = providencia.FirstOrDefault().Descricao;
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
    }
}
