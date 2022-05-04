using System.Collections.Generic;

namespace HardwareStore.Db.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalCount { get; set; }

        public double Price { get; set; }

        public string Characteristics { get; set; }

        public List<OrdersProducts> Orders { get; set; }
    }
}
