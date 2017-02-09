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
        public ChamadoDetalhe(string chamadoId)
        {
            this._chamadoId = chamadoId;
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(chamadoId))
                GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private async void GetData()
        {
            var chamado = await ApiReader.GetDataFromApi<Chamado>("/api/v1/mob/chamado/" + _chamadoId);
            if (chamado != null)
            {
                lblId.Text = chamado.ChamadoId.ToString();
                lblClienteValor.Text = chamado.NomeCliente;
                lblDeValor.Text = chamado.UsuarioResponsavel;
                lblTituloValor.Text = chamado.Titulo;
                lblVencimentoValor.Text = chamado.Vencimento.ToString();
                lblStatusValor.Text = chamado.Status  == 1 ? "Iniciado" : "Não Iniciado";
                lblDescValor.Text = chamado.Descricao;
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
