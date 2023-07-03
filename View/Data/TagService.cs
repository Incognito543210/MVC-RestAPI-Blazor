using Model.DTO;

namespace View.Data
{
    public class TagService
    {
        private readonly HttpClient _httpClient;

        public TagService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TagDto>> GetTagsAsync()
        {
            return (await _httpClient.GetFromJsonAsync<List<TagDto>>("/api/Tag"))!;
        }
    }
}
