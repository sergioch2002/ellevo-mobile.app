using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class ListaInstrucoes : ContentPage
    {
        public ListaInstrucoes()
        {
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
            var instrucoes = await ApiReader.GetDataFromApi<IEnumerable<Instrucao>>("/api/v1/mob/instrucao/naolidas");
            if (instrucoes != null)
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
                listView.ItemsSource = instrucoes.OrderByDescending(x => x.InstrucaoId);
                Instrucao inst = new Instrucao();
                listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
                {
                    inst = (Instrucao)listView.SelectedItem;
                    Navigation.InsertPageBefore(new pages.InstrucaoDetalhe(inst.InstrucaoId.ToString()), this);
                    await Navigation.PopAsync();
                };
                
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
