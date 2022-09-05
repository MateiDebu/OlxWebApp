using Core.Repositories;
using DataAccess.Database.Context;
using DataAccess.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Announcement.Repository
{
    public class AnnouncementRepository : IRepository<Ad>
    {
        private readonly OlxContext _olxContext;

        public AnnouncementRepository(OlxContext olxContext)
        {
            _olxContext = olxContext;
        }

        public async Task<Ad> Add(Ad element)
        {
            _olxContext.Announcements.Add(element);
            await _olxContext.SaveChangesAsync();
            return element;
        }
        public IEnumerable<Ad> GetAll(int offset, int limit)
        {
            return _olxContext.Announcements.Skip(offset).Take(limit);
        }
        public async Task Delete(int id)
        {
            var ad = await _olxContext.Announcements.FindAsync(id);
            if (ad != null)
            {
                _olxContext.Announcements.Remove(ad);
                await _olxContext.SaveChangesAsync();
            }
        }

        public async Task<Ad?> FindById(int id)
        {
            return await _olxContext.FindAsync<Ad>(id);
        }

        public async Task<Ad> Update(Ad element)
        {
            _olxContext.Entry(element).State = EntityState.Modified;
            await _olxContext.SaveChangesAsync();
            return element;
        }

        public IEnumerable<Ad> FindByName(string name)
        {
            throw new NotImplementedException();
        }
        public Ad Get()
        {
            throw new NotImplementedException();
        }
    }
}
