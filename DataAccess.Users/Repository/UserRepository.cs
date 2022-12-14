using Core.Repositories;
using DataAccess.Database.Context;
using DataAccess.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OlxContext _olxContext;

        public UserRepository(OlxContext olxContext)
        {
            _olxContext=olxContext;
        }
        public async  Task<ApplicationUser> Add(ApplicationUser element)
        {
            _olxContext.Users.Add(element);
            await _olxContext.SaveChangesAsync();
            return element; 
        }
        public async Task Delete(int id)
        {
            var user =await _olxContext.Users.FindAsync(id);
            if(user != null)
            {
                _olxContext.Users.Remove(user);
                await _olxContext.SaveChangesAsync();
            }
        }
        public IEnumerable<ApplicationUser> GetAll(int offset, int limit)
        {
            return _olxContext.Users.Skip(offset).Take(limit);
        }
        public async Task<ApplicationUser> Update(ApplicationUser element)
        {
            _olxContext.Entry(element).State=EntityState.Modified;
            await _olxContext.SaveChangesAsync();
            return element;
        }
        public async Task<ApplicationUser?>FindById(int id)
        {
            return await _olxContext.FindAsync<ApplicationUser>(id);
        }
    }
}

