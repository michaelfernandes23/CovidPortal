using CovidPortal.Domain.Entity;
using CovidPortal.Domain.Interfaces;
using CovidPortal.SQL.Infrastructure.Data;

namespace CovidPortal.SQL.Infrastructure.Repositories
{
    public class CovidCountryDetailSqlRepository : RepositoryBase<CovidCountryDetail>, ICovidCountryDetailSqlRepository
    {
        public CovidCountryDetailSqlRepository(ServiceDbContext dbContext) :
                base(dbContext)
        {
        }
    }
}
