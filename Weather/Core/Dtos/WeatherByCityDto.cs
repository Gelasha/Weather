using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Dtos
{
    public class WeatherByCityDto
    {
        public string City { get; set; }
        public string Spead { get; set; }
        public string Humadity { get; set; }
        public string Temp { get; set; }  
        public Object Fields { get; set; }
    }
}
