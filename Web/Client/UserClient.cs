using System;
using System.Net.Http;
using System.Threading.Tasks;
using FutterlisteNg.Shared;
using FutterlisteNg.Web.Configuration;
using log4net;
using Microsoft.AspNetCore.Components;

namespace FutterlisteNg.Web.Client
{
    public class UserClient : IUserClient
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(UserClient));
        
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        private string UserBaseUrl => _configuration.ApiBaseUrl + "user";

        public async Task<UserViewModel[]> All()
        {
            var result = await _httpClient.GetJsonAsync<UserViewModel[]>(UserBaseUrl);
            s_log.Info($"Found {result.Length} users.");
            return result;
        }
    }
}