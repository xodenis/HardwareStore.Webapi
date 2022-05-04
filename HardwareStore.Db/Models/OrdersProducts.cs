using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class OrdersProducts
    {
        public int Id { get; set; }

        public int Count { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
