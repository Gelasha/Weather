using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Dtos;
using Weather.Model;

namespace Weather.Mapping
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<SearchHistory, SearchHistoryDto>();
        }
    }
}
