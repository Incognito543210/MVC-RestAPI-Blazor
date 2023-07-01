using Model;
using Model.DTO;
using Model.MODEL;
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

        public async Task CreateUserAsync(UserDto user)
        {
            var wynik = await _httpClient.PostAsJsonAsync<UserDto>("/api/User", user);
            await LogRequest(wynik);
        }

        private async Task LogRequest(HttpResponseMessage wynik)
        {
            var str = await wynik.RequestMessage!.Content!.ReadAsStringAsync();
            var url = wynik.RequestMessage.RequestUri;
            _log.LogWarning(url + " przerwa " + str);
        }
    }
}