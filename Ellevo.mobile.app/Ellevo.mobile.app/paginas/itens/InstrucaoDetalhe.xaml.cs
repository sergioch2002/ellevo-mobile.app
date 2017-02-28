using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.paginas.novas;
using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class InstrucaoDetalhe : ContentPage
    {
        private string _instrucaoId;
        public InstrucaoDetalhe(string instrucaoId)
        {
            _instrucaoId = instrucaoId;

            InitializeComponent();
            SizeChanged += OnSizeChanged;
            if(!string.IsNullOrEmpty(instrucaoId))
                GetData();
            this.ToolbarItems.Add(new ToolbarItem("Lido", "lido.png", async () => { await MarcaLido(); }));
        }
        private async Task MarcaLido()
        {
            var response = await ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao/" + _instrucaoId + "/Lido/" + true, null);

            if (response == HttpStatusCode.OK)
            {
                await DisplayAlert("Sucesso", "Instrução lida", "Sair");

                await Navigation.PopAsync();
                GetData();
            }
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private async void GetData()
        {
            var instrucao = await ApiReader.GetDataFromApi<Instrucao>("/api/v1/mob/instrucao/" + _instrucaoId);
            if (instrucao != null)
            {
                    switch (instrucao.OrigemId)
                    {
                        case 1://Avulsa
                        instrucao.Origem = "Avulsa";
                            break;
                        case 2://Chamado
                        instrucao.Origem = "Chamado";
                            break;
                        case 3://Tarefa
                        instrucao.Origem = "Tarefa";
                            break;
                        case 4://CRM-Conta
                        instrucao.Origem = "CRM-Conta";
                            break;
                        case 5://CRM-Campanha
                        instrucao.Origem = "CRM-Campanha";
                            break;
                        case 6://Prospect
                        instrucao.Origem = "Prospect";
                            break;
                        default:
                            break;
                    }
                lblOrigem.Text = instrucao.Origem + ":";
                lblId.Text = instrucao.OrigemNumero.ToString();
                lblDataValor.Text = instrucao.DataCadastro.ToString();
                lblDeValor.Text = instrucao.Remetente;
                foreach (var item in instrucao.Destinatarios)
                    lblParaValor.Text += item.Nome + "; ";
                lblDescValor.Text = instrucao.Descricao;
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Erro ao carregar a Instrução",
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
