using Microsoft.VisualStudio.Services.OAuth;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WowArenaLadderStats
{
    public static class ApiClient
    {
        private static RestClient client;
        public static RestClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new RestClient(Constants.API_BASE_URL);
                    client.AddDefaultHeader("cache-control", "no-cache");
                    client.AddDefaultHeader("content-type", "application/x-www-form-urlencoded");
                    client.AddDefaultHeader("authorization", $"Bearer {GetAccessToken()}");
                }
                return client;
            }
        }
        public static string GetAccessToken()
        {
            var client = new RestClient(Constants.API_AUTH_URL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={Constants.CLIENT_ID}&client_secret={Constants.CLIENT_SECRET}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content);

            return tokenResponse.AccessToken;
        }

        public static string Get(string path)
        {
            var request = new RestRequest(path, Method.GET);
            IRestResponse response = Client.Execute(request);
            return response.Content;
        }
    }
}
