using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cmta.Clients.Spa.Features.Shared;
using Cmta.Hosts.General.Shared.Location;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Cmta.Clients.Spa.Api.Services
{
    public interface ILocationApiService
    {
        public Task<IEnumerable<Location>> GetLocations(Pagination pagination);
        public Task<IEnumerable<Location>> GetLocations(Pagination pagination, string? query);
    }
    public class LocationApiService : ILocationApiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public LocationApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public Task<IEnumerable<Location>> GetLocations(Pagination pagination)
        {
            return GetLocations(pagination, null);
        }

        public async Task<IEnumerable<Location>> GetLocations(Pagination pagination, string? query)
        {
            string endpoint = $"{_configuration["endpoints:general"]}location";
            var param = new Dictionary<string, string>();
            
            if (query != null)
                param.Add("q",query);

            var requestUri = new Uri(QueryHelpers.AddQueryString(endpoint, param)).ToString();

            return await _httpClient.GetJsonAsync<Location[]>(requestUri);
        }
    }
}