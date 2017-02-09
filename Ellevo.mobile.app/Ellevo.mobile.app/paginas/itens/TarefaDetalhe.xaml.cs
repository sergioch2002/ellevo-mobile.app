using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
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
            waitActivityIndicator.IsRunning = true;
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(tarefaId))
                GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private async void GetData()
        {
            
            var tarefa = await ApiReader.GetDataFromApi<Tarefa>("/api/v1/mob/tarefa/" + _tarefaId);
            waitActivityIndicator.IsRunning = false;
            if (tarefa != null)
            {
                lblId.Text = tarefa.TarefaId.ToString();
                lblClienteValor.Text = tarefa.NomeCliente;
                lblDeValor.Text = tarefa.UsuarioResponsavel;
                lblTituloValor.Text = tarefa.Titulo;
                lblVencimentoValor.Text = tarefa.Vencimento.ToString();
                lblStatusValor.Text = tarefa.Status == 1 ? "Iniciado" : "Não Iniciado";
                lblDescValor.Text = tarefa.Descricao;
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
    }
}
