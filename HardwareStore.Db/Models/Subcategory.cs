using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Db.Models
{
    public class Subcategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<Product> Products { get; set; }
    }
}
