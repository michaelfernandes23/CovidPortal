using System;
using System.Collections.Generic;
using System.Text;

namespace CovidPortal.Domain.DTO
{
    public class CovidDataResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string NewConfirmed { get; set; }
        public string TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
    }
}
