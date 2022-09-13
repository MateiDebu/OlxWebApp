using DataAccess.Database.Models;

namespace Core.Models
{
    public class AdForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public AdForList( Ad ad)
        {
            Id = ad.Id;
            Name = ad.Name;
            Description = ad.Description;
            UserName = ad.User.Name;
        }
    }
}
