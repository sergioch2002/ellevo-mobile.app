using Ellevo.mobile.app.controles;
using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
        List<InstrucaoDestinatario> usuariosSelecionados { get; set; }

        private InstrucaoDestinatario _grupo;
        private InstrucaoDestinatario _usuario;

        public NovaInstrucao2()
        {
            usuariosSelecionados = new List<app.InstrucaoDestinatario>();

            InitializeComponent();
            GetData();
        }

        private async void OnGrupoSelected(object sender, EventArgs e)
        {
            var grupo = (BindablePicker)sender;

            if (pickerUsuario.SelectedIndex > -1)
                pickerUsuario.SelectedIndex = -1;

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

            if (usuariosSelecionados.Count > 0)
                if (_usuario != null)
                    if (usuariosSelecionados.Exists(i => i.UsuarioId == _usuario.UsuarioId))
                        return;

            if (!string.IsNullOrEmpty(_usuario.Nome))
            {
                textDestinatarios.Text += _usuario.Nome + "; ";
                usuariosSelecionados.Add(_usuario);
            }

            
            _usuario = new InstrucaoDestinatario();
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
            i.Destinatarios = usuariosSelecionados;
            i.Descricao = textInstrucao.Text;
            i.OrigemId = 1;
            i.TipoDescricao = "txt";
            i.Lida = false;

            var response = await ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao", i);

            if (response == HttpStatusCode.Created)
            {
                await DisplayAlert("Sucesso", "Instrução criada com sucesso", "Sair");

                await Navigation.PopAsync();
            }
        }

        private void textDestinatarios_Focused(object sender, FocusEventArgs e)
        {
            pickerGrupo.Focus();
        }
    }
}
