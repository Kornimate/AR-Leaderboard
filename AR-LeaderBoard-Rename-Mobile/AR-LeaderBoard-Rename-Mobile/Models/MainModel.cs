using AR_LeaderBoard_Rename_Mobile.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AR_LeaderBoard_Rename_Mobile.Models
{
    public class MainModel : IMainModel
    {
        private readonly IConfiguration _config;

        public event EventHandler<bool>? RequestSent;
        public event EventHandler<int>? EntriesReceivedAsString;
        public MainModel(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendRequest(string baseAddress, string oldTeamName, string newTeamName)
        {
            var client = GetHttpClient(baseAddress, true);

            var jsonBody = JsonSerializer.Serialize(new
            {
                OldName = oldTeamName,
                NewName = newTeamName
            });

            var response = await client.PostAsync("/api/leaderboard/rename", new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            RequestSent?.Invoke(this, response.IsSuccessStatusCode);
        }

        public async Task GetNumberOfEntries(string baseAddress)
        {
            var client = GetHttpClient(baseAddress, true);

            var response = await client.GetAsync("/api/leaderboard/count");

            EntriesReceivedAsString?.Invoke(this, int.Parse(await response.Content.ReadAsStringAsync()));
        }

        private HttpClient GetHttpClient(string baseAddress, bool addApiKey)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _config["AuthorizationHeaderValue"]);

            if (addApiKey)
            {
                client.DefaultRequestHeaders.Add("XApiKey", _config["XApiKey"]);
            }
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
