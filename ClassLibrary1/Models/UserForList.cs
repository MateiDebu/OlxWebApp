using DataAccess.Database.Models;

namespace Core.Models
{
    public class UserForList
    {
        public UserForList(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
        public UserForList()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
