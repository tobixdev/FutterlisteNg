using System;
using System.Net.Http;
using System.Threading.Tasks;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Web.Configuration;
using log4net;
using Microsoft.AspNetCore.Components;

namespace FutterlisteNg.Web.Services
{
    public class UserClientService : IUserClientService
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(UserClientService));
        
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserClientService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        private string UserBaseUrl => _configuration.ApiBaseUrl + "user";

        public async Task<User[]> All()
        {
            var result = await _httpClient.GetJsonAsync<User[]>(UserBaseUrl);
            s_log.Info($"Found {result.Length} users.");
            return result;
        }
    }
}