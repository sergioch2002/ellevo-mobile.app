using Ellevo.mobile.app.objects;
using System;
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
            GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";
        }
        private async void GetData()
        {
            var instrucoes = await ApiReader.GetDataFromApi<Instrucao>("/api/v1/mob/instrucao/" + _instrucaoId);
            var enumerator = instrucoes.GetEnumerator();
            Instrucao instrucao = enumerator.Current;
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
                lblOrigem.Text = instrucao.Origem;
                lblId.Text = instrucao.InstrucaoId.ToString();
                lblDataValor.Text = instrucao.DataCadastro.ToString();
                lblDeValor.Text = instrucao.Remetente;
                lblDescValor.Text = instrucao.Titulo;
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
