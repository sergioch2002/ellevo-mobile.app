using Ellevo.mobile.app.controles;
using Ellevo.mobile.app.objects;
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

namespace Ellevo.mobile.app.paginas.novas
{

    public partial class NovaInstrucao2 : ContentPage
    {
        //List<InstrucaoDestinatario> grupos { get; set; }
        //List<InstrucaoDestinatario> usuarios { get; set; }

        private InstrucaoDestinatario _grupo;
        private InstrucaoDestinatario _usuario;

        public NovaInstrucao2()
        {
            InitializeComponent();
        }

        private async void OnGrupoSelected(object sender, EventArgs e)
        {
            var grupo = (BindablePicker)sender;

            if (pickerGrupo.SelectedIndex > -1)
                _grupo = (InstrucaoDestinatario)pickerGrupo.SelectedItem;

            await GetUsuariosData();
        }

        private async Task GetUsuariosData()
        {
            var usuarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/destinatariosusuarios/" + _grupo.UsuarioId);
            pickerUsuario.ItemsSource = usuarios.ToList();
        }

        private void OnUsuarioSelected(object sender, EventArgs e)
        {
            var usuario = (BindablePicker)sender;

            if (pickerUsuario.SelectedIndex > -1)
                _usuario = (InstrucaoDestinatario)pickerUsuario.SelectedItem;

            textDestinatarios.Text = "";
            textDestinatarios.Text += _usuario.Nome + "; ";
        }

        private async Task GetData()
        {
            var grupos = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/DestinatariosGrupos");

            pickerGrupo.ItemsSource = grupos.ToList();
            pickerGrupo.SelectedItem = _grupo;
        }

        private async void OnConfirClicked(object sender, EventArgs e)
        {
            Instrucao i = new Instrucao();
            i.Descricao = textInstrucao.Text;

            await ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao", i);
        }
    }
}
