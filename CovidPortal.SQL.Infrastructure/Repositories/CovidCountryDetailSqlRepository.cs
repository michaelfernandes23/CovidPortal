using CovidPortal.Domain.Entity;
using CovidPortal.SQL.Infrastructure.Data;
using CovidPortal.SQL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CovidPortal.SQL.Infrastructure.Repositories
{
    public class CovidCountryDetailSqlRepository : RepositoryBase<CovidCountryDetail>, ICovidCountryDetailSqlRepository
    {
        public CovidCountryDetailSqlRepository(ServiceDbContext dbContext, IDbConnection dbConnection) :
                base(dbContext, dbConnection)
        {
        }
    }
}
