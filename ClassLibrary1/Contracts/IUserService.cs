using Core.Models;

namespace Core.Contracts
{
    public interface IUserService
    {
        List<UserForList> GetAll(int offset,int limit);
        Task<UserForList?> GetById(int id);
        Task<UserForList> CreateUser(string name,string username,string password);
        Task<UserForList> ModifyUser(int userId, string name);
        Task DeleteUser(int userId);
    }
}
