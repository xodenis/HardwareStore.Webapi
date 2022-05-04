using System.Collections.Generic;

namespace HardwareStore.Db.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserInfo Info { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public Cart Cart { get; set; }
    }
}
