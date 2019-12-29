using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class SearchHistory
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public double Humadity { get; set; }
        public double Temp { get; set; }
    }
}
