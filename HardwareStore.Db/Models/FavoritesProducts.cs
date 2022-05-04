using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class FavoritesProducts
    {
        public int Id { get; set; }

        [ForeignKey("FavoritesId")]
        public Favorites Favorites { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
