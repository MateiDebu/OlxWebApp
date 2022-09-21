using Core.Contracts;
using Core.Models;
using Core.Repositories;
using DataAccess.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserForList> CreateUser(string name,string username,string password)
        {
            var user = new ApplicationUser() { Name = name, UserName=username};
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"User cannot be created because {result.Errors.First().Description}");
            }

            return new UserForList()
            {
                Id = user.Id,
                Name = user.Name,
                UserName=user.UserName,
            };
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
                UserName = element.UserName,
            }).ToList();
        }
        public async Task<UserForList?> GetById(int id)
        {
            var user = await _userRepository.FindById(id);
            return user == null ? null : new UserForList()
            {
                Id=user.Id,
                Name = user.Name,
                UserName = user.UserName
            };
        }
        public async Task<UserForList> ModifyUser(int userId, string name)
        {
            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User was not found");
            }

            user.Name = name;
            var updatedUser = await _userRepository.Update(user);
            return new UserForList()
            {
                Id=updatedUser.Id,
                Name=updatedUser.Name,  
                UserName=updatedUser.UserName
            };
        }
    }
}
