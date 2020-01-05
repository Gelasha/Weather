using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Dtos
{
    public class DefaultCityWeatherDto
    {
        public IEnumerable<BasicFieldsByCity> BasicFieldsByCity { get; set; }
        public IEnumerable<string> Date { get; set; }
    }

    public class BasicFieldsByCity
    {
        public string City { get; set; }
        public IEnumerable<BasicFields> Fields { get; set; }
    }

    public class BasicFields
    {
        public double AverageSpead { get; set; }
        public double AverageHumadity { get; set; }
        public double AverageTemp { get; set; }
        public DateTime DateFromApi { get; set; }
    }
}
