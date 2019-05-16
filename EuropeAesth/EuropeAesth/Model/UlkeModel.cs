using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class Country
    {
        public string id { get; set; }
        public string sortname { get; set; }
        public string name { get; set; }
    }

    public class States
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country_id { get; set; }
    }

    public class CountryResponse
    {
        public Country[] countries { get; set; }
    }

    public class StatesResponse
    {
        public States[] states { get; set; }
    }

}
