using Model;
using Model.DTO;
using Model.MODEL;
using System.Diagnostics;

namespace View.Data
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
    }
}
