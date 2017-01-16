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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }


        private void OnConfirmClicked(object sender, EventArgs args)
        {
            string res = PostRequestJson("", "");
            Button button = (Button)sender;
            if (button.Text != "Clicado")
                button.Text = res;
            else
                button.Text = "Confirma";
        }

        public string PostRequestJson(string endpoint, string json)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };


            string reasonPhrase = "";
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://desenv.0800net.com.br/mobile/api/v1/mob/configuracao");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("amx", "4d53bce03ec34c0a911182d4c228ee6c:A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");

                // GET
                var response = client.GetAsync(endpoint).Result;
                // Post
                // var response = client.PostAsJsonAsync("http://desenv.0800net.com.br/mobile", obj).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                else
                {
                    reasonPhrase = response.ReasonPhrase;
                    return reasonPhrase;
                    //    if (reasonPhrase.ToUpper() == "UNAUTHORIZED")
                    //    {
                    //        throw new KeyNotFoundException("Not authorized");
                    //    }
                    //    //var apps = response.Content.ReadAsAsync<HttpContent().Result;
                    //    //if (apps.ReturnCode.ToUpper() == "NOTFOUND")
                    //    //{
                    //    //    throw new KeyNotFoundException(apps.Result);
                    //    //}
                    //    //else
                    //    //{
                    //    //    throw new System.Exception(apps.Result);
                    //    //}
                }
            }
        }
    }
}
