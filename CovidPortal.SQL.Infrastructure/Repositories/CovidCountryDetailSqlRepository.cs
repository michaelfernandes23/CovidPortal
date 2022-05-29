using CovidPortal.Domain.Entity;
using CovidPortal.SQL.Infrastructure.Data;
using CovidPortal.SQL.Infrastructure.Interfaces;

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
