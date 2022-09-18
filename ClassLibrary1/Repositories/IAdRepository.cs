using DataAccess.Database.Models;

namespace Core.Repositories
{
    public interface IAdRepository
    {
        Task<Ad> CreateAd(int userId, string name, string description);
        Task<Ad> UpdateAd(Ad ad);
        Task  DeleteAd(int id);
        Task<Ad?> GetById(int id);
        IEnumerable<Ad> GetAllAds(int offset, int limit);
        IEnumerable<Ad> GetAllAdsForUser(int userId, int offset, int limit);
    }
}
