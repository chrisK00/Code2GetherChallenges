using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionItems
{
    public class temp
    {

        public class Rootobject
        {
            public Query query { get; set; }
            public Results results { get; set; }
        }

        public class Query
        {
            public string[] codes { get; set; }
            public object country { get; set; }
        }

        public class Results
        {
            public _42241[] _42241 { get; set; }
        }

        public class _42241
        {
            public string postal_code { get; set; }
            public string country_code { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string city_en { get; set; }
            public string state_en { get; set; }
            public string state_code { get; set; }
            public string province { get; set; }
            public string province_code { get; set; }
        }

    }
}
