using CovidPortal.Domain.Interfaces;
using System.Threading.Tasks;

namespace CovidPortal.SQL.Infrastructure.Data
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServiceDbContext dbContext;

        public UnitOfWork(ServiceDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int AffectedRows { get; private set; }

        public int Commit()
        {
            AffectedRows = dbContext.SaveChanges();
            return AffectedRows;
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                AffectedRows = await dbContext.SaveChangesAsync();
                return AffectedRows;
            }
            catch
            {
                throw;
            }

        }
    }
}