using System.Collections.Generic;

namespace HardwareStore.Core.Dto
{
    public class CartDto
    {
        public int UserId { get; set; }

        public List<CartProductsDto> Products { get; set; }
    }
}
