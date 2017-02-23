using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages.itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages.lists
{
    public partial class ListaAprovacoesTarefas : ContentPage
    {
        public ListaAprovacoesTarefas()
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
            var tarefas = await ApiReader.GetDataFromApi<IEnumerable<Tarefa>>("/api/v1/mob/tarefa/EmAprovacao");
            if (tarefas != null)
            {
                listView.ItemsSource = tarefas.OrderByDescending(x => x.TarefaId);
                Tarefa tarefa = new app.Tarefa();
                listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
                {
                    tarefa = (Tarefa)listView.SelectedItem;
                    await Navigation.PushAsync(new TarefaDetalhe(tarefa.TarefaId.ToString(), true));
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
