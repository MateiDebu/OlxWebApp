using Core.Models;

namespace Core.Contracts
{
    public interface IAnnoucementService
    {
        List<AnnouncementForList> GetAll(int offset, int limit);
        Task<AnnouncementForList> GetById(int id);
        Task<AnnouncementForList> CreateAd(int userId,string name,string description);
        Task<AnnouncementForList> ModifyNameAd(int id,string name);
        Task DeleteAd(int id);
    }
}
