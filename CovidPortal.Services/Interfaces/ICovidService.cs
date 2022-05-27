using CovidPortal.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidPortal.Services.Interfaces
{
    public interface ICovidService
    {
        /// <summary>
        /// Get all covid data
        /// </summary>
        /// <returns></returns>
        Task<List<CovidData>> GetAllCovidData();

        /// <summary>
        /// Get covid data based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CovidData> GetCovidDataById(string id);

        /// <summary>
        /// Create a new covid data
        /// </summary>
        /// <param name="covidData"></param>
        /// <returns></returns>
        Task<CovidData> SaveCovidData(CovidData covidData);

        /// <summary>
        /// Update a covid data based on id
        /// </summary>
        /// <param name="covidData"></param>
        /// <returns></returns>
        Task<CovidData> UpdateCovidData(CovidData covidData);

        /// <summary>
        /// Delete a covid data based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCovidData(string id);
    }
}
