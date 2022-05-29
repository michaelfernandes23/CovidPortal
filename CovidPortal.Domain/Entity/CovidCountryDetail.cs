using System.ComponentModel.DataAnnotations.Schema;

namespace CovidPortal.Domain.Entity
{
    [Table(nameof(CovidCountryDetail))]
    public class CovidCountryDetail : EntityBase
    {
        [Column(nameof(Name), TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(nameof(CountryCode), TypeName = "varchar(10)")]
        public string CountryCode { get; set; }
        [Column(nameof(NewConfirmed), TypeName = "int")]
        public int NewConfirmed { get; set; }
        [Column(nameof(TotalConfirmed), TypeName = "int")]
        public int TotalConfirmed { get; set; }
        [Column(nameof(NewDeaths), TypeName = "int")]
        public int NewDeaths { get; set; }
        [Column(nameof(TotalDeaths), TypeName = "int")]
        public int TotalDeaths { get; set; }
        [Column(nameof(NewRecovered), TypeName = "int")]
        public int NewRecovered { get; set; }
        [Column(nameof(TotalRecovered), TypeName = "int")]
        public int TotalRecovered { get; set; }
    }
}
