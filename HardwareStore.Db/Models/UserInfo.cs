using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
