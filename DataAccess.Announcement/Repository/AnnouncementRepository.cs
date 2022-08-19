using Core.Repositories;
using DataAccess.DataBase.Models;

namespace DataAccess.Announcement.Repository
{
    public class AnnouncementRepository : IRepository<Ad>
    {
        public Ad Add(Ad element)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ad> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ad> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Ad Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ad> GetAll()
        {
            throw new NotImplementedException();
        }

        public Ad Update(Ad element)
        {
            throw new NotImplementedException();
        }
    }
}
