using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Weather.Configuration;
using Weather.Dtos;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IConfiguration _configuration;

        private readonly IMapper _mapper;

        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public WeatherForecastController(IMapper mapper, ISearchHistoryRepository searchHistoryRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;            
            _searchHistoryRepository = searchHistoryRepository;            
        }

        public Config config 
        {
            get
            {
                return new Config(_configuration);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Forecast()
        {            
                try
                {
                    var list = new List<Object>();                    

                    foreach (var City in config.Cities)
                    {
                        string stringResult = await config.GetDataByCity(City);
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
                    }                    

                    return Ok(list);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
        }

        [HttpGet("[action]/{param}")]
        public async Task<IActionResult> Weather(string param)
        {            
                try
                {                   
                    if (!String.IsNullOrEmpty(param))
                    {
                    string stringResult = await config.GetDataByZipCodeOrCity(param);
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

        [HttpGet("[action]")]
        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistory()
        {
            var history = await _searchHistoryRepository.GetAllAsync();
            var userViewModel = _mapper.Map<IEnumerable<SearchHistoryDto>>(history);

            return userViewModel;
        }
    }


}
