using System;
using System.Collections.Generic;

namespace OrdersAPI.ResultsModel
{
    public class FiltersResult
    {
        public List<string> OrderNumbers { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<int> ProviderIds { get; set; }
        public List<string> NameItems { get; set; }
        public List<string> UnitItems { get; set; }
        public List<string> ProviderNames { get; set; }
    }
}