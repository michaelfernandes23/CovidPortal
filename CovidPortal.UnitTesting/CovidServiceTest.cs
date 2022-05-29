using AutoMapper;
using CovidPortal.Domain;
using CovidPortal.Domain.Entity;
using CovidPortal.Domain.Interfaces;
using CovidPortal.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace CovidPortal.UnitTesting
{
    public class CovidServiceTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<ILogger<CovidService>> logger;
        private readonly IMapper mapper;
        private readonly CovidService covidService;
        private readonly Mock<ICovidCountryDetailSqlRepository> covidCountryDetailSqlRepository;

        private const string expectedId = "284E2859-73CA-4DBC-94AE-648111F7EBA7";

        public CovidServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            logger = new Mock<ILogger<CovidService>>();

            var myProfile = new Mappings();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(mapperConfiguration);

            covidCountryDetailSqlRepository = new Mock<ICovidCountryDetailSqlRepository>();

            covidService = CreateCovidService();
        }

        [Fact]
        public async Task AddCovidTest()
        {
            covidCountryDetailSqlRepository.Setup(e => e.GetEntity(It.IsAny<Expression<Func<CovidCountryDetail, bool>>>()))
                            .Returns(Task.FromResult((CovidCountryDetail)null));

            CovidData covidData = await CreateCovidDataObject();
            var result = await covidService.SaveCovidData(covidData);
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task AddCovidTestIfNameIsEmpty()
        {
            CovidData covidData = new CovidData();
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => covidService.SaveCovidData(covidData));
        }

        [Fact]
        public async Task UpdateCovidTest()
        {
            CovidData covidData = await CreateCovidDataObject();
            var expectedId = covidData.Id;
            var result = await covidService.UpdateCovidData(covidData);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task DeleteCovidTest()
        {
            var deleteId = expectedId;
            await covidService.DeleteCovidData(expectedId);
            Assert.Equal(deleteId, expectedId);
        }

        [Fact]
        public async Task GetCovidTest()
        {
            var result = await covidService.GetCovidDataById(expectedId);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task GetAllCovidTest()
        {
            var result = await covidService.GetAllCovidData();
            Assert.True(result.Count() > 0);
        }

        #region Private Methods

        private Task<CovidData> CreateCovidDataObject()
        {
            return Task.FromResult(new CovidData() { Id = "284E2859-73CA-4DBC-94AE-648111F7EBA7", CountryCode = "ae", Name = "United Arab Emirates" });
        }

        private Task<CovidCountryDetail> CreateCovidCountryDetailObject()
        {
            return Task.FromResult(new CovidCountryDetail() { Id = "284E2859-73CA-4DBC-94AE-648111F7EBA7", CountryCode = "ae", Name = "United Arab Emirates" });
        }

        private Task<IEnumerable<CovidCountryDetail>> CreateMultipleCovidCountryDetailObjects()
        {
            List<CovidCountryDetail> obj = new List<CovidCountryDetail>()
                { 
                    new CovidCountryDetail() { Id = "284E2859-73CA-4DBC-94AE-648111F7EBA7", CountryCode = "ae", Name = "United Arab Emirates", NewConfirmed = 1 },
                    new CovidCountryDetail() { Id = "9935DD81-0E69-404D-8AAC-13D343F810E7", CountryCode = "qr", Name = "Qatar", NewConfirmed = 3 }
                };

            return Task.FromResult(obj.AsEnumerable());
        }

        private CovidService CreateCovidService()
        {
            unitOfWork.Setup(e => e.CommitAsync()).Returns(Task.FromResult(1));

            covidCountryDetailSqlRepository.Setup(e => e.GetEntity(It.IsAny<Expression<Func<CovidCountryDetail, bool>>>()))
                            .Returns(CreateCovidCountryDetailObject());

            covidCountryDetailSqlRepository.Setup(e => e.GetEntityById(It.IsAny<string>()))
                            .Returns(CreateCovidCountryDetailObject());

            covidCountryDetailSqlRepository.Setup(e => e.Add(It.IsAny<CovidCountryDetail>()))
                            .Returns(CreateCovidCountryDetailObject());

            covidCountryDetailSqlRepository.Setup(e => e.Update(It.IsAny<CovidCountryDetail>()))
                            .Returns(CreateCovidCountryDetailObject());

            covidCountryDetailSqlRepository.Setup(e => e.HasDataAsync(It.IsAny<Expression<Func<CovidCountryDetail, bool>>>()))
                            .ReturnsAsync(true);

            covidCountryDetailSqlRepository.Setup(e => e.GetAllAsync())
                            .Returns(CreateMultipleCovidCountryDetailObjects());

            return new CovidService(logger.Object,
                                    unitOfWork.Object,
                                    mapper,
                                    covidCountryDetailSqlRepository.Object
                                    );
        }

        #endregion Private Methods
    }
}
