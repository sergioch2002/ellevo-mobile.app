using Ellevo.mobile.app.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class TotalsPageSquares : ContentPage
    {
        TelaInicial telaInicial;
        public TotalsPageSquares()
        {
            telaInicial = new objects.TelaInicial();

            InitializeComponent();
            GetTotals(Sessao.UrlBase);
        }

        public async void GetTotals(string endpoint)
        {
            if (endpoint[endpoint.Length - 1] == '/')
                endpoint = endpoint.Substring(0, endpoint.Length - 1);

            endpoint += "/api/v1/mob/inicio/resumo";
            HttpClientHandler handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };

            string reasonPhrase = "";
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Sessao.Token.Token_type, Sessao.Token.Access_token);

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    telaInicial = JsonConvert.DeserializeObject<TelaInicial>(result);
                    //Atribuição de valores
                    lblInstrucaoValor.Text = telaInicial.InstrucaoTotal.ToString();
                    lblAcompanhamentoValor.Text = telaInicial.AcompanhamentosTotal.ToString();
                    lblChamadoValor.Text = telaInicial.ChamadosTotal.ToString();
                    lblTramiteValor.Text = telaInicial.TramitesTotal.ToString();
                    lblTarefaValor.Text = telaInicial.TarefasTotal.ToString();
                    lblProvidenciaValor.Text = telaInicial.ProvidenciasTotal.ToString();
                    lblAprovacaoValor.Text = telaInicial.ProcessoDeAprovacaoTotal.ToString();
                    //await Navigation.PushAsync(new LoginPage(conf));
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                }
            }
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LoginPage(null), this);
            await Navigation.PopAsync();
        }

        async void OnTappedInstrucao(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TotalsPageSquares());
        }
    }
}
