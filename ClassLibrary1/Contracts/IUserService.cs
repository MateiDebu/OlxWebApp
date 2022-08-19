using Core.Models;

namespace Core.Contracts
{
    public interface IUserService
    {
        List<UserForList> GetAll();
    }
}
