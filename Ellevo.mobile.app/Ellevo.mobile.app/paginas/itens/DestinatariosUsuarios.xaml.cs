using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.paginas.novas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ellevo.mobile.app.paginas.itens
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DestinatariosUsuarios : ContentPage
    {
        public event EventHandler UsuariosAdicionados;

        public DestinatariosUsuarios()
        {
            InitializeComponent();
        }
        public async Task GetData(InstrucaoDestinatario grupo)
        {
            var destinatarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/destinatariosusuarios/" + grupo.UsuarioId);

            UsuariosList.ItemsSource = destinatarios;

            UsuariosList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var usuario = (InstrucaoDestinatario)UsuariosList.SelectedItem;

                EventHandler handler = UsuariosAdicionados;
                if(handler != null)
                    handler(this, e);

                DisplayAlert("Atenção", "Usuario " + usuario.Nome + " escolhido!", "OK");
            };
        }

        private async void OnSairClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }
    }
    
}
