using Ellevo.mobile.app.controles;
using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.novas
{
    public partial class NovoTramite : ContentPage
    {
        private string _chamadoId;

        List<Status> status { get; set; }
        List<Atividade> atividades { get; set; }
        List<Motivo> motivos { get; set; }

        Status selectedStatus { get; set; }
        Atividade selectedAtividade { get; set; }
        Motivo selectedMotivo { get; set; }

        public NovoTramite(string chamadoId)
        {
            this._chamadoId = chamadoId;
            GetData();
            InitializeComponent();

            this.Title = "Novo Trâmite do chamado " + chamadoId;
        }

        private void OnStatusSelected(object sender, EventArgs e)
        {
            var status = (BindablePicker)sender;

            if (pickerStatus.SelectedIndex > -1)
                selectedStatus = (Status)status.SelectedItem;
        }

        private void OnMotivoSelected(object sender, EventArgs e)
        {
            var motivo = (BindablePicker)sender;

            if (pickerStatus.SelectedIndex > -1)
                selectedMotivo = (Motivo)motivo.SelectedItem;
        }

        private void OnAtividadeSelected(object sender, EventArgs e)
        {
            var atividade = (BindablePicker)sender;

            if (pickerStatus.SelectedIndex > -1)
                selectedAtividade = (Atividade)atividade.SelectedItem;
        }
        private async void OnConfClicked(object Confsender, EventArgs args)
        {
            Tramite tramite = new app.Tramite();
            tramite.Atividades = new List<app.Atividade>();
            tramite.Motivos = new List<app.Motivo>();
            tramite.Status = new List<app.Status>();

            tramite.Atividades.Add(selectedAtividade);
            tramite.Motivos.Add(selectedMotivo);
            tramite.Status.Add(selectedStatus);
            tramite.Descricao = lblDescValor.Text;
            tramite.ChamadoId = int.Parse(_chamadoId);
            tramite.TipoDescricao = "txt";
            tramite.Data = dpDataValor.Date;
            tramite.HoraInicio = DateTime.Now + tpInicio.Time;
            tramite.HoraFim = DateTime.Now + tpFim.Time;

            var response = await ApiWriter.SendDataToApi<Tramite>("/api/v1/mob/tramite", tramite);

            if (response == HttpStatusCode.OK)
            {
                await DisplayAlert("Sucesso", "Trâmite criado com sucesso", "Sair");

                await Navigation.PopAsync();
            }
        }
        private async void GetData()
        {
            var tarefa = await ApiReader.GetDataFromApi<Tramite>("/api/v1/mob/tramite/" + _chamadoId);
            status = new List<Status>(tarefa.Status);
            motivos = new List<app.Motivo>(tarefa.Motivos);
            atividades = new List<Atividade>(tarefa.Atividades);

            pickerStatus.ItemsSource = status;
            pickerStatus.SelectedItem = selectedStatus;

            pickerAtividades.ItemsSource = atividades;
            pickerAtividades.SelectedItem = selectedAtividade;

            pickerMotivos.ItemsSource = motivos;
            pickerMotivos.SelectedItem = selectedMotivo;


        }
    }
}
