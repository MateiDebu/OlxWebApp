using Core.Repositories;
using DataAccess.Database.Context;
using DataAccess.Database.Models;

namespace DataAccess.Users.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly OlxContext _olxContext;

        public UserRepository(OlxContext olxContext)
        {
            _olxContext=olxContext;
        }
        public User Add(User element)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public User Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _olxContext.Users;
        }

        public User Update(User element)
        {
            throw new NotImplementedException();
        }
    }
}
