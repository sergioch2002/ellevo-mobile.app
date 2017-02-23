using Ellevo.mobile.app.controles;
using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.itens
{
    public partial class Aprovacao : ContentPage
    {
        Motivo selectedMotivo { get; set; }
        int aprovacaoStatus { get; set; }
        string chamadoId { get; set; }
        string tarefaId { get; set; }
        public Aprovacao(Chamado chamado, Tarefa tarefa)
        {
            InitializeComponent();

            if (chamado != null)
            {
                this.Title = "Aprovação do Chamado " + chamado.ChamadoId.ToString();
                lblTituloValor.Text = chamado.Titulo;
                chamadoId = chamado.ChamadoId.ToString();
                GetData(chamado, null);
            }
            else if (tarefa != null)
            {
                this.Title = "Aprovação da Tarefa " + tarefa.TarefaId.ToString();
                lblTituloValor.Text = tarefa.Titulo;
                tarefaId = tarefa.TarefaId.ToString();
                GetData(null, tarefa);
            }
            else
                return;
        }

        private async void GetData(Chamado chamado, Tarefa tarefa)
        {
            List<Motivo> motivos = new List<app.Motivo>();
            if (chamado != null)
            {
                motivos = await ApiReader.GetDataFromApi<List<Motivo>>("/api/v1/mob/aprovacao/motivos/chamado/" + chamado.ChamadoId);
                
            }
            else
            {
                motivos = await ApiReader.GetDataFromApi<List<Motivo>>("/api/v1/mob/aprovacao/motivos/tarefa/" + tarefa.TarefaId);
            }

            bpMotivo.ItemsSource = motivos;

            List<string> status = new List<string> { "Aprova", "Reprova" };
            foreach (var item in status)
            {
                pickerStatus.Items.Add(item);
            }

            pickerStatus.SelectedIndexChanged += (statusSender, args) =>
            {
                aprovacaoStatus = (string)pickerStatus.Items[pickerStatus.SelectedIndex] == "Aprova" ? 1 : 0;
            };
        }
        private void OnMotivoSelected(object sender, EventArgs a)
        {
            var motivo = (BindablePicker)sender;

            if (bpMotivo.SelectedIndex > -1)
                selectedMotivo = (Motivo)motivo.SelectedItem;
        }
        private async void OnConfirmClicked(object sender, EventArgs a)
        {

            if (string.IsNullOrEmpty(tarefaId))
            {
                var response = await ApiWriter.SendDataToApi<string>("/api/v1/mob/aprovacao/Chamado/" + chamadoId + "/" + selectedMotivo.Id + "/" + aprovacaoStatus, entryComentarios.Text);
                if (response == HttpStatusCode.OK)
                {
                    await DisplayAlert("Sucesso", "Chamado aprovado/reprovado com sucesso", "Sair");

                    await Navigation.PopAsync();
                }
            }
            else
            {
                var response = await ApiWriter.SendDataToApi<string>("/api/v1/mob/aprovacao/Tarefa/" + tarefaId + "/" + selectedMotivo.Id + "/" + aprovacaoStatus, entryComentarios.Text);
                if (response == HttpStatusCode.OK)
                {
                    await DisplayAlert("Sucesso", "Tarefa aprovada/reprovada com sucesso", "Sair");

                    await Navigation.PopAsync();
                }
            }
        }
    }
}
