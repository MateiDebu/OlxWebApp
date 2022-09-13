using Core.Contracts;
using Core.Repositories;
using DataAccess.Database.Models;

namespace Business.Announcement.Services
{
    public class AdsService : IAdsService
    {
        private readonly IAdRepository _adRepository;
        public AdsService(IAdRepository adRepository)
        {
            _adRepository = adRepository;

        }
        public async Task<Ad> CreateAd(int userId, string name, string description)
        {
            var createdAd= await _adRepository.CreateAd(userId, name, description);
            return await _adRepository.GetById(createdAd.Id);
        }

        public async Task DeleteAd(int id)
        {
            var ad = await _adRepository.GetById(id);
            if (ad == null)
            {
                throw new InvalidOperationException("Ad not found");
            }
            await _adRepository.DeleteAd(ad);
        }

        public IEnumerable<Ad> GetAllAds(int offset, int limit)
        {
            return _adRepository.GetAllAds(offset, limit);
        }

        public IEnumerable<Ad> GetAllAdsForUser(int userId, int offset, int limit)
        {
            return _adRepository.GetAllAdsForUser(userId, offset, limit);
        }

        public async Task<Ad> GetById(int id)
        {
            var ad = await _adRepository.GetById(id);
            if (ad == null)
            {
                throw new InvalidOperationException("Ad not found");
            }
            return ad;
        }

        public async Task<Ad> UpdateAd(int id, string name, string description)
        {
            var ad = await _adRepository.GetById(id);
            if (ad == null)
            {
                throw new InvalidOperationException("Ad not found");
            }
            ad.Name = name;
            ad.Description = description;
            var updatedAd = await _adRepository.UpdateAd(ad);
            return updatedAd;
        }
    }
}
