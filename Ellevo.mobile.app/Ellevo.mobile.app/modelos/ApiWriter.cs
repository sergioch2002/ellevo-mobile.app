using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.objects
{
    public class ApiWriter
    {
        async public static Task<HttpStatusCode> SendDataToApi<T>(string endpoint, T data)
        {
            endpoint = endpoint.Insert(0, Sessao.UrlBase);
            StringContent queryString = null;

            if (data != null)
            {
                queryString = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }
            

            HttpClientHandler handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Sessao.Token.Token_type, Sessao.Token.Access_token);

                var response = await client.PostAsync(endpoint, queryString);

                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
        }
    }
}
