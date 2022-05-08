using System.Collections.Generic;

namespace HardwareStore.Db.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Subcategory> Subcategories { get; set; }

        public List<Product> Products { get; set; }
    }
}
