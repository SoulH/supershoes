using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace App.Utils
{
    public static class DefaultApiClient
    {
        static DefaultApiClient()
        {
            Client = new HttpClient();
            var bytes = ASCIIEncoding.ASCII.GetBytes("my_user:my_password");
            var value = Convert.ToBase64String(bytes);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", value);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static HttpClient Client { get; private set; }
    }
}