using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidPortal.Services.Interfaces
{
    public interface ICovidService
    {
        Task<object> GetCovidCountryData();
    }
}
