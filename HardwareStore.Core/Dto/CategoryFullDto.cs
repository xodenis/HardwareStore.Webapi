using System.Collections.Generic;

namespace HardwareStore.Core.Dto
{
    public class CategoryFullDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SubcategoryFullDto> Subcategories { get; set; }
    }
}
