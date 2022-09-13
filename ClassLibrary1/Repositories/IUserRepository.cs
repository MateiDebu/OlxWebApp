using DataAccess.Database.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll(int offset, int limit);
        Task<ApplicationUser?> FindById(int id);
        Task<ApplicationUser> Add(ApplicationUser element);
        Task<ApplicationUser> Update(ApplicationUser element);
        Task Delete(int id);
    }
}
