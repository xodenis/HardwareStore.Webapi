using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class Favorites
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<FavoritesProducts> Products { get; set; }
    }
}
