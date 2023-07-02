using Model;
using Model.DTO;
using Model.MODEL;
using System;
using System.Diagnostics;

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

            if(!response.IsSuccessStatusCode)
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
    }
}