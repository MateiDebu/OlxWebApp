using Core.Contracts;
using Core.Models;
using Core.Repositories;
using DataAccess.DataBase.Models;

namespace Business.Announcement.Services
{
    public class AnnouncementService : IAnnoucementService
    {
        private readonly IRepository<Ad> _adRepository;

        public AnnouncementService(IRepository<Ad> adRepository)
        {
            _adRepository = adRepository;
        }
    
        public async Task<AnnouncementForList> CreateAd(int userId,string name, string description)
        {
            var ad=new Ad() { UserId=userId, NameAd=name, Description=description};
            var addedAd = await _adRepository.Add(ad);
            return new AnnouncementForList()
            {
                Id = addedAd.Id,
                Name = addedAd.NameAd,
                Description = addedAd.Description,
            };
        }

        public async Task DeleteAd(int id)
        {
            await _adRepository.Delete(id);
        }

        public List<AnnouncementForList> GetAll(int offset, int limit)
        {
            var listOfAnnoucement=_adRepository.GetAll(offset, limit);
            return listOfAnnoucement.Select(a => new AnnouncementForList()
            {
                Id=a.Id,
                Name=a.NameAd,
                Description=a.Description,

            }).ToList();
        }

        public async Task<AnnouncementForList?> GetById(int id)
        {
            var ad=await _adRepository.FindById(id);
            return ad == null ? null : new AnnouncementForList()
            {
                Id = ad.Id,
                Name = ad.NameAd,
                Description = ad.Description,
            };
        }

        public async Task<AnnouncementForList> ModifyNameAd(int id,string name)
        {
            var ad = await _adRepository.FindById(id);
            if(ad == null)
            {
                throw new InvalidOperationException("Ad is not found");
            }

            ad.NameAd= name;
            var updatedAd=await _adRepository.Update(ad);
            return new AnnouncementForList()
            {
                Id = updatedAd.Id,
                Name = updatedAd.NameAd,
                Description = updatedAd.Description
            };
        }
    }
}
