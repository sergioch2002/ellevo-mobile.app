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
    public partial class NovaProvidencia : ContentPage
    {
        private string _tarefaId;

        List<Status> status { get; set; }
        List<Atividade> atividades { get; set; }
        List<Motivo> motivos { get; set; }

        Status selectedStatus { get; set; }
        Atividade selectedAtividade { get; set; }
        Motivo selectedMotivo { get; set; }

        public NovaProvidencia(string tarefaId)
        {
            this._tarefaId = tarefaId;
            GetData();
            InitializeComponent();
            
            this.Title = "Nova Providência da tarefa " + tarefaId;
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
            Providencia providencia = new app.Providencia();
            providencia.Atividades = new List<Atividade>();
            providencia.Motivos = new List<Motivo>();
            providencia.Status = new List<Status>();

            providencia.Atividades.Add(selectedAtividade);
            providencia.Motivos.Add(selectedMotivo);
            providencia.Status.Add(selectedStatus);
            providencia.Descricao = lblDescValor.Text;
            providencia.TarefaId = int.Parse(_tarefaId);
            providencia.TipoDescricao = "txt";
            providencia.Data = dpDataValor.Date;
            providencia.HoraInicio = DateTime.Now + tpInicio.Time;
            providencia.HoraFim = DateTime.Now + tpFim.Time;

            var response = await ApiWriter.SendDataToApi<Providencia>("/api/v1/mob/providencia", providencia);
            if (response == HttpStatusCode.OK)
            {
                await DisplayAlert("Sucesso", "Providência criada com sucesso", "Sair");

                await Navigation.PopAsync();
            }
        }
        private async void GetData()
        {
            var tarefa = await ApiReader.GetDataFromApi<Providencia>("/api/v1/mob/providencia/" + _tarefaId);
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
