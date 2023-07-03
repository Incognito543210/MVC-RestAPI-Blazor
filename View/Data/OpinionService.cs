using Model;
using Model.DTO;
using Model.MODEL;
using System.Diagnostics;
using View.Pages;

namespace View.Data
{
    public class OpinionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpinionService> _log;
        private const int _userId = 1;

        public OpinionService(HttpClient httpClient, ILogger<OpinionService> log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<List<OpinionDto>> GetOpinionsForRecipeAsync(int recipeId)
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<List<OpinionDto>>("/api/Opinion/" + recipeId))!;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<string> AddOpinionToRecipeAsync(UserDto user)
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
    }
}
