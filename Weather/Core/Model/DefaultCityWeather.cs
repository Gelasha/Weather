using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Dtos
{
    public class DefaultCityWeather
    {
        public IEnumerable<OpenWeatherResponse> list { get; set; }
        public City City { get; set; }
    }

    public class OpenWeatherResponse
    {        

        public Main Main { get; set; }

        public Wind Wind { get; set; }      

        public string Dt_txt { get; set; }
    }

    public class Main
    {
        public string Temp { get; set; }
        public string Humidity { get; set; }
    }

    public class Wind
    {
        public string Speed { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
    }
}
