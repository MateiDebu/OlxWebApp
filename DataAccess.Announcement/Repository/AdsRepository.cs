using Core.Repositories;
using DataAccess.Database.Context;
using DataAccess.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Announcement.Repository
{
    public class AdsRepository : IAdRepository
    {
        private readonly OlxContext _olxContext;

        public AdsRepository(OlxContext olxContext)
        {
            _olxContext = olxContext;
        }
        public async Task<Ad> CreateAd(int userId, string name, string description)
        {
            Ad ad = new Ad
            {
                UserId = userId,
                Name = name,
                Description = description
            };

            _olxContext.Announcements.Add(ad);
            await _olxContext.SaveChangesAsync();
            return ad;
        }
        public async Task DeleteAd(int id)
        {
            var ad=await _olxContext.Announcements.FindAsync(id);

            if (ad != null)
            {
                _olxContext.Announcements.Remove(ad);
                await _olxContext.SaveChangesAsync();
            }
        }
        public IEnumerable<Ad> GetAllAds(int offset, int limit)
        {
            return _olxContext.Announcements.Include(p=>p.User).Skip(offset).Take(limit);
        }
        public IEnumerable<Ad> GetAllAdsForUser(int userId, int offset, int limit)
        {
            return _olxContext.Announcements.Include(p => p.User).Where(ad => ad.UserId == userId).Skip(offset).Take(limit);
        }
        public async Task<Ad?> GetById(int id)
        {
            return await _olxContext.Announcements.Include(p=>p.User).SingleOrDefaultAsync(p=>p.Id==id);
        }
        public async Task<Ad> UpdateAd(Ad ad)
        {
            _olxContext.Entry(ad).State= EntityState.Modified;
            await _olxContext.SaveChangesAsync();
            return ad;
        }
    }
}
