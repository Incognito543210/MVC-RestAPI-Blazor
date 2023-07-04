using Model;
using Model.DTO;
using Model.MODEL;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace View.Data
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _log;

        public UserService(HttpClient httpClient, ILogger<UserService> log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<UserDto>("/api/User/" + id))!;
        }

        public async Task<string> CreateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync<UserDto>("/api/User", user);
            await LogRequest(response);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return errorMessage;
            }
            else
                return null;
        }

        private async Task LogRequest(HttpResponseMessage response)
        {
            var url = response.RequestMessage.RequestUri;

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                _log.LogWarning(url + " Error: " + errorMessage);
            }
            else
            {
                var str = await response.RequestMessage!.Content!.ReadAsStringAsync();
                _log.LogWarning(url + " " + str);
            }
        }

        public async Task<string> LoginAsync(string login, string password)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SessionDto>("/api/User/" + login + "," + password);
                var accessToken = response.AccessToken;
                var idk = ParseClaimsFromJwt(accessToken);

                return accessToken;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}