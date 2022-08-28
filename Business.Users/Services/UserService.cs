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

        public async  Task<UserForList> CreateUser(string name)
        {
            var user = new User() { Name = name };
            var addedUser= await _userRepository.Add(user);
            return new UserForList(addedUser);
        }

        public async Task DeleteUser(int userId)
        {
            await _userRepository.Delete(userId);
        }

        public List<UserForList> GetAll(int offset, int limit)
        {
            var listOfUsers = _userRepository.GetAll(offset, limit);
            return listOfUsers.Select(element => new UserForList
            {
                Id = element.Id,
                Name = element.Name,
            }).ToList();
        }

        public async Task<UserForList?> GetById(int id)
        {
            var user = await _userRepository.FindById(id);
            return user == null ? null : new UserForList(user);
        }

        public async Task<UserForList> ModifyUser(int userId, string name)
        {
            var user = await  _userRepository.FindById(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User was not found");
            }

            user.Name = name;
            var updatedUser= await _userRepository.Update(user);
            return new UserForList(updatedUser);

        }
    }
}
