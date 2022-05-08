using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int TotalCount { get; set; }

        public double Price { get; set; }

        public string PriceInfo { get; set; }

        public string Characteristics { get; set; }

        public List<OrdersProducts> Orders { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("SubcategoryId")]
        public Subcategory Subcategory { get; set; }
    }
}
