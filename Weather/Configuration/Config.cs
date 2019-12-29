using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather.Configuration
{
    public class Config
    {
        private IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiUrl
        {
            get
            {
                return _configuration.GetSection("OpenWeatherMap").GetSection("ApiAddress").Value;
            }
        }

        public string ApiKey
        {
            get
            {
                return _configuration.GetSection("OpenWeatherMap").GetSection("Key").Value;
            }
        }

        public string[] Cities
        {
            get
            {
                return _configuration.GetSection("OpenWeatherMap").GetSection("Cities").Get<string[]>();
            }
        }

        public async Task<string> GetDataByCity(string city)
        {
            var stringResult = String.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"/data/2.5/forecast?q={city}&appid=" + ApiKey);
                stringResult = await response.Content.ReadAsStringAsync();
                response.Dispose();
            }

            return stringResult;
        }

        public async Task<string> GetDataByZipCodeOrCity(string inputParameter)
        {
            var stringResult = String.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"/data/2.5/weather?zip={inputParameter},de&appid=" + ApiKey);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    response = await client.GetAsync($"/data/2.5/weather?q={inputParameter}&appid=" + ApiKey);
                }

                response.EnsureSuccessStatusCode();

                stringResult = await response.Content.ReadAsStringAsync();
            }

            return stringResult;
        }
    }
}
