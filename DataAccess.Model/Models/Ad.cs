using System.ComponentModel.DataAnnotations;

namespace DataAccess.Database.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
