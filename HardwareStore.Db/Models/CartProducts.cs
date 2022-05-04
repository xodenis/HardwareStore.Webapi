using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class CartProducts
    {
        public int Id { get; set; }

        public int Count { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
