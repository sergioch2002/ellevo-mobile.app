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
    public partial class LoginPage : ContentPage
    {
        Configuracoes configuracoes;
        Picker picker;
        public LoginPage(Configuracoes config)
        {
            configuracoes = config;
            
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            SetViews();
            picker = new Picker
            {
                Title = "Dominios",
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            foreach (var dominio in configuracoes.ListaDominios)
            {
                picker.Items.Add(dominio.Nome);
            }

            boxLogin.Children.Add(picker);

        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
        private void OnUserEntryCompleted(object sender, EventArgs args)
        {
            txtSenha.Focus();

        }
        private void OnPassEntryCompleted(object sender, EventArgs args)
        {
            picker.Focus();
        }
        private void SetViews()
        {
            lblDominio.Text = configuracoes.NomeSite;
        }

        private void OnEnterClicked(object sender, EventArgs args)
        {
            var animation = new Animation(callback: d => btnEntrar.Rotation = d,
                                  start: btnEntrar.Rotation,
                                  end: btnEntrar.Rotation + 360,
                                  easing: Easing.SpringOut);
            animation.Commit(btnEntrar, "Loop", length: 400);

            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {
                GetToken(Sessao.UrlBase);
            }
            else
                return;
        }

        public async void GetToken(string endpoint)
        {
            //endpoint = "http://desenv.0800net.com.br/mobile";
            if (endpoint[endpoint.Length - 1] == '/')
                endpoint = endpoint.Substring(0, endpoint.Length - 1);

            endpoint += "/api/seguranca/token";
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

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("username", txtUsuario.Text));
                //postData.Add(new KeyValuePair<string, string>("username", "davila0800net"));
                postData.Add(new KeyValuePair<string, string>("password", txtSenha.Text));
                //postData.Add(new KeyValuePair<string, string>("password", "123456"));
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                postData.Add(new KeyValuePair<string, string>("dominio", configuracoes.ListaDominios[picker.SelectedIndex].Id.ToString()));
                //postData.Add(new KeyValuePair<string, string>("dominio", "0"));
                postData.Add(new KeyValuePair<string, string>("client_id", "123"));
                
                HttpContent content = new FormUrlEncodedContent(postData);
                // Post
                var response = client.PostAsync(endpoint, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var conf = JsonConvert.DeserializeObject<Token>(result);

                    Sessao.Token = conf;

                    await Navigation.PushAsync(new Springboard());
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                }
            }
        }
    }
}
