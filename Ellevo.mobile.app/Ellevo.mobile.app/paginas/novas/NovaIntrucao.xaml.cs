using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.paginas.itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.novas
{
    public partial class NovaInstrucao : ContentPage
    {
        public NovaInstrucao()
        {
            InitializeComponent();
        }
        private async void OnTextChanged(object sender, EventArgs e)
        {
            var destinatarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/DestinatariosGrupos");

            Destinatarios destinatariosPage = new itens.Destinatarios(destinatarios);
            await Navigation.PushModalAsync(destinatariosPage);

            textDestinatarios.Text = "";
        }
        private async void OnConfirClicked(object sender, EventArgs e)
        {
            Instrucao i = new Instrucao();
            i.Descricao = textInstrucao.Text;

            await ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao", i);
        }

        
    }
}
