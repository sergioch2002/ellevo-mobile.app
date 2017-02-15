using Ellevo.mobile.app.objects;
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
        public ContaDetalhe(string contaId)
        {
            this._contaId = contaId;
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            if (!string.IsNullOrEmpty(contaId))
                GetData();
            this.Title += " " + contaId;
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
            var conta = await ApiReader.GetDataFromApi<Conta>("/api/v1/mob/conta/" + _contaId);
            if (conta != null)
            {
                lblOrigem.Text = "Conta: ";
                lblId.Text = conta.ContaId.ToString();
                lblRazao.Text = "Razão Social: ";
                lblRazaoValor.Text = conta.RazaoSocial;
                lblFantasia.Text = "Nome Fantasia: ";
                lblFantasiaValor.Text = conta.NomeFantasia;
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
