using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Ellevo.mobile.app
{
    public partial class Springboard : ContentPage
    {
        TelaInicial telaInicial;
        public Springboard()
        {
            telaInicial = new TelaInicial();
            InitializeComponent();
            SizeChanged += OnSizeChanged;

            GetTotals(Sessao.UrlBase);

        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetTotals(Sessao.UrlBase);
        }
        public async void GetTotals(string endpoint)
        {
            if (endpoint[endpoint.Length - 1] == '/')
                endpoint = endpoint.Substring(0, endpoint.Length - 1);
            waitActivityIndicator.IsRunning = true;
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
                waitActivityIndicator.IsRunning = false;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    telaInicial = JsonConvert.DeserializeObject<TelaInicial>(result);
                    //Atribuição de valores
                    Instrucao.Valor = telaInicial.InstrucaoTotal.ToString() + "    ";
                    Acompanhamento.Valor = telaInicial.AcompanhamentosTotal.ToString() + "    ";
                    Chamado.Valor = telaInicial.ChamadosTotal.ToString() + "    ";
                    Tramite.Valor = telaInicial.TramitesTotal.ToString() + "    ";
                    Tarefa.Valor = telaInicial.TarefasTotal.ToString() + "    ";
                    Providencia.Valor = telaInicial.ProvidenciasTotal.ToString() + "    ";
                    Aprovacao.Valor = telaInicial.ProcessoDeAprovacaoTotal.ToString() + "    ";
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                }
            }
        }
        async private void AprovacaoButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AprovacoesInicial());
        }
        async private void InstrucaoButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ListaInstrucoes(telaInicial.InstrucaoTotal));
        }
        async private void AcompanhamentoButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ListaAcompanhamentos(telaInicial.AcompanhamentosTotal));
        }
        async private void ChamadoButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ListaChamados(telaInicial.ChamadosTotal));
        }
        async private void TramiteButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ListaTramites(telaInicial.TramitesTotal));
        }
        async private void TarefaButtonTapped(object sender, EventArgs args)
        {
            
            await Navigation.PushAsync(new ListaTarefas(telaInicial.TarefasTotal));
            waitActivityIndicator.IsRunning = false;
        }
        async private void ProvidenciaButtonTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ListaProvidencias(telaInicial.ProvidenciasTotal));
        }
    }
}
