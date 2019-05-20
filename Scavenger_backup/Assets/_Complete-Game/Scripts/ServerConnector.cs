using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public sealed class ServerConnector 
{
   
        private static HttpClient client = new HttpClient();


        static ServerConnector()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://finestroguelike.azurewebsites.net/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static async Task<PlayerDTO> GetPlayerByName(string name)
        {
            PlayerDTO player = null;
            HttpResponseMessage response = await client.GetAsync(string.Concat("players/byName?name=", name));
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<PlayerDTO>();
            }
            return player;
        }
        public static async Task<List<PlayerDTO>> GetPlayers()
        {
            List<PlayerDTO> players = null;
            HttpResponseMessage response = await client.GetAsync("players");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<List<PlayerDTO>>();
            }
            return players;
        }
        public static async Task<List<PlayerDTO>> GetPlayersByLevel()
        {
            List<PlayerDTO> players = null;
            HttpResponseMessage response = await client.GetAsync("players/rankingByLevel");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<List<PlayerDTO>>();
            }
            return players;
        }
        public static async Task<List<PlayerDTO>> GetPlayersByTime()
        {
            List<PlayerDTO> players = null;
            HttpResponseMessage response = await client.GetAsync("players/rankingByTime");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<List<PlayerDTO>>();
            }
            return players;
        }

        public static async Task<bool> CheckIfUserExists(string name)
        {
            bool exists = false;
            string path = string.Concat("players/exists?name=", name);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                exists = await response.Content.ReadAsAsync<bool>();
            }
            return exists;
        }

        public static async Task<bool> CreateUser(string name, string password)
        {
            string body = string.Concat(name, ";;;", password);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "players", body);

            return response.StatusCode == HttpStatusCode.Created ? true : false;
        }

        public static async Task<bool> Login(string name, string password)
        {
            string body = string.Concat(name, ";;;", password);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "players/login", body);

            return response.Content.ReadAsStringAsync().Result.Equals("true") ? true : false;
            //   return response.StatusCode == HttpStatusCode.Created ? true : false;
        }

        public static async Task<bool> DeleteUser(string name)
        {
            string concated = string.Concat("\"", name, "\"");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://finestroguelike.azurewebsites.net/api/players"),
                Content = new StringContent(concated, Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode ? true : false;
        }

        public static async Task<PlayerDTO> UpdateAfterGame(GameInfoDTO gameInfo)
        {
        PlayerDTO player = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "players/gameInfo", gameInfo);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<PlayerDTO>();
            }

            return player;
        }
    
}
