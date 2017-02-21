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
    public partial class NovaIntrucao : ContentPage
    {
        public NovaIntrucao()
        {
            InitializeComponent();
        }

        private void OnConfirClicked(object sender, EventArgs e)
        {
            Instrucao i = new Instrucao();
            i.Descricao = textInstrucao.Text;
            i.Destinatarios = textDestinatarios.Text.Split(';').Select(x => new InstrucaoDestinatario { InstrucaoId = 0, Nome = x, UsuarioId = 0 }).ToList();

            ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao", i);
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            //var destinatarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/DestinatariosGrupos");

            await Navigation.PushModalAsync(new Destinatarios(), false);
        }
    }
}
