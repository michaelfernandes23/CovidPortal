using AutoMapper;
using CovidPortal.Domain.DTO;
using CovidPortal.Domain.Entity;
using CovidPortal.Services.Interfaces;
using CovidPortal.SQL.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidPortal.Services
{
    public class CovidService : ICovidService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CovidService> _logger;

        private readonly ICovidCountryDetailSqlRepository _covidCountryDetailSqlRepository;


        public CovidService(IUnitOfWork unitOfWork, 
                            IMapper mapper,
                            ICovidCountryDetailSqlRepository covidCountryDetailSqlRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _covidCountryDetailSqlRepository = covidCountryDetailSqlRepository;
        }

        public async Task<List<CovidData>> GetAllCovidData()
        {
            return _mapper.Map<List<CovidData>>(await _covidCountryDetailSqlRepository.GetAllAsync());
        }

        public async Task<CovidData> GetCovidDataById(string id)
        {
            var covidCountryDetail = await _covidCountryDetailSqlRepository.GetEntityById(id);
            if (covidCountryDetail == null)
                throw new ValidationException("Covid data does not exist");

            return _mapper.Map<CovidData>(covidCountryDetail);
        }

        public async Task<CovidData> SaveCovidData(CovidData covidData)
        {
            CovidCountryDetail tempEntity = await _covidCountryDetailSqlRepository.GetEntity(x => x.CountryCode == covidData.CountryCode);
            if (tempEntity != null)
                throw new ValidationException($"Covid data already exists for the country having country code {covidData.CountryCode}");

            CovidCountryDetail covidCountryDetail = _mapper.Map<CovidCountryDetail>(covidData);
            await _covidCountryDetailSqlRepository.Add(covidCountryDetail);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CovidData>(covidCountryDetail);
        }

        public async Task<CovidData> UpdateCovidData(CovidData covidData)
        {
            CovidCountryDetail covidCountryDetail = await _covidCountryDetailSqlRepository.GetEntityById(covidData.Id);
            if (covidCountryDetail == null)
                throw new ValidationException("Covid data does not exist");

            covidCountryDetail.Name = covidData.Name;
            covidCountryDetail.CountryCode = covidData.CountryCode;
            covidCountryDetail.NewConfirmed = covidData.NewConfirmed;
            covidCountryDetail.TotalConfirmed = covidData.TotalConfirmed;
            covidCountryDetail.NewDeaths = covidData.NewDeaths;
            covidCountryDetail.TotalDeaths = covidData.TotalDeaths;
            covidCountryDetail.NewRecovered = covidData.NewRecovered;
            covidCountryDetail.TotalRecovered = covidData.TotalRecovered;

            await _covidCountryDetailSqlRepository.Update(covidCountryDetail);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CovidData>(covidCountryDetail);
        }

        public async Task DeleteCovidData(string id)
        {
            CovidCountryDetail covidCountryDetail = await _covidCountryDetailSqlRepository.GetEntityById(id);
            if (covidCountryDetail == null)
                throw new ValidationException("Covid data does not exist");

            await _covidCountryDetailSqlRepository.Remove(covidCountryDetail);
            await _unitOfWork.CommitAsync();
        }
    }
}
