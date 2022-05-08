using System.Collections.Generic;

namespace HardwareStore.Core.Dto
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public List<SubcategoryDto> Subcategories { get; set; }
    }
}
