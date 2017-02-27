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
            DestinatariosUsuarios id = new DestinatariosUsuarios();
            id.UsuariosAdicionados += HandleUsuarioAdiconado;
        }
        private async void OnTextChanged(object sender, EventArgs e)
        {
            

            Destinatarios destinatariosPage = new itens.Destinatarios();
            await destinatariosPage.GetData();
            await Navigation.PushModalAsync(destinatariosPage);

            //var t = Task.Factory.StartNew(() => destinatariosPage.GetData().Wait());
            
            //t.Start();
            //Task.WaitAll(t);


            //var u = destinatariosPage.GetData(grupo).Result;

                textDestinatarios.Text = string.Empty;

           

        }

        private void HandleUsuarioAdiconado(object sender, EventArgs e)
        {
            InstrucaoDestinatario id;
            //textDestinatarios.Text = (InstrucaoDestinatario)e.
        }
        private async void OnConfirClicked(object sender, EventArgs e)
        {
            Instrucao i = new Instrucao();
            i.Descricao = textInstrucao.Text;

            await ApiWriter.SendDataToApi<Instrucao>("/api/v1/mob/instrucao", i);
        }

        
    }
}
