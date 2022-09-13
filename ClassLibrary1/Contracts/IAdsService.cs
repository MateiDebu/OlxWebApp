using DataAccess.Database.Models;

namespace Core.Contracts
{
    public interface IAdsService
    {
        public Task<Ad> CreateAd(int userId, string name, string description);
        public Task<Ad> UpdateAd(int id, string name, string description);
        public Task DeleteAd(int id);
        public Task<Ad> GetById(int id);
        public IEnumerable<Ad> GetAllAds(int offset,int limit);
        public IEnumerable<Ad> GetAllAdsForUser(int userId,int offset,int limit);
    }
}
