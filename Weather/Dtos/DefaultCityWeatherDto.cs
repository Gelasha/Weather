using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Dtos
{
    public class DefaultCityWeatherDto
    {
        public string City { get; set; }
        public IEnumerable<Fields> Fields { get; set; }
    }

    public class Fields
    {
        public double AverageSpead { get; set; }
        public double AverageHumadity { get; set; }
        public double AverageTemp { get; set; }
        public Object Date { get; set; }
    }
}
