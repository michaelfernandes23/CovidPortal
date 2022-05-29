using System.Threading.Tasks;

namespace CovidPortal.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}
