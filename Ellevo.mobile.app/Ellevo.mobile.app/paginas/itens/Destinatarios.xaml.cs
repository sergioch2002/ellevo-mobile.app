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
        public Destinatarios(IEnumerable<InstrucaoDestinatario> dest)
        {
            
            InitializeComponent();
            GruposList.ItemsSource = dest;
            GetData();
        }
        private async void GetData()
        {
            InstrucaoDestinatario grupo = new app.InstrucaoDestinatario();

            GruposList.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                grupo = (InstrucaoDestinatario)GruposList.SelectedItem;

                var destinatarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/destinatariosusuarios?" + grupo.UsuarioId);
                DestinatariosUsuarios destUsu = new DestinatariosUsuarios(destinatarios);
            };
        }
        private void OnSairClicked(object sender, EventArgs e)
        {
           
        }
    }
}
