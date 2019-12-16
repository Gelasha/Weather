using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Weather.Dtos;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ISearchHistoryRepository _searchHistoryRepository;

        private readonly ILogger<WeatherForecastController> _logger;       

        public WeatherForecastController(IMapper mapper, ILogger<WeatherForecastController> logger, ISearchHistoryRepository searchHistoryRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _searchHistoryRepository = searchHistoryRepository;
        }

        private static readonly string[] Cities = new[]
        {
            "Leipzig", "Berlin", "Munich",  "Leverkusen", "Hamburg"
        };

        [HttpGet("[action]")]
        public async Task<IActionResult> Forecast()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var list = new List<Object>();

                    client.BaseAddress = new Uri("http://api.openweathermap.org");

                    foreach (var City in Cities)
                    {                      
                        var response = await client.GetAsync($"/data/2.5/forecast?q={City}&appid=fcadd28326c90c3262054e0e6ca599cd");                        
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        var rawWeather = JsonConvert.DeserializeObject<DefaultCityWeather>(stringResult);

                        var result = from l in rawWeather.list
                                 group l by l.Dt_txt.Substring(0, 10) into tt
                                 select new Fields
                                 {
                                     Date = tt.Key,
                                     AverageSpead = Math.Round(tt.Average(x => Convert.ToDouble(x.Wind.Speed)),2),                                     
                                     AverageTemp = Math.Round(tt.Average(x => Convert.ToDouble(x.Main.Temp)),2)
                                 };

                        var dto = new DefaultCityWeatherDto
                        {
                            City = City,
                            Fields = result
                        };

                        list.Add(dto);

                        response.Dispose();
                    }                    

                    return Ok(list);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        [HttpGet("[action]/{param}")]
        public async Task<IActionResult> Weather(string param)
        {
            using (var client = new HttpClient())
            {
                try
                {                  
                    client.BaseAddress = new Uri("http://api.openweathermap.org");

                    if (!String.IsNullOrEmpty(param))
                    {
                        var response = await client.GetAsync($"/data/2.5/weather?zip={param},de&appid=fcadd28326c90c3262054e0e6ca599cd");                        

                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            response = await client.GetAsync($"/data/2.5/weather?q={param}&appid=fcadd28326c90c3262054e0e6ca599cd");                            
                        }                        

                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        var rawWeather = JsonConvert.DeserializeObject<WeatherByCity>(stringResult);
                                                
                        var dto = new WeatherByCityDto
                        {
                            City = rawWeather.Name,                            
                            Spead = rawWeather.Wind.Speed,
                            Humadity = rawWeather.Main.Humidity,
                            Temp = rawWeather.Main.Temp
                        };

                        SearchHistory history = new SearchHistory()
                        {
                            City = rawWeather.Name,                            
                            Temp = Convert.ToDouble(rawWeather.Main.Temp)
                        };

                        await _searchHistoryRepository.AddAsync(history);

                        return Ok(dto);
                    }
                    else
                        return Ok("City or zip code is not valid!");
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistory()
        {
            var history = await _searchHistoryRepository.GetAllAsync();
            var userViewModel = _mapper.Map<IEnumerable<SearchHistoryDto>>(history);

            return userViewModel;
        }
    }


}
