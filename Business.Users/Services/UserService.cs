using Core.Contracts;
using Core.Models;
using Core.Repositories;
using DataAccess.Database.Models;

namespace Business.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserForList> GetAll()
        {
            var listOfUsers = _userRepository.GetAll();
            return listOfUsers.Select(element => new UserForList
            {
                Id = element.Id,
                Name = element.Name,
            }).ToList();
        }
    }
}
