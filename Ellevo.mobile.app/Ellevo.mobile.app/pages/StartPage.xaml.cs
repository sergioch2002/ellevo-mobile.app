using Ellevo.mobile.app.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }


        private void OnConfirmClicked(object sender, EventArgs args)
        {
            var animation = new Animation(callback: d => btnConfirma.Rotation = d,
                                  start: btnConfirma.Rotation,
                                  end: btnConfirma.Rotation + 360,
                                  easing: Easing.SpringOut);
            animation.Commit(btnConfirma, "Loop", length: 400);

            if (string.IsNullOrEmpty(URL.Text))
                return;

            if (ValidateUrl(URL.Text.TrimStart().TrimEnd().Trim()))
            //if (true)
            {
                Sessao.UrlBase = URL.Text;
                //Sessao.UrlBase = "http://desenv.0800net.com.br/mobile";
                GetConfiguration(URL.Text.TrimStart().TrimEnd().Trim());
            }
        }

        private void OnEntryCompleted(object sender, EventArgs args)
        {
            Entry entry = (Entry)sender;

            if (ValidateUrl(entry.Text.TrimStart().TrimEnd().Trim()))
            {
                Sessao.UrlBase = URL.Text;
                //Sessao.UrlBase = "http://desenv.0800net.com.br/mobile";
                GetConfiguration(URL.Text);
            }

        }

        private bool ValidateUrl(string url)
        {
            Regex httpAndWWW = new Regex(@"/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[\-;:&=\+\$,\w]+@)?[A-Za-z0-9\.\-]+|(?:www\.|[\-;:&=\+\$,\w]+@)[A-Za-z0-9\.\-]+)((?:\/[\+~%\/\.\w\-]*)?\??(?:[\-\+=&;%@\.\w]*)#?(?:[\.\!\/\\\w]*))?)");
            Match matchHttpAndWWW = httpAndWWW.Match(url);

            Regex onlyWWW = new Regex(@"(?:www\.|[\-;:&=\+\$,\w]+@)");
            Match matchonlyWWW = onlyWWW.Match(url);

            Regex withoutWWW = new Regex(@"([A-Za-z]{3,9}:(?:\/\/)?)");
            Match matchwithoutWWW = withoutWWW.Match(url);

            if (!matchHttpAndWWW.Success && !matchonlyWWW.Success && !matchwithoutWWW.Success)
                return false;

            return true;
        }
        public async void GetConfiguration(string endpoint)
        {
            //endpoint = "http://desenv.0800net.com.br/mobile";
            if (endpoint[endpoint.Length - 1] == '/')
                endpoint = endpoint.Substring(0, endpoint.Length - 1);

            endpoint += "/api/v1/mob/configuracao";
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("amx", "4d53bce03ec34c0a911182d4c228ee6c:A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var conf = JsonConvert.DeserializeObject<Configuracoes>(result);

                    await Navigation.PushAsync(new LoginPage(conf));
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                }
            }
        }


    }
}
