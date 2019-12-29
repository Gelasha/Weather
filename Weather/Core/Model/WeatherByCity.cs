using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class WeatherByCity
    {
        public string Name { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        
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

}
