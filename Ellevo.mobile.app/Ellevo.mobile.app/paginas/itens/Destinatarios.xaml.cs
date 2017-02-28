using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.itens
{
    public partial class Destinatarios : ContentPage
    {

        public Destinatarios()
        {
            InitializeComponent();

            InstrucaoDestinatario grupoSelecionado = new app.InstrucaoDestinatario();

            GruposList.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                grupoSelecionado = (InstrucaoDestinatario)GruposList.SelectedItem;

                DestinatariosUsuarios destUsu = new DestinatariosUsuarios();
                await destUsu.GetData(grupoSelecionado);
                await Navigation.PushModalAsync(destUsu);
            };
        }
        public async Task GetData()
        {
            var grupo = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/DestinatariosGrupos");
            GruposList.ItemsSource = grupo;

            
        }
        private async void OnSairClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }
    }
}
